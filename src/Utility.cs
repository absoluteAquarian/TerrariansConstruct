using System;
using System.Collections.Generic;
using Terraria;
using TerrariansConstruct.ID;
using TerrariansConstruct.Materials;

namespace TerrariansConstruct {
	public static class Utility {
		public static T GetValueFromPartDictionary<T>(this Dictionary<int, Dictionary<int, T>> partDictionary, Material material, int partID) {
			int materialType = material.type;
			if (!partDictionary.TryGetValue(materialType, out var dictByPartID))
				throw new ArgumentException($"Unknown material type: \"{Lang.GetItemNameValue(materialType)}\" (ID: {material})");

			if (partID < 0 || partID >= MaterialPartID.TotalCount)
				throw new Exception($"Part ID {partID} was invalid");

			if (!dictByPartID.TryGetValue(partID, out T value))
				throw new ArgumentException($"Material type \"{Lang.GetItemNameValue(materialType)}\" (ID: {material}) did not have an entry for part ID {partID}");

			return value;
		}

		public static void SetValueInPartDictionary<T>(this Dictionary<int, Dictionary<int, T>> partDictionary, Material material, int partID, T value) {
			int materialType = material.type;
			if (!partDictionary.TryGetValue(materialType, out var dictByPartID))
				throw new ArgumentException($"Unknown material type: \"{Lang.GetItemNameValue(materialType)}\" (ID: {material})");

			//Ensure that the part exists
			if (partID < 0 || partID >= MaterialPartID.TotalCount)
				throw new Exception($"Part ID {partID} was invalid");

			dictByPartID[partID] = value;
		}

		public static bool TryGetValueFromPartDictionary<T>(this Dictionary<int, Dictionary<int, T>> partDictionary, Material material, int partID, out T value) {
			int materialType = material.type;
			if (!partDictionary.TryGetValue(materialType, out var dictByPartID)) {
				value = default;
				return false;
			}

			if (partID >= MaterialPartID.TotalCount) {
				value = default;
				return false;
			}

			return dictByPartID.TryGetValue(partID, out value);
		}

		public static void SafeSetValueInPartDictionary<T>(this Dictionary<int, Dictionary<int, T>> partDictionary, Material material, int partID, T value) {
			int materialType = material.type;
			if (!partDictionary.TryGetValue(materialType, out var dictByPartID))
				dictByPartID = partDictionary[materialType] = new();

			//Ensure that the part exists
			if (partID < 0 || partID >= MaterialPartID.TotalCount)
				throw new Exception($"Part ID {partID} was invalid");

			dictByPartID[partID] = value;
		}
	}
}
