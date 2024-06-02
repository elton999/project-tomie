using System;
using System.Collections;
using FMOD;
using FMOD.Studio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project.UI;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sound;

namespace Project.SoundEvent
{
    public class CutScene1 : GameObject
    {
        private SmashButton _smashButton;
        private CutScene _cutSceneSequence = new CutScene(FilePath.CUTSCENE_1_PATH);
        private FMOD.Studio.EventInstance _soundEventInstance;
        private GameObject _gameTitle;
        private bool _showBackground = false;

        private const string PITCH_PARAM = "pitch";
        private const string FINISHED_CUTSCENE_PARAM = "finished cutscene";

        public CutScene1(SmashButton button) => _smashButton = button;

        public override void Start()
        {
            _smashButton.OnReachMaxValueDelayed += HideButton;
            _soundEventInstance = SoundManager.Instance.GetEventInstance(FSPRO.Event.First_CutScene);
            _soundEventInstance.start();

            Scene.AddGameObject(_cutSceneSequence, Layers.UI);

            CreateTitleSprite();
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (_smashButton != null)
                _soundEventInstance.setParameterByName(PITCH_PARAM, _smashButton.Progress * 3.0f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_showBackground)
                spriteBatch.GraphicsDevice.Clear(Scene.BackgroundColor);
            base.Draw(spriteBatch);
        }

        private void HideButton()
        {
            _smashButton.OnReachMaxValueDelayed -= HideButton;
            _smashButton.Destroy();
            _smashButton = null;

            _cutSceneSequence.Play();
            _cutSceneSequence.OnFinish += OnFinishCutSceneSequence;

            _soundEventInstance.setParameterByName(PITCH_PARAM, 4.0f);
        }

        private void OnFinishCutSceneSequence()
        {
            _soundEventInstance.setParameterByName(FINISHED_CUTSCENE_PARAM, 1.0f);
            _soundEventInstance.setCallback(OnReachMark, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
            _showBackground = true;

            _cutSceneSequence.OnFinish -= OnFinishCutSceneSequence;
            _cutSceneSequence.Destroy();
            _cutSceneSequence = null;
        }

        #region title sequence
        private RESULT OnReachMark(EVENT_CALLBACK_TYPE type, IntPtr _event, IntPtr parameters)
        {
            UmbrellaToolsKit.EditorEngine.Log.Write("CutScene1 OnReachMark");
            ShowLogoTitle();

            return RESULT.OK;
        }

        private void ShowLogoTitle()
        {
            _gameTitle.Transparent = 1.0f;
            IEnumerator DestroyLogo()
            {
                yield return _gameTitle.CoroutineManagement.Wait(5000.0f);
                _gameTitle.Destroy();
                _gameTitle = null;
                _showBackground = false;
                yield return null;
            }

            _gameTitle.CoroutineManagement.StarCoroutine(DestroyLogo());
        }

        private void CreateTitleSprite()
        {
            _gameTitle = new GameObject();
            _gameTitle.Sprite = Content.Load<Texture2D>(FilePath.GAME_LOGO_SPRITE_PATH);
            _gameTitle.Transparent = 0.0f;
            Scene.AddGameObject(_gameTitle, Layers.UI);
        }
        #endregion
    }
}