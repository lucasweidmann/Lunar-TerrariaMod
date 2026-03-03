using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Lunar.Content.Items.Materials;
using Lunar.Content.Projectiles;

namespace Lunar.Content.Items.Weapons.Mage
{
    public class LunarStaff : ModItem
    {
        public override void SetDefaults()
        {
            // Helper method to quickly set basic magic weapon properties
            Item.DefaultToMagicWeapon(
                projType: ModContent.ProjectileType<LunarProjectile>(), // Our own projectile
                singleShotTime: 35, // useTime & useAnimation
                shotVelocity: 9f,
                hasAutoReuse: true
                );

            Item.damage = 45;
            Item.knockBack = 5f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item94; // Some electric sound
            Item.mana = 10; // This item uses 10 mana
            Item.width = 40;
            Item.height = 105;

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GoddessTear>(8)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override Vector2? HoldoutOffset() => new Vector2(-8f, -10f);
    }
}