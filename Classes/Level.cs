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
        public enum Direction { Up, Down, Left, Right }; //Used to return what direction level should move

        private Tile[,] tiles; //Two dimensional array: y, x
        private int size;

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
        public Level(Texture2D[] tileTextures, Texture2D[] hDivTextures, Texture2D[] vDivTexture, int size, double voidTilePercentage, GameWindow window, double x_Speed, double y_Speed) : base(null, 0, 0, x_Speed, y_Speed) //Level position not used, each tile has its own position
        {
            this.size = size;
            Random rnd = new Random(); //Used throughout method

            Vector2 startTilePos = new Vector2((int)(rnd.NextDouble() * 4 + (size / 2 - 2)), (int)(rnd.NextDouble() * 4 + (size / 2 - 2))); //Start tile position

            int x_Start = (int)((window.ClientBounds.Width / 2) - 150 - (startTilePos.X * 300));
            int y_Start = (int)((window.ClientBounds.Height / 2) - 150 - (startTilePos.Y * 300));
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

            /* Assigns voidtiles */
            for (int i = 0; i < size * size * voidTilePercentage; i++)
            {
                int rndX = rnd.Next(0, size);
                int rndY = rnd.Next(0, size);

                if (tiles[rndY, rndX].VoidTile == false && !(rndX == (int)startTilePos.X && rndY == (int)startTilePos.Y)) // To make sure void tiles don't split level and starttile isn't voidtile
                {
                    tiles[rndY, rndX].VoidTile = true;
                }
                else
                {
                    i--;
                }
            }

            /* Assigns neighbors to all tiles in tiles array */
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Tile[] temp = new Tile[4];
                    if (y == 0 && x == 0) //Top left corner
                    {
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if (y == 0 && x == size - 1) //Top right corner
                    {
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if (y == size - 1 && x == 0) //Bottom left corner
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                    }
                    else if (y == size - 1 && x == size - 1) //Bottom right corner
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[2] = tiles[y, x - 1]; //Left
                    }
                    else if (y == 0) //Top edge
                    {
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if (x == size - 1) //Right edge
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if (x == 0) //Left edge
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }
                    else if (y == size - 1) //Bottom edge
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[2] = tiles[y, x - 1]; //Left
                    }
                    else //All else
                    {
                        temp[0] = tiles[y - 1, x]; //Top
                        temp[1] = tiles[y, x + 1]; //Right
                        temp[2] = tiles[y, x - 1]; //Left
                        temp[3] = tiles[y + 1, x]; //Bottom
                    }

                    tiles[y, x].Neighbors = temp;
                }
            }

            /* Calls random-path-generator, if not all tiles have been checked in generation checked booleans are reset, neighboring voidtile of any non-checked tile is set
             * normal tile and process is repeated until all tiles are checked in level generating process. This is done to prevent any lone islands in specific cases. */
            List<Tile> nonCheckedTiles = new List<Tile>();
            do
            {
                nonCheckedTiles = new List<Tile>(); //Resets list

                RandomPathGen(startTilePos);
                
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (!tiles[y, x].BeenChecked && !tiles[y, x].VoidTile)
                        {
                            nonCheckedTiles.Add(tiles[y, x]);
                        }
                    }
                }

                if(nonCheckedTiles.Count != 0)
                {
                    for(;;)
                    {
                        Tile temp = nonCheckedTiles[rnd.Next(0, nonCheckedTiles.Count)].Neighbors[rnd.Next(0, 4)];

                        if (temp != null)
                        {
                            if (temp.VoidTile)
                            {
                                temp.VoidTile = false;
                                break;
                            }
                        }
                    }
                }

            } while (nonCheckedTiles.Count != 0);

            /* Removes treetile */
            tileTextures[4] = tileTextures[0]; //Removes treetile from list of texturechoices, treetile has index 4.

            for (int y = 0; y < size -1; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    if(tiles[y + 1, x].HDiv == null)
                    {
                        tiles[y, x].TileTexture = tileTextures[rnd.Next(0, tileTextures.Length)]; //Replaces texture with any non tree texture if tile doesnt have any divider under it 
                    }
                }
            }

        }

        /// <summary>
        /// Generates random maze through all available tiles. Works by adding tiles neighbors to common list then picking a random tile which is added to maze with an
        /// opening in the direction of origintile. Neighbors of this tile is then added to common list. If tile has been checked it's not processed and removed from list.
        /// Runs until list is empty.
        /// </summary>
        /// <param name="startTilePos"></param>
        public void RandomPathGen(Vector2 startTilePos)
        {
            /* Resets from previous run */
            foreach (Tile tile in tiles)
            {
                tile.BeenChecked = false;
                tile.ResetDividers();
                tile.OriginTile = new List<Tile>();
            }

            Random rnd = new Random();

            /* Generates first tile and it's neighbors */
            List<Tile> straightNeighbors = new List<Tile>();
            List<Tile> rightNeighbors = new List<Tile>();
            List<Tile> leftNeighbors = new List<Tile>();
            double straightChance = 0.20;
            double leftChance = 0.40; //Rightchance is 1.00 - straightchance - leftchance

            Tile startTile = tiles[(int)startTilePos.Y, (int)startTilePos.X];
            startTile.BeenChecked = true;
 
            leftNeighbors.Add(startTile.Neighbors[2]);
            rightNeighbors.Add(startTile.Neighbors[1]);
            straightNeighbors.Add(startTile.Neighbors[3]); //Put in straight neighbors, only "back-neighbor" to be added to list
            straightNeighbors.Add(startTile.Neighbors[0]);
            

            for (int i = 0; i < 4; i++)
            {
                if(startTile.Neighbors[i] != null)
                {
                    startTile.Neighbors[i].OriginTile.Add(startTile);
                }
            }

            /* Random generation. Works by adding neighbors to lists then picking random neighbor to remove wall between it and it's origin */
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

                if(targetTile == null)
                {
                    continue;
                }

                if (targetTile.VoidTile) //Removes divider between voidtiles
                {
                    for (int x = 0; x < size; x++) //Removes divider if tile is on top edge
                    {
                        if (targetTile == tiles[0, x])
                        {
                            targetTile.HDiv = null;
                        }
                    }
                    for (int y = 0; y < size; y++) //Removes divider if tile is on left edge
                    {
                        if (targetTile == tiles[y, 0])
                        {
                            targetTile.VDiv = null;
                        }
                    }

                    targetTile.RDiv = null; //Removes right divider, only when on right edge
                    targetTile.BDiv = null; //Removes bottom divider, only when on bottom edge

                    if (targetTile.Neighbors[0] != null)
                    {
                        if (targetTile.Neighbors[0].VoidTile)
                        {
                            targetTile.HDiv = null;
                        }
                    }
                    if (targetTile.Neighbors[1] != null)
                    {
                        if (targetTile.Neighbors[1].VoidTile)
                        {
                            targetTile.Neighbors[1].VDiv = null;
                        }
                    }
                    if (targetTile.Neighbors[2] != null)
                    {
                        if (targetTile.Neighbors[2].VoidTile)
                        {
                            targetTile.VDiv = null;
                        }
                    }
                    if (targetTile.Neighbors[3] != null)
                    {
                        if (targetTile.Neighbors[3].VoidTile)
                        {
                            targetTile.Neighbors[3].HDiv = null;
                        }
                    }
                }
                else if (!targetTile.BeenChecked) //Adds neighbors to neighbor list
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
        }

        /// <summary>
        /// Updates level.
        /// </summary>
        /// <param name="toMove"></param>
        /// <param name="player"></param>
        public void Update(List<Direction> toMove, Player player)
        {
            List<TileDivider> surroundingDividers = new List<TileDivider>();

            foreach (Tile tile in tiles)
            {
                tile.Update(toMove);
                if (tile.HDiv != null)
                {
                    tile.HDiv.Update(toMove);

                    /* Used for collison checking player optimally */
                    if(tile.HDiv.X_Pos > player.X_Pos - tile.HDiv.Width - player.Width && tile.HDiv.X_Pos < player.X_Pos + tile.HDiv.Width + player.Width && tile.HDiv.Y_Pos > player.Y_Pos - tile.HDiv.Height - player.Height && tile.HDiv.Y_Pos < player.Y_Pos + tile.HDiv.Height + player.Height)
                    {
                        surroundingDividers.Add(tile.HDiv);
                    }
                }
                if (tile.VDiv != null)
                {
                    tile.VDiv.Update(toMove);

                    /* Used for collison checking player optimally */
                    if (tile.VDiv.X_Pos > player.X_Pos - tile.VDiv.Width - player.Width && tile.VDiv.X_Pos < player.X_Pos + tile.VDiv.Width + player.Width && tile.VDiv.Y_Pos > player.Y_Pos - tile.VDiv.Height - player.Height && tile.VDiv.Y_Pos < player.Y_Pos + tile.VDiv.Height + player.Height)
                    {
                        surroundingDividers.Add(tile.VDiv);
                    }
                }
                if (tile.RDiv != null)
                {
                    tile.RDiv.Update(toMove);

                    /* Used for collison checking player optimally */
                    if (tile.RDiv.X_Pos > player.X_Pos - tile.RDiv.Width - player.Width && tile.RDiv.X_Pos < player.X_Pos + tile.RDiv.Width + player.Width && tile.RDiv.Y_Pos > player.Y_Pos - tile.RDiv.Height - player.Height && tile.RDiv.Y_Pos < player.Y_Pos + tile.RDiv.Height + player.Height)
                    {
                        surroundingDividers.Add(tile.RDiv);
                    }
                }
                if (tile.BDiv != null)
                {
                    tile.BDiv.Update(toMove);

                    /* Used for collison checking player optimally */
                    if (tile.BDiv.X_Pos > player.X_Pos - tile.BDiv.Width - player.Width && tile.BDiv.X_Pos < player.X_Pos + tile.BDiv.Width + player.Width && tile.BDiv.Y_Pos > player.Y_Pos - tile.BDiv.Height - player.Height && tile.BDiv.Y_Pos < player.Y_Pos + tile.BDiv.Height + player.Height)
                    {
                        surroundingDividers.Add(tile.BDiv);
                    }
                }
            }

            player.SurroundingDividers = surroundingDividers;
        }

        /// <summary>
        /// Draws level, overrides method in gameobject.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="window"></param>
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
