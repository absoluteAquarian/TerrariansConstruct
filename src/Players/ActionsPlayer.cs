using Terraria.ModLoader;

namespace TerrariansConstruct.Players {
	internal class ActionsPlayer : ModPlayer {
		public bool woodCheck;
		public int leadCheck;

		public override void ResetEffects() {
			woodCheck = false;
			leadCheck = -1;
		}
	}
}
