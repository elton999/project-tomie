using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace Project.UI
{
    public class MenuBase : GameObject
    {
        public struct Button
        {
            public string Name;
            public Color Color;

            public Action OnClick;
            public Action OnSelected;

            public Vector2 Position;
            public Vector2 TextPosition;

            public Rectangle CoordOnDefault;
            public Rectangle CoordOnSelect;
        }

        private int _selectIndex = 0;

        public List<Button> Buttons = new List<Button>();
        public SpriteFont Font;

        public Rectangle CoordOnDefault;
        public Rectangle CoordOnSelect;

        public Vector2 SpaceBetweenButtons = Vector2.UnitY;
        public Vector2 ButtonPadding = Vector2.One;

        public bool TextToCenter = true;
        public Color DefaultTextColor = Color.White;

        public void SelectNextButton()
        {
            _selectIndex++;
            _selectIndex %= Buttons.Count;
            SelectButton(_selectIndex);
        }

        public void SelectPreviousButton()
        {
            _selectIndex--;
            if (_selectIndex < 0) _selectIndex = Buttons.Count - 1;
            _selectIndex %= Buttons.Count;
            SelectButton(_selectIndex);
        }

        public void SelectButton(int index)
        {
            if (index < 0 || index >= Buttons.Count) return;

            Button button = Buttons[index];
            button.OnSelected?.Invoke();

            _selectIndex = index;
        }

        public void ClickOnSelectedButton()
        {
            Button button = Buttons[_selectIndex];
            button.OnClick?.Invoke();
        }

        public void AddButton(string name, Action onClick, Action onSelected)
        {
            Vector2 buttonSize = Vector2.One;
            Vector2 textSize = Font.MeasureString(name);

            if (CoordOnDefault != null)
                buttonSize = CoordOnDefault.Size.ToVector2();

            if (CoordOnDefault == null)
                buttonSize = textSize;

            var button = new Button()
            {
                Name = name,
                Color = DefaultTextColor,

                OnClick = onClick,
                OnSelected = onSelected,

                Position = ((buttonSize * Vector2.UnitY + SpaceBetweenButtons) * Buttons.Count).ToPoint().ToVector2(),

                TextPosition = (buttonSize * Vector2.UnitY + SpaceBetweenButtons) * Buttons.Count + (buttonSize / 2.0f - textSize / 2.0f).ToPoint().ToVector2(),

                CoordOnDefault = this.CoordOnDefault,
                CoordOnSelect = this.CoordOnSelect,
            };

            Buttons.Add(button);
        }

        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                var button = Buttons[i];
                bool isSelected = i == _selectIndex;

                if (Sprite != null && button.CoordOnDefault != null)
                    spriteBatch.Draw(Sprite, Position + button.Position, isSelected ? button.CoordOnSelect : button.CoordOnDefault, SpriteColor);

                spriteBatch.DrawString(Font, button.Name, Position + button.TextPosition, button.Color);
            }
        }

    }
}
