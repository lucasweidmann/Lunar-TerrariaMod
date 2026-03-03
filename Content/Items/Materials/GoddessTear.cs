using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace Lunar.Content.Items.Materials
{
    public class GoddessTear : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 26;
            Item.maxStack = 9999;
            Item.value = 400;
            Item.rare = ItemRarityID.Blue;
        }
    }
}