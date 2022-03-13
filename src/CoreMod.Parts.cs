using Terraria.ID;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			AddAllPartsOfType(this, ItemID.CopperBar, ItemRarityID.White, PartActions.Copper, "[c/ff6000:Energized I]", MaterialPartID.WeaponBowString);
			AddAllPartsOfType(this, ItemID.Wood, ItemRarityID.White, PartActions.Wood, "[c/996633:Wimpy I]", MaterialPartID.WeaponBowString);

			AddPart(this, ItemID.Cobweb, ItemRarityID.White, MaterialPartID.WeaponBowString, PartActions.Cobweb, null);
		}
	}
}
