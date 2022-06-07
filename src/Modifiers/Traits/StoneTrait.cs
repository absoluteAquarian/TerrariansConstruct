using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using TerrariansConstructLib.API.Sources;
using TerrariansConstructLib.DataStructures;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	internal sealed class StoneTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override bool CounterIncrements => false;

		public override Color TooltipColor => new(0xa0, 0xa0, 0xa0);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Stone";

		//Counter is kept track of manually
		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => -1;

		public override void OnTileDestroyed(Player player, BaseTCItem item, int x, int y, TileDestructionContext context) {
			if (Counter <= 0) {
				if (player.RollLuck(150) == 0) {
					//Lose 4 extra durability (5 total)
					item.TryReduceDurability(player, 4, new DurabilityModificationSource_Mining(context, x, y));

					// Between 20 and 50 tiles
					Counter = player.RollLuck(31) + 20;

					DisplayMessageAbovePlayer(player, Color.LightGray, "-5 DURABILITY");
				}
			} else
				Counter--;
		}

		public override void UseSpeedMultiplier(Player player, BaseTCItem item, ref StatModifier multiplier) {
			if (Counter > 0)
				multiplier += 0.08f;
		}
	}
}
