using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstructLib.API;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Tools {
	public sealed class TCPickaxe : BaseTCItem {
		public override int PartsCount => 4;

		public override bool? SafeIsLoadingEnabled(Mod mod) => true;

		public TCPickaxe() : base(CoreMod.RegisteredItems.Pickaxe) { }

		public override void SafeSetDefaults() {
			//useTime/useAnimation are overridden anyway
			Item.DefaultToMeleeWeapon(0, ItemUseStyleID.Swing, useTurn: true);
			Item.width = 36;
			Item.height = 36;
			Item.UseSound = SoundID.Item1;
		}

		public override void OnInitializedWithParts() {
			Item.pick = GetPickaxePower();
		}
	}
}
