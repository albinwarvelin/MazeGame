using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class Player : PhysicalObject
    {
        public enum Direction { Up, Down, Left, Right }; //Used to return what direction level should move

        private List<TileDivider> surroundingDividers; //Updated every frame to contain current surrounding dividers

        public Player(Texture2D texture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(texture, x_Pos, y_Pos, x_Speed, y_Speed)
        {

        }

        /// <summary>
        /// Updates player, returns directions for level to process
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public List<Direction> Update(GameWindow window)
        {
            List<Direction> directions = new List<Direction>();

            KeyboardState keyboardInput = Keyboard.GetState();
            if (keyboardInput.IsKeyDown(Keys.A) && !keyboardInput.IsKeyDown(Keys.D))
            {
                position.X -= speed.X;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if(collider != null)
                {
                    position.X = (float)(collider.X_Pos + collider.Width); //Reverse effect of position change
                }
                else
                {
                    if (position.X < window.ClientBounds.Width * 0.20)
                    {
                        position.X += speed.X;
                        directions.Add(Direction.Right); //Direction that level should move, inverted to player movement
                    }
                }
            }
            if (keyboardInput.IsKeyDown(Keys.W) && !keyboardInput.IsKeyDown(Keys.S))
            {
                position.Y -= speed.Y;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider != null)
                {
                    position.Y = (float)(collider.Y_Pos + collider.Height);
                }
                else
                {
                    if (position.Y < window.ClientBounds.Height * 0.30)
                    {
                        position.Y += speed.Y;
                        directions.Add(Direction.Down); //Direction that level should move, inverted to player movement
                    }
                }
            }
            if (keyboardInput.IsKeyDown(Keys.S) && !keyboardInput.IsKeyDown(Keys.W))
            {
                position.Y += speed.Y;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider != null)
                {
                    position.Y = (float)(collider.Y_Pos - texture.Height);
                }
                else
                {
                    if (position.Y > window.ClientBounds.Height * 0.70 - texture.Width)
                    {
                        position.Y -= speed.Y;
                        directions.Add(Direction.Up); //Direction that level should move, inverted to player movement
                    }
                }
            }
            if (keyboardInput.IsKeyDown(Keys.D) && !keyboardInput.IsKeyDown(Keys.A))
            {
                position.X += speed.X;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider != null)
                {
                    position.X = (float)(collider.X_Pos - texture.Width); //Reverse effect of position change
                }
                else
                {
                    if (position.X > window.ClientBounds.Width * 0.80 - texture.Height)
                    {
                        position.X -= speed.X;
                        directions.Add(Direction.Left); //Direction that level should move, inverted to player movement
                    }
                }
            }

            return directions;
        }

        /// <summary>
        /// Property for surrounding dividers, used to optimally check if player collides with barrier
        /// </summary>
        public List<TileDivider> SurroundingDividers
        {
            set { surroundingDividers = value; }
        }
    }
}
