using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Lunar.Content.Tiles;

namespace Lunar.Content.Items.Placeables
{

    public class LunarWorkbench : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Item.ResearchUnlockCount = 1; // Optional: Journey Mode research amount
        }

        public override void SetDefaults()
        {
            // Basic item settings
            Item.width = 34;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.White;

            // Placement behavior
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.consumable = true;

            // This item places the tile below
            Item.createTile = ModContent.TileType<LunarWorkbenchTile>();
        }

        /// <summary>
        /// Example recipe to craft the workbench item.
        /// Here we make it craftable at a vanilla Work Bench.
        /// </summary>
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}