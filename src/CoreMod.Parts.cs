using Terraria.Localization;
using TerrariansConstructLib;
using TerrariansConstructLib.API;
using TerrariansConstructLib.API.Stats;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct {
	partial class CoreMod {
		private void AddParts() {
			// === COPPER ===
			string copperTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Copper");
			AddShardPart(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip);
			AddHeadParts(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip);
			AddHandleParts(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip);
			AddExtraParts(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				},
				null);

			// === WOOD ===
			string woodTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Wood");
			HandlePartStats woodHandleStats = RegisteredMaterials.Wood.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] woodModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.Knockback", woodHandleStats.attackKnockback),
				new("Mods.TerrariansConstruct.ModifierText.Common.Durability", woodHandleStats.durability)
			};
			AddShardPart(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip, woodModifierTexts);
			AddHeadParts(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip, woodModifierTexts);
			AddHandleParts(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip, woodModifierTexts);
			AddExtraParts(RegisteredMaterials.Wood, TCPartActions.Wood, woodTooltip,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				},
				woodModifierTexts);

			// === COBWEB ===
			CoreLibMod.AddPart(this, RegisteredMaterials.Cobweb, RegisteredParts.WeaponBowString, PartActions.NoActions, null, null);

			// === GOLD ===
			string goldTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Gold");
			HandlePartStats goldHandleStats = RegisteredMaterials.GoldBar.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] goldModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.MiningSpeed", new(1f, goldHandleStats.miningSpeed)),
				new("Mods.TerrariansConstruct.ModifierText.Common.AttackSpeed", goldHandleStats.attackSpeed),
				new("Mods.TerrariansConstruct.ModifierText.Common.Durability", goldHandleStats.durability, useMultiplicativeOnly: true),
				new("Mods.TerrariansConstruct.ModifierText.Common.DurabilityAdd", goldHandleStats.durability, useAdditiveOnly: true)
			};

			AddShardPart(RegisteredMaterials.GoldBar, PartActions.NoActions, goldTooltip, goldModifierTexts);
			AddHeadParts(RegisteredMaterials.GoldBar, PartActions.NoActions, goldTooltip, goldModifierTexts);
			AddHandleParts(RegisteredMaterials.GoldBar, PartActions.NoActions, goldTooltip, goldModifierTexts);
			AddExtraParts(RegisteredMaterials.GoldBar, PartActions.NoActions, goldTooltip,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				},
				goldModifierTexts);

			// === IRON ===
			string ironTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Iron");
			HandlePartStats ironHandleStats = RegisteredMaterials.IronBar.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] ironModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.MiningSpeed", new(1f, ironHandleStats.miningSpeed)),
				new("Mods.TerrariansConstruct.ModifierText.Common.AttackSpeed", ironHandleStats.attackSpeed),
				new("Mods.TerrariansConstruct.ModifierText.Common.Knockback", ironHandleStats.attackKnockback),
			};
			AddShardPart(RegisteredMaterials.IronBar, PartActions.NoActions, ironTooltip, ironModifierTexts);
			AddHeadParts(RegisteredMaterials.IronBar, PartActions.NoActions, ironTooltip, ironModifierTexts);
			AddHandleParts(RegisteredMaterials.IronBar, PartActions.NoActions, ironTooltip, ironModifierTexts);
			AddExtraParts(RegisteredMaterials.IronBar, PartActions.NoActions, ironTooltip,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				},
				ironModifierTexts);

			// === LEAD ===
			string leadTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Lead");

			AddShardPart(RegisteredMaterials.IronBar, TCPartActions.Lead, leadTooltip, null);
			AddHeadParts(RegisteredMaterials.IronBar, TCPartActions.Lead, leadTooltip, null);
			AddHandleParts(RegisteredMaterials.IronBar, TCPartActions.Lead, leadTooltip, null);
			AddExtraParts(RegisteredMaterials.IronBar, TCPartActions.Lead, leadTooltip,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				},
				null);
		}

		private void AddShardPart(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, params ModifierText.CreationContext[]? modifierText)
			=> CoreLibMod.AddPart(this, material, CoreLibMod.RegisteredParts.Shard, commonActions, commonTooltip, modifierText);

		private void AddHeadParts(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, params ModifierText.CreationContext[]? modifierText) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolPickHead, commonActions, commonTooltip,  modifierText);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolAxeHead, commonActions, commonTooltip,  modifierText);
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolHammerHead, commonActions, commonTooltip,  modifierText);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponLongSwordBlade, commonActions, commonTooltip,  modifierText);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponShortSwordBlade, commonActions, commonTooltip,  modifierText);
			CoreLibMod.AddPart(this, material, RegisteredParts.WeaponBowHead, commonActions, commonTooltip,  modifierText);
		}

		private void AddHandleParts(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, params ModifierText.CreationContext[]? modifierText) {
			CoreLibMod.AddPart(this, material, RegisteredParts.ToolRod, commonActions, commonTooltip,  modifierText);
		}

		private void AddExtraParts(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, int[] parts, params ModifierText.CreationContext[]? modifierText) {
			for (int i = 0; i < parts.Length; i++)
				CoreLibMod.AddPart(this, material, parts[i], commonActions, commonTooltip, modifierText);
		}
	}
}
