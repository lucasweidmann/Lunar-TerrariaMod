		using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // Using another one library

namespace Lunar.Content.Items
{
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class LunarSword : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Lunar.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3)) // With 1/3 chance per tick (60 ticks = 1 second)...
            {
                // ...spawning dust
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), // Position to spawn
                hitbox.Width, hitbox.Height, // Width and Height
                DustID.Cloud, // Dust type. Check https://terraria.wiki.gg/wiki/Dust_IDs
                0, 0, // Speed X and Speed Y of dust, it have some randomization
                125); // Dust transparency, 0 - full visibility, 255 - full transparency

            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}