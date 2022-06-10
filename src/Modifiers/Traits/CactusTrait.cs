using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using TerrariansConstruct.Modifiers.ForgeModifiers;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	public sealed class CactusTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => ModContent.GetInstance<SpikyModifier>().TooltipColor;

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Cactus";

		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 1f;

		public override void OnHitByNPC(NPC npc, Player target, int damage, bool crit) {
			//OnHitByNPC is guaranteed to only run clientside...
			DamageNPC(npc, target, damage);
		}

		internal static int? MyPlayerOverride;

		public override void OnHitByNPCProjectile(Projectile projectile, NPC npc, Player target, int damage, bool crit) {
			//... whereas OnHitByNPCProjectile is not
			MyPlayerOverride = target.whoAmI;
			
			DamageNPC(npc, target, damage);

			MyPlayerOverride = null;
		}

		private void DamageNPC(NPC npc, Player player, int damageFromNPC) {
			double strength = Math.Pow(1.1, Math.Min(5, Tier)) - 1d;

			npc.StrikeNPC((int)(damageFromNPC * strength), 2f, (npc.Center.X > player.Center.X).ToDirectionInt());
		}
	}
}
