using System;
using TerrariansConstructLib.API.UI;
using TerrariansConstructLib.Registry;

namespace TerrariansConstruct.UI {
	//Handles drawing the material, shard and mold slots
	public sealed class TCUIItemMaterialSlot : TCUIItemSlot {
		private TCUIItemMaterialSlot(int materialSlotContext, float scale = 1f) : base(SlotContexts.ForgeMaterialSlot + materialSlotContext, scale){ }

		public static TCUIItemMaterialSlot AsMaterialShardSlot(float scale)
			=> new(-1, scale);

		public static TCUIItemMaterialSlot AsMaterialSlot(float scale)
			=> new(-2, scale);

		public static TCUIItemMaterialSlot AsMoldSlot(int partID, float scale) {
			if (partID < 0 || partID >= PartRegistry.Count)
				throw new ArgumentException("Part ID was invalid");

			return new(partID, scale);
		}
	}
}
