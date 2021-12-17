using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MazeGame.Classes
{
    class TileDivider : MovingObject
    {
        /// <summary>
        /// Constructor, passes variables to Moving object
        /// </summary>
        /// <param name="dividerTexture"></param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public TileDivider(Texture2D dividerTexture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) :base(dividerTexture, x_Pos, y_Pos, x_Speed, y_Speed)
        {

        }

        public void Update()
        {
            /*==========Temporary controls==========*/
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
            /*======================================*/

            //Add logic to update when player moves
        }
    }
}
