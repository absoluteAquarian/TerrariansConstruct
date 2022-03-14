using Terraria.UI;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.UI {
	public sealed class TCUIItemPartSlot : TCUIItemSlot {
		public readonly SlotConfiguration Configuration;

		public TCUIItemPartSlot(SlotConfiguration configuration, float scale = 1f) : base(SlotContexts.ForgeUI + configuration.partID, scale){
			Configuration = configuration;

			ValidItemFunc = item => item.IsAir || (item.ModItem is ItemPartItem pItem && pItem.part.partID == Configuration.partID);
			OnItemChanged = item => CoreMod.forgeUI.OnItemPartChanged(item, Configuration);
		}
	}
}
