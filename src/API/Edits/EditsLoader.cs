using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
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

			IL.Terraria.Player.ItemCheck_GetMeleeHitbox += Patch_Player_ItemCheck_GetMeleeHitbox;
			IL.Terraria.UI.ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += Patch_ItemSlot_Draw;

			ILHelper.LogILEdits = false;
		}

		private static void Patch_ItemSlot_Draw(ILContext il) {
			ILCursor c = new(il);

			int patchNum = 1;

			ILHelper.CompleteLog(CoreMod.Instance, c, beforeEdit: true);

			if (!c.TryGotoNext(MoveType.After, i => i.MatchLdloc(32),
				i => i.MatchLdloc(2),
				i => i.MatchMul(),
				i => i.MatchStloc(32)))
				goto bad_il;

			// TODO: hand icon isn't drawing at a smaller scale... why?
			c.Emit(OpCodes.Ldloc, 31);
			c.Emit(OpCodes.Ldloc_1);
			c.Emit(OpCodes.Ldarg_2);
			c.EmitDelegate<Func<Item, int, float>>((item, context) => item.type == ModContent.ItemType<ZAHANDO>() && context == ItemSlot.Context.MouseItem ? 0.25f : 1f);
			c.Emit(OpCodes.Mul);
			c.Emit(OpCodes.Stloc, 31);

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
