﻿using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.SoundEvent;
using UmbrellaToolsKit.EditorEngine;
using UmbrellaToolsKit.EditorEngine.Windows.GameSettings;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetManagement _assetManagement;
        private GameManagement _gameManagement;
        private UmbrellaToolsKit.Sound.SoundManager _soundManager;
        private GameDebuggerSettings _gameDebugger;

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
            _gameDebugger = GameSettingsProperty.GetProperty<GameDebuggerSettings>(FilePath.GAME_DEBUGGER_PATH);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _soundManager = new UmbrellaToolsKit.Sound.SoundManager(FilePath.FMOD_BANK_PATH);

            _assetManagement = new AssetManagement();
            _assetManagement.Set<Entities.Actors.Player>("Player", Layers.PLAYER);
            _assetManagement.Set<Entities.Actors.PlayerHitBox>("Player", Layers.PLAYER);

            // UI Settings
            _assetManagement.Set<SceneSettings>("Player", Layers.UI);
            _assetManagement.Set<UI.Dialogue>("Player", Layers.UI);
            _assetManagement.Set<UI.InteractionMenu>("Player", Layers.UI);

            // Props
            _assetManagement.Set<Entities.Props.DoorProp>("Door", Layers.MIDDLEGROUND);
            _assetManagement.Set<Entities.Actors.InteractionHitBox>("Door", Layers.MIDDLEGROUND);
            _assetManagement.Set<Entities.Props.LockerProp>("Locker", Layers.MIDDLEGROUND);
            _assetManagement.Set<Entities.Props.WindowProp>("Window", Layers.MIDDLEGROUND);

            _gameManagement.SceneManagement.MainScene.SetLevelLdtk(0);

            if (_gameDebugger.showInitialCutScene)
            {
                var smashButton = new UI.SmashButton();
                _gameManagement.SceneManagement.MainScene.AddGameObject(smashButton, Layers.UI);
                _gameManagement.SceneManagement.MainScene.AddGameObject(new CutScene1(smashButton), Layers.UI);
            }

            var inputSettings = GameSettingsProperty.GetProperty<InputGameSettings>(FilePath.INPUT_SETTINGS_PATH);
            inputSettings.BindAllInputs();
        }

        protected override void UnloadContent()
        {
            _soundManager.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            _gameManagement.Update(gameTime);

            _soundManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _gameManagement.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
