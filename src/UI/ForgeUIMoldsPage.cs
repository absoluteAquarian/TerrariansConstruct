using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariansConstructLib.API.UI;

namespace TerrariansConstruct.UI {
	internal class ForgeUIMoldsPage : ForgeUIPage {
		internal bool needsUpdate;

		public ForgeUIMoldsPage(string name) : base(name) {
			needsUpdate = true;
		}

		public override void DropAllItems() {
			//Don't drop any items
		}

		public override void OnSetAsActive() {
			needsUpdate = true;
		}

		public override void OnInitialize() {
			slots = new();
		}

		public void UpdateSlots() {
			slots ??= new();

			foreach (var s in slots)
				s?.Remove();

			RemoveAllChildren();

			slots.Clear();

			const int numSlotsPerRow = 10;
			int size = TextureAssets.InventoryBack9.Value.Width + 15;
			int count = (parentUI.entity.molds.Count + 1) / numSlotsPerRow + 1;

			List<UIElement> rows = new();
			
			for (int i = 0; i < count; i++) {
				UIElement row = new();
				row.Width.Set(0, 1f);
				row.Height.Set(size, 0f);
				rows.Add(row);
			}

			UIList rowsList = new();
			rowsList.Left.Set(20, 0f);
			rowsList.Top.Set(20, 0f);
			rowsList.Width.Set(size * numSlotsPerRow + 10, 0f);
			rowsList.Height.Set(-40, 1f);
			rowsList.AddRange(rows);
			rowsList.ListPadding = 4f;
			Append(rowsList);

			UIScrollbar scroll = new();
			scroll.Width.Set(20, 0);
			scroll.Height.Set(0, 0.825f);
			scroll.Left.Set(0, 0.95f);
			scroll.Top.Set(0, 0.1f);

			rowsList.SetScrollbar(scroll);
			rowsList.Append(scroll);

			int slotNum = 0;
			TCUIItemSlot slot;
			foreach (var mold in parentUI.entity.molds) {
				slot = new TCUIPartMoldSlot(slotNum, this, ItemSlot.Context.ChestItem);
				slot.SetItem(mold.Item);
				slot.Left.Set(size * (slotNum % numSlotsPerRow), 0);

				slots.Add(slot);

				rows[slotNum / numSlotsPerRow].Append(slot);

				++slotNum;
			}

			//Set an empty slot
			slot = new TCUIPartMoldSlot(slotNum, this, ItemSlot.Context.ChestItem);
			slot.Left.Set(size * (slotNum % numSlotsPerRow), 0);
			slots.Add(slot);

			rows[slotNum / numSlotsPerRow].Append(slot);
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

			if (needsUpdate)
				UpdateSlots();
		}
	}
}
