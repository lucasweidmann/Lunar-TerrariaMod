using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Lunar.Content.Items.Placeables
{
    public class LunarOre : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Tiles.LunarOreTile>();
        }
    }
}