using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using TerrariansConstruct.Modifiers.Traits;
using TerrariansConstructLib.API;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;
using TerrariansConstructLib.Projectiles;

namespace TerrariansConstruct.Modifiers.ForgeModifiers {
	//This modifier's logic is handled by LeadTrait if that trait is present
	internal class SpikyModifier : BaseModifier {
		public override int MaxTier => 5;

		public override string? VisualTexture => "Spiky";

		public override Color TooltipColor => new(0x23, 0x75, 0x00);

		public override string LangKey => "Mods.TerrariansConstruct.Modifiers.Spiky";

		public override int GetUpgradeTarget()
			=> Tier switch {
				1 => 10,
				2 => 17,
				3 => 25,
				4 => 40,
				5 => 60,
				_ => int.MaxValue
			};

		public override bool CanAcceptItemsForUpgrade(ItemData[] items, ref int upgradeCurrent, in int upgradeTarget) {
			if (!ItemsOnlyContain(items, ItemID.Stinger, ItemID.JungleSpores))
				return false;
			
			int stingers = CountItems(items, ItemID.Stinger);
			int spores = CountItems(items, ItemID.JungleSpores);
			
			int available = Math.Min(stingers, spores);

			if (available + upgradeCurrent > upgradeTarget)
				available = upgradeTarget - upgradeCurrent;

			if (available <= 0)
				return false;

			upgradeCurrent += available;
				
			RemoveItems(items, ItemID.Stinger, available);
			RemoveItems(items, ItemID.JungleSpores, available);

			return true;
		}

		//Copied from LeadTrait
		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void OnHitNPC(Player player, NPC target, BaseTCItem item, int damage, float knockBack, bool crit) {
			if (item.GetModifier<LeadTrait>() is not null)
				return;

			HitNPC(target, Math.Max(Tier, LeadTrait.LeadTierChanceMax), crit);
		}

		public override void OnHitPlayer(Player owner, Player target, BaseTCItem item, int damage, bool crit) {
			if (item.GetModifier<LeadTrait>() is not null)
				return;
			
			HitPlayer(target, Math.Max(Tier, LeadTrait.LeadTierChanceMax), crit);
		}

		public override void OnHitNPCWithProjectile(BaseTCProjectile projectile, NPC target, int damage, float knockBack, bool crit) {
			if (projectile.GetModifier<LeadTrait>() is not null)
				return;
			
			HitNPC(target, Math.Max(Tier, LeadTrait.LeadTierChanceMax), crit);
		}

		public override void OnHitPlayerWithProjectile(BaseTCProjectile projectile, Player target, int damage, bool crit) {
			if (projectile.GetModifier<LeadTrait>() is not null)
				return;
			
			HitPlayer(target, Math.Max(Tier, LeadTrait.LeadTierChanceMax), crit);
		}

		private static void HitNPC(NPC target, int tier, bool crit) {
			if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadTrait.LeadPoisonChance))
				target.AddBuff(BuffID.Poisoned, crit ? LeadTrait.LeadPoisonTime * 2 : LeadTrait.LeadPoisonTime);
		}

		private static void HitPlayer(Player target, int tier, bool crit) {
			if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadTrait.LeadPoisonChance))
				target.AddBuff(BuffID.Poisoned, crit ? LeadTrait.LeadPoisonTime * 2 : LeadTrait.LeadPoisonTime);
		}
	}
}
