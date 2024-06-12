using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using UmbrellaToolsKit.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.UI
{
    public class InteractionMenu : MenuBase
    {
        public override void Start()
        {
            Font = Scene.Content.Load<SpriteFont>(FilePath.FONT_PATH);
            Sprite = Scene.Content.Load<Texture2D>(FilePath.TILE_MAP_SPRITE_PATH);
            var atlas = Scene.Content.Load<AsepriteDefinitions>(FilePath.TILE_MAP_ATLAS_PATH);

            CoordOnDefault = atlas.Slices["default_button"].Item1;
            CoordOnSelect = atlas.Slices["selected_button"].Item1;

            SpaceBetweenButtons = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsShowingMenu) return;

            if (KeyBoardHandler.KeyPressed(Input.INTERACT))
                ClickOnSelectedButton();

            if (KeyBoardHandler.KeyPressed(Input.UP))
                SelectPreviousButton();

            if (KeyBoardHandler.KeyPressed(Input.DOWN))
                SelectNextButton();

            if (KeyBoardHandler.KeyPressed(Input.CANCEL))
                HideMenu();

        }
    }
}
