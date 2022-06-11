using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Projectiles {
	public class TCBulletProjectile : BaseTCProjectile {
		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bullet);
		}
	}
}
