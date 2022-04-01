using System;
using Terraria;
using Terraria.ID;
using TerrariansConstruct.Players;
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
				ref bool woodCheck = ref player.GetModPlayer<ActionsPlayer>().woodCheck;

				if (woodCheck)
					return;

				woodCheck = true;

				BaseTCItem? tc = item.AsTCItem();
				if (tc is null)
					return;

				int tier = tc.CountParts(CoreMod.RegisteredMaterials.Wood);
				int chance = tier * (tier + 1) / 2;  //Sum from 1 to N

				if (!Main.rand.NextBool(chance, 800))
					return;

				if (tc.HasPartOfType(partID, out int index)) {
					// Add 1 to 5 durability
					tc.TryIncreaseDurability(player, Main.rand.Next(1, 6), new DurabilityModificationSource_Regen(tc[index]));
				}
			})
			.MarkAsReadonly();

		public const int LeadPoisonChance = 25;
		public const int LeadPoisonTime = 10 * 60;
		public const int LeadTierChanceMax = 13;

		public static readonly ItemPartActionsBuilder Lead = new ItemPartActionsBuilder()
			.WithOnItemHitNPC((partID, item, owner, target, damage, knockback, crit) => {
				ref int leadCheck = ref owner.GetModPlayer<ActionsPlayer>().leadCheck;

				int tier = Math.Max(item.AsTCItem()?.CountParts(CoreMod.RegisteredMaterials.LeadBar) ?? 0, LeadTierChanceMax);

				if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadPoisonChance) && leadCheck != target.WhoAmIToTargettingIndex) {
					leadCheck = target.WhoAmIToTargettingIndex;
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime * 2 : LeadPoisonTime);
				}
			})
			.WithOnItemHitPlayer((partID, item, owner, target, damage, crit) => {
				ref int leadCheck = ref owner.GetModPlayer<ActionsPlayer>().leadCheck;

				int tier = Math.Max(item.AsTCItem()?.CountParts(CoreMod.RegisteredMaterials.LeadBar) ?? 0, LeadTierChanceMax);

				if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadPoisonChance) && leadCheck != target.whoAmI) {
					leadCheck = target.whoAmI;
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime / 3 * 2 : LeadPoisonTime / 3);
				}
			})
			.WithOnProjectileHitNPC((partID, projectile, target, damage, knockback, crit) => {
				ref int leadCheck = ref Main.player[projectile.owner].GetModPlayer<ActionsPlayer>().leadCheck;

				int tier = Math.Max(projectile.AsTCProjectile()?.CountParts(CoreMod.RegisteredMaterials.LeadBar) ?? 0, LeadTierChanceMax);

				if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadPoisonChance) && leadCheck != target.WhoAmIToTargettingIndex) {
					leadCheck = target.WhoAmIToTargettingIndex;
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime * 2 : LeadPoisonTime);
				}
			})
			.WithOnProjectileHitPlayer((partID, projectile, target, damage, crit) => {
				ref int leadCheck = ref Main.player[projectile.owner].GetModPlayer<ActionsPlayer>().leadCheck;

				int tier = Math.Max(projectile.AsTCProjectile()?.CountParts(CoreMod.RegisteredMaterials.LeadBar) ?? 0, LeadTierChanceMax);

				if (tier > 0 && Main.rand.NextBool(1 + (tier - 1) * 2, LeadPoisonChance) && leadCheck != target.whoAmI) {
					leadCheck = target.whoAmI;
					target.AddBuff(BuffID.Poisoned, crit ? LeadPoisonTime / 3 * 2 : LeadPoisonTime / 3);
				}
			})
			.MarkAsReadonly();
	}
}
