using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TerrariansConstruct.Items;
using TerrariansConstructLib;
using TerrariansConstructLib.API.Edits;

namespace TerrariansConstruct.API.Edits {
	internal static class EditsLoader {
		public static void Load() {
			ILHelper.LogILEdits = true;

			CoreLibMod.SetLoadingSubProgressText(Language.GetTextValue("Mods.TerrariansConstructLib.Loading.EditsLoader.ILEdits"));

			IL.Terraria.Main.DrawInterface_40_InteractItemIcon += Patch_Main_DrawInterface_40_InteractItemIcon;
			IL.Terraria.Player.ItemCheck_GetMeleeHitbox += Patch_Player_ItemCheck_GetMeleeHitbox;

			ILHelper.LogILEdits = false;
		}

		private static void Patch_Main_DrawInterface_40_InteractItemIcon(ILContext il) {
			FieldInfo Main_cursorScale = typeof(Main).GetField("cursorScale", BindingFlags.Public | BindingFlags.Static)!;

			ILHelper.EnsureAreNotNull((Main_cursorScale, typeof(Main).FullName + "::cursorScale"));

			ILCursor c = new(il);

			int patchNum = 1;

			ILHelper.CompleteLog(CoreMod.Instance, c, beforeEdit: true);

			if (!c.TryGotoNext(MoveType.After, i => i.MatchLdcR4(1f),
				i => i.MatchStloc(6),
				i => i.MatchLdsfld(Main_cursorScale),
				i => i.MatchStloc(6)))
				goto bad_il;

			c.Emit(OpCodes.Ldloc_0);
			c.Emit(OpCodes.Ldloc, 6);
			c.EmitDelegate<Func<int, float, float>>((num, num2) => {
				if (num == ModContent.ItemType<ZAHANDO>())
					num2 *= 0.45f;

				return num2;
			});
			c.Emit(OpCodes.Stloc, 6);

			ILHelper.UpdateInstructionOffsets(c);

			ILHelper.CompleteLog(CoreMod.Instance, c, beforeEdit: false);

			return;
			bad_il:
			throw new Exception("Unable to fully patch " + il.Method.Name + "()\n" +
				"Reason: Could not find instruction sequence for patch #" + patchNum);
		}

		private static void Patch_Player_ItemCheck_GetMeleeHitbox(ILContext il) {
			ILCursor c = new(il);

			int patchNum = 1;

			ILHelper.CompleteLog(CoreMod.Instance, c, beforeEdit: true);

			if (!c.TryGotoNext(MoveType.After, i => i.MatchStloc(2)))
				goto bad_il;

			patchNum++;

			//Edit width
			c.Emit(OpCodes.Ldloc_1);
			c.Emit(OpCodes.Ldarg_1);
			c.EmitDelegate<Func<int, Item, int>>((num, sItem) => {
				if (sItem.ModItem is ZAHANDO)
					num = 20;

				return num;
			});
			c.Emit(OpCodes.Stloc_1);
			//Edit height
			c.Emit(OpCodes.Ldloc_2);
			c.Emit(OpCodes.Ldarg_1);
			c.EmitDelegate<Func<int, Item, int>>((num2, sItem) => {
				if (sItem.ModItem is ZAHANDO)
					num2 = 20;

				return num2;
			});
			c.Emit(OpCodes.Stloc_2);

			ILHelper.UpdateInstructionOffsets(c);

			ILHelper.CompleteLog(CoreMod.Instance, c, beforeEdit: false);

			return;
			bad_il:
			throw new Exception("Unable to fully patch " + il.Method.Name + "()\n" +
				"Reason: Could not find instruction sequence for patch #" + patchNum);
		}
	}
}
