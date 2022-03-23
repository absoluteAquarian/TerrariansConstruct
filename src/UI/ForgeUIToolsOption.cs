using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariansConstructLib;
using TerrariansConstructLib.API.UI;

namespace TerrariansConstruct.UI {
	internal class ForgeUIToolsOption : UIElement {
		public readonly int registeredItemID;

		public ForgeUIToolsOption(int registeredItemID) {
			this.registeredItemID = registeredItemID;

			OnClick += (evt, e) => {
				if (object.ReferenceEquals(CoreMod.forgeUI.currentPage, CoreMod.forgeUI.pageTools))
					CoreMod.forgeUI.pageTools.ConfigureSlots(ForgeUISlotConfiguration.Get((e as ForgeUIToolsOption)!.registeredItemID));
			};

			UIText text = new(CoreLibMod.GetItemName(registeredItemID), 0.8f);
			Append(text);
		}
	}
}
