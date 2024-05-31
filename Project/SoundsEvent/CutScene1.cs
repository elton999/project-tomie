using Microsoft.Xna.Framework;
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

        private const string PITCH_PARAM = "pitch";

        public CutScene1(SmashButton button) => _smashButton = button;

        public override void Start()
        {
            _smashButton.OnReachMaxValue += HideButton;
            _soundEventInstance = SoundManager.Instance.GetEventInstance(FSPRO.Event.First_CutScene);
            _soundEventInstance.start();
            Scene.AddGameObject(_cutSceneSequence, Layers.UI);
            base.Start();
        }

        public override void OnDestroy()
        {
            _cutSceneSequence.Destroy();
            _smashButton.OnReachMaxValue -= HideButton;
        }
        public override void Update(GameTime gameTime)
        {
            _soundEventInstance.setParameterByName(PITCH_PARAM, _smashButton.Progress * 3.5f);
        }

        private void HideButton()
        {
            _smashButton.OnReachMaxValue -= HideButton;
            _smashButton.Destroy();
            _smashButton = null;

            _cutSceneSequence.Play();
        }
    }
}
