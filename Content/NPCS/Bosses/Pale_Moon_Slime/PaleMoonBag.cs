using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Lunar.Content.NPCS.Bosses.Pale_Moon_Slime
{
    public class PaleMoonBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 31;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemID.Gel, Main.rand.Next(120, 200));
            player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemID.FallenStar, Main.rand.Next(15, 30));

            if (Main.rand.NextBool(4))
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemID.MoonStone);
        }
    }
}