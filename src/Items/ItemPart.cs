using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrariansConstruct.API;
using TerrariansConstruct.ID;
using TerrariansConstruct.Materials;
using TerrariansConstruct.Registry;

namespace TerrariansConstruct.Items {
	public class ItemPart : TagSerializable {
		public delegate void PartItemFunc(int partID, Item item);
		public delegate void PartPlayerFunc(int partID, Player player, Item item);
		public delegate void PartProjectileFunc(int partID, Projectile projectile);
		public delegate void PartProjectileSpawnFunc(int partID, Projectile projectile, IEntitySource source, float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner, float ai0, float ai1);
		public delegate void PartProjectileHitNPCFunc(int partID, Projectile projectile, NPC target, int damage, float knockback, bool crit);
		public delegate void PartProjectileHitPlayerFunc(int partID, Projectile projectile, Player targetpartID, int damage, bool crit);
		public delegate void PartModifyWeaponDamageFunc(int partID, Player player, ref StatModifier damage, ref float flat);
		public delegate void PartModifyWeaponKnockbackFunc(int partID, Player player, ref StatModifier knockback, ref float flat);
		public delegate void PartModifyWeaponCritFunc(int partID, Player player, ref int crit);

		internal static PartsDictionary<ItemPart> partData;

		public void SetTooltip(string tooltip)
			=> SetTooltip(material, partID, tooltip);

		public static void SetTooltip(Material material, int partID, string tooltip) {
			if (material is not UnloadedMaterial)
				partData.Get(material, partID).tooltip = tooltip;
		}

		/// <summary>
		/// The material used to create this item part
		/// </summary>
		public Material material;

		/// <summary>
		/// The part type associated with this item part
		/// </summary>
		public int partID;

		public string tooltip;

		public PartItemFunc SetItemDefaults => PartActions.GetPartActions(material, partID).setItemDefaults;

		public PartProjectileFunc SetProjectileDefaults => PartActions.GetPartActions(material, partID).setProjectileDefaults;

		public PartPlayerFunc OnUse => PartActions.GetPartActions(material, partID).onUse;

		public PartPlayerFunc OnHold => PartActions.GetPartActions(material, partID).onHold;

		public PartPlayerFunc OnGenericHotkeyUsage => PartActions.GetPartActions(material, partID).onGenericHotkeyUsage;

		public PartProjectileSpawnFunc OnProjectileSpawn => PartActions.GetPartActions(material, partID).onProjectileSpawn;

		public PartProjectileHitNPCFunc OnHitNPC => PartActions.GetPartActions(material, partID).onHitNPC;

		public PartProjectileHitPlayerFunc OnHitPlayer => PartActions.GetPartActions(material, partID).onHitPlayer;

		public PartModifyWeaponDamageFunc ModifyWeaponDamage => PartActions.GetPartActions(material, partID).modifyWeaponDamage;

		public PartModifyWeaponKnockbackFunc ModifyWeaponKnockback => PartActions.GetPartActions(material, partID).modifyWeaponKnockback;

		public PartModifyWeaponCritFunc ModifyWeaponCrit => PartActions.GetPartActions(material, partID).modifyWeaponCrit;

		public PartProjectileFunc ProjectileAI => PartActions.GetPartActions(material, partID).projectileAI;

		public virtual void ExtraSerializeData(TagCompound tag) { }

		public TagCompound SerializeData() {
			TagCompound tag = new();

			tag["material"] = material;
			tag["part"] = partID;

			return tag;
		}

		public static Func<TagCompound, ItemPart> DESERIALIZER = tag => {
			Material material = tag.Get<Material>("material");

			if (material is null)
				material = tag.Get<UnloadedMaterial>("material");

			int partID = tag.GetInt("part");

			return new ItemPart(){
				material = material,
				partID = partID
			};
		};
	}
}
