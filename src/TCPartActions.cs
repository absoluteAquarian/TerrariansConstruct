using System;
using Terraria;
using TerrariansConstructLib;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct {
	/// <summary>
	/// A collection of action builders for base Terrarians' Construct materials
	/// </summary>
	public static class TCPartActions {
		public static readonly ItemPartActionsBuilder Wood = new ItemPartActionsBuilder()
			.WithOnUpdateInventory((partID, player, item) => {
				if (item.ModItem is not BaseTCItem tc || !Main.rand.NextBool(1, 5000))
					return;

				int max = tc.GetMaxDurability();

				tc.TryIncreaseDurability(Math.Max(1, max / 100));
			});
	}
}
