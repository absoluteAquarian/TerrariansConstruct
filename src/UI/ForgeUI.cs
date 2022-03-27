using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using TerrariansConstruct.TileEntities;
using TerrariansConstructLib;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct.UI {
	internal class ForgeUI : UIState {
		private UIDragablePanel panel;

		internal ForgeEntity entity;
		
		public ForgeUIMoldsPage pageMolds;
		//public ForgeUIPartsPage pageParts;
		public ForgeUIToolsPage pageTools;
		//public ForgeUIModifiersPage pageModifiers;

		public ForgeUIPage currentPage;

		public override void OnInitialize() {
			//Make the panel
			panel = new(true, "Molds", "Parts", "Tools", "Modifiers");

			panel.menus["Molds"].OnClick += (evt, e) => {
				if (!object.ReferenceEquals(currentPage, pageMolds)) {
					currentPage.DropAllItems();

					currentPage.Remove();

					panel.viewArea.Append(pageMolds);

					pageMolds.needsUpdate = true;

					currentPage = pageMolds;
				}
			};

			panel.menus["Tools"].OnClick += (evt, e) => {
				if (!object.ReferenceEquals(currentPage, pageTools)) {
					currentPage.DropAllItems();

					currentPage.Remove();

					panel.viewArea.Append(pageTools);

					ItemRegistry.TryGetConfiguration(CoreMod.RegisteredItems.Sword, out var configuration);
					pageTools.ConfigureSlots(configuration.ToArray());

					currentPage = pageTools;
				}
			};

			// TODO: don't drop items when switching pages nor when leaving the UI

			panel.Width.Set(600, 0);
			panel.Height.Set(400, 0);

			panel.HAlign = panel.VAlign = 0.5f;

			Append(panel);

			pageMolds = new() {
				parentUI = this
			};
			pageMolds.Width.Set(0, 1f);
			pageMolds.Height.Set(0, 1f);

			pageTools = new() {
				parentUI = this
			};
			pageTools.Width.Set(0, 1f);
			pageTools.Height.Set(0, 1f);

			currentPage = pageTools;
			panel.viewArea.Append(pageTools);
		}

		public void Show(int openingPlayer, ForgeEntity entity) {
			if (this.entity is not null && this.entity.viewingPlayer == openingPlayer)
				Hide(openingPlayer);
			
			this.entity = entity;
			this.entity.viewingPlayer = openingPlayer;

			CoreMod.forgeUIInterface.SetState(this);

			SoundEngine.PlaySound(SoundID.MenuOpen);

			if (Main.netMode == NetmodeID.MultiplayerClient && openingPlayer == Main.myPlayer) {
				var packet = NetHelper.GetPacket(MessageType.OpenForgeUI);
				packet.Write((ushort)entity.ID);
				packet.Write((ushort)openingPlayer);
				packet.Send(ignoreClient: openingPlayer);
			}
		}

		public void Hide(int closingPlayer) {
			if (entity is null || entity.viewingPlayer != closingPlayer)
				return;

			entity.viewingPlayer = -1;

			CoreMod.forgeUIInterface.SetState(null);

			SoundEngine.PlaySound(SoundID.MenuClose);

			if (Main.netMode != NetmodeID.Server && closingPlayer == Main.myPlayer)
				currentPage?.DropAllItems();

			if (Main.netMode == NetmodeID.MultiplayerClient && closingPlayer == Main.myPlayer) {
				var packet = NetHelper.GetPacket(MessageType.CloseForgeUI);
				packet.Write((ushort)closingPlayer);
				packet.Send(ignoreClient: Main.myPlayer);
			}

			entity = null!;
		}

		public static Texture2D GetToolOptionTexture(int registeredItemID) {
			int[] partIDs = CoreLibMod.GetItemValidPartIDs(registeredItemID);

			if (partIDs.Length >= (int)ColorMaterialType.Count)
				throw new Exception($"Registered item \"{CoreLibMod.GetItemName(registeredItemID)}\" has too many parts ({partIDs.Length}).  Report this to the Terrarians' Construct devs");

			ItemPart[] parts = partIDs.Select((p, i) => (p, i + ColorMaterial.StaticBaseType)).Select(t => new ItemPart() { partID = t.p, material = Material.FromItem(t.Item2) }).ToArray();

			return CoreLibMod.ItemTextures.Get(registeredItemID, parts);
		}
	}
}
