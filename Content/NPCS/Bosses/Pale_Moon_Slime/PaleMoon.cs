using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Lunar.Content.NPCS.Bosses.Pale_Moon_Slime
{
    public class PaleMoon : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            // só de noite, por exemplo
            if (Main.dayTime) return false;

            // não spawnar se já existir
            return !NPC.AnyNPCs(ModContent.NPCType<PaleMoonSlime>());
        }

        public override bool? UseItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<PaleMoonSlime>());
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Gel, 50)
                .AddIngredient(ItemID.FallenStar, 5)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}