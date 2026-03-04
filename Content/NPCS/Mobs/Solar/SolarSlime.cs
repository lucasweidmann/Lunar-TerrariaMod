using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Lunar.Content.NPCS.Mobs.Solar
{
    public class SolarSlime : ModNPC
    {
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerInTown) return 0f;
            if (Main.invasionType != 0) return 0f;
            if (Main.pumpkinMoon || Main.snowMoon) return 0f;

            if (!Main.dayTime && spawnInfo.Player.ZoneOverworldHeight)
            {
                return 0.05f;
            }

            return 0f;
        }
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 52;
            NPC.damage = 15;
            NPC.defense = 6;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 60f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = NPCAIStyleID.Slime;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
            Main.npcFrameCount[Type] = 2;
            NPC.alpha = 100;
            
        }
        public override void AI()   
        {
            Lighting.AddLight(NPC.Center, 0.1f, 0.3f, 0.8f);
            if (Main.rand.NextBool(4))
            {
                // Spawn de dust dentro do hitbox do NPC
                int dustIndex = Dust.NewDust(
                    NPC.position,
                    NPC.width,
                    NPC.height,
                    DustID.BlueTorch, // bom pra “glow azul”
                    NPC.velocity.X * 0.2f,
                    NPC.velocity.Y * 0.2f,
                    Alpha: 150,
                    newColor: default,
                    Scale: 1.2f
                );

                Dust d = Main.dust[dustIndex];
                d.noGravity = true;          // fica “flutuando”
                d.velocity *= 0.3f;          // mais suave
                d.fadeIn = 0.8f;             // aparece mais bonito
            }
        }
    }
}
