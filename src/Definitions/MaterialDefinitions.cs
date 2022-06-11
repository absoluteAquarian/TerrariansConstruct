using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstruct.Modifiers.ForgeModifiers;
using TerrariansConstruct.Modifiers.Traits;
using TerrariansConstructLib;
using TerrariansConstructLib.API.Definitions;
using TerrariansConstructLib.API.Stats;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Definitions {
	public abstract class TCBaseMaterialDefinition<T> : MaterialDefinition where T : BaseTrait {
		public readonly int itemType;
		public readonly IPartStats[] stats;

		public override Material? Material => Material.FromItem(itemType);

		public override BaseTrait? Trait => GetTrait<T>();

		public override IEnumerable<PartDefinition> ValidParts => CoreMod.AllTypicalParts;

		protected TCBaseMaterialDefinition(int itemType, params IPartStats[] stats) {
			this.itemType = itemType;
			this.stats = stats;
		}

		public override IEnumerable<IPartStats> GetMaterialStats() => stats;
	}
	
	//First sprite set
	public sealed class CopperMaterial : TCBaseMaterialDefinition<CopperTrait> {
		public CopperMaterial() : base(ItemID.CopperBar,
			new HeadPartStats(7, 2.1f, pickPower: 35, axePower: 35, hammerPower: 35, durability: 300, toolRange: -1),
			new HandlePartStats(),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(50, 1f))) { }
	}

	public sealed class WoodMaterial : TCBaseMaterialDefinition<WoodTrait> {
		public WoodMaterial() : base(ItemID.Wood,
			new HeadPartStats(5, 1f, useSpeed: 28, pickPower: 28, axePower: 28, hammerPower: 28, durability: 180, toolRange: -2),
			new HandlePartStats(attackKnockback: new StatModifier(0, 0.9f), durability: new StatModifier(0, 1.05f)),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(30, 1f))) { }
	}

	public sealed class CobwebMaterial : MaterialDefinition {
		public override Material? Material => Material.FromItem(ItemID.Cobweb);

		public override BaseTrait? Trait => GetTrait<CobwebTrait>();

		public override IEnumerable<PartDefinition> ValidParts
			=> new PartDefinition[] {
				CoreLibMod.GetPartDefinition<WeaponBowString>()!
			};

		public override int MaterialWorth => 8;

		public override IEnumerable<IPartStats> GetMaterialStats()
			=> new IPartStats[] {
				new ExtraPartStats()
					.With(CoreLibMod.KnownStatModifiers.BowDrawSpeed, StatModifier.Default)
					.SetValidPartIDs(CoreLibMod.KnownStatModifiers.BowDrawSpeed,
						CoreLibMod.PartType<WeaponBowString>())
					.With(CoreLibMod.KnownStatModifiers.BowArrowSpeed, StatModifier.Default)
					.SetValidPartIDs(CoreLibMod.KnownStatModifiers.BowArrowSpeed,
						CoreLibMod.PartType<WeaponBowString>())
			};
	}

	//Second sprite set
	public sealed class GoldMaterial : TCBaseMaterialDefinition<GoldTrait> {
		public GoldMaterial() : base(ItemID.GoldBar,
			new HeadPartStats(13, 6.5f, 7, useSpeed: 16, pickPower: 55, axePower: 55, hammerPower: 55, durability: 1200),
			new HandlePartStats(durability: new StatModifier(-100, 0.9f)),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(-10, 1f))) { }
	}

	public sealed class IronMaterial : TCBaseMaterialDefinition<IronTrait> {
		public IronMaterial() : base(ItemID.IronBar,
			new HeadPartStats(10, 3.3f, 0, useSpeed: 22, pickPower: 40, axePower: 40, hammerPower: 40, durability: 650),
			new HandlePartStats(miningSpeed: 1.05f, attackSpeed: new StatModifier(1f, 1.05f), attackKnockback: new StatModifier(1f, 1.4f)),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(45, 1f))) { }
	}

	public sealed class LeadMaterial : TCBaseMaterialDefinition<LeadTrait> {
		public LeadMaterial() : base(ItemID.LeadBar,
			new HeadPartStats(11, 2.9f, 0, useSpeed: 20, pickPower: 43, axePower: 43, hammerPower: 43, durability: 700),
			new HandlePartStats(),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(50, 1f))) { }
	}

	public sealed class PlatinumMaterial : TCBaseMaterialDefinition<PlatinumTrait> {
		public PlatinumMaterial() : base(ItemID.PlatinumBar,
			new HeadPartStats(15, 6f, 0, useSpeed: 19, pickPower: 59, axePower: 59, hammerPower: 59, durability: 1100),
			new HandlePartStats(),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(100, 1f))) { }
	}

	public sealed class SilverMaterial : TCBaseMaterialDefinition<SilverTrait> {
		public SilverMaterial() : base(ItemID.SilverBar,
			new HeadPartStats(11, 4f, 2, useSpeed: 21, pickPower: 45, axePower: 45, hammerPower: 45, durability: 950),
			new HandlePartStats(attackDamage: new StatModifier(0, 0.9f), attackKnockback: new StatModifier(0, 0.95f)),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(80, 1f))) { }
	}

	public sealed class StoneMaterial : TCBaseMaterialDefinition<StoneTrait> {
		public StoneMaterial() : base(ItemID.StoneBlock,
			new HeadPartStats(6, 1.5f, -3, useSpeed: 26, pickPower: 30, axePower: 30, hammerPower: 30, durability: 250, toolRange: -1),
			new HandlePartStats(durability: new StatModifier(-25, 0.85f)),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(-10, 0.95f))) { }
	}

	public sealed class TinMaterial : TCBaseMaterialDefinition<TinTrait> {
		public TinMaterial() : base(ItemID.TinBar,
			new HeadPartStats(9, 2.3f, 0, useSpeed: 22, pickPower: 35, axePower: 35, hammerPower: 35, durability: 350, toolRange: -1),
			new HandlePartStats(),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(55, 1f))) { }
	}

	public sealed class TungstenMaterial : TCBaseMaterialDefinition<TungstenTrait> {
		public TungstenMaterial() : base(ItemID.TungstenBar,
			new HeadPartStats(12, 3.9f, 0, useSpeed: 20, pickPower: 50, axePower: 50, hammerPower: 50, durability: 1050),
			new HandlePartStats(),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(90, 1f))) { }
	}

	//Third sprite set
	public sealed class CactusMaterial : TCBaseMaterialDefinition<CactusTrait> {
		public CactusMaterial() : base(ItemID.Cactus,
			new HeadPartStats(5, 1.7f, pickPower: 35, axePower: 35, hammerPower: 35, durability: 220),
			new HandlePartStats(),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(20, 0.95f))) { }
	}

	public sealed class GlassMaterial : TCBaseMaterialDefinition<GlassTrait> {
		public GlassMaterial() : base(ItemID.Glass,
			new HeadPartStats(3, knockback: 0.4f, crit: -5, pickPower: 30, axePower: 30, hammerPower: 30, durability: 20),
			new HandlePartStats(attackDamage: new StatModifier(0, 0.8f), attackKnockback: new StatModifier(0, 0.7f), durability: new StatModifier(-50, 0.8f)),
			new ExtraPartStats()
				.With(CoreLibMod.KnownStatModifiers.ExtraDurability, new StatModifier(-10, 0.7f))) { }
	}

	/*
	public static Material AshBlock { get; internal set; }
	public static Material BlueBrick { get; internal set; }
	public static Material BorealWood { get; internal set; }
	public static Material CandyCaneBlock { get; internal set; }
	public static Material Chain { get; internal set; }
	public static Material DirtBlock { get; internal set; }
	public static Material Ebonwood { get; internal set; }
	public static Material GraniteBlock { get; internal set; }
	public static Material GreenBrick { get; internal set; }
	public static Material GreenCandyCaneBlock { get; internal set; }
	public static Material HellstoneBar { get; internal set; }
	public static Material MarbleBlock { get; internal set; }
	public static Material Obsidian { get; internal set; }
	public static Material PickBrick { get; internal set; }
	public static Material PalmWood { get; internal set; }
	public static Material RichMahogany { get; internal set; }
	public static Material Shadewood { get; internal set; }
	*/
}
