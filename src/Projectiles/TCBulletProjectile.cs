using Terraria.ID;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Projectiles {
	public class TCBulletProjectile : BaseTCProjectile {
		public override int ProjectileDefinition => CoreLibMod.ProjectileType<BulletProjectile>();

		public override string ProjectileTypeName => "Bullet";

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bullet);
		}
	}
}
