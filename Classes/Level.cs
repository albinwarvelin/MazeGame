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
        protected int size;
        

        /// <summary>
        /// Generates new random level. Size shall not be less than 4.
        /// </summary>
        /// <param name="tileTextures"></param>
        /// <param name="hDivTextures"></param>
        /// <param name="vDivTexture"></param>
        /// <param name="size"></param>
        /// <param name="window"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public Level(Texture2D[] tileTextures, Texture2D[] hDivTextures, Texture2D[] vDivTexture, int size, GameWindow window, double x_Speed, double y_Speed) : base(null, 0, 0, x_Speed, y_Speed) //Level position not used, each tile has its own position
        {
            this.size = size;
            Random rnd = new Random(); //Used throughout method

            Vector2 startTilePos = new Vector2((int)(rnd.NextDouble() * 4 + (size / 2 - 2)), (int)(rnd.NextDouble() * 4 + (size / 2 - 2))); //Start tile position

            int x_Start = (int)((window.ClientBounds.Width / 2) + 150 - (startTilePos.X * 300));
            int y_Start = (int)((window.ClientBounds.Height / 2) + 150 - (startTilePos.Y * 300));
            tiles = new Tile[size, size];

            /* Assigns tiles to all values in tiles array */
            for (int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    if(y == size - 1 && x == size - 1) //Bottom corner tile
                    {
                        tiles[y, x] = new Tile(tileTextures[rnd.Next(0, tileTextures.Length)], hDivTextures[rnd.Next(0, hDivTextures.Length)], vDivTexture[rnd.Next(0, vDivTexture.Length)], Tile.TileType.Corner, x_Start + (x * 300), y_Start + (y * 300), x_Speed, y_Speed);
                    }
                    else if (x == size - 1)
                    {
                        tiles[y, x] = new Tile(tileTextures[rnd.Next(0, tileTextures.Length)], hDivTextures[rnd.Next(0, hDivTextures.Length)], vDivTexture[rnd.Next(0, vDivTexture.Length)], Tile.TileType.Right, x_Start + (x * 300), y_Start + (y * 300), x_Speed, y_Speed);
                    }
                    else if (y == size - 1)
                    {
                        tiles[y, x] = new Tile(tileTextures[rnd.Next(0, tileTextures.Length)], hDivTextures[rnd.Next(0, hDivTextures.Length)], vDivTexture[rnd.Next(0, vDivTexture.Length)], Tile.TileType.Bottom, x_Start + (x * 300), y_Start + (y * 300), x_Speed, y_Speed);
                    }
                    else
                    {
                        tiles[y, x] = new Tile(tileTextures[rnd.Next(0, tileTextures.Length)], hDivTextures[rnd.Next(0, hDivTextures.Length)], vDivTexture[rnd.Next(0, vDivTexture.Length)], Tile.TileType.Standard, x_Start + (x * 300), y_Start + (y * 300), x_Speed, y_Speed);
                    }
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
            List<Tile> straightNeighbors = new List<Tile>();
            List<Tile> rightNeighbors = new List<Tile>();
            List<Tile> leftNeighbors = new List<Tile>();
            double straightChance = 0.10;
            double leftChance = 0.45;

            Tile startTile = tiles[(int)startTilePos.Y, (int)startTilePos.X];
            startTile.BeenChecked = true;

            straightNeighbors.Add(startTile.Neighbors[0]);
            straightNeighbors.Add(startTile.Neighbors[3]); //Put in straight neighbors, only "back-neighbor" to be added to list
            rightNeighbors.Add(startTile.Neighbors[1]);
            leftNeighbors.Add(startTile.Neighbors[2]);

            for (int i = 0; i < 4; i++)
            {
                startTile.Neighbors[i].OriginTile.Add(startTile);
            }

            /* Random generation */
            while (straightNeighbors.Count + rightNeighbors.Count + leftNeighbors.Count != 0)
            {
                double rndDouble = rnd.NextDouble();
                Tile targetTile;

                if (rndDouble < straightChance)
                {
                    if (straightNeighbors.Count != 0)
                    {
                        targetTile = straightNeighbors[rnd.Next(0, straightNeighbors.Count)];
                        straightNeighbors.Remove(targetTile);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (rndDouble < straightChance + leftChance)
                {
                    if (leftNeighbors.Count != 0)
                    {
                        targetTile = leftNeighbors[rnd.Next(0, leftNeighbors.Count)];
                        leftNeighbors.Remove(targetTile);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (rightNeighbors.Count != 0)
                    {
                        targetTile = rightNeighbors[rnd.Next(0, rightNeighbors.Count)];
                        rightNeighbors.Remove(targetTile);
                    }
                    else
                    {
                        continue;
                    }
                }

                if (!targetTile.BeenChecked)
                {
                    Tile originTile = targetTile.OriginTile[rnd.Next(0, targetTile.OriginTile.Count)];

                    if (targetTile.Neighbors[0] == originTile) //Top
                    {
                        targetTile.HDiv = null; //Removes top divider of current tile

                        if (targetTile.Neighbors[3] != null) //Adds straight neighbor
                        {
                            straightNeighbors.Add(targetTile.Neighbors[3]);
                        }
                        if (targetTile.Neighbors[1] != null) //Adds left neighbor
                        {
                            leftNeighbors.Add(targetTile.Neighbors[1]);
                        }
                        if (targetTile.Neighbors[2] != null) //Adds right neighbor
                        {
                            rightNeighbors.Add(targetTile.Neighbors[2]);
                        }
                    }
                    else if (targetTile.Neighbors[1] == originTile) //Right
                    {
                        originTile.VDiv = null; //Removes left divider of origin tile

                        if (targetTile.Neighbors[2] != null) //Adds straight neighbor
                        {
                            straightNeighbors.Add(targetTile.Neighbors[2]);
                        }
                        if (targetTile.Neighbors[3] != null) //Adds left neighbor
                        {
                            leftNeighbors.Add(targetTile.Neighbors[3]);
                        }
                        if (targetTile.Neighbors[0] != null) //Adds right neighbor
                        {
                            rightNeighbors.Add(targetTile.Neighbors[0]);
                        }
                    }
                    else if (targetTile.Neighbors[2] == originTile) //Left
                    {
                        targetTile.VDiv = null; //Removes left divider of current tile

                        if (targetTile.Neighbors[1] != null) //Adds straight neighbor
                        {
                            straightNeighbors.Add(targetTile.Neighbors[1]);
                        }
                        if (targetTile.Neighbors[0] != null) //Adds left neighbor
                        {
                            leftNeighbors.Add(targetTile.Neighbors[0]);
                        }
                        if (targetTile.Neighbors[3] != null) //Adds right neighbor
                        {
                            rightNeighbors.Add(targetTile.Neighbors[3]);
                        }
                    }
                    else if (targetTile.Neighbors[3] == originTile) //Bottom
                    {
                        originTile.HDiv = null; //Removes top divider of origin tile

                        if (targetTile.Neighbors[0] != null) //Adds straight neighbor
                        {
                            straightNeighbors.Add(targetTile.Neighbors[0]);
                        }
                        if (targetTile.Neighbors[2] != null) //Adds left neighbor
                        {
                            leftNeighbors.Add(targetTile.Neighbors[2]);
                        }
                        if (targetTile.Neighbors[1] != null) //Adds right neighbor
                        {
                            rightNeighbors.Add(targetTile.Neighbors[1]);
                        }
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        if (targetTile.Neighbors[i] != null)
                        {
                            targetTile.Neighbors[i].OriginTile.Add(targetTile); //Adds origin tile to neighbor
                        }
                    }



                    targetTile.BeenChecked = true;
                }
            }

            /* Removes tree tile */
            tileTextures[4] = tileTextures[0]; //Removes tree tile from list

            for (int y = 0; y < size -1; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    if(tiles[y + 1, x].HDiv == null)
                    {
                        tiles[y, x].TileTexture = tileTextures[rnd.Next(0, tileTextures.Length)]; //Replaces texture with non tree texture if tile doesnt have any 
                    }
                }
            }

        }

        public void Update()
        {
            //foreach (Tile tile in tiles)
            //{
            //    tile.Update();
            //    if (tile.HDiv != null)
            //    {
            //        tile.HDiv.Update();
            //    }
            //    if (tile.VDiv != null)
            //    {
            //        tile.VDiv.Update();
            //    }
            //    if (tile.RDiv != null)
            //    {
            //        tile.RDiv.Update();
            //    }
            //    if (tile.BDiv != null)
            //    {
            //        tile.BDiv.Update();
            //    }
            //}
        }

        public override void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            foreach(Tile tile in tiles)
            {
                if (tile.X_Pos > -300 && tile.Y_Pos > -300 && tile.X_Pos < window.ClientBounds.Width && tile.Y_Pos < window.ClientBounds.Height) //Does not draw tiles outside window to optimize game
                {
                    tile.Draw(spriteBatch);
                }
            }

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (tiles[y, x].X_Pos > -300 && tiles[y, x].Y_Pos > -300 && tiles[y, x].X_Pos < window.ClientBounds.Width && tiles[y, x].Y_Pos < window.ClientBounds.Height) //Does not draw tiles outside window to optimize game
                    {
                        if (tiles[y, x].HDiv != null)
                        {
                            tiles[y, x].HDiv.Draw(spriteBatch);
                        }
                        if (tiles[y, x].VDiv != null)
                        {
                            tiles[y, x].VDiv.Draw(spriteBatch);
                        }
                        if (tiles[y, x].RDiv != null)
                        {
                            tiles[y, x].RDiv.Draw(spriteBatch);
                        }
                    }
                }
                for(int x = 0; x < size; x++)
                {
                    if (tiles[y, x].X_Pos > -300 && tiles[y, x].Y_Pos > -300 && tiles[y, x].X_Pos < window.ClientBounds.Width && tiles[y, x].Y_Pos < window.ClientBounds.Height) //Does not draw tiles outside window to optimize game
                    {
                        if (tiles[y, x].BDiv != null)
                        {
                            tiles[y, x].BDiv.Draw(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}
