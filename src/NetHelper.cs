using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariansConstruct.TileEntities;
using TerrariansConstructLib;

namespace TerrariansConstruct {
	public enum MessageType {
		OpenForgeUI,
		CloseForgeUI
	}

	public static class NetHelper {
		/// <inheritdoc cref="Mod.HandlePacket(BinaryReader, int)"/>
		internal static void HandlePacket(BinaryReader reader, int whoAmI) {
			MessageType msg = (MessageType)reader.ReadByte();

			int forgeEntityID;
			int forgeEntityPlayer;
			switch (msg) {
				case MessageType.OpenForgeUI:
					forgeEntityID = reader.ReadUInt16();
					forgeEntityPlayer = reader.ReadUInt16();

					if (Main.netMode == NetmodeID.Server) {
						var packet = GetPacket(msg);
						packet.Write((ushort)forgeEntityID);
						packet.Write((ushort)forgeEntityPlayer);
						packet.Send(ignoreClient: whoAmI);
					} else
						CoreMod.forgeUI.Show(forgeEntityPlayer, (TileEntity.ByID[forgeEntityID] as ForgeEntity)!);
					break;
				case MessageType.CloseForgeUI:
					forgeEntityPlayer = reader.ReadUInt16();

					if (Main.netMode == NetmodeID.Server) {
						var packet = GetPacket(msg);
						packet.Write((ushort)forgeEntityPlayer);
						packet.Send(ignoreClient: whoAmI);
					} else
						CoreMod.forgeUI.Hide(forgeEntityPlayer);
					break;
			}
		}

		public static ModPacket GetPacket(MessageType type) {
			ModPacket packet = CoreLibMod.Instance.GetPacket();
			packet.Write((byte)type);
			return packet;
		}

		public static int PlaceTileEntity<T>(int x, int y, int tileType, int style, int alternate = 0) where T : ModTileEntity {
			var entity = ModContent.GetInstance<T>();

			if (Main.netMode == NetmodeID.MultiplayerClient) {
				int width = 1, height = 1;
				if (TileObjectData.GetTileData(tileType, style, alternate) is TileObjectData obj) {
					width = obj.Width;
					height = obj.Height;

					x -= obj.Origin.X;
					y -= obj.Origin.Y;
				}

				NetMessage.SendTileSquare(Main.myPlayer, x, y, width, height);
				NetMessage.SendData(MessageID.TileEntityPlacement, number: x, number2: y, number3: entity.Type);

				return -1;
			}

			return entity.Place(x, y);
		}

		public static void SendTEUpdate(TileEntity entity)
			=> SendTEUpdate(entity.ID, entity.Position);

		public static void SendTEUpdate(int id, Point16 location) {
			if (Main.netMode != NetmodeID.MultiplayerClient)
				return;

			NetMessage.SendData(MessageID.TileEntitySharing, number: id, number2: location.X, number3: location.Y);
		}
	}
}
