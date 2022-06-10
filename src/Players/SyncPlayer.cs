using Terraria;
using Terraria.ModLoader;

namespace TerrariansConstruct.Players {
	internal class SyncPlayer : ModPlayer {
		public override void OnEnterWorld(Player player) {
			NetHelper.RequestOrePlacements();
		}
	}
}
