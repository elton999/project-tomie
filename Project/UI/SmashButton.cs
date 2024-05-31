using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using UmbrellaToolsKit.Sound;
using System.Collections;

namespace Project.UI
{
    public class SmashButton : GameObject
    {
        private AsepriteAnimation _animation;
        private float _cooldown = 0.0f;
        [ShowEditor] private float _maxCooldown = 0.2f;
        [ShowEditor] private float _animationCooldownValue = 50.0f;

        [ShowEditor] private float _minProgressValue = 4f;
        [ShowEditor] private float _progress = 0.0f;

        [ShowEditor] private bool _alReadyReachedMaxValue = false;
        [ShowEditor] private float _delayToCallCallBack = 2000.0f;

        private FMOD.Studio.EventInstance _typeEventInstance;

        private Texture2D _circleTexture;
        private AsepriteDefinitions _circleDefinitions;
        private Rectangle _circleFilled => _circleDefinitions.Slices["filled_circle"].Item1;
        private Rectangle _circleUnfilled => _circleDefinitions.Slices["unfilled_circle"].Item1;
        private Vector2 _circlePosition => (Scene.Sizes.ToVector2() / 2.0f).ToPoint().ToVector2() - Vector2.UnitY * 30;

        public float Progress => _progress;

        public Action OnReachMaxValue;

        public override void Start()
        {
            Sprite = Content.Load<Texture2D>(FilePath.SMASH_BUTTON_SPRITE_PATH);
            AsepriteDefinitions asepriteDefinitions = Content.Load<AsepriteDefinitions>(FilePath.SMASH_BUTTON_ATLAS_PATH);
            _animation = new AsepriteAnimation(asepriteDefinitions);
            _typeEventInstance = SoundManager.Instance.GetEventInstance(FSPRO.Event.UI_Click);

            _circleTexture = Content.Load<Texture2D>(FilePath.TILE_MAP_SPRITE_PATH);
            _circleDefinitions = Content.Load<AsepriteDefinitions>(FilePath.TILE_MAP_ATLAS_PATH);

            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            _animation.Play(gameTime, "tap", AsepriteAnimation.AnimationDirection.LOOP);
            float timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (KeyBoardHandler.KeyPressed(Input.INTERACT) && !_alReadyReachedMaxValue)
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
            spriteBatch.GraphicsDevice.Clear(Scene.BackgroundColor);

            if (_cooldown > 0.0f) Body = _animation.Body;
            else Body = _animation.AsepriteDefinitions.Bodys[0];

            base.Draw(spriteBatch);
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            base.DrawSprite(spriteBatch);
            Vector2 originSprite = (_circleUnfilled.Size.ToVector2() / 2.0f).ToPoint().ToVector2();
            spriteBatch.Draw(_circleTexture, _circlePosition, _circleUnfilled, Color.White, 1f, originSprite, 1.0f, spriteEffect, 0);
            spriteBatch.Draw(_circleTexture, _circlePosition, _circleFilled, Color.White, 1f, originSprite, Progress, spriteEffect, 0);
        }

        private void SetProgress(float progress)
        {
            if (_alReadyReachedMaxValue) return;

            _progress = Math.Clamp(_progress + progress, 0.0f, 1.0f);

            if (_progress == 1.0f)
                CoroutineManagement.StarCoroutine(CallEventDelay());
        }

        private IEnumerator CallEventDelay()
        {
            _alReadyReachedMaxValue = true;
            yield return CoroutineManagement.Wait(_delayToCallCallBack);
            OnReachMaxValue?.Invoke();
            yield return null;
        }

    }
}
