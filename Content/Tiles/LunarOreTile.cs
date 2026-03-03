using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Lunar.Content.Tiles
{
    public class LunarOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSpelunker[Type] = true; // brilha com poção
            Main.tileOreFinderPriority[Type] = 500;

            MineResist = 4f; // resistência pra minerar
            MinPick = 65;    // poder mínimo da picareta

            DustType = DustID.BlueTorch;
            HitSound = SoundID.Tink;

            AddMapEntry(new Color(100, 150, 255), CreateMapEntryName());
        }
    }
}