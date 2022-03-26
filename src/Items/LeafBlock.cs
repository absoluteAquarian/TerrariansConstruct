using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariansConstruct.Items {
	internal class LeafBlock : ModItem {
		public override string Texture => "TerrariansConstruct/Assets/Items/LeafBlock";

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(TileID.LeafBlock);
			Item.width = 16;
			Item.height = 16;
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddRecipeGroup(RecipeGroupID.Wood)
				.AddIngredient(ItemID.LeafWand)
				.AddTile(TileID.LivingLoom)
				.AddConsumeItemCallback((Recipe recipe, int type, ref int amount) => {
					if (type == ItemID.LeafWand)
						amount = 0;
				})
				.Register();

			CreateRecipe(10)
				.AddRecipeGroup(RecipeGroupID.Wood, 10)
				.AddIngredient(ItemID.LeafWand)
				.AddTile(TileID.LivingLoom)
				.AddConsumeItemCallback((Recipe recipe, int type, ref int amount) => {
					if (type == ItemID.LeafWand)
						amount = 0;
				})
				.Register();

			CreateRecipe(100)
				.AddRecipeGroup(RecipeGroupID.Wood, 100)
				.AddIngredient(ItemID.LeafWand)
				.AddTile(TileID.LivingLoom)
				.AddConsumeItemCallback((Recipe recipe, int type, ref int amount) => {
					if (type == ItemID.LeafWand)
						amount = 0;
				})
				.Register();
		}
	}
}
