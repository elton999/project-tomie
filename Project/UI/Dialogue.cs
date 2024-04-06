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

        private bool _isShowing = false;
        private bool _skip = false;
        [ShowEditor] private float _timeToWrite = 0.5f;

        private string _text;
        private string _formattedText;
        private string _renderText;

        public override void Start()
        {
            _size = new Point(200, 30);
            Position = new Vector2(30, 120);
            _font = Scene.Content.Load<SpriteFont>("Font");
            _font.LineSpacing = 7;
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (_isShowing && KeyBoardHandler.KeyPressed(Input.INTERACT))
                _skip = true;

            // if (KeyBoardHandler.KeyPressed(Input.INTERACT))
            //     Say("Hello World! it is just a little bit different than you think it is in the game world and you can see it in the game world by clicking on the button below and press the Enter key.");
            base.Update(gameTime);
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
            CoroutineManagement.StarCoroutine(WriteText());
        }

        public void Clear() => _isShowing = false;

        public IEnumerator WriteText()
        {
            for (int i = 0; i < _formattedText.Length; i++)
            {
                string currentLetter = _formattedText[i].ToString();
                _renderText += currentLetter;

                if (!_skip)
                    yield return CoroutineManagement.Wait(_timeToWrite);
            }

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
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(
                SpriteSortMode,
                BlendState,
                SamplerState,
                null,
                null,
                Effect,
                null
            );

            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (!_isShowing) return;
            spriteBatch.DrawString(_font, _renderText, Position, Color.White);
        }
        #endregion
    }
}
