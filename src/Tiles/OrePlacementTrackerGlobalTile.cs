using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariansConstruct.Tiles {
	internal class OrePlacementTrackerGlobalTile : GlobalTile {
		public override void PlaceInWorld(int i, int j, int type, Item item) {
			//This only runs when the player places the tile... great!
			if (TileID.Sets.Ore[type] && !CoreMod.disableOreTracking)
				NetHelper.SendOrePlaced(i, j);
		}

		public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem) {
			if (fail || effectOnly || !TileID.Sets.Ore[type])
				return;

			NetHelper.SendOreRemoval(i, j);
		}
	}
}
