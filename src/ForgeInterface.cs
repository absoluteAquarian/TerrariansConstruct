using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace TerrariansConstruct {
	internal class ForgeInterface : ModSystem {
		private GameTime lastUpdateGameTime;

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));

			if (mouseTextIndex != -1) {
				layers.Insert(mouseTextIndex - 1, new LegacyGameInterfaceLayer(
					"TerrariansConstruct: Forge UI",
					() => {
						CoreMod.forgeUIInterface.Draw(Main.spriteBatch, lastUpdateGameTime);

						return true;
					},
					InterfaceScaleType.UI));
			}
		}

		public override void UpdateUI(GameTime gameTime) {
			lastUpdateGameTime = gameTime;

			if (!Main.playerInventory && CoreMod.forgeUIInterface.CurrentState is not null)
				CoreMod.forgeUI.Hide(Main.myPlayer);

			CoreMod.forgeUIInterface.Update(gameTime);
		}
	}
}
