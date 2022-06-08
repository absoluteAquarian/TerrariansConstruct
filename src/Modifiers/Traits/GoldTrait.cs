using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using TerrariansConstruct.Systems;
using TerrariansConstructLib.DataStructures;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	internal sealed class GoldTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0xdd, 0xdd, 0x00);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Gold";

		//All this ability does is track tile destructions
		public override bool ShouldUpdateCounter(Player player) => false;

		//Needs to be greater than zero since the counter isn't being updated and it's "supposed to" increment,
		//just so that OnCounterTargetReached doesn't get called every tick
		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void OnTileDestroyed(Player player, BaseTCItem item, int x, int y, TileDestructionContext context) {
			//Conditions:
			// - player must have good luck
			// - destroyed tile must be an ore tile
			// - a 1/200 chance (affected by luck) must succeed
			// - the tile wasn't placed by a player
			// - ore tile type must have a registered item type
			if (player.luck >= 0 && TileID.Sets.Ore[context.type] && player.RollLuck(200) == 0 && !OrePlacementTracker.tilesPlacedByPlayers.Contains(new(x, y)) && CoreMod.oreTileToOreItem.TryGetValue(context.type, out int itemType)) {
				//Spawn another item
				Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 16, 16, itemType);

				DisplayMessageAbovePlayer(player, Color.Goldenrod, "Lucky!");
			}
		}
	}
}
