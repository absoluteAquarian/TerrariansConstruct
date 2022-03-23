using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrariansConstructLib.API.UI;

namespace TerrariansConstruct.UI {
	internal class ForgeUI : UIState {
		private UIDragablePanel panel;
		
		//public ForgeUIMoldsPage pageMolds;
		//public ForgeUIPartsPage pageParts;
		public ForgeUIToolsPage pageTools;
		//public ForgeUIModifiersPage pageModifiers;

		public ForgeUIPage currentPage;

		public override void OnInitialize() {
			//Make the panel
			panel = new(true, "Molds", "Parts", "Tools", "Modifiers");

			panel.menus["Tools"].OnClick += (evt, e) => {
				if (!object.ReferenceEquals(currentPage, pageTools)) {
					currentPage.DropAllItems();

					currentPage.Remove();

					panel.Append(pageTools);

					pageTools.ConfigureSlots(ForgeUISlotConfiguration.Get(CoreMod.RegisteredItems.Sword));

					currentPage = pageTools;
				}
			};

			panel.Width.Set(600, 0);
			panel.Height.Set(400, 0);

			panel.HAlign = panel.VAlign = 0.5f;

			Append(panel);

			pageTools = new ForgeUIToolsPage();
			pageTools.Width.Set(0, 1f);
			pageTools.Height.Set(0, 1f);

			//Make the header text
			UIText header = new("Weapons Forge", 1, true) {
				HAlign = 0.5f
			};
			header.Top.Set(10, 0);
			panel.Append(header);

			currentPage = pageTools;
		}
	}
}
