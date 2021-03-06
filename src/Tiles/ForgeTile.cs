using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariansConstruct.Items;
using TerrariansConstruct.TileEntities;
using TerrariansConstructLib;

namespace TerrariansConstruct.Tiles {
	internal class ForgeTile : ModTile {
		public override string Texture => "TerrariansConstruct/Assets/Tiles/ForgeTile";

		public const uint width = 3, height = 2;

		public override void SetStaticDefaults() {
			string mapName = "Tinker Table";

			Main.tileNoAttach[Type] = true;
			Main.tileFrameImportant[Type] = true;

			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidBottom, (int)width, 0);
			TileObjectData.newTile.CoordinateHeights = Utility.Create1DArray(16, height);
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.Height = (int)height;
			TileObjectData.newTile.Width = (int)width;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.WaterDeath = false;
			TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.Origin = new Point16((int)width / 2, (int)height - 1);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<ForgeEntity>().Hook_AfterPlacement, -1, 0, false);

			TileObjectData.addTile(Type);

			ModTranslation name = CreateMapEntryName();
			name.SetDefault(mapName);
			AddMapEntry(new Color(0xd1, 0x89, 0x32), name);

			MineResist = 3f;
			//Wood/dirt sound
			HitSound = SoundID.Dig;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, (int)width * 16, (int)height * 16, ModContent.ItemType<ForgeItem>());

			ModContent.GetInstance<ForgeEntity>().Kill(i, j);
		}

		public override void MouseOver(int i, int j) {
			Tile tile = Main.tile[i, j];
			Point16 pos = new Point16(i, j) - tile.TileCoord();

			if (Utility.TryGetTileEntity(pos, out ForgeEntity? entity) && entity is not null) {
				Player player = Main.LocalPlayer;

				player.mouseInterface = true;
				player.noThrow = 2;
				player.cursorItemIconEnabled = true;
				player.cursorItemIconID = ModContent.ItemType<ForgeItem>();
			}
		}

		public override bool RightClick(int i, int j) {
			Tile tile = Main.tile[i, j];
			Point16 pos = new Point16(i, j) - tile.TileCoord();

			if (Utility.TryGetTileEntity(pos, out ForgeEntity? entity) && entity is not null) {
				CoreMod.forgeUI.Show(Main.myPlayer, entity);
				return true;
			}

			return false;
		}
	}
}
