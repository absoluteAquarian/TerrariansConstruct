using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariansConstruct.Definitions;
using TerrariansConstructLib;
using TerrariansConstructLib.API;
using TerrariansConstructLib.API.Definitions;
using TerrariansConstructLib.API.UI;

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
					IEnumerable<ForgeUISlotConfiguration> configuration;

					if (ItemDefinitionLoader.Get((e.Parent as ForgeUIToolsOption)!.registeredItemID) is TCItemDefinition definition)
						configuration = definition.GetForgeSlotConfiguration();
					else
						configuration = ItemDefinitionLoader.Get(CoreLibMod.ItemType<Sword>())!.GetForgeSlotConfiguration();

					CoreMod.forgeUI.pageTools.ConfigureSlots(configuration.ToArray());
				}
			};

			UIText text = new(ItemDefinitionLoader.Get(registeredItemID)?.Name ?? "Unknown Item");
			text.Left.Set(40, 0f);
			panel.Append(text);

			//Get the dummy texture
			var texture = ForgeUI.GetToolOptionTexture(registeredItemID);

			UIImage image = new(texture);
			image.Left.Set(-2, 0f);
			image.Top.Set(-4, 0f);
			panel.Append(image);
		}
	}
}
