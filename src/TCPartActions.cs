using Terraria.Audio;
using Terraria.ID;
using TerrariansConstructLib;
using TerrariansConstructLib.Items;

namespace TerrariansConstruct {
	/// <summary>
	/// A collection of action builders for base Terrarians' Construct materials
	/// </summary>
	public static class TCPartActions {
		public static readonly ItemPartActionsBuilder Copper = new ItemPartActionsBuilder()
			.WithOnGenericHotkeyUsage((part, player, item) => {
				if (item.ModItem is BaseTCItem tc && tc.CopperChargeReady) {
					tc.copperChargeActivated = true;

					SoundEngine.PlaySound(SoundID.Item92.WithVolume(0.7f).WithPitchVariance(0.1f), player.Center);
					SoundEngine.PlaySound(SoundID.Item93.WithVolume(0.37f).WithPitchVariance(0.18f), player.Center);
				}
			})
			.MarkAsReadonly();
	}
}
