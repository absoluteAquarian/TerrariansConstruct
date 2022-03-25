using Terraria.ModLoader;
using TerrariansConstruct.Tiles;

namespace TerrariansConstruct.Items {
	internal class ForgeItem : ModItem {
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Forge");
			Tooltip.SetDefault("The crafting station for creating Terrarians' Construct item parts and constructed items.\n" +
				"Right click once placed to open the Forge UI, where said crafting is performed.");
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<ForgeTile>());
		}
	}
}
