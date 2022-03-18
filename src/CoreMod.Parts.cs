using Terraria.ID;
using TerrariansConstructLib;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			CoreLibMod.AddAllPartsOfType(this, ItemID.CopperBar, TCPartActions.Copper, "[c/ff6000:Energized I]", null, RegisteredParts.WeaponBowString);
			CoreLibMod.AddAllPartsOfType(this, ItemID.Wood, TCPartActions.Wood, "[c/996633:Wimpy I]", "[c/996633:-10% knockback]", RegisteredParts.WeaponBowString);

			CoreLibMod.AddPart(this, ItemID.Cobweb, ItemRarityID.White, RegisteredParts.WeaponBowString, TCPartActions.Cobweb, null, null);
		}
	}
}
