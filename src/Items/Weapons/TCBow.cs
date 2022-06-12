using Terraria.ID;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	public class TCBow : BaseTCItem {
		public override int ItemDefinition => CoreLibMod.ItemType<Bow>();

		public override int UseAmmoIDClassification => AmmoID.Arrow;

		public override void SafeSetDefaults() {
			//useTime/useAnimation are overridden anyway
			Item.DefaultToBow(0, 0f);
			Item.width = 24;
			Item.height = 44;
		}
	}
}
