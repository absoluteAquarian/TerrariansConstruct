using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.API;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.UI {
	internal class ForgeUIToolsPage : ForgeUIPage {
		public UIText createWeapon;

		private ForgeUISlotConfiguration[] currentConfiguration;

		public override void OnInitialize() {
			//Make the rest of the text
			createWeapon = new("Creating: n/a", 1.3f){
				HAlign = 0.7f
			};
			createWeapon.Top.Set(58, 0);
			Append(createWeapon);

			slots = new();

			//Weapon choices list
			UIList list = new();
			list.SetPadding(0);
			list.Width.Set(220, 0f);
			list.Height.Set(0, 0.9f);
			list.Left.Set(20, 0);
			list.Top.Set(0, 0.05f);
			Append(list);

			UIScrollbar scroll = new();
			scroll.Width.Set(20, 0);
			scroll.Height.Set(0, 0.825f);
			scroll.Left.Set(0, 0.95f);
			scroll.Top.Set(0, 0.1f);

			list.SetScrollbar(scroll);
			list.Append(scroll);
			list.ListPadding = 10;

			for (int i = 0; i < ItemDefinitionLoader.Count; i++) {
				var option = new ForgeUIToolsOption(i);
				option.Height.Set(32, 0f);
				option.Width.Set(0, 0.8f);
				option.Left.Set(10, 0f);

				list.Add(option);
			}
		}

		public void ConfigureSlots(ForgeUISlotConfiguration[] configurations) {
			int[] parts = configurations.Select(s => s.partID).ToArray();

			if (!CoreLibMod.TryFindItem(parts, out int registeredItemID)) {
				CoreMod.Instance.Logger.Warn("Part ID sequence did not correspond to an existing item:\n" +
					string.Join(", ", parts.Select(PartDefinitionLoader.GetIdentifier)));

				//Default to the sword slots
				ConfigureSlots(ItemDefinitionLoader.Get(CoreLibMod.ItemType<Sword>())!.GetForgeSlotConfiguration().ToArray());
			}

			Item[] items = slots.Where((s, i) => i < slots.Count - 1).Select(s => s.StoredItem).ToArray();
			Item tool = slots.Count == 0 ? new() : slots[^1].StoredItem;

			int prevSlotsCount = slots.Count - 1;

			foreach (var slot in slots)
				slot.Remove();  //Remove the old instance

			slots.Clear();

			const int left = 200, top = 100;
			int size = TextureAssets.InventoryBack9.Value.Width + 15;

			HashSet<int> itemsToDrop = new();

			foreach (var configuration in configurations) {
				//ValidItemFunc and OnItemChanged are both set by the constructor
				TCUIItemSlot slot = new TCUIItemPartSlot(configuration);
				slot.Left.Set(left + size * (configuration.position % 5), 0);
				slot.Top.Set(top + size * (configuration.position / 5), 0);

				slots.Add(slot);

				//Drop the item if it was in a slot that's no longer present
				if (configuration.slot >= prevSlotsCount)
					itemsToDrop.Add(configuration.slot);
			}

			const int areaSize = 8;
			Point tl = (Main.LocalPlayer.Center - new Vector2(areaSize / 2)).ToPoint();
			Rectangle area = new(tl.X, tl.Y, areaSize, areaSize);

			foreach (var slot in itemsToDrop) {
				Item item = items[slot];

				//Spawn a clone of the item
				Utility.DropItem(new EntitySource_DebugCommand("TerrariansConstruct:ForgeUIToolsPage"), item, area);
			}

			//Add the result slot
			TCUIItemSlot result = new(){
				ValidItemFunc = item => item.IsAir,
				VAlign = 0.5f
			};
			result.Left.Set(-size - 40, 1f);

			slots.Add(result);

			foreach (var slot in slots)
				Append(slot);

			currentConfiguration = configurations;

			createWeapon.SetText("Crafting: " + Lang.GetItemNameValue(ItemDefinitionLoader.Get(registeredItemID)!.ItemType));
		}

		internal void OnItemPartChanged(Item item, ForgeUISlotConfiguration configuration) {
			// If all of the items are valid, display the new constructed item, otherwise clear its slot
			if (!CheckIfResultIsValid())
				slots[^1].SetItem(new Item());
			else {
				Item? display = null;

				int[] parts = currentConfiguration.Select(s => s.partID).ToArray();

				if (CoreLibMod.TryFindItem(parts, out int registeredItemID)) {
					//Convert the item part items into item parts, then generate the resulting item
					int type = CoreLibMod.GetItemType(registeredItemID);

					display = new Item(type);

					if (display.ModItem is not BaseTCItem tc)
						throw new InvalidOperationException($"Item \"{display.Name}\" (ID: {type}) was not a registered Terrarians' Construct item");
					
					ItemPart[] partObjs = currentConfiguration.Select(c => slots[c.slot]).Select(u => (u.StoredItem.ModItem as ItemPartItem)!.part).ToArray();

					//Sanity check
					if (partObjs.Length != tc.PartsCount)
						throw new Exception("Configuration slots length did not match registered item's PartsCount property");

					tc.InitializeWithParts(partObjs);
				}

				if (display is null)
					slots[^1].SetItem(new Item());
			}
		}

		private bool CheckIfResultIsValid() {
			for (int i = 0; i < slots.Count - 1; i++) {
				if (slots[i] is not TCUIItemPartSlot partSlot || partSlot.StoredItem.ModItem is not ItemPartItem partItem)
					return false;

				if (partSlot.Configuration.partID != partItem.part.partID)
					return false;
			}

			return true;
		}
	}
}
