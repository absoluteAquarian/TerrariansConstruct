using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Projectiles {
	[Autoload(false)]
	public class TCBulletProjectile : BaseTCProjectile {
		public override void SafeSetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bullet);
		}
	}
}
