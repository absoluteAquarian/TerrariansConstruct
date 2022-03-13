using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariansConstruct.Projectiles {
	[Autoload(false)]
	public class TCBulletProjectile : BaseTCProjectile {
		public override void SafeSetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bullet);
		}
	}
}
