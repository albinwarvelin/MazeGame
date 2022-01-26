using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
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

        /// <summary>
        /// Updates object, moves in given direcitons.
        /// </summary>
        /// <param name="toMove"></param>
        public virtual void Update(List<Level.Direction> toMove)
        {
            if (toMove.Contains(Level.Direction.Up))
            {
                position.Y -= speed.Y;
            }
            if (toMove.Contains(Level.Direction.Right))
            {
                position.X += speed.X;
            }
            if (toMove.Contains(Level.Direction.Left))
            {
                position.X -= speed.X;
            }
            if (toMove.Contains(Level.Direction.Down))
            {
                position.Y += speed.Y;
            }
        }
    }
}
