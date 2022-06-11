using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using TerrariansConstructLib.API.Sources;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	public sealed class GlassTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override bool CounterIncrements => false;

		public override Color TooltipColor => new(0xc8, 0xf6, 0xfe);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Glass";

		//Counter is kept track of manually
		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => -1;

		public override void ModifyHitNPC(Player player, NPC target, BaseTCItem item, ref int damage, ref float knockBack, ref bool crit) {
			Trigger(player, item, target, ref damage, ref knockBack);
		}

		public override void ModifyHitPlayer(Player owner, Player target, BaseTCItem item, ref int damage, ref bool crit) {
			float knockBack = 0f;
			Trigger(owner, item, target, ref damage, ref knockBack);
		}

		private void Trigger(Player player, BaseTCItem item, Entity target, ref int damage, ref float knockBack) {
			int tier = Math.Min(10, Tier);
			int chance = tier * 5;

			if (player.RollLuck(100) < chance) {
				bool tool = item.HasAnyToolPower();
				if (!item.TryReduceDurability(player, 9, new DurabilityModificationSource_HitEntity(target, tool)))
					return;
				
				DisplayMessageAbovePlayer(player, TooltipColor, $"-{(tool ? 20 : 10)} DURABILITY");

				SoundEngine.PlaySound(SoundID.Shatter, player.Center);

				damage = (int)(damage * (1f + tier * 0.5f));
				knockBack *= 1f + tier * 0.125f;
			}
		}
	}
}
