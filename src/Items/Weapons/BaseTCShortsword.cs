using TerrariansConstructLib.ID;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed shortsword item
	/// </summary>
	public class BaseTCShortsword : BaseTCItem {
		public override int PartsCount => 3;

		public override string VisualsFolderPath => "TerrariansConstruct/Assets/Visuals/Shortsword";

		public override string WeaponTypeName => "Shortsword";

		public BaseTCShortsword() : base(MaterialPartID.WeaponShortSwordBlade, MaterialPartID.WeaponShortSwordGuard, MaterialPartID.ToolRod) { }
	}
}
