using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Projectiles {
	public class TCArrowProjectile : BaseTCProjectile {
		public override int ProjectileDefinition => CoreLibMod.ProjectileType<ArrowProjectile>();

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			AIType = ProjectileID.WoodenArrowFriendly;

			DrawOriginOffsetX = 11f;
			DrawOriginOffsetY = 29;
		}

		public override void SetSpriteEffects(ref SpriteEffects effects) {
			effects |= SpriteEffects.FlipVertically;
		}
	}
}
