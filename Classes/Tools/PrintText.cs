using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeGame
{
    /// <summary>
    /// Used to print text in game.
    /// </summary>
    class PrintText
    {
        private readonly SpriteFont font;
        private Vector2 position;

        /// <summary>
        /// Creates new instance of printtext.
        /// </summary>
        /// <param name="font">Spritefont to use when printing text.</param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        public PrintText(SpriteFont font, int x_Pos, int y_Pos)
        {
            this.font = font;
            position = new Vector2(x_Pos, y_Pos);
        }

        /// <summary>
        /// Prints provided text.
        /// </summary>
        /// <param name="text">Text to print.</param>
        /// <param name="spriteBatch"></param>
        /// <param name="color">Color of text.</param>
        public void Print(string text, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(font, text, position, color);
        }

        /// <summary>
        /// X Position property.
        /// </summary>
        public int X_Pos
        {
            get { return (int)position.X; }
            set { position.X = value; }
        }
    }
}
