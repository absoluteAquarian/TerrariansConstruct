using Terraria;
using Terraria.ModLoader;

namespace TerrariansConstruct.Buffs {
	internal class CopperAbilityActive : ModBuff {
		public override string Texture => "TerrariansConstruct/Assets/Buffs/" + nameof(CopperAbilityActive);

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Energized I: Active");
			Description.SetDefault("The Copper parts in your item are releasing their energy,\n" +
				"boosting your mining and attack speed");
		}

		public override bool ReApply(Player player, int time, int buffIndex) {
			player.buffTime[buffIndex] = time;

			return false;
		}
	}
}
