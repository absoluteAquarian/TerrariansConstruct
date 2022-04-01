using Terraria;
using TerrariansConstructLib.Abilities;
using TerrariansConstructLib.API.Sources;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Abilities {
	internal class WoodAbility : BaseAbility {
		public override bool IsSingleton => true;

		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void OnUpdateInventory(Player player, BaseTCItem item) {
			int tier = GetTierFromItem<WoodAbility>(item);
			int chance = tier * (tier + 1) / 2;  //Sum from 1 to N
			
			if (!Main.rand.NextBool(chance, 800))
				return;

			// Add 1 to 5 durability
			item.TryIncreaseDurability(player, Main.rand.Next(1, 6), new DurabilityModificationSource_Regen(this));
		}
	}
}
