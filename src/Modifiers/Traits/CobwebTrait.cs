using Microsoft.Xna.Framework;
using System;
using Terraria;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	public sealed class CobwebTrait : BaseTrait {
		public override Color TooltipColor => new(0x9e, 0xad, 0xae);

		public override bool IsSingleton => true;

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Cobweb";

		//This ability does not use a counter
		public override bool ShouldUpdateCounter(Player player) => false;

		//Needs to be greater than zero since the counter isn't being updated and it's "supposed to" increment,
		//just so that OnCounterTargetReached doesn't get called every tick
		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override bool CanConsumeAmmo(BaseTCItem weapon, BaseTCItem ammo, Player player) {
			//+5% chance to not consume ammo per tier, capped at +20%
			int chance = Math.Min(20, 5 * Tier);
			return player.RollLuck(100) >= chance;
		}
	}
}
