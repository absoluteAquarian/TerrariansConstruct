using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstructLib.API;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed sword item
	/// </summary>
	public class TCSword : BaseTCItem {
		public override int PartsCount => 3;

		public override string VisualsFolderPath => "TerrariansConstruct/Assets/Visuals/Sword";

		public override bool? SafeIsLoadingEnabled(Mod mod) => true;

		public TCSword() : base(CoreMod.RegisteredItems.Sword){ }

		public override void SafeSetDefaults() {
			Item.DefaultToMeleeWeapon(24, ItemUseStyleID.Swing, useTurn: true);
			Item.width = 32;
			Item.height = 32;
		}
	}
}
