using Terraria.ID;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.API;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Tools {
	public class TCHammer : BaseTCItem {
		public override int ItemDefinition => CoreLibMod.ItemType<Hammer>();

		public override void SafeSetDefaults() {
			//useTime/useAnimation are overridden anyway
			Item.DefaultToMeleeWeapon(0, ItemUseStyleID.Swing, useTurn: true);
			Item.width = 36;
			Item.height = 36;
			Item.UseSound = SoundID.Item1;
		}

		public override void OnInitializedWithParts() {
			Item.hammer = GetHammerPower();
		}
	}
}
