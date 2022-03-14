using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariansConstruct.UI;

namespace TerrariansConstruct {
	public partial class CoreMod : Mod {
		internal static UserInterface forgeUIInterface;
		internal static ForgeUI forgeUI;

		public override void Load() {
			AddParts();

			if (!Main.dedServ) {
				forgeUIInterface = new();
				forgeUI = new();

				forgeUI.Activate();
			}
		}
	}
}