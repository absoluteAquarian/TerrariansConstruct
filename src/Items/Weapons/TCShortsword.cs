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

		public override bool? SafeIsLoadingEnabled(Mod mod) => true;

		public TCShortsword() : base(CoreMod.RegisteredItems.Shortsword) { }

		public override void SafeSetDefaults() {
			//useTime/useAnimation are overridden anyway
			Item.DefaultToMeleeWeapon(0, ItemUseStyleID.Rapier, useTurn: true);
			Item.width = 32;
			Item.height = 32;
			Item.UseSound = SoundID.Item1;
		}
	}
}
