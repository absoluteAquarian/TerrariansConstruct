using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariansConstruct.ID;
using TerrariansConstruct.Projectiles;

namespace TerrariansConstruct.Items {
	[Autoload(false)]
	public class BaseTCItem : ModItem {
		public ItemPart[] parts = Array.Empty<ItemPart>();

		public int ammoReserve, ammoReserveMax;

		public BaseTCItem(params ItemPart[] parts) {
			this.parts = parts;
		}

		public void SetUseNoAmmo() {
			Item.shoot = ProjectileID.None;
			Item.ammo = 0;
		}

		public void SetUseAmmo(int constructedAmmoID) {
			Item.shoot = ConstructedAmmoID.GetProjectileType(constructedAmmoID);
			Item.useAmmo = TerrariansConstruct.GetAmmoID(constructedAmmoID);
		}

		public float copperPartCharge;
		public const float CopperPartChargeMax = 6f * 2.5f * 60 * 60;  //6 velocity for at least 2.5 minutes
		public bool copperChargeActivated;

		public bool CopperChargeReady => !copperChargeActivated && copperPartCharge >= CopperPartChargeMax;

		public virtual string TooltipText => null;

		public sealed override void SetStaticDefaults() {
			SafeSetStaticDefaults();

			DisplayName.SetDefault("Constructed Weapon");
			Tooltip.SetDefault((TooltipText is not null ? TooltipText + "\n" : "") +
				"<PART_TYPES>\n" +
				"<PART_TOOLTIPS>\n" +
				"<MODIFIERS>\n" +
				"<AMMO_COUNT>");
		}

		/// <inheritdoc cref="SetStaticDefaults"/>
		public virtual void SafeSetStaticDefaults() { }

		public sealed override void SetDefaults() {
			SafeSetDefaults();

			for (int i = 0; i < parts.Length; i++)
				parts[i].SetItemDefaults(parts[i].partID, Item);

			Item.maxStack = 1;
			Item.consumable = false;
		}

		/// <inheritdoc cref="SetDefaults"/>
		public virtual void SafeSetDefaults() { }

		public override bool CanConsumeAmmo(Player player) {
			return base.CanConsumeAmmo(player);
		}

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat) {
			for (int i = 0; i < parts.Length; i++)
				parts[i].ModifyWeaponDamage(parts[i].partID, player, ref damage, ref flat);
		}

		public override void ModifyWeaponKnockback(Player player, ref StatModifier knockback, ref float flat) {
			for (int i = 0; i < parts.Length; i++)
				parts[i].ModifyWeaponKnockback(parts[i].partID, player, ref knockback, ref flat);
		}

		public override void ModifyWeaponCrit(Player player, ref int crit) {
			for (int i = 0; i < parts.Length; i++)
				parts[i].ModifyWeaponCrit(parts[i].partID, player, ref crit);
		}

		public bool HasPartOfType(int type, out int partIndex) {
			for (int i = 0; i < parts.Length; i++) {
				if (parts[i].material.type == type) {
					partIndex = i;
					return true;
				}
			}

			partIndex = -1;
			return false;
		}

		public override void HoldItem(Player player) {
			for (int i = 0; i < parts.Length; i++)
				parts[i].OnHold(parts[i].partID, player, Item);

			//Hardcoded here to make the ability only apply once, regardless of how many parts are Copper
			// TODO: have a flag or something dictate if a part's ability should only activate once
			if (HasPartOfType(ItemID.CopperBar, out _)) {
				if (copperChargeActivated) {
					copperPartCharge -= CopperPartChargeMax / (7 * 60);  //7 seconds of usage

					const int area = 6;

					if (Main.rand.NextFloat() < 0.3f) {
						Vector2 velocity = Vector2.UnitX.RotatedByRandom(MathHelper.Pi) * 3f;

						Dust.NewDust(player.Center - new Vector2(area / 2f), area, area, DustID.MartianSaucerSpark, velocity.X, velocity.Y);
					}

					if (copperPartCharge < 0) {
						copperPartCharge = 0;
						copperChargeActivated = false;
					}
				} else if (player.velocity.Y == 0 && (player.controlLeft || player.controlRight)) {
					copperPartCharge += Math.Abs(player.velocity.X);

					if (copperPartCharge > CopperPartChargeMax)
						copperPartCharge = CopperPartChargeMax;
				}
			}
		}

		public override bool? UseItem(Player player) {
			for (int i = 0; i < parts.Length; i++)
				parts[i].OnUse(parts[i].partID, player, Item);

			return true;
		}

		public override void SaveData(TagCompound tag) {
			tag["parts"] = parts.ToList();
		}

		public override void LoadData(TagCompound tag) {
			parts = tag.GetList<ItemPart>("parts").ToArray();
		}
	}
}
