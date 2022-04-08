using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using TerrariansConstruct.Modifiers.ForgeModifiers;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Modifiers.Traits {
	internal class LeadTrait : BaseTrait {
		public const int LeadPoisonChance = 25;
		public const int LeadPoisonTime = 10 * 60;
		public const int LeadTierChanceMax = 13;

		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0x23, 0x75, 0x00);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Lead";

		public override bool IsEquivalentForTier(Type type, out uint tierWorth)
			=> base.IsEquivalentForTier(type, out tierWorth) || typeof(SpikyModifier).IsAssignableFrom(type);

		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void OnHitNPC(Player player, NPC target, BaseTCItem item, int damage, float knockBack, bool crit)
			=> HitNPC(target, Math.Max(Tier, LeadTierChanceMax), crit);

		public override void OnHitPlayer(Player owner, Player target, BaseTCItem item, int damage, bool crit)
			=> HitPlayer(target, Math.Max(Tier, LeadTierChanceMax), crit);

		public override void OnHitNPCWithProjectile(BaseTCProjectile projectile, NPC target, int damage, float knockBack, bool crit)
			=> HitNPC(target, Math.Max(Tier, LeadTierChanceMax), crit);

		public override void OnHitPlayerWithProjectile(BaseTCProjectile projectile, Player target, int damage, bool crit)
			=> HitPlayer(target, Math.Max(Tier, LeadTierChanceMax), crit);

		private static void HitNPC(NPC target, int tier, bool crit) {
			if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadPoisonChance))
				target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime * 2 : LeadPoisonTime);
		}

		private static void HitPlayer(Player target, int tier, bool crit) {
			if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadPoisonChance))
				target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime * 2 : LeadPoisonTime);
		}
	}
}
