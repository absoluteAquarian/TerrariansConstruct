using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariansConstruct.Buffs;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	internal sealed class CopperTrait : BaseTrait {
		public override bool IsSingleton => true;

		public override Color TooltipColor => new(0xff, 0x60, 0x00);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Copper";

		public bool activated;

		public bool IsReady => !activated && Counter >= GetExpectedCounterTarget(null!);

		//Counter updating is done manually
		public override bool ShouldUpdateCounter(Player player) => false;

		public override double GetExpectedCounterTarget(Player player) => 6f * 0.5f * 60 * 60;  //6 velocity for at least 30 seconds

		public override void SaveData(TagCompound tag) {
			base.SaveData(tag);

			tag["activated"] = activated;
		}

		public override void LoadData(TagCompound tag) {
			base.LoadData(tag);

			activated = tag.GetBool("activated");
		}

		public static readonly SoundStyle Item92Custom = SoundID.Item92 with { Volume = 0.7f, PitchVariance = 0.1f };
		public static readonly SoundStyle Item93Custom = SoundID.Item93 with { Volume = 0.37f, PitchVariance = 0.18f };

		public override void OnAbilityHotkeyPressed(Player player) {
			if (IsReady) {
				activated = true;

				SoundEngine.PlaySound(Item92Custom, player.Center);
				SoundEngine.PlaySound(Item93Custom, player.Center);
			}
		}

		public override void OnHoldItem(Player player, BaseTCItem item) {
			if (!activated && player.velocity.Y == 0 && (player.controlLeft || player.controlRight)) {
				Counter += Math.Abs(player.velocity.X);

				double max = GetExpectedCounterTarget(player);
				if (Counter > max)
					Counter = max;
			}

			if (IsReady && !player.HasBuff<CopperAbilityReady>())
				player.AddBuff(ModContent.BuffType<CopperAbilityReady>(), 2);
		}

		public override void OnUpdateInventory(Player player, BaseTCItem item) {
			double max = GetExpectedCounterTarget(player);
			if (activated) {
				Counter -= max / (7 * 60);  //7 seconds of usage

				const int area = 20;

				if(Main.rand.NextFloat() < 0.6f) {
					Vector2 velocity = Vector2.UnitX.RotatedByRandom(MathHelper.Pi) * 3f;

					Dust.NewDust(player.Center - new Vector2(area / 2f), area, area, DustID.MartianSaucerSpark, velocity.X, velocity.Y);

					if (Counter < 0) {
						Counter = 0;
						activated = false;
					}

					player.AddBuff(ModContent.BuffType<CopperAbilityActive>(), (int)(Counter / max * (7 * 60)));
				}
			}
		}

		public override void UseSpeedMultiplier(Player player, BaseTCItem item, ref StatModifier multiplier) {
			if (activated)
				multiplier += 0.5f;
		}
	}
}
