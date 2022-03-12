using Terraria.ID;
using TerrariansConstruct.ID;
using TerrariansConstruct.Items;
using TerrariansConstruct.Materials;
using TerrariansConstruct.Registry;

namespace TerrariansConstruct {
	partial class TerrariansConstruct {
		private void AddParts() {
			AddAllPartsOfType(ItemID.CopperBar, ItemRarityID.White, PartActions.Copper, "[c/ff6000:Energized I]");
			AddAllPartsOfType(ItemID.Wood, ItemRarityID.White, PartActions.Wood, "[c/996633:Wimpy I]");
		}

		/// <summary>
		/// Registers the actions for all parts of the material type, <paramref name="materialType"/>, with the given <paramref name="rarity"/>
		/// </summary>
		/// <param name="materialType">The item ID</param>
		/// <param name="rarity">The item rarity</param>
		/// <param name="actions">The actions</param>
		/// <param name="tooltipForAllParts">The tooltip that will be assigned to all parts.  Can be modified via <seealso cref="ItemPart.SetTooltip(Material, int, string)"/></param>
		public void AddAllPartsOfType(int materialType, int rarity, ItemPartActionsBuilder actions, string tooltipForAllParts = null) {
			Material material = new(){
				type = materialType,
				rarity = rarity
			};

			for (int partID = 0; partID < MaterialPartID.TotalCount; partID++) {
				ItemPartItem item = ItemPartItem.Create(material, partID, actions, tooltipForAllParts);

				AddContent(item);
				
				ItemPartItem.registeredPartsByItemID[item.Type] = item.part;
			}
		}
	}
}
