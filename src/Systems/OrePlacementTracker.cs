using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerrariansConstruct.Systems {
	internal class OrePlacementTracker : ModSystem {
		public static HashSet<Point16> tilesPlacedByPlayers;

		public override void OnWorldLoad() {
			tilesPlacedByPlayers = new();
		}

		public override void OnWorldUnload() {
			tilesPlacedByPlayers = null!;
		}

		public override void SaveWorldData(TagCompound tag) {
			tag["placed"] = tilesPlacedByPlayers.ToList();
		}

		public override void LoadWorldData(TagCompound tag) {
			if (tag.GetList<Point16>("placed") is var list)
				tilesPlacedByPlayers = new(list);
		}
	}
}
