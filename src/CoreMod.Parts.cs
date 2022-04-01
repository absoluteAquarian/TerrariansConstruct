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

			AddAllTypicalParts(RegisteredMaterials.CopperBar, PartActions.NoActions, copperTooltip, null);

			// === WOOD ===
			string woodTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Wood");
			HandlePartStats woodHandleStats = RegisteredMaterials.Wood.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] woodModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.Knockback", woodHandleStats.attackKnockback),
				new("Mods.TerrariansConstruct.ModifierText.Common.Durability", woodHandleStats.durability, useMultiplicativeOnly: true),
				new("Mods.TerrariansConstruct.ModifierText.Common.DurabilityAdd", woodHandleStats.durability, useAdditiveOnly: true)
			};

			AddAllTypicalParts(RegisteredMaterials.Wood, PartActions.NoActions, woodTooltip, woodModifierTexts);

			// === COBWEB ===
			CoreLibMod.AddPart(this, RegisteredMaterials.Cobweb, RegisteredParts.WeaponBowString, PartActions.NoActions, null, null);

			// === GOLD ===
			string goldTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Gold");
			HandlePartStats goldHandleStats = RegisteredMaterials.GoldBar.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] goldModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.Durability", goldHandleStats.durability, useMultiplicativeOnly: true),
				new("Mods.TerrariansConstruct.ModifierText.Common.DurabilityAdd", goldHandleStats.durability, useAdditiveOnly: true)
			};

			AddAllTypicalParts(RegisteredMaterials.GoldBar, PartActions.NoActions, goldTooltip, goldModifierTexts);

			// === IRON ===
			string ironTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Iron");
			HandlePartStats ironHandleStats = RegisteredMaterials.IronBar.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] ironModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.MiningSpeed", new(1f, ironHandleStats.miningSpeed), positiveValueIsGoodModifier: false),
				new("Mods.TerrariansConstruct.ModifierText.Common.AttackSpeed", ironHandleStats.attackSpeed, positiveValueIsGoodModifier: false),
				new("Mods.TerrariansConstruct.ModifierText.Common.Knockback", ironHandleStats.attackKnockback)
			};

			AddAllTypicalParts(RegisteredMaterials.IronBar, PartActions.NoActions, ironTooltip, ironModifierTexts);

			// === LEAD ===
			string leadTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Lead");

			AddAllTypicalParts(RegisteredMaterials.LeadBar, PartActions.NoActions, leadTooltip, null);

			// === PLATINUM ===
			AddAllTypicalParts(RegisteredMaterials.PlatinumBar, PartActions.NoActions, null, null);

			// === SILVER ===
			string silverTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Silver");
			HandlePartStats silverHandleStats = RegisteredMaterials.SilverBar.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] silverModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.Damage", silverHandleStats.attackDamage, useMultiplicativeOnly: true),
				new("Mods.TerrariansConstruct.ModifierText.Common.Knockback", silverHandleStats.attackKnockback, useMultiplicativeOnly: true)
			};

			AddAllTypicalParts(RegisteredMaterials.SilverBar, PartActions.NoActions, silverTooltip, silverModifierTexts);

			// === STONE ===
			string stoneTooltip = Language.GetTextValue("Mods.TerrariansConstruct.PartTooltips.Stone");
			HandlePartStats stoneHandleStats = RegisteredMaterials.StoneBlock.GetStat<HandlePartStats>(StatType.Handle)!;

			ModifierText.CreationContext[] stoneModifierTexts = new ModifierText.CreationContext[] {
				new("Mods.TerrariansConstruct.ModifierText.Common.Durability", stoneHandleStats.durability, useMultiplicativeOnly: true),
				new("Mods.TerrariansConstruct.ModifierText.Common.DurabilityAdd", stoneHandleStats.durability, useAdditiveOnly: true)
			};

			AddAllTypicalParts(RegisteredMaterials.StoneBlock, PartActions.NoActions, stoneTooltip, stoneModifierTexts);

			// === TIN ===
			AddAllTypicalParts(RegisteredMaterials.TinBar, PartActions.NoActions, null, null);

			// === TUNGSTEN ===
			AddAllTypicalParts(RegisteredMaterials.TungstenBar, PartActions.NoActions, null, null);
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

		private void AddAllTypicalParts(Material material, ItemPartActionsBuilder commonActions, string? commonTooltip, params ModifierText.CreationContext[]? modifierText) {
			AddShardPart(material, commonActions, commonTooltip, modifierText);
			AddHeadParts(material, commonActions, commonTooltip, modifierText);
			AddHandleParts(material, commonActions, commonTooltip, modifierText);
			AddExtraParts(material, commonActions, commonTooltip,
				new[] {
					RegisteredParts.ToolBinding,
					RegisteredParts.WeaponSwordGuard,
					RegisteredParts.WeaponShortSwordGuard
				},
				modifierText);
		}
	}
}
