using System.IO;

namespace TerrariansConstruct {
	partial class CoreMod {
		public override void HandlePacket(BinaryReader reader, int whoAmI)
			=> NetHelper.HandlePacket(reader, whoAmI);
	}
}
