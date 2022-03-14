using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed sword item
	/// </summary>
	public class TCSword : BaseTCItem {
		public override int PartsCount => 3;

		public override string VisualsFolderPath => "TerrariansConstruct/Assets/Visuals/Sword";

		public TCSword() : base(CoreMod.RegisteredItems.Sword){ }

		public override void SafeSetDefaults() {
			
		}
	}
}
