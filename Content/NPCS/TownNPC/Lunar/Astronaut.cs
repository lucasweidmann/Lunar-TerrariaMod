using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Lunar.Content.NPCS.TownNPC.Lunar
{
    public class Astronaut : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 56;
            NPC.damage = 15;
            NPC.defense = 6;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = NPCAIStyleID.Passive;
            AIType = NPCID.Guide;
            NPC.friendly = true;
            AnimationType = NPCID.Guide;
            Main.npcFrameCount[Type] = 25;
        }
    }
}
