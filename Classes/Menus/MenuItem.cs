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
        private readonly string text;
        private PrintText printText;
        bool beenPressed = false;
        private Color color = Color.White;

        public MenuItem(Texture2D texture, SpriteFont font, string text, int x_Pos, int y_Pos):base(texture, x_Pos, y_Pos)
        {
            this.text = text;
            Vector2 textSize = font.MeasureString(text);
            printText = new PrintText(font,(int)(x_Pos + (texture.Width / 2) - (textSize.X / 2)),(int)(y_Pos + (texture.Height / 2) - (textSize.Y / 2)));
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
            printText.Print(text, spriteBatch, Color.Black); 
        }
    }
}
