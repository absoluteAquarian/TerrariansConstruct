using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstruct.Systems;
using TerrariansConstruct.TileEntities;

namespace TerrariansConstruct {
	public enum MessageType {
		OpenForgeUI,
		CloseForgeUI,
		SendOrePlacement,
		SendOreRemoval,
		RequestOrePlacements,
		ReceiveOrePlacements
	}

	public static class NetHelper {
		/// <inheritdoc cref="Mod.HandlePacket(BinaryReader, int)"/>
		internal static void HandlePacket(BinaryReader reader, int whoAmI) {
			MessageType msg = (MessageType)reader.ReadByte();

			int forgeEntityID;
			int forgeEntityPlayer;
			short x, y;
			int syncPlayer, syncCount;
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
				case MessageType.SendOrePlacement:
					x = reader.ReadInt16();
					y = reader.ReadInt16();

					OrePlacementTracker.tilesPlacedByPlayers.Add(new(x, y));

					if (Main.netMode == NetmodeID.Server) {
						var packet = GetPacket(msg);
						packet.Write(x);
						packet.Write(y);
						packet.Send(ignoreClient: whoAmI);
					}
					break;
				case MessageType.SendOreRemoval:
					x = reader.ReadInt16();
					y = reader.ReadInt16();

					OrePlacementTracker.tilesPlacedByPlayers.Remove(new(x, y));

					if (Main.netMode == NetmodeID.Server) {
						var packet = GetPacket(msg);
						packet.Write(x);
						packet.Write(y);
						packet.Send(ignoreClient: whoAmI);
					}
					break;
				case MessageType.RequestOrePlacements:
					syncPlayer = reader.ReadInt32();

					if (Main.netMode == NetmodeID.Server) {
						var packet = GetPacket(MessageType.ReceiveOrePlacements);
						
						packet.Write(OrePlacementTracker.tilesPlacedByPlayers.Count);

						foreach (var tile in OrePlacementTracker.tilesPlacedByPlayers) {
							packet.Write(tile.X);
							packet.Write(tile.Y);
						}

						packet.Send(toClient: syncPlayer);
					}
					break;
				case MessageType.ReceiveOrePlacements:
					if (Main.netMode == NetmodeID.MultiplayerClient) {
						syncCount = reader.ReadInt32();

						for (int i = 0; i < syncCount; i++) {
							x = reader.ReadInt16();
							y = reader.ReadInt16();

							OrePlacementTracker.tilesPlacedByPlayers.Add(new(x, y));
						}
					}
					break;
			}
		}

		public static ModPacket GetPacket(MessageType type) {
			ModPacket packet = CoreMod.Instance.GetPacket();
			packet.Write((byte)type);
			return packet;
		}

		public static int PlaceTileEntity<T>(int x, int y, uint width, uint height) where T : ModTileEntity {
			var entity = ModContent.GetInstance<T>();

			if (Main.netMode == NetmodeID.MultiplayerClient) {
				NetMessage.SendTileSquare(Main.myPlayer, x, y, (int)width, (int)height);
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

		public static void SendOrePlaced(int x, int y) {
			OrePlacementTracker.tilesPlacedByPlayers.Add(new(x, y));

			if (Main.netMode == NetmodeID.MultiplayerClient) {
				var packet = GetPacket(MessageType.SendOrePlacement);
				packet.Write(x);
				packet.Write(y);
				packet.Send(ignoreClient: Main.myPlayer);
			}
		}

		public static void SendOreRemoval(int x, int y) {
			OrePlacementTracker.tilesPlacedByPlayers.Remove(new(x, y));

			if (Main.netMode == NetmodeID.MultiplayerClient) {
				var packet = GetPacket(MessageType.SendOreRemoval);
				packet.Write(x);
				packet.Write(y);
				packet.Send(ignoreClient: Main.myPlayer);
			}
		}

		public static void RequestOrePlacements() {
			if (Main.netMode == NetmodeID.MultiplayerClient) {
				var packet = GetPacket(MessageType.RequestOrePlacements);
				packet.Write(Main.myPlayer);
				packet.Send(ignoreClient: Main.myPlayer);
			}
		}
	}
}
