using System;
using System.Collections.Generic;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariansConstruct.ID;
using TerrariansConstruct.Materials;
using TerrariansConstruct.Registry;

namespace TerrariansConstruct.Items {
	/// <summary>
	/// Represents an instance of an item part item
	/// </summary>
	[Autoload(false)]
	public sealed class ItemPartItem : ModItem {
		public override string Texture {
			get {
				StringBuilder asset = new();

				if (part.partID >= 0 && part.partID < MaterialPartID.TotalCount) {
					//Modded part ID
					asset.Append(MaterialPartID.registeredIDsToAssetFolders[part.partID]);
				} else
					throw new Exception("Part ID was invalid");

				asset.Append('/');

				if (part.material is UnloadedMaterial)
					asset.Append("Unloaded");
				else if (ItemID.Search.TryGetName(part.material.type, out string materialName))
					asset.Append(materialName);
				else if (ModContent.GetModItem(part.material.type) is ModItem mItem)
					asset.Append(mItem.Name);
				else
					throw new Exception("Invalid material type ID");

				return asset.ToString();
			}
		}

		/// <summary>
		/// The information for the item part
		/// </summary>
		public ItemPart part;

		internal static Dictionary<int, ItemPart> registeredPartsByItemID;

		public ItemPartItem(ItemPart part) {
			this.part = part;
		}

		public static ItemPartItem Create(Material material, int partID, ItemPartActionsBuilder builder, string tooltip) {
			if (material is UnloadedMaterial)
				return null;

			int materialType = material.type;

			if (!PartActions.builders.TryGetValue(materialType, out var buildersByPartID))
				buildersByPartID = PartActions.builders[materialType] = new();

			if (buildersByPartID.ContainsKey(partID))
				throw new ArgumentException($"The part type \"{MaterialPartID.registeredIDsToNames[partID]}\" has already been assigned to the material type \"{Lang.GetItemNameValue(materialType)}\" (ID: {materialType})");

			buildersByPartID[partID] = builder;

			return new ItemPartItem(new ItemPart() {
				material = material,
				partID = partID,
				tooltip = tooltip
			});
		}

		public override bool IsLoadingEnabled(Mod mod)
			=> false;

		public override bool CanStack(Item item2)
			=> item2.ModItem is ItemPartItem pItem
				&& part.material.type == pItem.part.material.type && part.partID == pItem.part.partID;

		public override void SetStaticDefaults() {
			part = registeredPartsByItemID[Type];

			DisplayName.SetDefault(Lang.GetItemNameValue(part.material.type) + " " + MaterialPartID.registeredIDsToNames[part.partID]);

			if (part.tooltip is not null)
				Tooltip.SetDefault(part.tooltip);
		}

		public override void SetDefaults() {
			part = registeredPartsByItemID[Type];

			Item.DamageType = DamageClass.NoScaling;
			Item.rare = part.material.rarity;
		}

		public override void SaveData(TagCompound tag) {
			tag["part"] = part;
		}

		public override void LoadData(TagCompound tag) {
			part = tag.Get<ItemPart>("part");
		}
	}
}
