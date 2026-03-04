using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Lunar.Content.Tiles
{
    /// <summary>
    /// This is the placed workbench in the world.
    /// The important part for "crafting station" behavior is:
    ///   - AdjTiles includes the tile(s) you want it to count as (optional)
    ///   - Recipes can require this tile type via .AddTile(ModContent.TileType<MyWorkbenchTile>())
    /// </summary>
    public class LunarWorkbenchTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            // --- Core tile properties ---
            Main.tileSolidTop[Type] = true;     // Stand on it like a table/workbench
            Main.tileFrameImportant[Type] = true; // Frame matters for multi-tiles
            Main.tileNoAttach[Type] = true;     // Can't attach other tiles to it
            Main.tileTable[Type] = true;        // Acts like a table (helps some NPC housing checks)

            // --- Placement data (2 tiles wide x 1 tile tall, like a bench) ---
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.CoordinateHeights = new[] { 18 }; // 1 tile high
            TileObjectData.addTile(Type);

            // --- Map entry (name + color on minimap) ---
            AddMapEntry(new Color(200, 200, 200), CreateMapEntryName());

            // --- Dust & sound (cosmetic) ---
            DustType = DustID.WoodFurniture;
            HitSound = SoundID.Dig;

            // --- IMPORTANT: Crafting station identity ---
            // Option A (recommended): Recipes explicitly require THIS tile type:
            //     .AddTile(ModContent.TileType<MyWorkbenchTile>())
            //
            // Option B (optional): Make your tile ALSO count as a vanilla Work Bench for recipe adjacency:
            // This means recipes that require TileID.WorkBenches will work near your station.
            AdjTiles = new int[] { TileID.WorkBenches };
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}