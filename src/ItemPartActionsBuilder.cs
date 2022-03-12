using System;
using Terraria;
using TerrariansConstruct.Items;

namespace TerrariansConstruct {
	public class ItemPartActionsBuilder {
		public ItemPart.PartItemFunc setItemDefaults;
		public ItemPart.PartProjectileFunc setProjectileDefaults;
		public ItemPart.PartPlayerFunc onUse;
		public ItemPart.PartPlayerFunc onHold;
		public ItemPart.PartPlayerFunc onGenericHotkeyUsage;
		public ItemPart.PartProjectileSpawnFunc onProjectileSpawn;
		public ItemPart.PartProjectileHitNPCFunc onHitNPC;
		public ItemPart.PartProjectileHitPlayerFunc onHitPlayer;
		public ItemPart.PartModifyWeaponDamageFunc modifyWeaponDamage;
		public ItemPart.PartModifyWeaponKnockbackFunc modifyWeaponKnockback;
		public ItemPart.PartModifyWeaponCritFunc modifyWeaponCrit;
		public ItemPart.PartProjectileFunc projectileAI;

		public ItemPartActionsBuilder WithItemDefaults(ItemPart.PartItemFunc setItemDefaults) {
			this.setItemDefaults = setItemDefaults;
			return this;
		}

		public ItemPartActionsBuilder WithProjectileDefaults(ItemPart.PartProjectileFunc setProjectileDefaults) {
			this.setProjectileDefaults = setProjectileDefaults;
			return this;
		}

		public ItemPartActionsBuilder WithOnUse(ItemPart.PartPlayerFunc onUse) {
			this.onUse = onUse;
			return this;
		}

		public ItemPartActionsBuilder WithOnHold(ItemPart.PartPlayerFunc onHold) {
			this.onHold = onHold;
			return this;
		}

		public ItemPartActionsBuilder WithOnGenericHotkeyUsage(ItemPart.PartPlayerFunc onGenericHotkeyUsage) {
			this.onGenericHotkeyUsage = onGenericHotkeyUsage;
			return this;
		}

		public ItemPartActionsBuilder WithOnProjectileSpawn(ItemPart.PartProjectileSpawnFunc onProjectileSpawn) {
			this.onProjectileSpawn = onProjectileSpawn;
			return this;
		}

		public ItemPartActionsBuilder WithOnHitNPC(ItemPart.PartProjectileHitNPCFunc onHitNPC) {
			this.onHitNPC = onHitNPC;
			return this;
		}

		public ItemPartActionsBuilder WithOnHitPlayer(ItemPart.PartProjectileHitPlayerFunc onHitPlayer) {
			this.onHitPlayer = onHitPlayer;
			return this;
		}

		public ItemPartActionsBuilder WithModifyWeaponDamage(ItemPart.PartModifyWeaponDamageFunc modifyWeaponDamage) {
			this.modifyWeaponDamage = modifyWeaponDamage;
			return this;
		}

		public ItemPartActionsBuilder WithModifyWeaponKnockback(ItemPart.PartModifyWeaponKnockbackFunc modifyWeaponKnockback) {
			this.modifyWeaponKnockback = modifyWeaponKnockback;
			return this;
		}

		public ItemPartActionsBuilder WithModifyWeaponCrit(ItemPart.PartModifyWeaponCritFunc modifyWeaponCrit) {
			this.modifyWeaponCrit = modifyWeaponCrit;
			return this;
		}

		public ItemPartActionsBuilder WithAI(ItemPart.PartProjectileFunc projectileAI) {
			this.projectileAI = projectileAI;
			return this;
		}
	}
}
