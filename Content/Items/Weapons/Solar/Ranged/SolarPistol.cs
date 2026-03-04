using Microsoft.Xna.Framework;
using Lunar.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Lunar.Content.Items.Weapons.Solar.Ranged
{
    public class SolarPistol : ModItem
    {
        public override void SetDefaults()
        {
            // Visual properties
            Item.width = 70;
            Item.height = 28;
            Item.scale = 1.05f;
            Item.useStyle = ItemUseStyleID.Shoot; // Use style for guns
            Item.rare = ItemRarityID.Blue;

            // Combat properties
            Item.damage = 50; // Gun damage + bullet damage = final damage
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 15; // Delay between shots.
            Item.useAnimation = 15; // How long shoot animation lasts in ticks.
            Item.knockBack = 4.5f; // Gun knockback + bullet knockback = final knockback
            Item.autoReuse = true;

            // Other properties
            Item.value = 10000;
            Item.UseSound = SoundID.Item11; // Gun use sound

            // Gun properties
            Item.noMelee = true; // Item not dealing damage while held, we don’t hit mobs in the head with a gun
            Item.shoot = ProjectileID.PurificationPowder; // What kind of projectile the gun fires, does not mean anything here because it is replaced by ammo
            Item.shootSpeed = 16f; // Speed of a projectile. Mainly measured by eye
            Item.useAmmo = AmmoID.Bullet; // What ammo gun uses
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GoddessTear>(10)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}