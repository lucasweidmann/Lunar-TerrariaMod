using Microsoft.Xna.Framework;
using Lunar.Content.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Lunar.Content.Items.Weapons.LunarWeapons.Ranged
{
    public class LunarGun : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 28;
            Item.scale = 1.05f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.knockBack = 4.5f;
            Item.autoReuse = true;

            Item.value = 10000;
            Item.UseSound = SoundID.Item11;

            Item.noMelee = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
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