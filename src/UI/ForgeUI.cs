using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using TerrariansConstruct.Definitions;
using TerrariansConstruct.TileEntities;
using TerrariansConstructLib;
using TerrariansConstructLib.API;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Materials;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.UI {
	internal class ForgeUI : UIState {
		internal UIDragablePanel panel;

		internal ForgeEntity entity;
		
		public ForgeUIMoldsPage pageMolds;
		//public ForgeUIPartsPage pageParts;
		public ForgeUIToolsPage pageTools;
		//public ForgeUIModifiersPage pageModifiers;

		public ForgeUIPage currentPage;

		public Dictionary<string, ForgeUIPage> pages;

		public override void OnInitialize() {
			//Make the panel
			panel = new(true, "Molds", "Parts", "Tools", "Modifiers");

			pages = new();

			panel.OnMenuClose += () => Hide(Main.myPlayer);

			foreach (var page in panel.menus.Values)
				page.OnClick += (evt, e) => pages[(e as UITextPanel<string>)!.Text].SetAsActivePage();

			// TODO: don't drop items when switching pages nor when leaving the UI

			panel.Width.Set(720, 0);
			panel.Height.Set(550, 0);

			panel.HAlign = panel.VAlign = 0.5f;

			Append(panel);

			pages["Molds"] = pageMolds = new("Molds") {
				parentUI = this
			};
			pageMolds.Width.Set(0, 1f);
			pageMolds.Height.Set(0, 1f);

			pages["Tools"] = pageTools = new("Tools") {
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
			Main.playerInventory = true;

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
			var data = ItemDefinitionLoader.Get(registeredItemID)!;
			int[] partIDs = data.GetValidPartIDs().ToArray();

			if (partIDs.Length >= (int)ColorMaterialType.Count)
				throw new Exception($"Registered item \"{data.Name}\" has too many parts ({partIDs.Length})");

			ItemPart[] parts = partIDs.Select((p, i) => (p, i + ColorMaterial.StaticBaseType)).Select(t => new ItemPart() { partID = t.p, material = Material.FromItem(t.Item2) }).ToArray();

			return CoreLibMod.ItemTextures.Get(registeredItemID, parts, Array.Empty<BaseTrait>());
		}
	}
}
