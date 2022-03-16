using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstructLib.API;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed shortsword item
	/// </summary>
	public class TCShortsword : BaseTCItem {
		public override int PartsCount => 3;

		public override string VisualsFolderPath => "TerrariansConstruct/Assets/Visuals/Shortsword";

		public override bool? SafeIsLoadingEnabled(Mod mod) => true;

		public TCShortsword() : base(CoreMod.RegisteredItems.Shortsword) { }

		public override void SafeSetDefaults() {
			Item.DefaultToMeleeWeapon(16, ItemUseStyleID.Thrust, useTurn: true);
			Item.width = 32;
			Item.height = 32;
		}
	}
}
