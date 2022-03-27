using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariansConstruct.Abilities;
using TerrariansConstruct.Items.Tools;
using TerrariansConstruct.Items.Weapons;
using TerrariansConstruct.Projectiles;
using TerrariansConstruct.UI;
using TerrariansConstructLib;
using TerrariansConstructLib.API.Stats;
using TerrariansConstructLib.Materials;

namespace TerrariansConstruct {
	public partial class CoreMod : Mod {
		internal static UserInterface forgeUIInterface;
		internal static ForgeUI forgeUI;

		public static CoreMod Instance => ModContent.GetInstance<CoreMod>();

		public const string RecipeGroup_AnyWorkbench = "TerrariansConstruct:AnyWorkbench";

		internal static Dictionary<ushort, int> oreTileToOreItem;
		internal static bool disableOreTracking;

		public override void Load() {
			Utility.ForceLoadModHJsonLocalization(this);

			CoreLibMod.SetLoadingSubProgressText(Language.GetTextValue("Mods.TerrariansConstruct.Loading.Parts"));

			AddParts();

			oreTileToOreItem = new() {
				[TileID.Copper] = ItemID.CopperOre,
				[TileID.Iron] = ItemID.IronOre,
				[TileID.Silver] = ItemID.SilverOre,
				[TileID.Gold] = ItemID.GoldOre,
				[TileID.Tin] = ItemID.TinOre,
				[TileID.Lead] = ItemID.LeadOre,
				[TileID.Tungsten] = ItemID.TungstenOre,
				[TileID.Platinum] = ItemID.PlatinumOre,
				[TileID.Demonite] = ItemID.DemoniteOre,
				[TileID.Crimtane] = ItemID.CrimtaneOre,
				[TileID.Meteorite] = ItemID.Meteorite,
				[TileID.Hellstone] = ItemID.Hellstone,
				[TileID.Cobalt] = ItemID.CobaltOre,
				[TileID.Mythril] = ItemID.MythrilOre,
				[TileID.Adamantite] = ItemID.AdamantiteOre,
				[TileID.Palladium] = ItemID.PalladiumOre,
				[TileID.Orichalcum] = ItemID.OrichalcumOre,
				[TileID.Titanium] = ItemID.TitaniumOre,
				[TileID.Chlorophyte] = ItemID.ChlorophyteOre,
				[TileID.LunarOre] = ItemID.LunarOre
			};

			disableOreTracking = false;

			if (!Main.dedServ) {
				CoreLibMod.SetLoadingSubProgressText(Language.GetTextValue("Mods.TerrariansConstruct.Loading.ForgeUI"));

				forgeUIInterface = new();
				forgeUI = new();

				forgeUI.Activate();
			}

			CoreLibMod.SetLoadingSubProgressText("");
		}

		public override void Unload() {
			forgeUI = null!;
			forgeUIInterface = null!;

			oreTileToOreItem = null!;
		}

		public override void AddRecipeGroups() {
			CoreLibMod.RegisterRecipeGroup(RecipeGroup_AnyWorkbench, ItemID.WorkBench,
				ItemID.WorkBench,
				ItemID.BambooWorkbench,
				ItemID.BlueDungeonWorkBench,
				ItemID.BoneWorkBench,
				ItemID.BorealWoodWorkBench,
				ItemID.CactusWorkBench,
				ItemID.CrystalWorkbench,
				ItemID.DynastyWorkBench,
				ItemID.EbonwoodWorkBench,
				ItemID.FleshWorkBench,
				ItemID.FrozenWorkBench,
				ItemID.GlassWorkBench,
				ItemID.GoldenWorkbench,
				ItemID.GothicWorkBench,
				ItemID.GraniteWorkBench,
				ItemID.GreenDungeonWorkBench,
				ItemID.HoneyWorkBench,
				ItemID.LesionWorkbench,
				ItemID.LihzahrdWorkBench,
				ItemID.LivingWoodWorkBench,
				ItemID.MarbleWorkBench,
				ItemID.MartianWorkBench,
				ItemID.MeteoriteWorkBench,
				ItemID.MushroomWorkBench,
				ItemID.NebulaWorkbench,
				ItemID.ObsidianWorkBench,
				ItemID.PalmWoodWorkBench,
				ItemID.PearlwoodWorkBench,
				ItemID.PinkDungeonWorkBench,
				ItemID.PumpkinWorkBench,
				ItemID.RichMahoganyWorkBench,
				ItemID.SandstoneWorkbench,
				ItemID.ShadewoodWorkBench,
				ItemID.SkywareWorkbench,
				ItemID.SlimeWorkBench,
				ItemID.SolarWorkbench,
				ItemID.SpiderWorkbench,
				ItemID.SpookyWorkBench,
				ItemID.StardustWorkbench,
				ItemID.SteampunkWorkBench,
				ItemID.VortexWorkbench);
		}

