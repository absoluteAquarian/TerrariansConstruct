using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using TerrariansConstructLib.DataStructures;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	internal sealed class TinTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0xbb, 0xa5, 0x7c);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Tin";

		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void ModifyToolPower(Player player, BaseTCItem item, TileDestructionContext context, ref int power) {
			if (context.pickaxe && TileID.Sets.CanBeDugByShovel[context.type]) {
				//+5% increased damage against "soft" tiles per tier
				int tier = Math.Min(10, Tier);
				power += tier * 5;
			}
		}
	}
}
