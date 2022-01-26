using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    abstract class PhysicalObject : MovingObject
    {
        public PhysicalObject(Texture2D texture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) :base(texture, x_Pos, y_Pos, x_Speed, y_Speed)
        {

        }

        /// <summary>
        /// Checks collision with current object and parameter object. Returns boolean.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public virtual bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(texture.Width), Convert.ToInt32(texture.Height)); //Own object
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height)); //Other object

            return myRect.Intersects(otherRect);
        }
    }
}
