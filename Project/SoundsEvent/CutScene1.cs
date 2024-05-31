using Microsoft.Xna.Framework;
using Project.UI;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sound;

namespace Project.SoundEvent
{
    public class CutScene1 : GameObject
    {
        private SmashButton _smashButton;
        private FMOD.Studio.EventInstance _soundEventInstance;
        private bool _reachedMaxProgress = false;

        private const string PITCH_PARAM = "pitch";

        public CutScene1(SmashButton button) => _smashButton = button;

        public override void Start()
        {
            _smashButton.OnReachMaxValue += HideButton;
            _soundEventInstance = SoundManager.Instance.GetEventInstance(FSPRO.Event.First_CutScene);
            _soundEventInstance.start();
            base.Start();
        }

        public override void OnDestroy() => _smashButton.OnReachMaxValue -= HideButton;

        public override void Update(GameTime gameTime)
        {
            if (!_reachedMaxProgress)
                _soundEventInstance.setParameterByName(PITCH_PARAM, _smashButton.Progress * 3.0f);
        }

        private void HideButton()
        {
            _smashButton.OnReachMaxValue -= HideButton;
            _smashButton.Destroy();
            _smashButton = null;
        }
    }
}
