using Terraria.ID;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.API;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed sword item
	/// </summary>
	public class TCSword : BaseTCItem {
		public override int ItemDefinition => CoreLibMod.ItemType<Sword>();

		public override void SafeSetDefaults() {
			//useTime/useAnimation are overridden anyway
			Item.DefaultToMeleeWeapon(0, ItemUseStyleID.Swing, useTurn: true);
			Item.width = 32;
			Item.height = 32;
			Item.UseSound = SoundID.Item1;
		}
	}
}
