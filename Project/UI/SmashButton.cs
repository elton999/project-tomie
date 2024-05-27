using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using UmbrellaToolsKit.Sound;

namespace Project.UI
{
    public class SmashButton : GameObject
    {
        private AsepriteAnimation _animation;
        private float _cooldown = 0.0f;
        [ShowEditor] private float _maxCooldown = 0.2f;
        [ShowEditor] private float _animationCooldownValue = 50.0f;

        private float _minProgressValue = 4f;
        private float _progress = 0.0f;

        private FMOD.Studio.EventInstance _typeEventInstance;

        public float Progress => _progress;

        public override void Start()
        {
            Sprite = Content.Load<Texture2D>(FilePath.SMASH_BUTTON_SPRITE_PATH);
            AsepriteDefinitions asepriteDefinitions = Content.Load<AsepriteDefinitions>(FilePath.SMASH_BUTTON_ATLAS_PATH);
            _animation = new AsepriteAnimation(asepriteDefinitions);
            _typeEventInstance = SoundManager.Instance.GetEventInstance(FSPRO.Event.UI_Click);
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            _animation.Play(gameTime, "tap", AsepriteAnimation.AnimationDirection.LOOP);
            float timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyBoardHandler.KeyPressed(Input.INTERACT))
            {
                _cooldown = Math.Min(_maxCooldown, _cooldown + _animationCooldownValue * timer);
                SetProgress(_minProgressValue * timer);
                _typeEventInstance.start();
            }
            else
                SetProgress(_animationCooldownValue * 0.01f * -timer);

            _cooldown = Math.Max(0, _cooldown - timer);

            Position = (Scene.Sizes.ToVector2() / 2.0f - Body.Size.ToVector2() / 2.0f).ToPoint().ToVector2();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_cooldown > 0.0f)
                Body = _animation.Body;
            else
                Body = _animation.AsepriteDefinitions.Bodys[0];
            spriteBatch.GraphicsDevice.Clear(Scene.BackgroundColor);
            base.Draw(spriteBatch);
        }

        private void SetProgress(float progress)
        {
            _progress = Math.Clamp(_progress + progress, 0.0f, 1.0f);
            System.Console.WriteLine(_progress);
        }

    }
}
