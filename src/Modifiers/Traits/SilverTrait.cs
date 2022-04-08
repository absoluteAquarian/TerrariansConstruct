using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using TerrariansConstructLib.API.Sources;
using TerrariansConstructLib.Items;
using TerrariansConstructLib.Modifiers;

namespace TerrariansConstruct.Modifiers.Traits {
	internal sealed class SilverTrait : BaseTrait {
		public override Color TooltipColor => new(0x99, 0xb4, 0xcc);

		public override string LangKey => "Mods.TerrariansConstruct.PartTooltips.Silver";

		//All this ability does is make certain NPCs take more damage
		public override bool ShouldUpdateCounter(Player player) => false;

		//Needs to be greater than zero since the counter isn't being updated and it's "supposed to" increment,
		//just so that OnCounterTargetReached doesn't get called every tick
		public override double GetExpectedCounterTarget(Player player) => 1f;

		public static bool CountsForIncreasedDamage(int npcType)
			=> NPCID.Sets.Zombies[npcType] || NPCID.Sets.Skeletons[npcType] || NPCID.Sets.DemonEyes[npcType]
				|| npcType == NPCID.EyeofCthulhu || npcType == NPCID.SkeletronHead || npcType == NPCID.SkeletronHand
				|| npcType == NPCID.WallofFlesh || npcType == NPCID.WallofFleshEye
				|| npcType == NPCID.Retinazer || npcType == NPCID.Spazmatism
				|| npcType == NPCID.SkeletronPrime || npcType == NPCID.PrimeCannon || npcType == NPCID.PrimeLaser || npcType == NPCID.PrimeSaw || npcType == NPCID.PrimeVice;

		private int critCheck;
		public override void OnUpdate(Player player, bool counterWasReset) {
			critCheck = -1;
		}

		public override void ModifyHitNPC(Player player, NPC target, BaseTCItem item, ref int damage, ref float knockBack, ref bool crit) {
			if (CountsForIncreasedDamage(target.type)) {
				damage = (int)(damage * 1.5f);
				knockBack *= 1.1f;

				if (!crit && critCheck != target.whoAmI) {
					critCheck = target.whoAmI;

					int tier = Math.Max(Tier, 10);
					if (Main.rand.NextBool(tier * 10, 100))
						crit = true;
				}
			}
		}

		public override void PreModifyDurability(Player player, BaseTCItem item, IDurabilityModificationSource source, ref int amount) {
			//Increased damage also results in more durability loss
			if (amount < 0 && source is DurabilityModificationSource_HitEntity hitEntity && hitEntity.entity is NPC npc && CountsForIncreasedDamage(npc.type))
				--amount;
		}
	}
}
