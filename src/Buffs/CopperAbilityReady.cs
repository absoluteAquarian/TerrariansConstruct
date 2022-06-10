using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariansConstruct.Buffs {
	internal class CopperAbilityReady : ModBuff {
		public override string Texture => "TerrariansConstruct/Assets/Buffs/" + nameof(CopperAbilityReady);

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Energized I: Ready");
			Description.SetDefault("The Copper parts in your item are ready to release their energy");
			Main.buffNoTimeDisplay[Type] = true;
		}
	}
}
