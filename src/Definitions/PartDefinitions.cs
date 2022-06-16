using TerrariansConstructLib.API.Definitions;
using TerrariansConstructLib.API.Stats;

namespace TerrariansConstruct.Definitions {
	public sealed class ToolRod : PartDefinition {
		public override StatType StatType => StatType.Handle;

		public override int MaterialCost => 4 * 2;
	}

	public sealed class ToolBinding : PartDefinition {
		public override StatType StatType => StatType.Extra;

		public override int MaterialCost => 6 * 2;
	}

	public sealed class ToolPickHead : PartDefinition {
		public override StatType StatType => StatType.Head;

		public override string DisplayName => "Pickaxe Head";

		public override int MaterialCost => 11;

		public override ToolType ToolType => ToolType.Pickaxe;
	}

	public sealed class ToolAxeHead : PartDefinition {
		public override StatType StatType => StatType.Head;

		public override string DisplayName => "Axe Head";

		public override int MaterialCost => 7 * 2;

		public override ToolType ToolType => ToolType.Axe;
	}

	public sealed class ToolHammerHead : PartDefinition {
		public override StatType StatType => StatType.Head;

		public override string DisplayName => "Hammer Head";

		public override int MaterialCost => 8 * 2;

		public override ToolType ToolType => ToolType.Hammer;
	}

	public sealed class WeaponSwordGuard : PartDefinition {
		public override StatType StatType => StatType.Extra;

		public override string DisplayName => "Sword Guard";

		public override int MaterialCost => 7;
	}

	public sealed class WeaponLongSwordBlade : PartDefinition {
		public override StatType StatType => StatType.Head;

		public override string DisplayName => "Sword Blade";

		public override int MaterialCost => 6 * 2;
	}

	public sealed class WeaponShortSwordBlade : PartDefinition {
		public override StatType StatType => StatType.Head;

		public override string DisplayName => "Shortsword Blade";

		public override int MaterialCost => 5 * 2;
	}

	public sealed class WeaponBowHead : PartDefinition {
		public override StatType StatType => StatType.Head;

		public override string DisplayName => "Bow Head";

		public override int MaterialCost => 9;
	}

	public sealed class WeaponBowString : PartDefinition {
		public override StatType StatType => StatType.Extra;

		public override string DisplayName => "String";

		public override int MaterialCost => 5 * 2;
	}

	public sealed class WeaponShortSwordGuard : PartDefinition {
		public override StatType StatType => StatType.Extra;

		public override string DisplayName => "Shortsword Guard";

		public override int MaterialCost => 3 * 2;
	}

	public sealed class AmmoArrowHead : PartDefinition {
		public override StatType StatType => StatType.Head;

		public override string DisplayName => "Arrow Head";

		public override int MaterialCost => 5 * 2;
	}

	public sealed class AmmoArrowShaft : PartDefinition {
		public override StatType StatType => StatType.Handle;

		public override string DisplayName => "Arrow Shaft";

		public override int MaterialCost => 7;
	}

	public sealed class AmmoArrowFletching : PartDefinition {
		public override StatType StatType => StatType.Extra;

		public override string DisplayName => "Arrow Fletching";

		public override int MaterialCost => 4 * 2;
	}
}
