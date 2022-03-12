using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace TerrariansConstruct.Materials {
	public class Material : TagSerializable {
		/// <summary>
		/// The ID of the item used as the material
		/// </summary>
		public int type = -1;

		/// <summary>
		/// The rarity of the material
		/// </summary>
		public int rarity;

		public virtual TagCompound SerializeData() {
			TagCompound tag = new();

			if (type < ItemID.Count) {
				tag["mod"] = "Terraria";
				tag["id"] = type;
			} else {
				if (ModContent.GetModItem(type) is not ModItem item)
					throw new Exception("Material item type was invalid");

				tag["mod"] = item.Mod.Name;
				tag["name"] = item.Name;
			}

			tag["rarity"] = rarity;

			return tag;
		}

		public static readonly Func<TagCompound, Material> DESERIALIZER = tag => {
			string mod = tag.GetString("mod");

			int type;
			if (mod == "Terraria")
				type = tag.GetInt("id");
			else {
				string name = tag.GetString("name");

				if (!ModLoader.TryGetMod(mod, out Mod instance) || !instance.TryFind(name, out ModItem item)) {
					//Inform the thing using the material that it was an unloaded material
					return null;
				}

				type = item.Type;
			}

			int rarity = tag.GetInt("rarity");

			return new Material(){
				type = type,
				rarity = rarity
			};
		};
	}
}
