using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariansConstruct.Tiles;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.TileEntities {
	internal class ForgeEntity : ModTileEntity {
		internal List<PartMold> molds = new();

		internal int viewingPlayer;

		public override bool IsTileValidForEntity(int x, int y) {
			Tile tile = Main.tile[x, y];
			return tile.HasTile && tile.TileType == ModContent.TileType<ForgeTile>();
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
			=> NetHelper.PlaceTileEntity<ForgeEntity>(i - (int)ForgeTile.width / 2, j - (int)ForgeTile.height + 1, ForgeTile.width, ForgeTile.height);

		public override void OnKill() {
			if (CoreMod.forgeUI.entity == this)
				CoreMod.forgeUI.Hide(Main.myPlayer);
		}

		public override void NetSend(BinaryWriter writer) {
			writer.Write((ushort)viewingPlayer);

			writer.Write(molds.Count);

			foreach (var mold in molds)
				ItemIO.Send(mold.Item, writer, writeStack: true);
		}

		public override void NetReceive(BinaryReader reader) {
			viewingPlayer = reader.ReadUInt16();

			int count = reader.ReadInt32();
			molds = new();

			for (int i = 0; i < count; i++) {
				Item item = ItemIO.Receive(reader, readStack: true);

				if (item.ModItem is PartMold mold)
					molds.Add(mold);
			}
		}

		public override void SaveData(TagCompound tag) {
			tag["molds"] = molds.Select(m => ItemIO.Save(m.Item)).ToList();
		}

		public override void LoadData(TagCompound tag) {
			if (tag.GetList<TagCompound>("molds") is var list)
				molds = list.Select(t => (ItemIO.Load(t).ModItem as PartMold)!).ToList();
		}
	}
}
