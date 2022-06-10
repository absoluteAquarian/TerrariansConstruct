using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariansConstruct.API.Edits;
using TerrariansConstruct.Definitions;
using TerrariansConstruct.Items.Tools;
using TerrariansConstruct.Items.Weapons;
using TerrariansConstruct.Modifiers.ForgeModifiers;
using TerrariansConstruct.Modifiers.Traits;
using TerrariansConstruct.Projectiles;
using TerrariansConstruct.UI;
using TerrariansConstructLib;
using TerrariansConstructLib.API;
using TerrariansConstructLib.API.Definitions;
using TerrariansConstructLib.API.Stats;
using TerrariansConstructLib.Materials;

namespace TerrariansConstruct {
	public partial class CoreMod : Mod {
		internal static UserInterface forgeUIInterface;
		internal static ForgeUI forgeUI;

		public static CoreMod Instance => ModContent.GetInstance<CoreMod>();

		public const string RecipeGroup_AnyWorkbench = "TerrariansConstruct:AnyWorkbench";

		internal static Dictionary<ushort, int> oreTileToOreItem;
		internal static bool disableOreTracking;

		public static IEnumerable<PartDefinition> AllRemaningTypicalParts
			=> new PartDefinition[] {
				ModContent.GetInstance<ToolBinding>(),
				ModContent.GetInstance<WeaponSwordGuard>(),
				ModContent.GetInstance<WeaponShortSwordGuard>()
			};

		public static IEnumerable<PartDefinition> AllTypicalParts
			=> PartDefinitionLoader.ShartPart()
			.Concat(PartDefinitionLoader.AllHeadParts())
			.Concat(PartDefinitionLoader.AllHandleParts())
			.Concat(AllRemaningTypicalParts);

		public override void Load() {
			Utility.ForceLoadModHJsonLocalization(this);

			oreTileToOreItem = new() {
				[TileID.Copper] = ItemID.CopperOre,
				[TileID.Iron] = ItemID.IronOre,
				[TileID.Silver] = ItemID.SilverOre,
				[TileID.Gold] = ItemID.GoldOre,
				[TileID.Tin] = ItemID.TinOre,
				[TileID.Lead] = ItemID.LeadOre,
				[TileID.Tungsten] = ItemID.TungstenOre,
				[TileID.Platinum] = ItemID.PlatinumOre,
				[TileID.Demonite] = ItemID.DemoniteOre,
				[TileID.Crimtane] = ItemID.CrimtaneOre,
				[TileID.Meteorite] = ItemID.Meteorite,
				[TileID.Hellstone] = ItemID.Hellstone,
				[TileID.Cobalt] = ItemID.CobaltOre,
				[TileID.Mythril] = ItemID.MythrilOre,
				[TileID.Adamantite] = ItemID.AdamantiteOre,
				[TileID.Palladium] = ItemID.PalladiumOre,
				[TileID.Orichalcum] = ItemID.OrichalcumOre,
				[TileID.Titanium] = ItemID.TitaniumOre,
				[TileID.Chlorophyte] = ItemID.ChlorophyteOre,
				[TileID.LunarOre] = ItemID.LunarOre
			};

			disableOreTracking = false;

			if (!Main.dedServ) {
				CoreLibMod.SetLoadingSubProgressText(Language.GetTextValue("Mods.TerrariansConstruct.Loading.ForgeUI"));

				forgeUIInterface = new();
				forgeUI = new();

				forgeUI.Activate();
			}

			EditsLoader.Load();

			CoreLibMod.SetLoadingSubProgressText("");
		}

		public override void Unload() {
			forgeUI = null!;
			forgeUIInterface = null!;

			oreTileToOreItem = null!;
		}

		public override void AddRecipeGroups() {
			CoreLibMod.RegisterRecipeGroup(RecipeGroup_AnyWorkbench, ItemID.WorkBench,
				ItemID.WorkBench,
				ItemID.BambooWorkbench,
				ItemID.BlueDungeonWorkBench,
				ItemID.BoneWorkBench,
				ItemID.BorealWoodWorkBench,
				ItemID.CactusWorkBench,
				ItemID.CrystalWorkbench,
				ItemID.DynastyWorkBench,
				ItemID.EbonwoodWorkBench,
				ItemID.FleshWorkBench,
				ItemID.FrozenWorkBench,
				ItemID.GlassWorkBench,
				ItemID.GoldenWorkbench,
				ItemID.GothicWorkBench,
				ItemID.GraniteWorkBench,
				ItemID.GreenDungeonWorkBench,
				ItemID.HoneyWorkBench,
				ItemID.LesionWorkbench,
				ItemID.LihzahrdWorkBench,
				ItemID.LivingWoodWorkBench,
				ItemID.MarbleWorkBench,
				ItemID.MartianWorkBench,
				ItemID.MeteoriteWorkBench,
				ItemID.MushroomWorkBench,
				ItemID.NebulaWorkbench,
				ItemID.ObsidianWorkBench,
				ItemID.PalmWoodWorkBench,
				ItemID.PearlwoodWorkBench,
				ItemID.PinkDungeonWorkBench,
				ItemID.PumpkinWorkBench,
				ItemID.RichMahoganyWorkBench,
				ItemID.SandstoneWorkbench,
				ItemID.ShadewoodWorkBench,
				ItemID.SkywareWorkbench,
				ItemID.SlimeWorkBench,
				ItemID.SolarWorkbench,
				ItemID.SpiderWorkbench,
				ItemID.SpookyWorkBench,
				ItemID.StardustWorkbench,
				ItemID.SteampunkWorkBench,
				ItemID.VortexWorkbench);
		}
	}
}