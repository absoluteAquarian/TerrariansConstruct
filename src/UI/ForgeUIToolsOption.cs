using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariansConstructLib;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct.UI {
	internal class ForgeUIToolsOption : UIElement {
		public readonly int registeredItemID;

		public ForgeUIToolsOption(int registeredItemID) {
			this.registeredItemID = registeredItemID;

			OnClick += (evt, e) => {
				if (object.ReferenceEquals(CoreMod.forgeUI.currentPage, CoreMod.forgeUI.pageTools)) {
					if (ItemRegistry.TryGetConfiguration((e as ForgeUIToolsOption)!.registeredItemID, out var configuration))
						CoreMod.forgeUI.pageTools.ConfigureSlots(configuration.ToArray());
					else {
						ItemRegistry.TryGetConfiguration(CoreMod.RegisteredItems.Sword, out configuration);
						CoreMod.forgeUI.pageTools.ConfigureSlots(configuration.ToArray());
					}
				}
			};

			UIText text = new(CoreLibMod.GetItemName(registeredItemID), 0.8f);
			Append(text);
		}
	}
}
