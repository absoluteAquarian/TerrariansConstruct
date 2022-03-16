using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Projectiles {
	[Autoload(false)]
	public class TCBulletProjectile : BaseTCProjectile {
		public override bool? SafeIsLoadingEnabled(Mod mod) => true;

		public override void SafeSetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Bullet);
		}
	}
}
