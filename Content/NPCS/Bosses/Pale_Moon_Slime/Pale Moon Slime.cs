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
            NPC.aiStyle = -1;
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 6, 0, 0);
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
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
            return false;
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.1f, 0.3f, 0.8f);

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

            if (!player.active || player.dead)
            {
                NPC.velocity.Y -= 0.08f;
                NPC.velocity.X *= 0.98f;
                NPC.timeLeft = 10;
                return;
            }

            ApplyLowGravity();

            NPC.direction = (player.Center.X > NPC.Center.X) ? 1 : -1;

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

            if (NPC.velocity.Y > 10f)
                NPC.velocity.Y = 10f;
        }

        private void ApplyLowGravity()
        {
            float lunarGravity = 0.08f;

            if (NPC.velocity.Y != 0f)
                NPC.velocity.Y += lunarGravity;

            if (!NPC.collideY)
            {
                NPC.velocity.X *= 0.995f;
                NPC.velocity.Y *= 0.999f;
            }
        }

        private void DoIdle(Player player, bool phase2)
        {
            Timer++;

            if (NPC.collideY)
                NPC.velocity.X *= 0.92f;

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

            if (NPC.collideY && Timer == 1)
            {
                Vector2 toPlayer = player.Center - NPC.Center;
                float dir = Math.Sign(toPlayer.X);
                float hopX = phase2 ? 6.2f : 5.0f;
                float hopY = phase2 ? 11.5f : 10.0f;
                float distance = toPlayer.Length();

                if (distance > 600f)
                    hopX += 1.0f;

                NPC.velocity.X = dir * hopX;
                NPC.velocity.Y = -hopY;

                SoundEngine.PlaySound(SoundID.Item24, NPC.Center);
            }

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
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<PaleMoonBag>()));
            npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 1, 10, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.MoonStone, 5, 1, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 80, 140));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool? CanFallThroughPlatforms()
        {
            Player player = Main.player[NPC.target];
            if (player.Center.Y > NPC.Center.Y + 40f)
                return true;

            return false;
        }
    }

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