using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit.Utils;
using UmbrellaToolsKit.ParticlesSystem;

namespace Project.UI
{
    public class SmashButtonParticleEfx : ParticlesSystem
    {
        public SmashButtonParticleEfx()
        {
            EmitsFor = TypeEmitter.FOR_TIME;
            ParticleVelocityAngle = -227.0f;
            ParticleAngleRotation = 90.0f;
            EmitterTime = 300.0f;
            ParticleRadiusSpawn = 5f;
            ParticleTransparent = 1f;
            MaxParticles = 700;
            ParticleMaxScale = 1.5f;
            ParticleVelocity = 120f;
            ParticleDecreaseScale = true;
            ParticleScaleSpeed = 3.0f / 10000.0f;
            ParticleLifeTime = 3000.0f;
        }

        public override void Start()
        {
            Sprites.Add(Content.Load<Texture2D>(FilePath.BUTTON_SMASH_FINISH_PARTICLE_1_SPRITE_PATH));
            Sprites.Add(Content.Load<Texture2D>(FilePath.BUTTON_SMASH_FINISH_PARTICLE_2_SPRITE_PATH));
            Sprites.Add(Content.Load<Texture2D>(FilePath.BUTTON_SMASH_FINISH_PARTICLE_3_SPRITE_PATH));
            Sprites.Add(Content.Load<Texture2D>(FilePath.BUTTON_SMASH_FINISH_PARTICLE_3_SPRITE_PATH));

            CheatListener.AddCheat(Keys.F2, Play);
            base.Start();
        }

        public override void OnDestroy() => CheatListener.RemoveCheat(Keys.F2);
    }
}
