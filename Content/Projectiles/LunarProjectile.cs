using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Lunar.Content.Projectiles
{
    public class LunarProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Magic;
            Projectile.scale = 1f;
            Projectile.penetrate = 3;
            Projectile.aiStyle = 0;
            Projectile.width = Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 90;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 0;
        }

        public override void AI()
        {
            Projectile.rotation = 0f;
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Type])
                    Projectile.frame = 0;
            }

            if (Main.netMode != NetmodeID.Server)
            {
                Dust dust = Dust.NewDustPerfect(
                    Position: Projectile.Center,
                    Type: DustID.Electric,
                    Velocity: Vector2.Zero,
                    Alpha: 100,
                    newColor: Color.White,
                    Scale: 0.9f
                );

                dust.noGravity = true;
                dust.fadeIn = -1f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            int numDust = 20;

            for (int i = 0; i < numDust; i++)
            {
                Vector2 velocity = Vector2.One.RotatedBy(MathHelper.ToRadians(360 / numDust * i));
                Dust.NewDustPerfect(Projectile.Center, DustID.BlueTorch, velocity).noGravity = true;
            }
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
    }
}