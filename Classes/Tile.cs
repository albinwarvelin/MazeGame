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
        protected bool beenChecked = false;
        protected TileDivider hDiv;
        protected TileDivider vDiv;

        protected Tile[] neighbors;

        public Tile(Texture2D tileTexture, Texture2D hDivTexture, Texture2D vDivTexture, double x_Pos, double y_Pos, double x_Speed, double y_Speed):base(tileTexture, x_Pos, y_Pos, x_Speed, y_Speed)
        {
            hDiv = new TileDivider(hDivTexture, x_Pos, y_Pos - 25, x_Speed, y_Speed);
            vDiv = new TileDivider(vDivTexture, x_Pos - 25, y_Pos, x_Speed, y_Speed);
        }

        public void Update()
        {
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

        public Tile[] Neighbors
        {
            get { return neighbors; }
            set { neighbors = value; }
        }
    }
}
