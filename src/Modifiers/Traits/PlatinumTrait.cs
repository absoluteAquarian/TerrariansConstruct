using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Modifiers.Traits {
	internal sealed class PlatinumTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0x99, 0xb4, 0xcc);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Platinum";

		//All this ability does is make NPCs take more damage
		public override bool ShouldUpdateCounter(Player player) => false;

		//Needs to be greater than zero since the counter isn't being updated and it's "supposed to" increment,
		//just so that OnCounterTargetReached doesn't get called every tick
		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void ModifyHitNPC(Player player, NPC target, BaseTCItem item, ref int damage, ref float knockBack, ref bool crit) {
			ModifyNPCHitStats(ref damage, ref knockBack, target.life >= target.lifeMax);
		}

		public override void ModifyHitNPCWithProjectile(BaseTCProjectile projectile, NPC target, ref int damage, ref float knockBack, ref bool crit, ref int hitDirection) {
			ModifyNPCHitStats(ref damage, ref knockBack, target.life >= target.lifeMax);
		}

		public override void ModifyHitPlayer(Player owner, Player target, BaseTCItem item, ref int damage, ref bool crit) {
			ModifyPlayerHitStats(ref damage, target.statLife >= target.statLifeMax2);
		}

		public override void ModifyHitPlayerWithProjectile(BaseTCProjectile projectile, Player target, ref int damage, ref bool crit) {
			ModifyPlayerHitStats(ref damage, target.statLife >= target.statLifeMax2);
		}

		private void ModifyNPCHitStats(ref int damage, ref float knockBack, bool atFullHealth) {
			//+20% damage on full-health NPCs per tier, additive
			//+10% knockback on full-health NPCs per tier, additive
			ModifyHitStats(ref damage, ref knockBack, atFullHealth, 0.5f, 0.1f);
		}

		private void ModifyPlayerHitStats(ref int damage, bool atFullHealth) {
			//+10% damage on full-health players per tier, additive
			float knockBack = 0;
			ModifyHitStats(ref damage, ref knockBack, atFullHealth, 0.3f, 0f);
		}

		private void ModifyHitStats(ref int damage, ref float knockBack, bool atFullHealth, float damageFactor, float knockBackFactor) {
			StatModifier damageBonus = new(atFullHealth ? 1f + Tier * damageFactor : 1f, 1f);
			damage = (int)Math.Ceiling(damageBonus.ApplyTo(damage));

			//Tier bonus capped at level 10
			StatModifier kbBonus = new(atFullHealth ? 1f + Math.Max(10, Tier) * knockBackFactor : 1f, 1f);
			knockBack = kbBonus.ApplyTo(knockBack);
		}
	}
}
