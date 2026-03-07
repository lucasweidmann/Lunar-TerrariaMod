using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Lunar.Content.Items.Materials;
using Lunar.Content.Projectiles;

namespace Lunar.Content.Items.Weapons.LunarWeapons.Mage
{
    public class LunarStaff : ModItem
    {
        public override void ModifyShootStats(
            Player player,
            ref Vector2 position,
            ref Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback)
        {
            position.Y -= 28f;
            position.X -= -15;
        }

        public override void SetDefaults()
        {
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;

            Item.DefaultToMagicWeapon(
                projType: ModContent.ProjectileType<LunarProjectile>(),
                singleShotTime: 35,
                shotVelocity: 9f,
                hasAutoReuse: true
            );

            Item.damage = 45;
            Item.knockBack = 5f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item94;
            Item.mana = 10;
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
            if (Main.MouseWorld.X > player.Center.X)
                player.direction = 1;
            else
                player.direction = -1;

            player.itemRotation = 0f;
        }
    }
}