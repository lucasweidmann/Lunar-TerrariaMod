using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Lunar.Content.NPCS.Bosses.Pale_Moon_Slime
{
    [AutoloadBossHead]
    public class PaleMoonSlime : ModNPC
    {
        // Estados simples
        private enum ActionState
        {
            Idle = 0,
            Hop = 1
        }

        private ref float State => ref NPC.ai[0];
        private ref float Timer => ref NPC.ai[1];

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
            NPC.alpha = 100;
        }

        public override void SetDefaults()
        {
            NPC.width = 164;
            NPC.height = 120;
            NPC.damage = 40;
            NPC.defense = 16;
            NPC.lifeMax = 12000;

            NPC.boss = true;
            NPC.aiStyle = -1; // AI custom
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 6, 0, 0);

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.noTileCollide = false;
            NPC.noGravity = false;

            NPC.lavaImmune = true;

            NPC.netAlways = true;

            // Música do boss (opcional)
            // Music = MusicID.Boss2;

            // Se quiser boss bag:
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedLunarSlime, -1);
        }

        public override void OnSpawn(IEntitySource source)
        {
            State = (float)ActionState.Idle;
            Timer = 0;
            NPC.TargetClosest(false);
        }

        public override bool CheckActive()
        {
            // não some do nada igual NPC comum
            return false;
        }

        public override void AI()
        {
            // brilho
            Lighting.AddLight(NPC.Center, 0.1f, 0.3f, 0.8f);

            // dust azul
            if (Main.rand.NextBool(4))
            {
                int dustIndex = Dust.NewDust(
                    NPC.position,
                    NPC.width,
                    NPC.height,
                    DustID.BlueTorch,
                    NPC.velocity.X * 0.2f,
                    NPC.velocity.Y * 0.2f,
                    Alpha: 150,
                    newColor: default,
                    Scale: 1.2f
                );

                Dust d = Main.dust[dustIndex];
                d.noGravity = true;
                d.velocity *= 0.3f;
                d.fadeIn = 0.8f;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.TargetClosest(false);

            Player player = Main.player[NPC.target];

            // Se o jogador morreu ou sumiu, sobe e foge (despawns controlado)
            if (!player.active || player.dead)
            {
                NPC.velocity.Y -= 0.08f;
                NPC.velocity.X *= 0.98f;
                NPC.timeLeft = 10;
                return;
            }

            // "Gravidade lunar": cai bem mais devagar
            ApplyLowGravity();

            // olhar pro player
            NPC.direction = (player.Center.X > NPC.Center.X) ? 1 : -1;

            // fase 2 (vida baixa): mais agressivo
            bool phase2 = NPC.life < NPC.lifeMax * 0.5f;

            switch ((ActionState)State)
            {
                case ActionState.Idle:
                    DoIdle(player, phase2);
                    break;

                case ActionState.Hop:
                    DoHop(player, phase2);
                    break;
            }

            // Limitar queda pra não ficar absurdamente rápida
            if (NPC.velocity.Y > 10f)
                NPC.velocity.Y = 10f;
        }

        private void ApplyLowGravity()
        {
            // gravidade padrão do Terraria em NPCs é ~0.3 por tick (depende de estilo).
            // Aqui forçamos uma queda bem menor.
            float lunarGravity = 0.08f;

            // Se estiver no ar, aplica gravidade baixa
            if (NPC.velocity.Y != 0f)
                NPC.velocity.Y += lunarGravity;

            // Um pouquinho de "arrasto" no ar pra parecer flutuar
            if (!NPC.collideY)
            {
                NPC.velocity.X *= 0.995f;
                NPC.velocity.Y *= 0.999f;
            }
        }

        private void DoIdle(Player player, bool phase2)
        {
            Timer++;

            // Desacelera no chão
            if (NPC.collideY)
                NPC.velocity.X *= 0.92f;

            // Tempo até pular
            int idleTime = phase2 ? 45 : 65;

            if (Timer >= idleTime)
            {
                Timer = 0;
                State = (float)ActionState.Hop;
                NPC.netUpdate = true;
            }
        }

        private void DoHop(Player player, bool phase2)
        {
            Timer++;

            // Só inicia o pulo quando estiver no chão
            if (NPC.collideY && Timer == 1)
            {
                Vector2 toPlayer = player.Center - NPC.Center;
                float dir = Math.Sign(toPlayer.X);

                // Força horizontal e vertical (mais "lua": vertical alto, queda lenta)
                float hopX = phase2 ? 6.2f : 5.0f;
                float hopY = phase2 ? 11.5f : 10.0f;

                // “Lead” básico: se o player estiver longe, aumenta um pouco o X
                float distance = toPlayer.Length();
                if (distance > 600f) hopX += 1.0f;

                NPC.velocity.X = dir * hopX;
                NPC.velocity.Y = -hopY;

                SoundEngine.PlaySound(SoundID.Item24, NPC.Center); // som “lunar” improvisado
            }

            // Depois de um tempo no ar / após aterrissar, volta pro idle
            int hopDuration = phase2 ? 40 : 55;

            if (Timer >= hopDuration && NPC.collideY)
            {
                Timer = 0;
                State = (float)ActionState.Idle;
                NPC.netUpdate = true;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Boss bag (modo expert/master)
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PaleMoonBag>()));

            // Drops no normal
            npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 1, 10, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.MoonStone, 5, 1, 1)); // exemplo
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 80, 140));

            // dinheiro já vem do NPC.value
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool? CanFallThroughPlatforms()
        {
            // opcional: slime pode atravessar plataforma pra perseguir
            Player player = Main.player[NPC.target];
            if (player.Center.Y > NPC.Center.Y + 40f)
                return true;

            return false;
        }
    }

    // Sistema simples de "downed" (se você quiser registrar que o boss foi derrotado)
    public class DownedBossSystem : ModSystem
    {
        public static bool downedLunarSlime;

        public override void ClearWorld()
        {
            downedLunarSlime = false;
        }

        public override void SaveWorldData(Terraria.ModLoader.IO.TagCompound tag)
        {
            tag["downedLunarSlime"] = downedLunarSlime;
        }

        public override void LoadWorldData(Terraria.ModLoader.IO.TagCompound tag)
        {
            downedLunarSlime = tag.ContainsKey("downedLunarSlime") && tag.GetBool("downedLunarSlime");
        }
    }
}