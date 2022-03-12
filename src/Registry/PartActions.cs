using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariansConstruct.API;
using TerrariansConstruct.Items;
using TerrariansConstruct.Materials;

namespace TerrariansConstruct.Registry {
	/// <summary>
	/// A collection of action builders for base Terrarians' Construct materials
	/// </summary>
	public static class PartActions {
		internal static PartsDictionary<ItemPartActionsBuilder> builders;

		public static ItemPartActionsBuilder GetPartActions(Material material, int partID)
			=> material is UnloadedMaterial ? new() : builders.Get(material, partID);

		public static readonly ItemPartActionsBuilder NoActions = new();

		public static readonly ItemPartActionsBuilder Copper = new ItemPartActionsBuilder()
			.WithOnGenericHotkeyUsage((part, player, item) => {
				var tc = item.ModItem as BaseTCItem;

				if (tc.CopperChargeReady) {
					tc.copperChargeActivated = true;

					SoundEngine.PlaySound(SoundID.Item92.WithVolume(0.7f).WithPitchVariance(0.1f), player.Center);
					SoundEngine.PlaySound(SoundID.Item93.WithVolume(0.37f).WithPitchVariance(0.18f), player.Center);
				}
			});

		public static readonly ItemPartActionsBuilder Wood = new ItemPartActionsBuilder()
			.WithModifyWeaponKnockback((int partID, Player player, ref StatModifier knockback, ref float flat) => {
				knockback -= 0.1f;
			});
	}
}
