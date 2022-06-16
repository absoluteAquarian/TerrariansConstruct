using Terraria.ID;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Ammo {
	public class TCArrow : BaseTCItem {
		public override int ItemDefinition => CoreLibMod.ItemType<Arrow>();

		public override int AmmoIDClassification => AmmoID.Arrow;
	}
}
