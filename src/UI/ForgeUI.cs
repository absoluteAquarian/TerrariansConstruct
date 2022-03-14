using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.ID;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.UI {
	internal class ForgeUI : UIState {
		private UIDragablePanel panel;

		private List<UIText> text;

		private List<TCUIItemSlot> slots;

		private SlotConfiguration[] currentConfiguration;

		public override void OnInitialize() {
			//Make the panel
			panel = new();

			panel.Width.Set(600, 0);
			panel.Height.Set(400, 0);

			panel.HAlign = panel.VAlign = 0.5f;

			Append(panel);

			//Make the header text
			UIText header = new("Weapons Forge", 1, true) {
				HAlign = 0.5f
			};
			header.Top.Set(10, 0);
			panel.Append(header);

			//Make the rest of the text
			text = new();

			UIText createWeapon = new("Creating: n/a", 1.3f){
				HAlign = 0.7f
			};
			createWeapon.Top.Set(58, 0);
			text.Add(createWeapon);

			foreach (var t in text)
				panel.Append(t);

			slots = new();

			ConfigureSlots(SlotConfiguration.Weapon_Sword);
		}

		public void ConfigureSlots(SlotConfiguration[] configurations) {
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

			foreach (var slot in itemsToDrop) {
				Item item = items[slot];

				//Spawn a clone of the item
				Main.LocalPlayer.QuickSpawnClonedItem(Main.LocalPlayer.GetItemSource_Misc(item.type), item, item.stack);
			}

			//Add the result slot
			TCUIItemSlot result = new(){
				ValidItemFunc = item => item.IsAir,
				VAlign = 0.5f
			};
			result.Left.Set(-size - 40, 1f);

			slots.Add(result);

			foreach (var slot in slots)
				panel.Append(slot);

			currentConfiguration = configurations;
		}

		internal void OnItemPartChanged(Item item, SlotConfiguration configuration) {
			// If all of the items are valid, display the new constructed item, otherwise clear its slot
			if (!CheckIfResultIsValid())
				slots[^1].SetItem(new Item());
			else {
				Item display = new();
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

	public struct SlotConfiguration {
		//   0  1  2  3  4
		//   5  6  7  8  9
		//  10 11 12 13 14
		//  15 16 17 18 19
		//  20 21 22 23 24

		public static readonly SlotConfiguration[] Weapon_Sword = new SlotConfiguration[] {
			(0,  4, MaterialPartID.WeaponLongSwordBlade),
			(1, 12, MaterialPartID.WeaponSwordGuard),
			(2, 20, MaterialPartID.ToolRod)
		};

		public static readonly SlotConfiguration[] Weapon_Shortsword = new SlotConfiguration[] {
			(0,  4, MaterialPartID.WeaponShortSwordBlade),
			(1, 12, MaterialPartID.WeaponSwordGuard),
			(2, 20, MaterialPartID.ToolRod)
		};

		public static readonly SlotConfiguration[] Weapon_Bow = new SlotConfiguration[] {
			(0, 16, MaterialPartID.WeaponBowString),
			(1,  7, MaterialPartID.WeaponBowHead),
			(2, 13, MaterialPartID.WeaponBowHead)
		};

		public static readonly SlotConfiguration[] Tool_Pickaxe = new SlotConfiguration[] {
			(0,  7, MaterialPartID.ToolPickHead),
			(1, 13, MaterialPartID.ToolPickHead),
			(2, 12, MaterialPartID.ToolBinding),
			(3, 16, MaterialPartID.ToolRod)
		};

		public static readonly SlotConfiguration[] Tool_Axe = new SlotConfiguration[] {
			(0,  8, MaterialPartID.ToolAxeHead),
			(2, 12, MaterialPartID.ToolBinding),
			(3, 16, MaterialPartID.ToolRod)
		};

		public static readonly SlotConfiguration[] Tool_Hammer = new SlotConfiguration[] {
			(0,  7, MaterialPartID.ToolHammerHead),
			(1, 13, MaterialPartID.ToolHammerHead),
			(2, 12, MaterialPartID.ToolBinding),
			(3, 16, MaterialPartID.ToolRod)
		};

		public readonly int slot;
		public readonly int position;
		public readonly int partID;

		public SlotConfiguration(int slot, int position, int partID) {
			this.slot = slot;
			this.position = position;
			this.partID = partID;
		}

		public static implicit operator (int slot, int position, int partID)(SlotConfiguration configuration)
			=> (configuration.slot, configuration.position, configuration.partID);

		public static implicit operator SlotConfiguration((int slot, int position, int partID) tuple)
			=> new(tuple.slot, tuple.position, tuple.partID);
	}
}
