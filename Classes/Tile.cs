using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class Tile : MovingObject
    {
        /* When neighbor is added to list, its origin direction is also added.
        If neighbor is added multiple times multiple origin directions are added. 
        When removing a wall, a random direction is chosen. */
        List<Tile> originTile = new List<Tile>(); 

        public enum TileType { Standard, Right, Bottom, Corner }

        protected bool beenChecked = false;
        protected TileDivider hDiv; //Top
        protected TileDivider vDiv; //Left
        protected TileDivider rDiv; //Right, only used for right side tiles
        protected TileDivider bDiv; //Bottom, only used for bottom tiles

        protected Tile[] neighbors; //0 = Top, 1 = Right, 2 = Left, 3 = Bottom

        /// <summary>
        /// Constructor, creates horizontal and vertical dividers in top and left positions
        /// </summary>
        /// <param name="tileTexture"></param>
        /// <param name="hDivTexture"></param>
        /// <param name="vDivTexture"></param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public Tile(Texture2D tileTexture, Texture2D hDivTexture, Texture2D vDivTexture, TileType tileType, double x_Pos, double y_Pos, double x_Speed, double y_Speed):base(tileTexture, x_Pos, y_Pos, x_Speed, y_Speed)
        {
            hDiv = new TileDivider(hDivTexture, x_Pos, y_Pos - 25, x_Speed, y_Speed);
            vDiv = new TileDivider(vDivTexture, x_Pos - 25, y_Pos, x_Speed, y_Speed);

            switch (tileType)
            {
                case TileType.Right:
                    rDiv = new TileDivider(vDivTexture, x_Pos + 275, y_Pos, x_Speed, y_Speed);
                    break;
                case TileType.Bottom:
                    bDiv = new TileDivider(hDivTexture, x_Pos, y_Pos + 275, x_Speed, y_Speed);
                    break;
                case TileType.Corner:
                    rDiv = new TileDivider(vDivTexture, x_Pos + 275, y_Pos, x_Speed, y_Speed);
                    bDiv = new TileDivider(hDivTexture, x_Pos, y_Pos + 275, x_Speed, y_Speed);
                    break;
            }
            
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

        /// <summary>
        /// Checked property, returns bool.
        /// </summary>
        public bool BeenChecked
        {
            get { return beenChecked; }
            set { beenChecked = value; }
        }

        /// <summary>
        /// Horizontal divider property, returns TileDivider
        /// </summary>
        public TileDivider HDiv
        {
            get { return hDiv; }
            set { hDiv = value; }
        }

        /// <summary>
        /// Vertical divider property, returns TileDivider
        /// </summary>
        public TileDivider VDiv
        {
            get { return vDiv; }
            set { vDiv = value; }
        }

        /// <summary>
        /// Right side divider property, returns TileDivider
        /// </summary>
        public TileDivider RDiv
        {
            get { return rDiv; }
            set { rDiv = value; }
        }

        /// <summary>
        /// Bottom divider, returns TileDivider
        /// </summary>
        public TileDivider BDiv
        {
            get { return bDiv; }
            set { bDiv = value; }
        }

        /// <summary>
        /// Neighbors list property, used in level generation by using linking a list. Contains up to four neighbors.
        /// </summary>
        public Tile[] Neighbors
        {
            get { return neighbors; }
            set { neighbors = value; }
        }

        /// <summary>
        /// OriginTile list property, used in level generation. Contains tile that added current tile to neighbor list
        /// </summary>
        public List<Tile> OriginTile
        {
            get { return originTile; }
            set { originTile = value; }
        }
    }
}
