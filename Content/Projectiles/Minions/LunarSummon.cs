using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Lunar.Content.NPCS.Mobs.Lunar;
using Lunar.Content.Buffs;

namespace Lunar.Content.Projectiles.Minions
{
    public class LunarSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 1;

            ProjectileID.Sets.MinionTargettingFeature[Type] = true;
            ProjectileID.Sets.MinionSacrificable[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 21;

            Projectile.friendly = true;
            Projectile.minion = true; // <-- aqui é o certo
            Projectile.DamageType = DamageClass.Summon;

            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;

            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override bool? CanCutTiles() => false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead)
            {
                player.ClearBuff(ModContent.BuffType<LunarSummonBuff>());
                return;
            }

            // mantém vivo enquanto tiver o buff
            if (player.HasBuff(ModContent.BuffType<LunarSummonBuff>()))
                Projectile.timeLeft = 2;

            // posição "idle" perto do player
            Vector2 idlePos = player.Center + new Vector2(player.direction * -40f, -30f);
            Vector2 toIdle = idlePos - Projectile.Center;
            float distIdle = toIdle.Length();

            // procura alvo
            NPC target = FindTarget(player, out float targetDist);

            if (target != null)
            {
                // segue/ataca: encosta no alvo e causa contato (friendly)
                Vector2 toTarget = target.Center - Projectile.Center;
                float speed = 8.5f;
                float inertia = 18f;

                Vector2 desiredVel = toTarget.SafeNormalize(Vector2.Zero) * speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + desiredVel) / inertia;
            }
            else
            {
                // volta pro player
                float speed = 7.5f;
                float inertia = 22f;

                if (distIdle > 600f)
                {
                    // teleport se muito longe (evita sumir)
                    Projectile.Center = idlePos;
                    Projectile.velocity = Vector2.Zero;
                    Projectile.netUpdate = true;
                }
                else
                {
                    Vector2 desiredVel = toIdle.SafeNormalize(Vector2.Zero) * speed;
                    if (distIdle < 40f) desiredVel *= distIdle / 40f;

                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + desiredVel) / inertia;
                }
            }

            // direção/rotação simples
            if (Projectile.velocity.X != 0)
                Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
        }

        private NPC FindTarget(Player player, out float dist)
        {
            dist = 900f;

            // target manual do player
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (npc.CanBeChasedBy(this))
                {
                    float d = Vector2.Distance(Projectile.Center, npc.Center);
                    if (d < dist)
                    {
                        dist = d;
                        return npc;
                    }
                }
            }

            NPC best = null;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.CanBeChasedBy(this)) continue;

                float d = Vector2.Distance(Projectile.Center, npc.Center);
                if (d < dist)
                {
                    dist = d;
                    best = npc;
                }
            }
            return best;
        }
    }
}