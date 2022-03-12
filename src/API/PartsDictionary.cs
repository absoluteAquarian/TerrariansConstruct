using System.Collections.Generic;
using System.Runtime.Loader;
using TerrariansConstruct.Materials;

namespace TerrariansConstruct.API {
	public class PartsDictionary<T> : Dictionary<int, Dictionary<int, T>> {
		private static class Cache {
			public static T instance;

			static Cache() {
				instance = default;

				AssemblyLoadContext ctx = AssemblyLoadContext.GetLoadContext(typeof(T).Assembly);

				if (ctx is not null)
					ctx.Unloading += _ => instance = default;
			}
		}

		public T Get(Material material, int partID)
			=> material is UnloadedMaterial
				? Cache.instance
				: this.GetValueFromPartDictionary(material, partID);

		public bool TryGet(Material material, int partID, out T value) {
			if (material is UnloadedMaterial) {
				value = Cache.instance;
				return true;
			}

			return this.TryGetValueFromPartDictionary(material, partID, out value);
		}

		public void Set(Material material, int partID, T value) {
			if (material is UnloadedMaterial)
				return;

			this.SafeSetValueInPartDictionary(material, partID, value);
		}
	}
}
