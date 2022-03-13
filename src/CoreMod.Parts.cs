using Terraria.ID;
using TerrariansConstructLib;
using TerrariansConstructLib.ID;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			CoreLibMod.LogAddedParts = true;

			CoreLibMod.AddAllPartsOfType(this, ItemID.CopperBar, ItemRarityID.White, TCPartActions.Copper, "[c/ff6000:Energized I]", MaterialPartID.WeaponBowString);
			CoreLibMod.AddAllPartsOfType(this, ItemID.Wood, ItemRarityID.White, TCPartActions.Wood, "[c/996633:Wimpy I]", MaterialPartID.WeaponBowString);

			CoreLibMod.AddPart(this, ItemID.Cobweb, ItemRarityID.White, MaterialPartID.WeaponBowString, TCPartActions.Cobweb, null);

			CoreLibMod.LogAddedParts = false;
		}
	}
}
