using System.Collections;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project.UI
{
    public class Dialogue : GameObject
    {
        private SpriteFont _font;
        private Point _size;

        protected bool _isShowing = false;
        protected bool _skipTextAnimation = false;
        [ShowEditor] private float _timeToWrite = 0.5f;

        private string _text;
        private string _formattedText;
        private string _renderText;

        protected bool _isAnimationDone = false;

        public bool IsShowing => _isShowing;

        public override void Start()
        {
            _size = new Point(200, 30);
            Position = new Vector2(30, 112);
            _font = Scene.Content.Load<SpriteFont>("Font");
            _font.LineSpacing = 7;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            if (_isShowing && KeyBoardHandler.KeyPressed(Input.INTERACT))
                _skipTextAnimation = true;

            // if (KeyBoardHandler.KeyPressed(Input.INTERACT))
            //     Say("Hello World! it is just a little bit different than you think it is in the game world and you can see it in the game world by clicking on the button below and press the Enter key.");
            base.Update(deltaTime);
        }

        public void Say(string text)
        {
            _isShowing = true;
            _text = text;
            _formattedText = "";
            _renderText = "";

            _text = _text.Trim();
            _text = _text.Replace("\n", "");
            FormatText();

            CoroutineManagement.ClearCoroutines();
            CoroutineManagement.StarCoroutine(WriteTextAnimation());
        }

        public void Clear()
        {
            _isShowing = false;
            _skipTextAnimation = false;
            _isAnimationDone = false;
        }

        public IEnumerator WriteTextAnimation()
        {
            for (int letterIndex = 0; letterIndex < _formattedText.Length; letterIndex++)
            {
                string currentLetter = _formattedText[letterIndex].ToString();
                _renderText += currentLetter;

                if (!_skipTextAnimation)
                    yield return CoroutineManagement.Wait(_timeToWrite);
            }
            _isAnimationDone = true;
            yield return null;
        }

        private void FormatText()
        {
            int lineSize = 0;
            foreach (string word in _text.Split(" "))
            {
                string wordFormatted = word + " ";
                var size = _font.MeasureString(wordFormatted);
                if (lineSize + size.X > _size.X)
                {
                    wordFormatted = $"\n{wordFormatted}";
                    lineSize = 0;
                }
                _formattedText += wordFormatted;
                lineSize += (int)size.X;
            }
        }

        #region Drawing
        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (!_isShowing) return;
            spriteBatch.DrawString(_font, _renderText, Position, Color.White);
        }
        #endregion
    }
}
