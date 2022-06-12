using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.UI;
using TerrariansConstructLib;
using TerrariansConstructLib.API.UI;

namespace TerrariansConstruct.UI {
	internal class ForgeUIPage : UIElement {
		protected List<TCUIItemSlot> slots;

		internal ForgeUI parentUI;

		public readonly string Name;

		public ForgeUIPage(string name) {
			Name = name;
		}
		
		public virtual void DropAllItems() {
			const int areaSize = 8;
			Point tl = (Main.LocalPlayer.Center - new Vector2(areaSize / 2)).ToPoint();
			Rectangle area = new(tl.X, tl.Y, areaSize, areaSize);

			for (int i = 0; i < slots.Count; i++) {
				if (i < slots.Count - 1 && !slots[i].StoredItem.IsAir)
					Utility.DropItem(new EntitySource_DebugCommand("TerrariansConstruct:ForgeUIPage"), slots[i].StoredItem, area);

				slots[i].SetItem(new Item());
			}
		}

		public virtual void OnSetAsActive() { }

		internal void SetAsActivePage() {
			if (!object.ReferenceEquals(parentUI.currentPage, this)) {
				parentUI.panel.SetActivePage(Name);

				parentUI.currentPage.DropAllItems();

				parentUI.currentPage.Remove();

				parentUI.panel.viewArea.Append(this);

				OnSetAsActive();

				parentUI.currentPage = this;
			}
		}
	}
}
