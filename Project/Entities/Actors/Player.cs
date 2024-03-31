using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit.Sprite;

namespace Project.Entities.Actors
{
    public class Player : Square
    {
        public override void Start()
        {
            size = new Point(20, 55);
            SquareColor = Color.Blue;
            tag = "player";
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            Scene.Camera.Position = Position;
            base.Update(gameTime);
        }
    }
}
