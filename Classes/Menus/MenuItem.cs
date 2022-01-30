using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    /// <summary>
    /// Menuitem, used in all menus. Contains method for presschecking.
    /// </summary>
    class MenuItem : GameObject
    {
        public enum Alignment { Left, Mid }

        protected string text;
        private readonly SpriteFont font;
        protected PrintText printText;
        bool beenPressed = false;
        private Color color = Color.White;

        /// <summary>
        /// Creates new menuitem.
        /// </summary>
        /// <param name="texture">Background texture.</param>
        /// <param name="font">Text font.</param>
        /// <param name="text">Text to be displayed on top of background.</param>
        /// <param name="alignment">Text alignment, left or middle.</param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        public MenuItem(Texture2D texture, SpriteFont font, string text, Alignment alignment, int x_Pos, int y_Pos) : base(texture, x_Pos, y_Pos)
        {
            this.text = text;
            this.font = font;

            /* Measures width and height of text to align it correctly */
            Vector2 textSize;
            if (text != "")
            {
                textSize = font.MeasureString(text);
            }
            else
            {
                textSize = font.MeasureString("a");
            }

            if (alignment == Alignment.Left)
            {
                printText = new PrintText(font, (int)(x_Pos + 100), (int)(y_Pos + (texture.Height / 2) - (textSize.Y / 2)));
            }
            else
            {
                printText = new PrintText(font, (int)(x_Pos + (texture.Width / 2) - (textSize.X / 2)), (int)(y_Pos + (texture.Height / 2) - (textSize.Y / 2)));
            }

        }

        /// <summary>
        /// Checks if menuitem is pressed on, paints it light blue if mouse hovers on it and returns boolean if press is released ontop of item.
        /// </summary>
        /// <param name="mouseState">Mousestate to check for.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Recentes text in middle, only for mid alignment.
        /// </summary>
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

        /// <summary>
        /// Draws menuitem by drawing text on top of background.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
            printText.Print(text, spriteBatch, Color.Black);
        }

        /// <summary>
        /// Text property.
        /// </summary>
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
