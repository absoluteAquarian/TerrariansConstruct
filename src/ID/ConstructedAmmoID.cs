using ReLogic.Reflection;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstruct.Projectiles;

namespace TerrariansConstruct.ID {
	public static class ConstructedAmmoID {
		public const int Bullet = 0;
		public const int Arrow = 1;
		public const int Rocket = 2;
		public const int Dart = 3;
		public const int Coin = 4;

		public const int Count = 5;

		public static readonly IdDictionary Search = IdDictionary.Create(typeof(ConstructedAmmoID), typeof(int));

		internal static int Register<T>(string name, int ammoID) where T : BaseTCProjectile {
			if (name is null)
				throw new ArgumentNullException(nameof(name));

			if (ammoID <= 0)
				throw new ArgumentOutOfRangeException(nameof(ammoID), "Ammo ID must be a positive, non-zero integer");

			int next = nextID;

			registeredIDsToNames[nextID++] = name;
			registeredIDsToAmmoIDs[nextID] = ammoID;

			return next;
		}

		public static int GetProjectileType(int constructedAmmoID)
			=> registeredIDsToBaseProjectileTypes.TryGetValue(constructedAmmoID, out int type) ? type : ProjectileID.None;

		internal static void Load() {
			registeredIDsToNames = new(){
				[Bullet] = "Bullet",
				[Arrow] = "Arrow",
				[Rocket] = "Rocket",
				[Dart] = "Dart",
				[Coin] = "Coin"
			};

			registeredIDsToAmmoIDs = new(){
				[Bullet] = AmmoID.Bullet,
				[Arrow] = AmmoID.Arrow,
				[Rocket] = AmmoID.Rocket,
				[Dart] = AmmoID.Dart,
				[Coin] = AmmoID.Coin
			};
			
			registeredIDsToBaseProjectileTypes = new(){
				[Bullet] = ModContent.ProjectileType<TCBulletProjectile>()
			};

			nextID = registeredIDsToNames.Count;
		}

		internal static void Unload() {
			registeredIDsToNames = null;
			nextID = -1;
		}

		public static int TotalCount => nextID;

		internal static Dictionary<int, string> registeredIDsToNames;
		internal static Dictionary<int, int> registeredIDsToAmmoIDs;
		internal static Dictionary<int, int> registeredIDsToBaseProjectileTypes;
		internal static int nextID;
	}
}
