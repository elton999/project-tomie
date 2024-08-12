using Microsoft.Xna.Framework;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Components
{
    public class DebugActorComponent : Component
    {
        private Square _square;

        public override void Start()
        {
            _square = new Square
            {
                size = GameObject.size,
                SquareColor = Color.Blue,
                Position = GameObject.Position
            };
            GameObject.Scene.AddGameObject(_square, Layers.MIDDLEGROUND);
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            _square.Position = GameObject.Position;
            base.Update(deltaTime);
        }
    }
}
