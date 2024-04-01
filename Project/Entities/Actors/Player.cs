using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using Project.Components;

namespace Project.Entities.Actors
{
    public class Player : Actor
    {
        public override void Start()
        {
            size = new Point(20, 55);
            tag = "player";
            base.Start();

            AddComponent<MoveActorComponent>().SetVelocity(20f);
            AddComponent<InputMovementComponent>();
            AddComponent<DebugActorComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            Scene.Camera.Target = Position;
            base.Update(gameTime);
        }
    }
}
