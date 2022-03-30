using System;
using Terraria;
using Terraria.ID;
using TerrariansConstructLib;
using TerrariansConstructLib.API.Sources;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct {
	/// <summary>
	/// A collection of action builders for base Terrarians' Construct materials
	/// </summary>
	public static class TCPartActions {
		public static readonly ItemPartActionsBuilder Wood = new ItemPartActionsBuilder()
			.WithOnUpdateInventory((partID, player, item) => {
				if (item.ModItem is not BaseTCItem tc || !Main.rand.NextBool(1, 5000))
					return;

				int max = tc.GetMaxDurability();

				if (tc.HasPartOfType(partID, out int index))
					tc.TryIncreaseDurability(player, Math.Max(1, max / 100), new DurabilityModificationSource_Regen(tc[index]));
			})
			.MarkAsReadonly();

		public const int LeadPoisonChance = 25;
		public const int LeadPoisonTime = 10 * 60;

		public static readonly ItemPartActionsBuilder Lead = new ItemPartActionsBuilder()
			.WithOnItemHitNPC((partID, item, owner, target, damage, knockback, crit) => {
				if (Main.rand.NextBool(LeadPoisonChance))
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime * 2 : LeadPoisonTime);
			})
			.WithOnItemHitPlayer((partID, item, owner, target, damage, crit) => {
				if (Main.rand.NextBool(LeadPoisonChance))
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime / 3 * 2 : LeadPoisonTime / 3);
			})
			.WithOnProjectileHitNPC((partID, projectile, target, damage, knockback, crit) => {
				if (Main.rand.NextBool(LeadPoisonChance))
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime * 2 : LeadPoisonTime);
			})
			.WithOnProjectileHitPlayer((partID, projectile, target, damage, crit) => {
				if (Main.rand.NextBool(LeadPoisonChance))
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime / 3 * 2 : LeadPoisonTime / 3);
			})
			.MarkAsReadonly();
	}
}
