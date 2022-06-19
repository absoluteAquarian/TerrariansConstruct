using System;
using Terraria.ModLoader;
using TerrariansConstruct.Items.Ammo;
using TerrariansConstruct.Projectiles;
using TerrariansConstructLib.API.Definitions;

namespace TerrariansConstruct.Definitions {
	public sealed class ShortswordProjectile : TCProjectileDefinition {
		public override int ProjectileType => ModContent.ProjectileType<TCShortswordProjectile>();
	}

	public sealed class BulletProjectile : TCProjectileDefinition {
		public override int ProjectileType => ModContent.ProjectileType<TCBulletProjectile>();

		public override int AmmoItemType => 0;  // TODO: TCBullet item
	}

	public sealed class ArrowProjectile : TCProjectileDefinition {
		public override int ProjectileType => ModContent.ProjectileType<TCArrowProjectile>();

		public override int AmmoItemType => ModContent.ItemType<TCArrow>();
	}
}
