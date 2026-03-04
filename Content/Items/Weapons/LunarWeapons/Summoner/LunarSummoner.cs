using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Lunar.Content.Projectiles.Minions;
using Lunar.Content.Buffs;

namespace Lunar.Content.Items.Weapons.LunarWeapons.Summoner
{
    public class LunarSummoner : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Faz contar como staff (animação de "segurar" tipo cajado)
            Item.staff[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;                 // ajuste
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;                   // ajuste
            Item.width = 40;
            Item.height = 44;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing; // pode trocar pra Shoot se preferir
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(0, 1, 50, 0);
            Item.rare = ItemRarityID.Orange;

            Item.UseSound = SoundID.Item44;

            Item.shoot = ModContent.ProjectileType <LunarSummon>();
            Item.buffType = ModContent.BuffType<LunarSummonBuff>();
            Item.shootSpeed = 10f; // usado só pra calcular posição; minion ignora velocidade depois
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // aplica buff ao usar (padrão de summon staff)
            player.AddBuff(Item.buffType, 2);

            // spawna o minion no mouse
            Vector2 spawnPos = Main.MouseWorld;
            Projectile.NewProjectile(source, spawnPos, Vector2.Zero, type, damage, knockback, player.whoAmI);

            // bloqueia shoot padrão
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FallenStar, 8)
                .AddIngredient(ItemID.Gel, 25)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}