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
            EmitsFor = ParticlesSystem.TypeEmitter.FOR_TIME;
            ParticleVelocityAngle = -227.0f;
            ParticleAngleRotation = 180.0f;
            EmitterTime = 20.0f;
            ParticleRadiusSpawn = 20f;
            ParticleTransparent = 1f;
            MaxParticles = 200;
            ParticleMaxScale = 2f;
            ParticleVelocity = 50f;
        }

        public override void Start()
        {
            Sprite = new Texture2D(Scene.ScreenGraphicsDevice, 1, 1);
            Sprite.SetData(new Color[1] { Color.White });
            Sprites.Add(Sprite);

            CheatListener.AddCheat(Keys.F2, Play);
            base.Start();
        }

        public override void OnDestroy()
        {
            CheatListener.RemoveCheat(Keys.F2);
        }
    }
}
