using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using TerrariansConstruct.Items;
using TerrariansConstructLib;

namespace TerrariansConstruct.Players {
	internal class StartingPlayer : ModPlayer {
		public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath) {
			if (TCConfig.Instance.NewPlayersStartWithOnlyHand) {
				//Clear the tools from the "Terraria" list, then add the Fist item to the front of the list
				if (itemsByMod.TryGetValue("Terraria", out var list)) {
					List<Item> noTools = list.Where(i => i.pick == 0 && i.axe == 0 && i.hammer == 0).ToList();
					noTools.Insert(0, new Item(ModContent.ItemType<ZAHANDO>()));

					list.Clear();
					list.AddRange(noTools);
				}
			}
		}
	}
}
