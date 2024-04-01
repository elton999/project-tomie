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
            _square = new Square();
            _square.size = GameObject.size;
            _square.SquareColor = Color.Blue;
            _square.Position = GameObject.Position;
            GameObject.Scene.AddGameObject(_square, Layers.MIDDLEGROUND);
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            _square.Position = GameObject.Position;
            base.Update(gameTime);
        }
    }
}
