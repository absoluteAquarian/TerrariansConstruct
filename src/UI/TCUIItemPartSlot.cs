using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.UI {
	public sealed class TCUIItemPartSlot : TCUIItemSlot {
		public readonly ForgeUISlotConfiguration Configuration;

		public TCUIItemPartSlot(ForgeUISlotConfiguration configuration, float scale = 1f) : base(SlotContexts.ForgeUIItemPartSlot + configuration.partID, scale){
			Configuration = configuration;

			ValidItemFunc = item => item.IsAir || (item.ModItem is ItemPartItem pItem && pItem.part.partID == Configuration.partID);
			OnItemChanged = item => {
				if (object.ReferenceEquals(CoreMod.forgeUI.currentPage, CoreMod.forgeUI.pageTools))
					CoreMod.forgeUI.pageTools.OnItemPartChanged(item, Configuration);
			};
		}
	}
}
