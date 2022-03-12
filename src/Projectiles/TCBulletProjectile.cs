using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariansConstruct.Projectiles {
	[Autoload(false)]
	public class TCBulletProjectile : BaseTCProjectile {
		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bullet);
		}
	}
}
