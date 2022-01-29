using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class Background : MovingObject
    {
        private readonly int x_Size;
        private readonly int y_Size;

        /// <summary>
        /// Creates a background that automatically scales to window size. Background can be moved
        /// </summary>
        /// <param name="window"></param>
        /// <param name="texture"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public Background(GameWindow window, Texture2D texture, double x_Speed, double y_Speed) : base(texture, 0, 0, x_Speed, y_Speed)
        {
            x_Size = (window.ClientBounds.Width / texture.Width) + 2;
            y_Size = (window.ClientBounds.Height / texture.Height) + 2;
        }

        /// <summary>
        /// Updates and moves background according to enums provided.
        /// </summary>
        /// <param name="directions"></param>
        public void Update(List<Level.Direction> directions)
        {
            foreach(Level.Direction direction in directions)
            {
                switch(direction)
                {
                    case Level.Direction.Left:
                        position.X -= speed.X;
                        break;
                    case Level.Direction.Right:
                        position.X += speed.X;
                        break;
                    case Level.Direction.Up:
                        position.Y -= speed.Y;
                        break;
                    case Level.Direction.Down:
                        position.Y += speed.Y;
                        break;
                }
            }

            if(position.X < -texture.Width)
            {
                position.X += texture.Width;
            }
            else if (position.X > 0)
            {
                position.X -= texture.Width;
            }

            if (position.Y < -texture.Height)
            {
                position.Y += texture.Height;
            }
            else if (position.Y > 0)
            {
                position.Y -= texture.Height;
            }
        }

        /// <summary>
        /// Draws background.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < x_Size; x++)
            {
                for(int y = 0; y < y_Size; y++)
                {
                    Vector2 texturePos = new Vector2(position.X + (x * texture.Width), position.Y + (y * texture.Height));

                    spriteBatch.Draw(texture, texturePos, Color.White);
                }
            }
        }
    }
}
