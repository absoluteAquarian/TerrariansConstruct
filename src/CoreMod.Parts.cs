using Terraria.ID;
using TerrariansConstructLib.ID;
using LibMod = TerrariansConstructLib.CoreMod;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			LibMod.AddAllPartsOfType(this, ItemID.CopperBar, ItemRarityID.White, TCPartActions.Copper, "[c/ff6000:Energized I]", MaterialPartID.WeaponBowString);
			LibMod.AddAllPartsOfType(this, ItemID.Wood, ItemRarityID.White, TCPartActions.Wood, "[c/996633:Wimpy I]", MaterialPartID.WeaponBowString);

			LibMod.AddPart(this, ItemID.Cobweb, ItemRarityID.White, MaterialPartID.WeaponBowString, TCPartActions.Cobweb, null);
		}
	}
}
