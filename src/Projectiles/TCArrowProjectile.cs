using Terraria.ID;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Projectiles {
	public class TCArrowProjectile : BaseTCProjectile {
		public override int ProjectileDefinition => CoreLibMod.ProjectileType<ArrowProjectile>();

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
		}
	}
}
