using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstruct.Definitions;
using TerrariansConstruct.Projectiles;
using TerrariansConstructLib;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed shortsword item
	/// </summary>
	public class TCShortsword : BaseTCItem {
		public override int ItemDefinition => CoreLibMod.ItemType<Shortsword>();

		public override void SafeSetDefaults() {
			Item.useStyle = ItemUseStyleID.Rapier; // Makes the player do the proper arm motion
			Item.width = 32;
			Item.height = 32;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee;
			Item.autoReuse = false;
			Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
			Item.noMelee = true; // The projectile will do the damage and not the item

			Item.shoot = ModContent.ProjectileType<TCShortswordProjectile>(); // The projectile is what makes a shortsword work
			Item.shootSpeed = 2.1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values
		}
	}
}
