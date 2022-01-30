using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeGame
{
    /// <summary>
    /// Abstract physical object, used in cases where object needs to be collission checked.
    /// </summary>
    abstract class PhysicalObject : MovingObject
    {
        /// <summary>
        /// Creates physicalobject, passes variables to baseclasses.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public PhysicalObject(Texture2D texture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(texture, x_Pos, y_Pos, x_Speed, y_Speed)
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
