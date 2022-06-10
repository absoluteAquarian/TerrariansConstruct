using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using TerrariansConstructLib.API;

namespace TerrariansConstruct.Items {
	/// <summary>
	/// Fists
	/// </summary>
	public sealed class ZAHANDO : ModItem {
		public override string Texture => "TerrariansConstruct/Assets/Items/ZAHANDO";

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Fist");
			Tooltip.SetDefault("'What ho, Muscle Wizard! Might you cast a spell?'\n" +
				"'Ho ho ho ho! Of course, young adventurer! I CAST FIST!!'");
		}

		public override void SetDefaults() {
			Item.DefaultToMeleeWeapon(16, ItemUseStyleID.Swing, useTurn: true);
			Item.pick = 20;
			Item.axe = 3;
			Item.noUseGraphic = true;
			Item.DamageType = DamageClass.Melee;
			Item.damage = 8;
			Item.knockBack = 1.4f;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.tileBoost = -3;
		}

		public override void AddRecipes() {
			//No ingredients nor tile needed
			CreateRecipe()
				.Register();
		}

		public override void PostUpdate() {
			//Should not exist in the world
			Item.TurnToAir();
		}

		public override bool? PrefixChance(int pre, UnifiedRandom rand) => pre == 0;
	}
}