		// Method used by the library mod to load the parts
		// NOTE: this will be called before your mod's Load hook and before it's registered to ContentInstance<T>!
		public static void RegisterTCItemParts(Mod mod) {
			RegisteredParts.ToolRod =               RegisterPart(mod, 4 * 2, nameof(RegisteredParts.ToolRod),               "Tool Rod",         StatType.Handle);
			RegisteredParts.ToolBinding =           RegisterPart(mod, 6 * 2, nameof(RegisteredParts.ToolBinding),           "Tool Binding",     StatType.Extra);
			RegisteredParts.ToolPickHead =          RegisterPart(mod, 11,    nameof(RegisteredParts.ToolPickHead),          "Pickaxe Head",     StatType.Head);
			RegisteredParts.ToolAxeHead =           RegisterPart(mod, 7 * 2, nameof(RegisteredParts.ToolAxeHead),           "Axe Head",         StatType.Head);
			RegisteredParts.ToolHammerHead =        RegisterPart(mod, 8 * 2, nameof(RegisteredParts.ToolHammerHead),        "Hammer Head",      StatType.Head);
			RegisteredParts.WeaponSwordGuard =      RegisterPart(mod, 7,     nameof(RegisteredParts.WeaponSwordGuard),      "Sword Guard",      StatType.Extra);
			RegisteredParts.WeaponLongSwordBlade =  RegisterPart(mod, 6 * 2, nameof(RegisteredParts.WeaponLongSwordBlade),  "Sword Blade",      StatType.Head);
			RegisteredParts.WeaponShortSwordBlade = RegisterPart(mod, 5 * 2, nameof(RegisteredParts.WeaponShortSwordBlade), "Shortsword Blade", StatType.Head);
			RegisteredParts.WeaponBowHead =         RegisterPart(mod, 9,     nameof(RegisteredParts.WeaponBowHead),         "Bow Head",         StatType.Head);
			RegisteredParts.WeaponBowString =       RegisterPart(mod, 5 * 2, nameof(RegisteredParts.WeaponBowString),       "String",           StatType.Extra);
			RegisteredParts.WeaponShortSwordGuard = RegisterPart(mod, 3 * 2, nameof(RegisteredParts.WeaponShortSwordGuard), "Shortsword Guard", StatType.Extra);

			CoreLibMod.SetToolPartFlags(RegisteredParts.ToolPickHead, isPick: true);
			CoreLibMod.SetToolPartFlags(RegisteredParts.ToolAxeHead, isAxe: true);
			CoreLibMod.SetToolPartFlags(RegisteredParts.ToolHammerHead, isHammer: true);
		}

		private static int RegisterPart(Mod mod, int materialCost, string internalName, string name, StatType type)
			=> CoreLibMod.RegisterPart(mod, internalName, name, materialCost, true, "Assets/Parts/" + internalName, type);

		// Method used by the library mod to load the ammo
		// NOTE: this will be called before your mod's Load hook and before it's registered to ContentInstance<T>!
		public static void RegisterTCAmmunition(Mod mod) {
			RegisteredAmmo.Bullet = CoreLibMod.RegisterAmmo(mod, "Bullet", AmmoID.Bullet, nameof(TCBulletProjectile));
		}

		// Method used by the library mod to load the items used by the Forge UI
		// NOTE: this will be called before your mod's Load hook and before it's registered to ContentInstance<T>!
		public static void RegisterTCItems(Mod mod) {
			string visualsFolder = "Assets/Visuals/";

			//Forge UI Slots:
			//   0  1  2  3  4
			//   5  6  7  8  9
			//  10 11 12 13 14
			//  15 16 17 18 19
			//  20 21 22 23 24

			RegisteredItems.Sword = CoreLibMod.RegisterItem(mod, "Sword", "Sword", nameof(TCSword), visualsFolder + "Sword", 1f,
				(0,  4, RegisteredParts.WeaponLongSwordBlade),
				(1, 12, RegisteredParts.WeaponSwordGuard),
				(2, 20, RegisteredParts.ToolRod));

			RegisteredItems.Shortsword = CoreLibMod.RegisterItem(mod, "Shortsword", "Shortsword", nameof(TCShortsword), visualsFolder + "Shortsword", 0.65f,
				(0,  4, RegisteredParts.WeaponShortSwordBlade),
				(1, 12, RegisteredParts.WeaponShortSwordGuard),
				(2, 20, RegisteredParts.ToolRod));

			RegisteredItems.Pickaxe = CoreLibMod.RegisterItem(mod, "Pickaxe", "Pickaxe", nameof(TCPickaxe), visualsFolder + "Pickaxe", 0.8f,
				(0, 12, RegisteredParts.ToolBinding),
				(1,  7, RegisteredParts.ToolPickHead),
				(2, 13, RegisteredParts.ToolPickHead),
				(3, 16, RegisteredParts.ToolRod));

			/*
			ForgeUISlotConfiguration.Register(RegisteredItems.Axe,
				(0, 12, RegisteredParts.ToolBinding),
				(1,  8, RegisteredParts.ToolAxeHead),
				(2, 16, RegisteredParts.ToolRod));

			ForgeUISlotConfiguration.Register(RegisteredItems.Hammer,
				(0, 12, RegisteredParts.ToolBinding),
				(1,  8, RegisteredParts.ToolHammerHead),
				(2, 16, RegisteredParts.ToolRod));

			ForgeUISlotConfiguration.Register(RegisteredItems.Bow,
				(0,  7, RegisteredParts.WeaponBowHead),
				(1, 13, RegisteredParts.WeaponBowHead),
				(2, 16, RegisteredParts.WeaponBowString));
			*/
		}

