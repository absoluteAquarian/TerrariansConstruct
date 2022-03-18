using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariansConstruct.Items.Weapons;
using TerrariansConstruct.Projectiles;
using TerrariansConstruct.UI;
using TerrariansConstructLib;

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
			// Now that the part types are created, we can use them here
			SlotConfiguration.Initialize();
		}

		// Method used by the library mod to load the parts
		// NOTE: this will be called before your mod's Load hook and before it's registered to ContentInstance<T>!
		public static void RegisterTCItemParts(Mod mod) {
			RegisteredParts.ToolRod =               RegisterPart(mod, 4 * 2,  nameof(RegisteredParts.ToolRod),               "Tool Rod");
			RegisteredParts.ToolBinding =           RegisterPart(mod, 6 * 2,  nameof(RegisteredParts.ToolBinding),           "Tool Binding");
			RegisteredParts.ToolPickHead =          RegisterPart(mod, 11,     nameof(RegisteredParts.ToolPickHead),          "Pickaxe Head");
			RegisteredParts.ToolAxeHead =           RegisterPart(mod, 7 * 2,  nameof(RegisteredParts.ToolAxeHead),           "Axe Head");
			RegisteredParts.ToolHammerHead =        RegisterPart(mod, 8 * 2,  nameof(RegisteredParts.ToolHammerHead),        "Hammer Head");
			RegisteredParts.WeaponSwordGuard =      RegisterPart(mod, 7,      nameof(RegisteredParts.WeaponSwordGuard),      "Sword Guard");
			RegisteredParts.WeaponLongSwordBlade =  RegisterPart(mod, 6 * 2,  nameof(RegisteredParts.WeaponLongSwordBlade),  "Sword Blade");
			RegisteredParts.WeaponShortSwordBlade = RegisterPart(mod, 5 * 2,  nameof(RegisteredParts.WeaponShortSwordBlade), "Shortsword Blade");
			RegisteredParts.WeaponBowHead =         RegisterPart(mod, 9,      nameof(RegisteredParts.WeaponBowHead),         "Bow Head");
			RegisteredParts.WeaponBowString =       RegisterPart(mod, 20 * 2, nameof(RegisteredParts.WeaponBowString),       "String");
			RegisteredParts.WeaponShortSwordGuard = RegisterPart(mod, 3 * 2,  nameof(RegisteredParts.WeaponShortSwordGuard), "Shortsword Guard");
		}

		private static int RegisterPart(Mod mod, int materialCost, string internalName, string name)
			=> CoreLibMod.RegisterPart(mod, internalName, name, materialCost, true, "Assets/Parts/" + internalName);

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
		}
	}
}