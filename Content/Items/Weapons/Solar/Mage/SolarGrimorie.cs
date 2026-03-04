using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Lunar.Content.Items.Materials;
using Lunar.Content.Projectiles;

namespace Lunar.Content.Items.Weapons.Solar.Mage
{
    public class SolarGrimorie : ModItem
    {
        public override void ModifyShootStats(
            Player player,
            ref Vector2 position,
            ref Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback)
                {
            // sobe o spawn 20 pixels
            position.Y -= 28f;
            position.X -= -15;
                }

        public override void SetDefaults()
        {
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
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
            Item.width = 25;
            Item.height = 66;

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<GoddessTear>(8)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override Vector2? HoldoutOffset() => new Vector2(-3f, -10f);
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            // Define a direção baseada na posição horizontal do mouse
            if (Main.MouseWorld.X > player.Center.X)
                player.direction = 1;
            else
                player.direction = -1;

            // Mantém o item reto (sem rotação)
            player.itemRotation = 0f;
        }
    }
}