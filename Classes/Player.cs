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

        public Player(Texture2D texture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(texture, x_Pos, y_Pos, x_Speed, y_Speed)
        {

        }

        public Direction[] Update(GameWindow window, TileDivider[] toCheck) //Returns enum list for level-update to process
        {
            KeyboardState keyboardInput = Keyboard.GetState();
            if (keyboardInput.IsKeyDown(Keys.A))
            {
                position.X += speed.X;
            }
            if (keyboardInput.IsKeyDown(Keys.W))
            {
                position.Y += speed.Y;
            }
            if (keyboardInput.IsKeyDown(Keys.S))
            {
                position.Y -= speed.Y;
            }
            if (keyboardInput.IsKeyDown(Keys.D))
            {
                position.X -= speed.X;
            }
        }
    }
}
