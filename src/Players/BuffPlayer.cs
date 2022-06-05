using Terraria.ModLoader;
using TerrariansConstruct.Buffs;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Players {
	internal class BuffPlayer : ModPlayer {
		public override void PreUpdateBuffs() {
			if (Player.HeldItem.ModItem is not BaseTCItem) {
				Player.ClearBuff(ModContent.BuffType<CopperAbilityReady>());
				Player.ClearBuff(ModContent.BuffType<CopperAbilityActive>());
			}
		}
	}
}
