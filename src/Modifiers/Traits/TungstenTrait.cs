using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using TerrariansConstructLib;
using TerrariansConstructLib.API.Sources;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	internal class TungstenTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0x99, 0x66, 0x33);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Tungsten";

		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void UseSpeedMultiplier(Player player, BaseTCItem item, ref StatModifier multiplier) {
			if (player.Bottom.Y / 16d > Main.worldSurface) {
				//Linearly increase the bonus the further down the player is, up to a maximum of +10% per Tier
				multiplier += (float)GetBonus(player, 0.1, Tier);
			}
		}

		public override bool CanLoseDurability(Player player, BaseTCItem item, IDurabilityModificationSource source) {
			if (player.Bottom.Y / 16d <= Main.worldSurface) {
				//No bonus
				return true;
			} else {
				//Linearly increase the bonus the further down the player is, up to a maximum of +5% per Tier up to a maximum of +25%
				double chance = GetBonus(player, 5d, Math.Min(5, Tier));

				return player.RollLuck(100) >= chance;
			}
		}

		private static double GetBonus(Player player, double strengthPerTier, int tier) {
			double bonusTotal = strengthPerTier * tier;

			double depthFactor = Utility.InverseLerp(Main.worldSurface, Main.maxTilesY, player.Bottom.Y / 16d);
			depthFactor = Utils.Clamp(depthFactor, 0d, 1d);

			return bonusTotal * depthFactor;
		}
	}
}
