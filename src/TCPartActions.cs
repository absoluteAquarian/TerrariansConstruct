﻿using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstructLib;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct {
	/// <summary>
	/// A collection of action builders for base Terrarians' Construct materials
	/// </summary>
	public static class TCPartActions {
		public static readonly ItemPartActionsBuilder Copper = new ItemPartActionsBuilder(isReadonly: true)
			.WithOnGenericHotkeyUsage((part, player, item) => {
				var tc = item.ModItem as BaseTCItem;

				if (tc.CopperChargeReady) {
					tc.copperChargeActivated = true;

					SoundEngine.PlaySound(SoundID.Item92.WithVolume(0.7f).WithPitchVariance(0.1f), player.Center);
					SoundEngine.PlaySound(SoundID.Item93.WithVolume(0.37f).WithPitchVariance(0.18f), player.Center);
				}
			});

		public static readonly ItemPartActionsBuilder Wood = new ItemPartActionsBuilder(isReadonly: true)
			.WithModifyWeaponKnockback((int partID, Player player, ref StatModifier knockback, ref float flat) => {
				knockback *= 0.9f;
			});

		public static readonly ItemPartActionsBuilder Cobweb = new ItemPartActionsBuilder(isReadonly: true)
			.WithItemDefaults((partID, item) => {
				if (partID != CoreMod.RegisteredParts.WeaponBowString)
					throw new Exception($"Cobweb is only expected to be used on {nameof(CoreMod)}.{nameof(CoreMod.RegisteredParts)}.{nameof(CoreMod.RegisteredParts.WeaponBowString)} parts");

				//No-op, but it's here for an example
				item.shootSpeed *= 1;
			});
	}
}