using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using TerrariansConstructLib;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Materials;

namespace TerrariansConstruct.Items {
	[Autoload(false)]
	public sealed class RecipeBrowserResult : ModItem {
		public readonly string texture;

		public readonly int registeredItemID;

		public override string Name => texture;

		internal static Dictionary<int, int> browserResultTypeToRegisteredItemID;

		public RecipeBrowserResult(string texture, int registeredItemID) {
			this.texture = texture;
			this.registeredItemID = registeredItemID;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			int[] parts = CoreLibMod.GetItemValidPartIDs(registeredItemID);

			tooltips.Add(new(Mod, "CraftedUsing", "Crafted using:"));

			for (int i = 0; i < parts.Length; i++) {
				string partName = CoreLibMod.GetPartName(parts[i]);

				tooltips.Add(new(Mod, "Part_" + i, "  " + partName));
			}
		}

		public override void AddRecipes() {
			Recipe recipe = CreateRecipe();

			int[] parts = CoreLibMod.GetItemValidPartIDs(browserResultTypeToRegisteredItemID[Type]);

			Material material = new UnknownMaterial();

			for (int i = 0; i < parts.Length; i++) {
				int type = CoreLibMod.GetItemPartItemType(material, parts[i]);

				recipe.AddIngredient(type);
			}

			// TODO: crafting station?
			recipe.Register();
		}
	}
}
