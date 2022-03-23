using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariansConstruct.Items.Tools;
using TerrariansConstruct.Items.Weapons;
using TerrariansConstruct.Projectiles;
using TerrariansConstruct.UI;
using TerrariansConstructLib;
using TerrariansConstructLib.API.Stats;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Materials;

namespace TerrariansConstruct {
	public partial class CoreMod : Mod {
		internal static UserInterface forgeUIInterface;
		internal static ForgeUI forgeUI;

		public static CoreMod Instance => ModContent.GetInstance<CoreMod>();

		public override void Load() {
			AddParts();

			if (!Main.dedServ) {
				forgeUIInterface = new();
				forgeUI = new();

				forgeUI.Activate();
			}
		}

		public override void PostSetupContent() {
			//   0  1  2  3  4
			//   5  6  7  8  9
			//  10 11 12 13 14
			//  15 16 17 18 19
			//  20 21 22 23 24

			ForgeUISlotConfiguration.Register(RegisteredItems.Sword,
				(0,  4, RegisteredParts.WeaponLongSwordBlade),
				(1, 12, RegisteredParts.WeaponSwordGuard),
				(2, 20, RegisteredParts.ToolRod));

			ForgeUISlotConfiguration.Register(RegisteredItems.Shortsword,
				(0,  4, RegisteredParts.WeaponShortSwordBlade),
				(1, 12, RegisteredParts.WeaponShortSwordGuard),
				(2, 20, RegisteredParts.ToolRod));

			ForgeUISlotConfiguration.Register(RegisteredItems.Bow,
				(0,  7, RegisteredParts.WeaponBowHead),
				(1, 13, RegisteredParts.WeaponBowHead),
				(2, 16, RegisteredParts.WeaponBowString));

			ForgeUISlotConfiguration.Register(RegisteredItems.Pickaxe,
				(0, 12, RegisteredParts.ToolBinding),
				(1,  7, RegisteredParts.ToolPickHead),
				(2, 13, RegisteredParts.ToolPickHead),
				(3, 16, RegisteredParts.ToolRod));

			ForgeUISlotConfiguration.Register(RegisteredItems.Axe,
				(0, 12, RegisteredParts.ToolBinding),
				(1,  8, RegisteredParts.ToolAxeHead),
				(2, 16, RegisteredParts.ToolRod));

			ForgeUISlotConfiguration.Register(RegisteredItems.Hammer,
				(0, 12, RegisteredParts.ToolBinding),
				(1,  8, RegisteredParts.ToolHammerHead),
				(2, 16, RegisteredParts.ToolRod));
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

			RegisteredItems.Sword = CoreLibMod.RegisterItem(mod, "Sword", "Sword", nameof(TCSword), visualsFolder + "Sword",
				RegisteredParts.WeaponLongSwordBlade, RegisteredParts.WeaponSwordGuard, RegisteredParts.ToolRod);

			RegisteredItems.Shortsword = CoreLibMod.RegisterItem(mod, "Shortsword", "Shortsword", nameof(TCShortsword), visualsFolder + "Shortsword",
				RegisteredParts.WeaponShortSwordBlade, RegisteredParts.WeaponShortSwordGuard, RegisteredParts.ToolRod);

			RegisteredItems.Pickaxe = CoreLibMod.RegisterItem(mod, "Pickaxe", "Pickaxe", nameof(TCPickaxe), visualsFolder + "Pickaxe",
				RegisteredParts.ToolBinding, RegisteredParts.ToolPickHead, RegisteredParts.ToolPickHead, RegisteredParts.ToolRod);
		}

		// Method used by the library mod to load the items used by the Forge UI
		// NOTE: this will be called before your mod's Load hook and before it's registered to ContentInstance<T>!
		public static void RegisterTCMaterials(Mod mod) {
			RegisteredMaterials.CopperBar = CoreLibMod.RegisterMaterialStats(ItemID.CopperBar, 1,
				new HeadPartStats(7, 2.1f, 0, 20, 35, 300),
				new HandlePartStats(),
				new ExtraPartStats()
					.With(CoreLibMod.KnownStatModifiers.ExtraDurability, StatModifier.One));

			RegisteredMaterials.Wood = CoreLibMod.RegisterMaterialStats(ItemID.Wood, 1,
				new HeadPartStats(5, 1f, 0, 28, 28, 180),
				new HandlePartStats(attackKnockback: new StatModifier(1, 0.9f), durability: new StatModifier(1f, 1.05f)));

			RegisteredMaterials.Cobweb = CoreLibMod.RegisterMaterialStats(ItemID.Cobweb, 8,
				new ExtraPartStats()
					.With(CoreLibMod.KnownStatModifiers.BowDrawSpeed, StatModifier.One)
					.With(CoreLibMod.KnownStatModifiers.BowArrowSpeed, StatModifier.One));
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
		}
	}
}