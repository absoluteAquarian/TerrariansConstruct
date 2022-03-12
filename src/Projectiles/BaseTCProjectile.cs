using System;
using Terraria;
using Terraria.ModLoader;
using TerrariansConstruct.Items;

namespace TerrariansConstruct.Projectiles {
	[Autoload(false)]
	public class BaseTCProjectile : ModProjectile {
		public ItemPart[] parts = Array.Empty<ItemPart>();

		public sealed override void SetStaticDefaults() {
			SafeSetStaticDefaults();

			DisplayName.SetDefault("Constructed Projectile");
		}

		public virtual void SafeSetStaticDefaults() { }

		public sealed override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			for (int i = 0; i < parts.Length; i++)
				parts[i].OnHitNPC(parts[i].partID, Projectile, target, damage, knockback, crit);
		}

		public sealed override void OnHitPlayer(Player target, int damage, bool crit) {
			for (int i = 0; i < parts.Length; i++)
				parts[i].OnHitPlayer(parts[i].partID, Projectile, target, damage, crit);
		}

		public sealed override void AI() {
			for (int i = 0; i < parts.Length; i++)
				parts[i].ProjectileAI(parts[i].partID, Projectile);

			SafeAI();
		}

		public virtual void SafeAI() { }
	}
}
