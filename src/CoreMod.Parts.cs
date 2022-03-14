using Terraria.ID;
using TerrariansConstructLib;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			CoreLibMod.LogAddedParts = true;

			CoreLibMod.AddAllPartsOfType(this, ItemID.CopperBar, ItemRarityID.White, TCPartActions.Copper, "[c/ff6000:Energized I]", RegisteredParts.WeaponBowString);
			CoreLibMod.AddAllPartsOfType(this, ItemID.Wood, ItemRarityID.White, TCPartActions.Wood, "[c/996633:Wimpy I]", RegisteredParts.WeaponBowString);

			CoreLibMod.AddPart(this, ItemID.Cobweb, ItemRarityID.White, RegisteredParts.WeaponBowString, TCPartActions.Cobweb, null);
			
			CoreLibMod.AddAllPartsOfMaterial(this, new UnloadedMaterial(), PartActions.NoActions, "[c/fc51ff:Unloaded Part]");
			CoreLibMod.AddAllPartsOfMaterial(this, new UnknownMaterial(), PartActions.NoActions, "[c/616161:Unknown Part]");

			CoreLibMod.LogAddedParts = false;
		}
	}
}
