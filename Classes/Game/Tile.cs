using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class Tile : MovingObject, ISetSpeed
    {
        public enum TileType { Standard, Right, Bottom, Corner } //Used when determining if tile needs divider in bottom or right position

        /* Parameters, stored to use if tile is reset */
        private readonly Texture2D hDivTexture;
        private readonly Texture2D vDivTexture;
        private readonly TileType tileType;

        /* When neighbor is added to list, its origin direction is also added.
        If neighbor is added multiple times multiple origin directions are added. 
        When removing a wall, a random direction is chosen. */
        List<Tile> originTile = new List<Tile>(); 

        private bool beenChecked = false;
        private bool voidTile = false; //If tile is void it doesn't have any ground (texture), and player shall not be able to

        private TileDivider hDiv; //Top
        private TileDivider vDiv; //Left
        private TileDivider rDiv; //Right, only used for right side tiles
        private TileDivider bDiv; //Bottom, only used for bottom tiles

        protected Tile[] neighbors; //0 = Top, 1 = Right, 2 = Left, 3 = Bottom

        /// <summary>
        /// Constructor, assigns dividers to tile according to parameters. Stores parameters if needed for resetting tile.
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
            this.hDivTexture = hDivTexture;
            this.vDivTexture = vDivTexture;
            this.tileType = tileType;

            ResetDividers();
        }

        /// <summary>
        /// Resets all dividers to state specified by parameters in construction
        /// </summary>
        public void ResetDividers()
        {
            hDiv = new TileDivider(hDivTexture, position.X, position.Y - 25, speed.X, speed.Y);
            vDiv = new TileDivider(vDivTexture, position.X - 25, position.Y, speed.X, speed.Y);

            switch (tileType)
            {
                case TileType.Right:
                    rDiv = new TileDivider(vDivTexture, position.X + 275, position.Y, speed.X, speed.Y);
                    break;
                case TileType.Bottom:
                    bDiv = new TileDivider(hDivTexture, position.X, position.Y + 275, speed.X, speed.Y);
                    break;
                case TileType.Corner:
                    rDiv = new TileDivider(vDivTexture, position.X + 275, position.Y, speed.X, speed.Y);
                    bDiv = new TileDivider(hDivTexture, position.X, position.Y + 275, speed.X, speed.Y);
                    break;
            }
        }

        public void Update(List<Level.Direction> toMove) //toMove contains enums for direction level should move, opposite to player movement
        {
            if(toMove.Contains(Level.Direction.Up))
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

        public void SetSpeed(double x_Speed, double y_Speed)
        {
            speed.X = (float) x_Speed;
            speed.Y = (float) y_Speed;

            if(vDiv != null)
            {
                vDiv.SetSpeed(x_Speed, y_Speed);
            }
            if (hDiv != null)
            {
                hDiv.SetSpeed(x_Speed, y_Speed);
            }
            if (rDiv != null)
            {
                rDiv.SetSpeed(x_Speed, y_Speed);
            }
            if (bDiv != null)
            {
                bDiv.SetSpeed(x_Speed, y_Speed);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!voidTile)
            {
                base.Draw(spriteBatch);
            }
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
        /// Voidtile property, returns bool
        /// </summary>
        public bool VoidTile
        {
            get { return voidTile; }
            set { voidTile = value; }
        }

        /// <summary>
        /// Tile texture property, returns Texture2D.
        /// </summary>
        public Texture2D TileTexture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        /// <summary>
        /// Horizontal divider property, returns TileDivider.
        /// </summary>
        public TileDivider HDiv
        {
            get { return hDiv; }
            set { hDiv = value; }
        }

        /// <summary>
        /// Vertical divider property, returns TileDivider.
        /// </summary>
        public TileDivider VDiv
        {
            get { return vDiv; }
            set { vDiv = value; }
        }

        /// <summary>
        /// Right side divider property, returns TileDivider.
        /// </summary>
        public TileDivider RDiv
        {
            get { return rDiv; }
            set { rDiv = value; }
        }

        /// <summary>
        /// Bottom divider, returns TileDivider.
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
        /// OriginTile list property, used in level generation. Contains tile that added current tile to neighbor list.
        /// </summary>
        public List<Tile> OriginTile
        {
            get { return originTile; }
            set { originTile = value; }
        }
    }
}
