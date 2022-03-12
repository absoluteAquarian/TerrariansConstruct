namespace TerrariansConstruct.API.Edits {
	internal static class EditsLoader {
		public static void Load() {
			On.Terraria.Projectile.NewProjectile_IEntitySource_float_float_float_float_int_int_float_int_float_float += Detours.Vanilla.Hook_Projectile_NewProjectile;
		}
	}
}
