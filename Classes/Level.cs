using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class Level : MovingObject
    {
        protected Tile[,] tiles; //Two dimensional array

        /// <summary>
        /// Generates new random level. Size shall not be less than 4.
        /// </summary>
        /// <param name="tileTexture"></param>
        /// <param name="dividerTexture"></param>
        /// <param name="size"></param>
        public Level(Texture2D tileTexture, Texture2D hDivTexture, Texture2D vDivTexture, int size, GameWindow window, double x_Speed, double y_Speed) : base(tileTexture, 0, 0, x_Speed, y_Speed) //Position Overwritten in level initialization
        {
            Random rnd = new Random(); //Used throughout method

            Vector2 startTile = new Vector2((int)(rnd.NextDouble() * 4 + (size / 2 - 2)), (int)(rnd.NextDouble() * 4 + (size / 2 - 2))); //Start tile position

            int x_Start = (int)((window.ClientBounds.Width / 2) + 150 - (startTile.X * 300));
            int y_Start = (int)((window.ClientBounds.Height / 2) + 150 - (startTile.Y * 300));
            tiles = new Tile[size, size];

            /* Assigns tiles to all values in tiles array */
            for (int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    tiles[y, x] = new Tile(tileTexture, hDivTexture, vDivTexture, x_Start + (x * 300), y_Start + (y * 300), 0, 0);
                }
            }

            /* Assigns neighbors to all tiles in tiles array */
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Tile[] temp = new Tile[4];
                    if(y == 0 && x == 0) //Top left corner
                    {
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if(y == 0 && x == size - 1) //Top right corner
                    {
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if(y == size - 1 && x == 0) //Bottom left corner
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                    }
                    else if(y == size - 1 && x == size - 1) //Bottom right corner
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[2] = tiles[y, x - 1]; //Left
                    }
                    else if(y == 0) //Top edge
                    {
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if(x == size - 1) //Right edge
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if(x == 0) //Left edge
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if(y == size - 1)
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[2] = tiles[y, x - 1]; //Left
                    }
                    else
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }


                    tiles[y, x].Neighbors = temp;
                }
            }

            /* Generates first tile and it's neighbors */
            List<Tile> neighbors = new List<Tile>();
            tiles[(int)startTile.Y, (int)startTile.X].BeenChecked = true;
            for (int i = 0; i < 4; i++)
            {
                Tile targetTile = tiles[(int)startTile.Y, (int)startTile.X];
                neighbors.Add(targetTile.Neighbors[i]);
                targetTile.Neighbors[i].OriginTile.Add(targetTile);
            }

            /* Random generation */
            while (neighbors.Count != 0)
            {
                Tile targetTile = neighbors[rnd.Next(0, neighbors.Count)];

                if(!targetTile.BeenChecked)
                {
                    Tile originTile = targetTile.OriginTile[rnd.Next(0, targetTile.OriginTile.Count)];

                    if (targetTile.Neighbors[0] == originTile) //Top
                    {
                        targetTile.HDiv = null; //Removes top divider of current tile
                    }
                    else if (targetTile.Neighbors[1] == originTile) //Right
                    {
                        originTile.VDiv = null; //Removes left divider of origin tile
                    }
                    else if (targetTile.Neighbors[2] == originTile) //Left
                    {
                        targetTile.VDiv = null; //Removes left divider of current tile
                    }
                    else if (targetTile.Neighbors[3] == originTile) //Bottom
                    {
                        originTile.HDiv = null; //Removes top divider of origin tile
                    }

                    for(int i = 0; i < 4; i++)
                    {
                        neighbors.Add(targetTile.Neighbors[i]); //Adds neighbors to list
                        targetTile.Neighbors[i].OriginTile.Add(targetTile); //Adds origin tile to neighbor
                    }

                    targetTile.BeenChecked = true;

                    neighbors.Remove(targetTile);
                }
            }
        }

        public void Update()
        {
            foreach(Tile tile in tiles)
            {
                tile.Update();
                tile.HDiv.Update();
                tile.VDiv.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(Tile tile in tiles)
            {
                tile.Draw(spriteBatch);
            }

            foreach(Tile tile in tiles)
            {
                if(tile.VDiv != null)
                {
                    tile.VDiv.Draw(spriteBatch);
                }
                if(tile.HDiv != null)
                {
                    tile.HDiv.Draw(spriteBatch);
                }
            }
        }
    }
}
