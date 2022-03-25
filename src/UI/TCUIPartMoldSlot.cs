using Terraria;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.UI {
	internal class TCUIPartMoldSlot : TCUIItemSlot {
		public readonly int ListIndex;

		public readonly ForgeUIMoldsPage parentPage;

		public TCUIPartMoldSlot(int slotNum, ForgeUIMoldsPage parentPage, int context = 4, float scale = 1f) : base(context, scale) {
			ListIndex = slotNum;
			this.parentPage = parentPage;

			ValidItemFunc = item => item.IsAir || item.ModItem is PartMold;
			OnItemChanged = item => {
				if (item.IsAir) {
					//Item was moved to the mouse
					parentPage.parentUI.entity.molds.RemoveAt(ListIndex);

					parentPage.needsUpdate = true;
				} else if (!Main.mouseItem.IsAir) {
					//Mold was placed into the slot.  Store both the old item and the current item
					parentPage.parentUI.entity.molds.Insert(ListIndex + 1, (Main.mouseItem.ModItem as PartMold)!);

					parentPage.needsUpdate = true;

					//Clear the mouse item
					Main.mouseItem = new();
				}

				NetHelper.SendTEUpdate(parentPage.parentUI.entity);
			};
		}
	}
}
