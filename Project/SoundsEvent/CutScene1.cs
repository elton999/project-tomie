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

        public CutScene1(SmashButton button) => _smashButton = button;

        public override void Start()
        {
            _soundEventInstance = SoundManager.Instance.GetEventInstance(FSPRO.Event.First_CutScene);
            _soundEventInstance.start();
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (_smashButton != null)
            {
                _soundEventInstance.setParameterByName("pitch", _smashButton.Progress * 3.5f);
                if (_smashButton.Progress * 3.5f >= 3.0f)
                {
                    System.Console.WriteLine("removed");
                    _smashButton.Destroy();
                    _smashButton = null;
                }
            }
            base.Update(gameTime);
        }
    }
}
