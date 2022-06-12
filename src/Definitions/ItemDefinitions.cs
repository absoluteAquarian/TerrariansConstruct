using System.Collections.Generic;
using Terraria.ModLoader;
using TerrariansConstruct.Items.Tools;
using TerrariansConstruct.Items.Weapons;
using TerrariansConstructLib;
using TerrariansConstructLib.API.Definitions;
using TerrariansConstructLib.API.UI;

namespace TerrariansConstruct.Definitions {
	//Forge UI Slots:
	//   0  1  2  3  4
	//   5  6  7  8  9
	//  10 11 12 13 14
	//  15 16 17 18 19
	//  20 21 22 23 24
	
	public sealed class Sword : TCItemDefinition {
		public override int ItemType => ModContent.ItemType<TCSword>();

		public override IEnumerable<ForgeUISlotConfiguration> GetForgeSlotConfiguration()
			=> new ForgeUISlotConfiguration[] {
				(0,  8, CoreLibMod.PartType<WeaponLongSwordBlade>()),
				(1, 12, CoreLibMod.PartType<WeaponSwordGuard>()),
				(2, 16, CoreLibMod.PartType<ToolRod>())
			};
	}

	public sealed class Shortsword : TCItemDefinition {
		public override int ItemType => ModContent.ItemType<TCShortsword>();

		public override IEnumerable<ForgeUISlotConfiguration> GetForgeSlotConfiguration()
			=> new ForgeUISlotConfiguration[] {
				(0,  8, CoreLibMod.PartType<WeaponShortSwordBlade>()),
				(1, 12, CoreLibMod.PartType<WeaponShortSwordGuard>()),
				(2, 16, CoreLibMod.PartType<ToolRod>())
			};

		public override float UseSpeedMultiplier => 0.65f;
	}

	public sealed class Pickaxe : TCItemDefinition {
		public override int ItemType => ModContent.ItemType<TCPickaxe>();

		public override IEnumerable<ForgeUISlotConfiguration> GetForgeSlotConfiguration()
			=> new ForgeUISlotConfiguration[] {
				(0, 12, CoreLibMod.PartType<ToolBinding>()),
				(1,  7, CoreLibMod.PartType<ToolPickHead>()),
				(2, 13, CoreLibMod.PartType<ToolPickHead>()),
				(3, 16, CoreLibMod.PartType<ToolRod>())
			};
	}

	public sealed class Axe : TCItemDefinition {
		public override int ItemType => ModContent.ItemType<TCAxe>();

		public override IEnumerable<ForgeUISlotConfiguration> GetForgeSlotConfiguration()
			=> new ForgeUISlotConfiguration[] {
				(0, 12, CoreLibMod.PartType<ToolBinding>()),
				(1,  8, CoreLibMod.PartType<ToolAxeHead>()),
				(2, 16, CoreLibMod.PartType<ToolRod>())
			};

		public override float UseSpeedMultiplier => 1 / 0.6f;
	}

	public sealed class Hammer : TCItemDefinition {
		public override int ItemType => ModContent.ItemType<TCHammer>();

		public override IEnumerable<ForgeUISlotConfiguration> GetForgeSlotConfiguration()
			=> new ForgeUISlotConfiguration[] {
				(0, 12, CoreLibMod.PartType<ToolBinding>()),
				(1,  8, CoreLibMod.PartType<ToolHammerHead>()),
				(2, 16, CoreLibMod.PartType<ToolRod>())
			};

		public override float UseSpeedMultiplier => 1 / 0.4f;
	}

	/*
	ForgeUISlotConfiguration.Register(RegisteredItems.Bow,
		(0,  7, RegisteredParts.WeaponBowHead),
		(1, 13, RegisteredParts.WeaponBowHead),
		(2, 16, RegisteredParts.WeaponBowString));

	public static int Hamaxe { get; internal set; }
	public static int Hampixe { get; internal set; }  //Hammer, pickaxe and axe	
	*/
}
