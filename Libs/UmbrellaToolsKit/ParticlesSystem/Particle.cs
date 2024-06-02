using Microsoft.Xna.Framework;

namespace UmbrellaToolsKit.ParticlesSystem
{
    public class Particle : GameObject
    {
        public float LifeTime = 10000f;
        public Vector2 Velocity = new Vector2(0, 1);
        public float Angle = 0;
        public bool DecreaseScale = false;
        public float DecreaseScaleSpeed = 10.0f;

        public override void Start()
        {
            Origin = -(new Vector2(Sprite.Width, Sprite.Height) / 2.0f);
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            LifeTime -= deltaTime;
            Rotation += Angle * deltaTime;
            Position += Velocity * deltaTime;
            if (DecreaseScale) Scale = MathHelper.Max(Scale - deltaTime * DecreaseScaleSpeed, 0.0f);

            Origin = Vector2.One / 2f;
        }
    }
}