using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MazeGame
{
    /// <summary>
    /// Tile divider class, used for dividers in level.
    /// </summary>
    class TileDivider : PhysicalObject, ISetSpeed
    {
        /// <summary>
        /// Creates new TileDivider, passes parameters to Physicalobject.
        /// </summary>
        /// <param name="dividerTexture">Texture of object.</param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public TileDivider(Texture2D dividerTexture, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(dividerTexture, x_Pos, y_Pos, x_Speed, y_Speed)
        {

        }

        /// <summary>
        /// Updates object, moves in given direcitons.
        /// </summary>
        /// <param name="toMove">List of directions to move. Moves one speed unit in each given direction.</param>
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

        /// <summary>
        /// Sets speed of TileDivider.
        /// </summary>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public void SetSpeed(double x_Speed, double y_Speed)
        {
            speed.X = (float)x_Speed;
            speed.Y = (float)y_Speed;
        }
    }
}
