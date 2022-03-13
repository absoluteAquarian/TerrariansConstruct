using System;
using Terraria.ID;
using TerrariansConstruct.ID;
using TerrariansConstruct.Items;
using TerrariansConstruct.Materials;
using TerrariansConstruct.Registry;

namespace TerrariansConstruct {
	partial class TerrariansConstruct {
		private void AddParts() {
			AddAllPartsOfType(ItemID.CopperBar, ItemRarityID.White, PartActions.Copper, "[c/ff6000:Energized I]", MaterialPartID.WeaponBowString);
			AddAllPartsOfType(ItemID.Wood, ItemRarityID.White, PartActions.Wood, "[c/996633:Wimpy I]", MaterialPartID.WeaponBowString);

			AddPart(ItemID.Cobweb, ItemRarityID.White, MaterialPartID.WeaponBowString, PartActions.Cobweb, null);
		}

		/// <summary>
		/// Registers the part items for the material, <paramref name="materialType"/>, with the given rarity, <paramref name="rarity"/>
		/// </summary>
		/// <param name="materialType">The item ID</param>
		/// <param name="rarity">The item rarity</param>
		/// <param name="actions">The actions</param>
		/// <param name="tooltipForAllParts">The tooltip that will be assigned to all parts.  Can be modified via <seealso cref="ItemPart.SetTooltip(Material, int, string)"/></param>
		/// <param name="partIDsToIgnore">The IDs to ignore when iterating to create the part items</param>
		public void AddAllPartsOfType(int materialType, int rarity, ItemPartActionsBuilder actions, string tooltipForAllParts, params int[] partIDsToIgnore) {
			Material material = new(){
				type = materialType,
				rarity = rarity
			};

			for (int partID = 0; partID < MaterialPartID.TotalCount; partID++) {
				if (Array.IndexOf(partIDsToIgnore, partID) > -1)
					continue;

				AddPart(material, partID, actions, tooltipForAllParts);
			}
		}

		/// <summary>
		/// Registers a part item for the material, <paramref name="material"/>
		/// </summary>
		/// <param name="material">The material instance</param>
		/// <param name="partID">The part ID</param>
		/// <param name="actions">The actions</param>
		/// <param name="tooltip">The tooltip for this part.  Can be modified via <seealso cref="ItemPart.SetTooltip(Material, int, string)"/></param>
		public void AddPart(Material material, int partID, ItemPartActionsBuilder actions, string tooltip) {
			ItemPartItem item = ItemPartItem.Create(material, partID, actions, tooltip);

			AddContent(item);
				
			ItemPartItem.registeredPartsByItemID[item.Type] = item.part;
		}

		/// <summary>
		/// Registers a part item for the material, <paramref name="materialType"/>, with the given rarity, <paramref name="rarity"/>
		/// </summary>
		/// <param name="materialType">The item ID</param>
		/// <param name="rarity">The item rarity</param>
		/// <param name="partID">The part ID</param>
		/// <param name="actions">The actions</param>
		/// <param name="tooltip">The tooltip for this part.  Can be modified via <seealso cref="ItemPart.SetTooltip(Material, int, string)"/></param>
		public void AddPart(int materialType, int rarity, int partID, ItemPartActionsBuilder actions, string tooltip)
			=> AddPart(new Material(){ type = materialType, rarity = rarity }, partID, actions, tooltip);
	}
}
