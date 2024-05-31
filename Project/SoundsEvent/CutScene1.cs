using System.Collections;
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

        public CutScene1(SmashButton button) => _smashButton = button;

        public override void Start()
        {
            _soundEventInstance = SoundManager.Instance.GetEventInstance(FSPRO.Event.First_CutScene);
            _soundEventInstance.start();
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (!_reachedMaxProgress)
            {
                _soundEventInstance.setParameterByName("pitch", _smashButton.Progress * 3.0f);
                if (_smashButton.Progress >= 1.0f && !_reachedMaxProgress)
                    CoroutineManagement.StarCoroutine(HideButtonScreen());
            }
            base.Update(gameTime);
        }

        public IEnumerator HideButtonScreen()
        {
            _reachedMaxProgress = true;
            yield return CoroutineManagement.Wait(2000.0f);
            _smashButton.Destroy();
            _smashButton = null;
            yield return null;
        }
    }
}
