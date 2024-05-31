using Microsoft.Xna.Framework;
using UmbrellaToolsKit;

namespace Project
{
    public class SceneSettings : GameObject
    {
        public Color BackgroundColor => new Color(24.0f / 255.0f, 20.0f / 255.0f, 37.0f / 255.0f);

        public override void Start()
        {
            Tag = "SceneSettings";
            Scene.BackgroundColor = BackgroundColor;
        }
    }
}
