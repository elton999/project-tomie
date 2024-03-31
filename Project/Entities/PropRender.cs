using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace Project.Entities
{
    public class PropRender : GameObject
    {
        private AsepriteDefinitions _atlas;
        private string _sliceName;

        public override void Start()
        {
            _atlas = Scene.Content.Load<AsepriteDefinitions>(FilePath.TILE_MAP_ATLAS_PATH);
            Sprite = Scene.Content.Load<Texture2D>(FilePath.TILE_MAP_SPRITE_PATH);

            if (!_atlas.Slices.ContainsKey(_sliceName)) return;

            Body = _atlas.Slices[_sliceName].Item1;
            Origin = _atlas.Slices[_sliceName].Item2;
        }

        public void SetSpriteName(string name) => _sliceName = name;
    }
}
