using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariansConstructLib;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct.UI {
	internal class ForgeUIToolsOption : UIElement {
		public readonly int registeredItemID;

		public ForgeUIToolsOption(int registeredItemID) {
			this.registeredItemID = registeredItemID;

			SetPadding(0);

			UIPanel panel = new();
			panel.Width.Set(0, 1f);
			panel.Height.Set(0, 1f);
			Append(panel);

			panel.OnClick += (evt, e) => {
				if (object.ReferenceEquals(CoreMod.forgeUI.currentPage, CoreMod.forgeUI.pageTools)) {
					if (ItemRegistry.TryGetConfiguration((e as ForgeUIToolsOption)!.registeredItemID, out var configuration))
						CoreMod.forgeUI.pageTools.ConfigureSlots(configuration.ToArray());
					else {
						ItemRegistry.TryGetConfiguration(CoreMod.RegisteredItems.Sword, out configuration);
						CoreMod.forgeUI.pageTools.ConfigureSlots(configuration.ToArray());
					}
				}
			};

			UIText text = new(CoreLibMod.GetItemName(registeredItemID));
			text.Left.Set(40, 0f);
			panel.Append(text);

			//Get the dummy texture
			int type = CoreLibMod.GetItemType(registeredItemID);

			UIImage image = new(TextureAssets.Item[type]);
			image.Left.Set(8, 0f);

		}
	}
}
