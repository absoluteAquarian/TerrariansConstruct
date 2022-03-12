using System;
using Terraria.ModLoader.IO;

namespace TerrariansConstruct.Materials {
	public sealed class UnloadedMaterial : Material {
		public string modName, itemName;

		public override TagCompound SerializeData() {
			TagCompound tag = new();

			tag["mod"] = modName;
			tag["name"] = itemName;
			tag["rarity"] = rarity;

			return tag;
		}

		public static new Func<TagCompound, UnloadedMaterial> DESERIALIZER = tag => {
			string mod = tag.GetString("mod");
			string name = tag.GetString("name");
			int rarity = tag.GetInt("rarity");

			return new UnloadedMaterial(){
				modName = mod,
				itemName = name,
				rarity = rarity,
				type = -1
			};
		};
	}
}
