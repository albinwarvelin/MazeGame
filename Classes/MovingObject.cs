using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    /// <summary>
    /// MovingObject is a subclass of GameObject, used for all moving objects in game. Contains a 2d speed vector.
    /// </summary>
    abstract class MovingObject : GameObject
    {
        protected Vector2 speed;

        /// <summary>
        /// Constructor, assigns values to speed vector and passes rest to GameObject.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public MovingObject(Texture2D texture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(texture, x_Pos, y_Pos)
        {
            speed.X = (float)x_Speed;
            speed.Y = (float)y_Speed;
        }
    }
}