		// Method used by the library mod to load the items used by the Forge UI
		// NOTE: this will be called before your mod's Load hook and before it's registered to ContentInstance<T>!
		public static void RegisterTCMaterials(Mod mod) {
			//head part: damage, knockback, crit, useSpeed, pickaxe power, axe power, hammer power, durability

			RegisteredMaterials.CopperBar = CoreLibMod.RegisterMaterialStats(ItemID.CopperBar, 1, new CopperAbility(),
				new HeadPartStats(7, 2.1f, pickPower: 35, axePower: 35, hammerPower: 35, durability: 300),
				new HandlePartStats(),
				new ExtraPartStats());

			RegisteredMaterials.Wood = CoreLibMod.RegisterMaterialStats(ItemID.Wood, 1, null,
				new HeadPartStats(5, 1f, useSpeed: 28, pickPower: 28, axePower: 28, hammerPower: 28, durability: 180),
				new HandlePartStats(attackKnockback: new StatModifier(1, 0.9f), durability: new StatModifier(1f, 1.05f)));

			RegisteredMaterials.Cobweb = CoreLibMod.RegisterMaterialStats(ItemID.Cobweb, 8, null,
				new ExtraPartStats()
					.With(CoreLibMod.KnownStatModifiers.BowDrawSpeed, StatModifier.One)
					.With(CoreLibMod.KnownStatModifiers.BowArrowSpeed, StatModifier.One)
					.SetValidPartIDs(CoreLibMod.KnownStatModifiers.BowDrawSpeed,
						RegisteredParts.WeaponBowString)
					.SetValidPartIDs(CoreLibMod.KnownStatModifiers.BowArrowSpeed,
						RegisteredParts.WeaponBowString));

			RegisteredMaterials.GoldBar = CoreLibMod.RegisterMaterialStats(ItemID.GoldBar, 1, new GoldAbility(),
				new HeadPartStats(13, 6.5f, 7, useSpeed: 16, pickPower: 55, axePower: 55, hammerPower: 55, durability: 1500),
				new HandlePartStats(miningSpeed: 1.1f, attackSpeed: new StatModifier(1f, 1.08f), durability: new StatModifier(-100, 0.9f)),
				new ExtraPartStats());
		}

		public static class RegisteredAmmo {
			public static int Bullet { get; internal set; }
			public static int Arrow { get; internal set; }
			public static int Flare { get; internal set; }
			public static int Coin { get; internal set; }
			public static int Dart { get; internal set; }
			public static int Rocket { get; internal set; }
		}

		public static class RegisteredParts {
			public static int ToolRod { get; internal set; }
			public static int ToolBinding { get; internal set; }
			public static int ToolPickHead { get; internal set; }
			public static int ToolAxeHead { get; internal set; }
			public static int ToolHammerHead { get; internal set; }
			public static int WeaponSwordGuard { get; internal set; }
			public static int WeaponLongSwordBlade { get; internal set; }
			public static int WeaponShortSwordBlade { get; internal set; }
			public static int WeaponBowHead { get; internal set; }
			public static int WeaponBowString { get; internal set; }
			public static int WeaponShortSwordGuard { get; internal set; }
		}

		public static class RegisteredItems {
			public static int Sword { get; internal set; }
			public static int Shortsword { get; internal set; }
			public static int Pickaxe { get; internal set; }
			public static int Axe { get; internal set; }
			public static int Hammer { get; internal set; }
			public static int Bow { get; internal set; }
			public static int Hamaxe { get; internal set; }
			public static int Hampixe { get; internal set; }  //Hammer, pickaxe and axe
		}

		public static class RegisteredMaterials {
			public static Material CopperBar { get; internal set; }
			public static Material Wood { get; internal set; }
			public static Material Cobweb { get; internal set; }
			public static Material GoldBar { get; internal set; }
			public static Material IronBar { get; internal set; }
			public static Material LeadBar { get; internal set; }
			public static Material PlatinumBar { get; internal set; }
			public static Material SilverBar { get; internal set; }
			public static Material StoneBlock { get; internal set; }
			public static Material TinBar { get; internal set; }
			public static Material TungstenBar { get; internal set; }
			public static Material Cloud { get; internal set; }
			public static Material LeafBlock { get; internal set; }
			public static Material RainCloud { get; internal set; }
			public static Material Rope { get; internal set; }
			public static Material Silk { get; internal set; }
			public static Material SnowCloud { get; internal set; }
			public static Material Vine { get; internal set; }
		}
	}
}