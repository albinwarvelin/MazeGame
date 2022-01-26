using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    static class GameElements
    {
        public enum State { Startup, Menu, HighScore, Run, Paused, Cleared, Failed, Quit };

        public static State currentState; //Current gamestate
        public static State lastState = State.Startup; //Last gamestate, used when switching states, if not equal to current state methods will most likely initialize the new state

        public static Menu currentMenu;

        static int x_Sp_Player; //General speed for game
        static int y_Sp_Player; //General speed for game

        private static Background background;
        private static Level level; //Games level
        private static Player player; //Player

        private static Texture2D skyTexture;
        private static Texture2D timerTexture;
        private static Texture2D[] tileTextures = new Texture2D[9];
        private static Texture2D[] hDivTextures = new Texture2D[4];
        private static Texture2D[] vDivTextures = new Texture2D[4];
        private static Texture2D[] playerTextures = new Texture2D[9];
        private static Texture2D[] endPortalTextures = new Texture2D[8];
        private static Texture2D[] menuItemTextures = new Texture2D[2];
        private static Texture2D[] menuBannerTextures = new Texture2D[1];
        private static Texture2D greyedOut;
        private static Texture2D levelCleared;
        private static SpriteFont publicPixel20pt;
        private static SpriteFont publicPixel24pt;

        public static void Initialize()
        {
            x_Sp_Player = 11;
            y_Sp_Player = 11;
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            skyTexture = content.Load<Texture2D>("assets/background/sky");
            publicPixel20pt = content.Load<SpriteFont>("assets/fonts/publicpixel20pt");
            publicPixel24pt = content.Load<SpriteFont>("assets/fonts/publicpixel24pt");
            timerTexture = content.Load<Texture2D>("assets/level/timerbackground");
            menuItemTextures[0] = content.Load<Texture2D>("assets/menus/block350");
            menuItemTextures[1] = content.Load<Texture2D>("assets/menus/block400");
            menuBannerTextures[0] = content.Load<Texture2D>("assets/menus/levelcleared");
            greyedOut = content.Load<Texture2D>("assets/menus/greyedout");
            levelCleared = content.Load<Texture2D>("assets/menus/levelcleared");

            for (int i = 0; i < 9; i++) //Loads tiles
            {
                tileTextures[i] = content.Load<Texture2D>("assets/level/grassTexture" + i);
            }
            for (int i = 0; i < 4; i++) //Loads horizontal dividers
            {
                hDivTextures[i] = content.Load<Texture2D>("assets/level/horizontalHedge" + i);
            }
            for(int i = 0; i < 4; i++) //Loads vertical dividers
            {
                vDivTextures[i] = content.Load<Texture2D>("assets/level/verticalHedge" + i);
            }
            for(int i = 0; i < 9; i++) //Loads player textures
            {
                playerTextures[i] = content.Load<Texture2D>("assets/player/player" + i);
            }
            for(int i = 0; i < 8; i++)
            {
                endPortalTextures[i] = content.Load<Texture2D>("assets/level/endPortal" + i);
            }
        }

        public static State MenuUpdate() //Updates menu state
        {
            return State.Menu;
        }

        public static void MenuDraw(SpriteBatch spriteBatch) //Draws menu
        {

        }

        public static State HighScoreUpdate() //Updates highscore state
        {
            return State.HighScore;
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch) //Draws highscore
        {

        }

        public static State RunUpdate(GameWindow window, GameTime gameTime) //Updates run state
        {
            if(lastState != State.Run)
            {
                background = new Background(window, skyTexture, 9, 9);
                level = new Level(window, gameTime, tileTextures, hDivTextures, vDivTextures, endPortalTextures, timerTexture, publicPixel20pt, 7, 0.17, x_Sp_Player, y_Sp_Player); //TODO change speed to player speed
                player = new Player(playerTextures, gameTime, (window.ClientBounds.Width / 2) - (playerTextures[0].Width / 2), (window.ClientBounds.Height / 2) - (playerTextures[0].Height / 2), x_Sp_Player, y_Sp_Player); //Change texture
            }

            List<Level.Direction> directions = player.Update(window);
            level.Update(gameTime, directions, player); //Updates player first, then uses the enum list provided by method to update 
            background.Update(directions);

            if(level.EndPortal.CheckWin(player))
            {
                return State.Cleared;
            }
            else if(level.Timer.HasEnded)
            {
                return State.Failed;
            }
            else
            {
                return State.Run;
            }
        }

        public static void RunDraw(SpriteBatch spriteBatch, GameWindow window) //Draws run
        {
            background.Draw(spriteBatch);
            level.Draw(spriteBatch, window);
            player.Draw(spriteBatch);
            level.EndPortal.DrawTop(spriteBatch);
            level.DrawTimer(spriteBatch);
        }

        public static State PausedUpdate() //Updates paused
        {
            return State.Paused;
        }

        public static void PausedDraw(SpriteBatch spriteBatch) //Draws paused
        {

        }

        public static State ClearedUpdate(GameWindow window) //Updates cleared
        {
            if(lastState != State.Cleared)
            {
                currentMenu = new ClearedMenu(window, publicPixel24pt, menuItemTextures, menuBannerTextures[0], greyedOut, 0);
            }

            return currentMenu.Update();
        }

        public static void ClearedDraw(SpriteBatch spriteBatch, GameWindow window) //Draws cleared
        {
            background.Draw(spriteBatch);
            level.Draw(spriteBatch, window);
            level.EndPortal.DrawTop(spriteBatch);
            currentMenu.Draw(spriteBatch);
        }

        public static State FailedUpdate() //Updates failed
        {
            return State.Failed;
        }

        public static void FailedDraw(SpriteBatch spriteBatch) //Draws failed
        {

        }

        public static Level Level
        {
            get { return level; }   
        }
    }
}
