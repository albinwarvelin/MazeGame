using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class MenuItem : GameObject
    {
        public enum Alignment { Left, Mid }

        protected string text;
        private readonly SpriteFont font;
        protected PrintText printText;
        bool beenPressed = false;
        private Color color = Color.White;

        public MenuItem(Texture2D texture, SpriteFont font, string text, Alignment alignment, int x_Pos, int y_Pos) : base(texture, x_Pos, y_Pos)
        {
            this.text = text;
            this.font = font;

            Vector2 textSize;
            if (text != "")
            {
                textSize = font.MeasureString(text);
            }
            else
            {
                textSize = font.MeasureString("a");
            }

            if(alignment == Alignment.Left)
            {
                printText = new PrintText(font, (int)(x_Pos + 100), (int)(y_Pos + (texture.Height / 2) - (textSize.Y / 2)));
            }
            else
            {
                printText = new PrintText(font, (int)(x_Pos + (texture.Width / 2) - (textSize.X / 2)), (int)(y_Pos + (texture.Height / 2) - (textSize.Y / 2)));
            }
            
        }

        public bool CheckPress(MouseState mouseState)
        {
            if (mouseState.X > position.X && mouseState.Y > position.Y && mouseState.X < (position.X + texture.Width) && mouseState.Y < (position.Y + texture.Height))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    beenPressed = true;
                }
                if (mouseState.LeftButton == ButtonState.Released && beenPressed)
                {
                    beenPressed = false;
                    return true;
                }

                color = Color.LightSteelBlue;
            }
            else
            {
                beenPressed = false;
                color = Color.White;
            }

            return false;
        }

        public void ReCenterText()
        {
            Vector2 textSize;
            if (text != "")
            {
                textSize = font.MeasureString(text);
            }
            else
            {
                textSize = font.MeasureString("a");
            }

            printText.X_Pos = (int)(position.X + (texture.Width / 2) - (textSize.X / 2));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
            printText.Print(text, spriteBatch, Color.Black);
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
