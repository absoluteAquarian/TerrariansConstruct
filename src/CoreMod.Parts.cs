using Terraria.Localization;
using Terraria.ModLoader;
using TerrariansConstructLib;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			string copperTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Copper");
			AddShardPart(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip);
			AddHeadParts(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip);
			AddHandleParts(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip);
			AddExtraParts(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip, null, null,
				RegisteredParts.ToolBinding,
				RegisteredParts.WeaponSwordGuard,
				RegisteredParts.WeaponShortSwordGuard);

			string woodTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Wood");
			string woodModifierText = "Mods.TerrariansConstruct.ModifierText.Common.Knockback";
			StatModifier woodModifier = new(1f, 0.9f);
			AddShardPart(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip, woodModifierText, woodModifier);
			AddHeadParts(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip, woodModifierText, woodModifier);
			AddHandleParts(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip, woodModifierText, woodModifier);
			AddExtraParts(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip, woodModifierText, woodModifier,
				RegisteredParts.ToolBinding,
				RegisteredParts.WeaponSwordGuard,
				RegisteredParts.WeaponShortSwordGuard);

			CoreLibMod.AddPart(this, RegisteredMaterials.Cobweb, RegisteredParts.WeaponBowString, PartActions.NoActions, null, null);
		}

		private void AddShardPart(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, string? modifierTextLangKey = null, StatModifier? modifierStat = null)
			=> CoreLibMod.AddPart(this, material, CoreLibMod.RegisteredParts.Shard, commonActions, commonTooltip, modifierTextLangKey, modifierStat);

		private void AddHeadParts(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, string? modifierTextLangKey = null, StatModifier? modifierStat = null) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolPickHead, commonActions, commonTooltip, modifierTextLangKey, modifierStat);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolAxeHead, commonActions, commonTooltip, modifierTextLangKey, modifierStat);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolHammerHead, commonActions, commonTooltip, modifierTextLangKey, modifierStat);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponLongSwordBlade, commonActions, commonTooltip, modifierTextLangKey, modifierStat);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponShortSwordBlade, commonActions, commonTooltip, modifierTextLangKey, modifierStat);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponBowHead, commonActions, commonTooltip, modifierTextLangKey, modifierStat);
		}

		private void AddHandleParts(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, string? modifierTextLangKey = null, StatModifier? modifierStat = null) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolRod, commonActions, commonTooltip, modifierTextLangKey, modifierStat);
		}

		private void AddExtraParts(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, string? modifierTextLangKey, StatModifier? modifierStat, params int[] parts) {
			for (int i = 0; i < parts.Length; i++)
				CoreLibMod.AddPart(this, material, parts[i], commonActions, commonTooltip, modifierTextLangKey, modifierStat);
		}
	}
}
