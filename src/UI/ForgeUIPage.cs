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

		internal virtual void DropAllItems() {
			const int areaSize = 8;
			Point tl = (Main.LocalPlayer.Center - new Vector2(areaSize / 2)).ToPoint();
			Rectangle area = new(tl.X, tl.Y, areaSize, areaSize);

			for (int i = 0; i < slots.Count; i++) {
				if (i < slots.Count - 1 && !slots[i].StoredItem.IsAir)
					Utility.DropItem(new EntitySource_DebugCommand(), slots[i].StoredItem, area);

				slots[i].SetItem(new Item());
			}
		}
	}
}
