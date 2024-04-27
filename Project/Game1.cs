﻿using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetManagement _assetManagement;
        private GameManagement _gameManagement;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameManagement = new GameManagement(this);
            _gameManagement.Game = this;
            _gameManagement.Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetManagement = new AssetManagement();
            _assetManagement.Set<Entities.Actors.Player>("Player", Layers.PLAYER);

            // UI Settings
            _assetManagement.Set<UI.Dialogue>("Player", Layers.UI);
            _assetManagement.Set<UI.InteractionMenu>("Player", Layers.UI);

            // Props
            _assetManagement.Set<Entities.Props.DoorProp>("Door", Layers.MIDDLEGROUND);
            _assetManagement.Set<Entities.Actors.InteractionHitBox>("Door", Layers.MIDDLEGROUND);
            _assetManagement.Set<Entities.Props.LockerProp>("Locker", Layers.MIDDLEGROUND);
            _assetManagement.Set<Entities.Props.WindowProp>("Window", Layers.MIDDLEGROUND);

            _gameManagement.SceneManagement.MainScene.SetLevelLdtk(0);

            // Inputs
            KeyBoardHandler.AddInput(Input.EXIT, Keys.Escape);
            // moviment
            KeyBoardHandler.AddInput(Input.LEFT, Keys.Left);
            KeyBoardHandler.AddInput(Input.RIGHT, Keys.Right);
            // UI inputs
            KeyBoardHandler.AddInput(Input.UP, Keys.Up);
            KeyBoardHandler.AddInput(Input.DOWN, Keys.Down);

            KeyBoardHandler.AddInput(Input.INTERACT, Keys.Space);
        }

        protected override void Update(GameTime gameTime)
        {
            if (KeyBoardHandler.KeyPressed(Input.EXIT))
                Exit();

            _gameManagement.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _gameManagement.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
