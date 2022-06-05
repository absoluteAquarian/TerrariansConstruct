using TerrariansConstructLib;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			// === COPPER ===
			AddAllTypicalParts(RegisteredMaterials.CopperBar);

			// === WOOD ===
			AddAllTypicalParts(RegisteredMaterials.Wood);

			// === COBWEB ===
			CoreLibMod.AddPart(this, RegisteredMaterials.Cobweb, RegisteredParts.WeaponBowString);

			// === GOLD ===
			AddAllTypicalParts(RegisteredMaterials.GoldBar);

			// === IRON ===
			AddAllTypicalParts(RegisteredMaterials.IronBar);

			// === LEAD ===
			AddAllTypicalParts(RegisteredMaterials.LeadBar);

			// === PLATINUM ===
			AddAllTypicalParts(RegisteredMaterials.PlatinumBar);

			// === SILVER ===
			AddAllTypicalParts(RegisteredMaterials.SilverBar);

			// === STONE ===
			AddAllTypicalParts(RegisteredMaterials.StoneBlock);

			// === TIN ===
			AddAllTypicalParts(RegisteredMaterials.TinBar);

			// === TUNGSTEN ===
			AddAllTypicalParts(RegisteredMaterials.TungstenBar);
		}

		private void AddShardPart(Material material)
			=> CoreLibMod.AddPart(this, material, CoreLibMod.RegisteredParts.Shard);

		private void AddHeadParts(Material material) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolPickHead);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolAxeHead);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolHammerHead);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponLongSwordBlade);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponShortSwordBlade);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponBowHead);
		}

		private void AddHandleParts(Material material) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolRod);
		}

		private void AddExtraParts(Material material, int[] parts) {
			for (int i = 0; i < parts.Length; i++)
				CoreLibMod.AddPart(this, material, parts[i]);
		}

		private void AddAllTypicalParts(Material material) {
			AddShardPart(material);
			AddHeadParts(material);
			AddHandleParts(material);
			AddExtraParts(material,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				});
		}
	}
}
