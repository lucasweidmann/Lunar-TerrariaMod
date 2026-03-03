using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Lunar.Content.World
{
    public class LunarWorldGen : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int index = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (index != -1)
            {
                tasks.Insert(index + 1, new PassLegacy("Lunar Ores", GenerateLunarOre));
            }
        }

        private void GenerateLunarOre(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Spreading Lunar Ore";

            int maxSpawns = (int)(Main.maxTilesX * Main.maxTilesY * 0.00030);

            for (int i = 0; i < maxSpawns; i++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);

                WorldGen.TileRunner(
                    x,
                    y,
                    WorldGen.genRand.Next(4, 7),   // tamanho do minério
                    WorldGen.genRand.Next(3, 6),   // quantidade
                    ModContent.TileType<Tiles.LunarOreTile>() // seu tile
                );
            }
        }
    }
}