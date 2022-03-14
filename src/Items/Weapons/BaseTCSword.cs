using TerrariansConstructLib.ID;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct.Items.Weapons {
	/// <summary>
	/// A constructed sword item
	/// </summary>
	public class BaseTCSword : BaseTCItem {
		public override int PartsCount => 3;

		public override string VisualsFolderPath => "TerrariansConstruct/Assets/Visuals/Sword";

		public override string WeaponTypeName => "Sword";

		public BaseTCSword() : base(MaterialPartID.WeaponLongSwordBlade, MaterialPartID.WeaponSwordGuard, MaterialPartID.ToolRod){ }
	}
}
