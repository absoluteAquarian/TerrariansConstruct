using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstruct.Tiles;

namespace TerrariansConstruct.Items {
	internal class ForgeItem : ModItem {
		public override string Texture => "TerrariansConstruct/Assets/Items/ForgeItem";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Tinker Table");
			Tooltip.SetDefault("The crafting station for creating Terrarians' Construct item parts and constructed items.\n" +
				"Right click once placed to open the Forge UI, where said crafting is performed.");
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<ForgeTile>());
			Item.width = 32;
			Item.height = 18;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddRecipeGroup(CoreMod.RecipeGroup_AnyWorkbench)
				.AddIngredient(ItemID.Book, 5)
				.AddIngredient(ItemID.Silk, 20)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
