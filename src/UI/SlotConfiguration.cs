namespace TerrariansConstruct.UI {
	public struct SlotConfiguration {
		public static SlotConfiguration[] Weapon_Sword { get; private set; }

		public static SlotConfiguration[] Weapon_Shortsword { get; private set; }

		public static SlotConfiguration[] Weapon_Bow { get; private set; }

		public static SlotConfiguration[] Tool_Pickaxe { get; private set; }

		public static SlotConfiguration[] Tool_Axe { get; private set; }

		public static SlotConfiguration[] Tool_Hammer { get; private set; }

		internal static void Initialize() {
			//   0  1  2  3  4
			//   5  6  7  8  9
			//  10 11 12 13 14
			//  15 16 17 18 19
			//  20 21 22 23 24
			Weapon_Sword = new SlotConfiguration[] {
				(0,  4, CoreMod.RegisteredParts.WeaponLongSwordBlade),
				(1, 12, CoreMod.RegisteredParts.WeaponSwordGuard),
				(2, 20, CoreMod.RegisteredParts.ToolRod)
			};

			Weapon_Shortsword = new SlotConfiguration[] {
				(0,  4, CoreMod.RegisteredParts.WeaponShortSwordBlade),
				(1, 12, CoreMod.RegisteredParts.WeaponShortSwordGuard),
				(2, 20, CoreMod.RegisteredParts.ToolRod)
			};

			Weapon_Bow = new SlotConfiguration[] {
				(0,  7, CoreMod.RegisteredParts.WeaponBowHead),
				(1, 13, CoreMod.RegisteredParts.WeaponBowHead),
				(2, 16, CoreMod.RegisteredParts.WeaponBowString)
			};

			Tool_Pickaxe = new SlotConfiguration[] {
				(0, 12, CoreMod.RegisteredParts.ToolBinding),
				(1,  7, CoreMod.RegisteredParts.ToolPickHead),
				(2, 13, CoreMod.RegisteredParts.ToolPickHead),
				(3, 16, CoreMod.RegisteredParts.ToolRod)
			};

			Tool_Axe = new SlotConfiguration[] {
				(0, 12, CoreMod.RegisteredParts.ToolBinding),
				(1,  8, CoreMod.RegisteredParts.ToolAxeHead),
				(2, 16, CoreMod.RegisteredParts.ToolRod)
			};

			Tool_Hammer = new SlotConfiguration[] {
				(0, 12, CoreMod.RegisteredParts.ToolBinding),
				(1,  8, CoreMod.RegisteredParts.ToolHammerHead),
				(2, 16, CoreMod.RegisteredParts.ToolRod)
			};
		}

		public readonly int slot;
		public readonly int position;
		public readonly int partID;

		public SlotConfiguration(int slot, int position, int partID) {
			this.slot = slot;
			this.position = position;
			this.partID = partID;
		}

		public static implicit operator (int slot, int position, int partID)(SlotConfiguration configuration)
			=> (configuration.slot, configuration.position, configuration.partID);

		public static implicit operator SlotConfiguration((int slot, int position, int partID) tuple)
			=> new(tuple.slot, tuple.position, tuple.partID);
	}
}
