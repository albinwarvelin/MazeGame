using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeGame
{
    /// <summary>
    /// GameObject is the baseclass for all visible objects in game. Contains texture variable and 2d position vector.
    /// </summary>
    abstract class GameObject
    {
        protected Texture2D texture;
        protected Vector2 position;

        /// <summary>
        /// Constructor, assigns values to class variables.
        /// Position 0,0 is in top left corner.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        public GameObject(Texture2D texture, double x_Pos, double y_Pos)
        {
            this.texture = texture;
            position.X = (float)x_Pos;
            position.Y = (float)y_Pos;
        }

        /// <summary>
        /// Draws gameObject in position.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Draws gameObject in position, overload is used in cases when windowborders are needed to optimize game.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="window"></param>
        public virtual void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// Texture2D property.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        /// <summary>
        /// X Position property.
        /// </summary>
        public double X_Pos { get { return position.X; } }

        /// <summary>
        /// Y Position property.
        /// </summary>
        public double Y_Pos { get { return position.Y; } }

        /// <summary>
        /// Texture width property.
        /// </summary>
        public double Width { get { return texture.Width; } }

        /// <summary>
        /// Texture height property.
        /// </summary>
        public double Height { get { return texture.Height; } }
    }
}
