using ReLogic.Reflection;
using System.Collections.Generic;

namespace TerrariansConstruct.ID {
	public static class MaterialPartID {
		public const int ToolRod = 0;
		public const int ToolBinding = 1;
		public const int ToolPickHead = 2;
		public const int ToolAxeHead = 3;
		public const int ToolHammerHead = 4;
		public const int WeaponSwordGuard = 5;
		public const int WeaponLongSwordBlade = 6;
		public const int WeaponShortSwordBlade = 7;

		public const int Count = 8;

		public static readonly IdDictionary Search = IdDictionary.Create(typeof(MaterialPartID), typeof(int));

		internal static int Register(string name, string assetFolderPath) {
			int next = nextID;

			registeredIDsToNames[next] = name;
			registeredIDsToAssetFolders[next] = assetFolderPath;

			nextID++;
			
			return next;
		}

		internal static void Load() {
			nextID = Count;

			registeredIDsToNames = new(){
				[ToolRod] =               "Tool Rod",
				[ToolBinding] =           "Binding",
				[ToolPickHead] =          "Pickaxe Head",
				[ToolAxeHead] =           "Axe Head",
				[ToolHammerHead] =        "Hammer Head",
				[WeaponSwordGuard] =      "Sword Guard",
				[WeaponLongSwordBlade] =  "Sword Blade",
				[WeaponShortSwordBlade] = "Shortsword Blade",
			};

			string folder = "TerrariansConstruct/Assets/Parts/";

			registeredIDsToAssetFolders = new(){
				[ToolRod] =               folder + "ToolRod",
				[ToolBinding] =           folder + "ToolBinding",
				[ToolPickHead] =          folder + "ToolPickHead",
				[ToolAxeHead] =           folder + "ToolAxeHead",
				[ToolHammerHead] =        folder + "ToolHammerHead",
				[WeaponSwordGuard] =      folder + "WeaponSwordGuard",
				[WeaponLongSwordBlade] =  folder + "WeaponLongSwordBlade",
				[WeaponShortSwordBlade] = folder + "WeaponShortSwordBlade",
			};
		}

		internal static void Unload() {
			registeredIDsToNames = null;
			registeredIDsToAssetFolders = null;

			nextID = -1;
		}

		internal static int nextID;

		public static int TotalCount => nextID;

		internal static Dictionary<int, string> registeredIDsToNames;
		internal static Dictionary<int, string> registeredIDsToAssetFolders;
	}
}
