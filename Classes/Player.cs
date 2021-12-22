using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class Player : MovingObject
    {
        public enum Direction { Up, Down, Left, Right }; //Used to return what direction level should move

        private List<TileDivider> surroundingDividers; //Updated every frame to contain current surrounding dividers

        public Player(Texture2D texture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(texture, x_Pos, y_Pos, x_Speed, y_Speed)
        {

        }

        public List<Direction> Update(GameWindow window) //Returns enum list for level-update to process
        {
            List<Direction> directions = new List<Direction>();

            KeyboardState keyboardInput = Keyboard.GetState();
            if (keyboardInput.IsKeyDown(Keys.A))
            {
                if (position.X > window.ClientBounds.Width * 0.20)
                {
                    position.X -= speed.X;
                }
                else
                {
                    directions.Add(Direction.Right); //Direction that level should move, inverted to player movement
                }
            }
            if (keyboardInput.IsKeyDown(Keys.W))
            {
                if (position.Y > window.ClientBounds.Height * 0.20)
                {
                    position.Y -= speed.Y;
                }
                else
                {
                    directions.Add(Direction.Down); //Direction that level should move, inverted to player movement
                }
            }
            if (keyboardInput.IsKeyDown(Keys.S))
            {
                if (position.Y < window.ClientBounds.Height * 0.80 - texture.Width)
                {
                    position.Y += speed.Y;
                }
                else
                {
                    directions.Add(Direction.Up); //Direction that level should move, inverted to player movement
                }
            }
            if (keyboardInput.IsKeyDown(Keys.D))
            {
                if (position.X < window.ClientBounds.Width * 0.80 - texture.Height)
                {
                    position.X += speed.X;
                }
                else
                {
                    directions.Add(Direction.Left); //Direction that level should move, inverted to player movement
                }
            }

            return directions;
        }

        public List<TileDivider> SurroundingDividers
        {
            set { surroundingDividers = value; }
        }
    }
}
