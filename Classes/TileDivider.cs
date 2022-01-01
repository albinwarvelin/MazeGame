using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace MazeGame.Classes
{
    class TileDivider : PhysicalObject
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

        public void Update(List<Player.LevelDirection> toMove)
        {
            if (toMove.Contains(Player.LevelDirection.Up))
            {
                position.Y -= speed.Y;
            }
            if (toMove.Contains(Player.LevelDirection.Right))
            {
                position.X += speed.X;
            }
            if (toMove.Contains(Player.LevelDirection.Left))
            {
                position.X -= speed.X;
            }
            if (toMove.Contains(Player.LevelDirection.Down))
            {
                position.Y += speed.Y;
            }
        }
    }
}
