using Microsoft.Xna.Framework;
using Terraria;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	internal sealed class IronTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0xbb, 0xbb, 0x99);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Iron";

		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;
	}
}
