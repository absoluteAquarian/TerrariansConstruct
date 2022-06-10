using Microsoft.Xna.Framework;
using Terraria;
using TerrariansConstructLib.API.Sources;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	public class WoodTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0x99, 0x66, 0x33);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Wood";

		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void OnUpdateInventory(Player player, BaseTCItem item) {
			int chance = Tier * (Tier + 1) / 2;  //Sum from 1 to N
			
			if (player.RollLuck(800) >= chance)
				return;

			// Add 1 to 5 durability
			item.TryIncreaseDurability(player, Main.rand.Next(1, 6), new DurabilityModificationSource_Regen(this));
		}
	}
}
