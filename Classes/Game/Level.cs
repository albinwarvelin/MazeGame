using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MazeGame
{
    /// <summary>
    /// Level class, used for game.
    /// </summary>
    class Level : MovingObject, ISetSpeed
    {
        public enum Direction { Up, Down, Left, Right }; //Used to return what direction level should move

        private readonly Tile[,] tiles; //Two dimensional array: y, x
        private readonly int size;
        private readonly EndPortal endPortal;
        private readonly Timer timer;
        private readonly MenuItem superSpeedMenuItem;


        /// <summary>
        /// Creates new level.
        /// </summary>
        /// <param name="window">GameWindow, used to determine what tiles to draw.</param>
        /// <param name="tileTextures">All tile textures.</param>
        /// <param name="hDivTextures">All horizontal divider textures.</param>
        /// <param name="vDivTexture">All vertical divider textures.</param>
        /// <param name="endPortalTextures">All endportal textures.</param>
        /// <param name="timerTexture">Background texture of timer overlay.</param>
        /// <param name="font">Font of text in overlays.</param>
        /// <param name="remainingTime">Remaining time from last level in seconds.</param>
        /// <param name="size">Size of one side. Total tiles in level becomes square of size.</param>
        /// <param name="voidTilePercentage">Percentage of tiles that should be voidtile. Will often be a little less as levelcreation adjusts so no tiles are non-reachable.</param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public Level(GameWindow window, Texture2D[] tileTextures, Texture2D[] hDivTextures, Texture2D[] vDivTexture, Texture2D[] endPortalTextures, Texture2D timerTexture, SpriteFont font, int remainingTime, int size, double voidTilePercentage, double x_Speed, double y_Speed) : base(null, 0, 0, x_Speed, y_Speed) //Level position not used, each tile has its own position
        {
            this.size = size;
            Random rnd = new Random(); //Used throughout method

            timer = new Timer(timerTexture, font, 35 + remainingTime / 2 + size * 3, 20, 20); //Sets new timer
            superSpeedMenuItem = new MenuItem(timerTexture, font, "Superspeed:" + GameElements.SuperSpeedsLeft, MenuItem.Alignment.Mid, window.ClientBounds.Width - timerTexture.Width - 20, 20);

            Vector2 startTilePos = new Vector2((int)(rnd.NextDouble() * 4 + (size / 2 - 2)), (int)(rnd.NextDouble() * 4 + (size / 2 - 2))); //Determines what tile should be starting tile.

            int x_Start = (int)((window.ClientBounds.Width / 2) - 150 - (startTilePos.X * 300)); //Position of top left tile on screen.
            int y_Start = (int)((window.ClientBounds.Height / 2) - 150 - (startTilePos.Y * 300)); //Position of top left tile on screen.
            tiles = new Tile[size, size];

            /* Assigns tiles to all values in tiles array */
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (y == size - 1 && x == size - 1) //Bottom corner tile
                    {
                        tiles[y, x] = new Tile(tileTextures[rnd.Next(0, tileTextures.Length)], hDivTextures[rnd.Next(0, hDivTextures.Length)], vDivTexture[rnd.Next(0, vDivTexture.Length)], Tile.TileType.Corner, x_Start + (x * 300), y_Start + (y * 300), x_Speed, y_Speed);
                    }
                    else if (x == size - 1) //Right side tiles
                    {
                        tiles[y, x] = new Tile(tileTextures[rnd.Next(0, tileTextures.Length)], hDivTextures[rnd.Next(0, hDivTextures.Length)], vDivTexture[rnd.Next(0, vDivTexture.Length)], Tile.TileType.Right, x_Start + (x * 300), y_Start + (y * 300), x_Speed, y_Speed);
                    }
                    else if (y == size - 1) //Bottom tiles
                    {
                        tiles[y, x] = new Tile(tileTextures[rnd.Next(0, tileTextures.Length)], hDivTextures[rnd.Next(0, hDivTextures.Length)], vDivTexture[rnd.Next(0, vDivTexture.Length)], Tile.TileType.Bottom, x_Start + (x * 300), y_Start + (y * 300), x_Speed, y_Speed);
                    }
                    else //Rest
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
            List<Tile> nonCheckedTiles;
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

                if (nonCheckedTiles.Count != 0)
                {
                    for (; ; )
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
            Texture2D tempTexture = tileTextures[4];
            tileTextures[4] = tileTextures[0]; //Removes treetile from list of texturechoices, treetile has index 4.

            for (int y = 0; y < size - 1; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (tiles[y + 1, x].HDiv == null)
                    {
                        tiles[y, x].TileTexture = tileTextures[rnd.Next(0, tileTextures.Length)]; //Replaces texture with any non tree texture if tile doesnt have any divider under it 
                    }
                }
            }

            tileTextures[4] = tempTexture;

            /* Sets random border to endPortal */
            bool continueLoop = true;
            while (continueLoop)
            {
                int index = rnd.Next(size);
                switch (rnd.Next(4))
                {
                    case 0: //Top
                        if (!tiles[0, index].VoidTile)
                        {
                            TileDivider previous = tiles[0, index].HDiv;
                            EndPortal temp = new EndPortal(new Texture2D[] { endPortalTextures[2], endPortalTextures[3] }, EndPortal.Type.Top, previous.X_Pos, previous.Y_Pos - 375, x_Speed, y_Speed);

                            tiles[0, index].HDiv = temp;
                            endPortal = temp;
                            continueLoop = false;
                        }
                        break;
                    case 1: //Right
                        if (!tiles[index, size - 1].VoidTile)
                        {
                            TileDivider previous = tiles[index, size - 1].RDiv;
                            EndPortal temp = new EndPortal(new Texture2D[] { endPortalTextures[4], endPortalTextures[5] }, EndPortal.Type.Right, previous.X_Pos + 25, previous.Y_Pos - 100, x_Speed, y_Speed);

                            tiles[index, size - 1].RDiv = temp;
                            endPortal = temp;
                            continueLoop = false;
                        }
                        break;
                    case 2: //Left
                        if (!tiles[index, 0].VoidTile)
                        {
                            TileDivider previous = tiles[index, 0].VDiv;
                            EndPortal temp = new EndPortal(new Texture2D[] { endPortalTextures[0], endPortalTextures[1] }, EndPortal.Type.Left, previous.X_Pos - 275, previous.Y_Pos - 100, x_Speed, y_Speed);

                            tiles[index, 0].VDiv = temp;
                            endPortal = temp;
                            continueLoop = false;
                        }
                        break;
                    case 3: //Bottom
                        if (!tiles[size - 1, index].VoidTile)
                        {
                            TileDivider previous = tiles[size - 1, index].BDiv;
                            EndPortal temp = new EndPortal(new Texture2D[] { endPortalTextures[6], endPortalTextures[7] }, EndPortal.Type.Bottom, previous.X_Pos, previous.Y_Pos + 25, x_Speed, y_Speed);

                            tiles[size - 1, index].BDiv = temp;
                            endPortal = temp;
                            continueLoop = false;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Generates random maze through all available tiles. Works by adding tiles neighbors to common list then picking a random tile which is added to maze with an
        /// opening in the direction of origintile. Neighbors of this tile is then added to common list. If tile has been checked it's not processed and removed from list.
        /// Runs until list is empty.
        /// </summary>
        /// <param name="startTilePos">Index of starting tile.</param>
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
                if (startTile.Neighbors[i] != null)
                {
                    startTile.Neighbors[i].OriginTile.Add(startTile);
                }
            }

            /* Random generation. Works by adding neighbors to lists then picking random neighbor to remove wall between it and it's origin. */
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

                if (targetTile == null)
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
        /// Updates level and its tiles and dividers, returns directions player should move.
        /// </summary>
        /// <param name="toMove">List of directions level should move.</param>
        /// <param name="player">Player object.</param>
        public List<TileDivider> Update(List<Direction> toMove, Player player)
        {
            timer.Update();

            List<TileDivider> surroundingDividers = new List<TileDivider>();

            foreach (Tile tile in tiles)
            {
                tile.Update(toMove);
                if (tile.HDiv != null)
                {
                    tile.HDiv.Update(toMove);

                    /* Used for collison checking player optimally */
                    if (tile.HDiv.X_Pos > player.X_Pos - tile.HDiv.Width - player.Width && tile.HDiv.X_Pos < player.X_Pos + tile.HDiv.Width + player.Width && tile.HDiv.Y_Pos > player.Y_Pos - tile.HDiv.Height - player.Height && tile.HDiv.Y_Pos < player.Y_Pos + tile.HDiv.Height + player.Height)
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

            return surroundingDividers;
        }

        /// <summary>
        /// Sets level speed.
        /// </summary>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public void SetSpeed(double x_Speed, double y_Speed)
        {
            speed.X = (float)x_Speed;
            speed.Y = (float)y_Speed;

            foreach (Tile tile in tiles)
            {
                tile.SetSpeed(x_Speed, y_Speed);
            }
        }

        /// <summary>
        /// Draws tiles and tile dividers, overrides method in gameobject.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="window"></param>
        public override void Draw(SpriteBatch spriteBatch, GameWindow window)
        {
            /* Draws tiles */
            foreach (Tile tile in tiles)
            {
                if (tile.X_Pos > -300 && tile.Y_Pos > -300 && tile.X_Pos < window.ClientBounds.Width && tile.Y_Pos < window.ClientBounds.Height) //Does not draw tiles outside window to optimize game
                {
                    tile.Draw(spriteBatch);
                }
            }

            /* Draws tiledividers in optimal way in order to not overlap eachother in unnatural ways. */
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (tiles[y, x].X_Pos > -300 && tiles[y, x].Y_Pos > -300 && tiles[y, x].X_Pos < window.ClientBounds.Width && tiles[y, x].Y_Pos < window.ClientBounds.Height + 100) //Does not draw tiles outside window to optimize game
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
                for (int x = 0; x < size; x++)
                {
                    if (tiles[y, x].X_Pos > -300 && tiles[y, x].Y_Pos > -300 && tiles[y, x].X_Pos < window.ClientBounds.Width && tiles[y, x].Y_Pos < window.ClientBounds.Height + 100) //Does not draw tiles outside window to optimize game
                    {
                        if (tiles[y, x].BDiv != null)
                        {
                            tiles[y, x].BDiv.Draw(spriteBatch);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draws timer and superspeed counter.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawOverlay(SpriteBatch spriteBatch)
        {
            timer.Draw(spriteBatch);
            superSpeedMenuItem.Text = "Superspeed:" + GameElements.SuperSpeedsLeft;
            superSpeedMenuItem.Draw(spriteBatch);
        }

        /// <summary>
        /// Endportal property.
        /// </summary>
        public EndPortal EndPortal
        {
            get { return endPortal; }
        }

        /// <summary>
        /// Timer property.
        /// </summary>
        public Timer Timer
        {
            get { return timer; }
        }
    }
}
