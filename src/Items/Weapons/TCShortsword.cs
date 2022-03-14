using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed shortsword item
	/// </summary>
	public class TCShortsword : BaseTCItem {
		public override int PartsCount => 3;

		public override string VisualsFolderPath => "TerrariansConstruct/Assets/Visuals/Shortsword";

		public TCShortsword() : base(CoreMod.RegisteredItems.Shortsword) { }
	}
}
