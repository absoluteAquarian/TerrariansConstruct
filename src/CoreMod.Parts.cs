using TerrariansConstructLib;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			// === COPPER ===
			AddAllTypicalParts(RegisteredMaterials.CopperBar, PartActions.NoActions);

			// === WOOD ===
			AddAllTypicalParts(RegisteredMaterials.Wood, PartActions.NoActions);

			// === COBWEB ===
			CoreLibMod.AddPart(this, RegisteredMaterials.Cobweb, RegisteredParts.WeaponBowString, PartActions.NoActions);

			// === GOLD ===
			AddAllTypicalParts(RegisteredMaterials.GoldBar, PartActions.NoActions);

			// === IRON ===
			AddAllTypicalParts(RegisteredMaterials.IronBar, PartActions.NoActions);

			// === LEAD ===
			AddAllTypicalParts(RegisteredMaterials.LeadBar, PartActions.NoActions);

			// === PLATINUM ===
			AddAllTypicalParts(RegisteredMaterials.PlatinumBar, PartActions.NoActions);

			// === SILVER ===
			AddAllTypicalParts(RegisteredMaterials.SilverBar, PartActions.NoActions);

			// === STONE ===
			AddAllTypicalParts(RegisteredMaterials.StoneBlock, PartActions.NoActions);

			// === TIN ===
			AddAllTypicalParts(RegisteredMaterials.TinBar, PartActions.NoActions);

			// === TUNGSTEN ===
			AddAllTypicalParts(RegisteredMaterials.TungstenBar, PartActions.NoActions);
		}

		private void AddShardPart(Material material, ItemPartActionsBuilder commonActions)
			=> CoreLibMod.AddPart(this, material, CoreLibMod.RegisteredParts.Shard, commonActions);

		private void AddHeadParts(Material material, ItemPartActionsBuilder commonActions) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolPickHead, commonActions);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolAxeHead, commonActions);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolHammerHead, commonActions);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponLongSwordBlade, commonActions);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponShortSwordBlade, commonActions);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponBowHead, commonActions);
		}

		private void AddHandleParts(Material material, ItemPartActionsBuilder commonActions) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolRod, commonActions);
		}

		private void AddExtraParts(Material material, ItemPartActionsBuilder commonActions, int[] parts) {
			for (int i = 0; i < parts.Length; i++)
				CoreLibMod.AddPart(this, material, parts[i], commonActions);
		}

		private void AddAllTypicalParts(Material material, ItemPartActionsBuilder commonActions) {
			AddShardPart(material, commonActions);
			AddHeadParts(material, commonActions);
			AddHandleParts(material, commonActions);
			AddExtraParts(material, commonActions,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				});
		}
	}
}
