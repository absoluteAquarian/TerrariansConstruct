using Terraria.ID;
using TerrariansConstructLib;
using TerrariansConstructLib.Materials;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			CoreLibMod.LogAddedParts = true;

			CoreLibMod.AddAllPartsOfType(this, ItemID.CopperBar, ItemRarityID.White, TCPartActions.Copper, "[c/ff6000:Energized I]", null, RegisteredParts.WeaponBowString);
			CoreLibMod.AddAllPartsOfType(this, ItemID.Wood, ItemRarityID.White, TCPartActions.Wood, "[c/996633:Wimpy I]", "[c/996633:-10% knockback]", RegisteredParts.WeaponBowString);

			CoreLibMod.AddPart(this, ItemID.Cobweb, ItemRarityID.White, RegisteredParts.WeaponBowString, TCPartActions.Cobweb, null, null);

			CoreLibMod.LogAddedParts = false;
		}

		private void AddMolds() {
			CoreLibMod.LogAddedParts = true;

			CoreLibMod.AddAllPartMoldsOfTier(this, new Material(){ type = ItemID.Wood, rarity = ItemRarityID.White }, RegisteredMoldTiers.Wood, 20, TileID.WorkBenches, 12, "Assets/Parts");
			CoreLibMod.AddAllPartMoldsOfTier(this, new Material(){ type = ItemID.GoldBar, rarity = ItemRarityID.Blue }, RegisteredMoldTiers.Gold, 24, TileID.Anvils, 14, "Assets/Parts");

			CoreLibMod.LogAddedParts = false;
		}
	}
}
