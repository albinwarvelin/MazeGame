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
        public enum State { Startup, Menu, NameChoosing, Settings, HighScore, Run, Paused, Cleared, Failed, Quit };

        public static State currentState; //Current gamestate
        public static State lastState = State.Startup; //Last gamestate, used when switching states, if not equal to current state methods will most likely initialize the new state

        private static Menu currentMenu;

        private static int x_Sp_Player = 11; //General speed for game
        private static int y_Sp_Player = 11; //General speed for game

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
        private static Texture2D[] menuItemTextures = new Texture2D[7];
        private static Texture2D[] menuControlTextures = new Texture2D[2];
        private static Texture2D[] menuBannerTextures = new Texture2D[4];
        private static Texture2D[] listTextures = new Texture2D[3];
        private static Texture2D greyedOut;
        private static SpriteFont publicPixel20pt;
        private static SpriteFont publicPixel24pt;

        public static void Initialize()
        {
            HighScore.LoadHighScores();
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            skyTexture = content.Load<Texture2D>("assets/background/sky");
            publicPixel20pt = content.Load<SpriteFont>("assets/fonts/publicpixel20pt");
            publicPixel24pt = content.Load<SpriteFont>("assets/fonts/publicpixel24pt");
            timerTexture = content.Load<Texture2D>("assets/level/timerbackground");
            menuItemTextures[0] = content.Load<Texture2D>("assets/menus/block350");
            menuItemTextures[1] = content.Load<Texture2D>("assets/menus/block400");
            menuItemTextures[2] = content.Load<Texture2D>("assets/menus/block500");
            menuItemTextures[3] = content.Load<Texture2D>("assets/menus/block600");
            menuItemTextures[4] = content.Load<Texture2D>("assets/menus/block700");
            menuItemTextures[5] = content.Load<Texture2D>("assets/menus/block1200");
            menuItemTextures[6] = content.Load<Texture2D>("assets/menus/block1400");
            menuControlTextures[0] = content.Load <Texture2D>("assets/menus/blockarrowright");
            menuControlTextures[1] = content.Load<Texture2D>("assets/menus/blockarrowleft");
            menuBannerTextures[0] = content.Load<Texture2D>("assets/menus/levelcleared");
            menuBannerTextures[1] = content.Load<Texture2D>("assets/menus/mainmenu");
            menuBannerTextures[2] = content.Load<Texture2D>("assets/menus/levelfailed");
            menuBannerTextures[3] = content.Load<Texture2D>("assets/menus/highscores");
            listTextures[0] = content.Load<Texture2D>("assets/menus/highscorelisttop");
            listTextures[1] = content.Load<Texture2D>("assets/menus/highscorelistmid");
            listTextures[2] = content.Load<Texture2D>("assets/menus/highscorelistbottom");
            greyedOut = content.Load<Texture2D>("assets/menus/greyedout");

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

        public static State MenuUpdate(GameWindow window) //Updates menu state
        {
            if (lastState != State.Menu)
            {
                currentMenu = new MainMenu(window, publicPixel24pt, menuItemTextures, menuBannerTextures[1], skyTexture);
            }

            return currentMenu.Update();
        }

        public static void MenuDraw(SpriteBatch spriteBatch) //Draws menu
        {
            currentMenu.Draw(spriteBatch);
        }

        public static State HighScoreUpdate(GameWindow window) //Updates highscore state
        {
            if (lastState != State.HighScore)
            {
                currentMenu = new HighScoreMenu(window, publicPixel24pt, publicPixel20pt, menuItemTextures, listTextures, menuControlTextures, menuBannerTextures[3], skyTexture);
            }

            return currentMenu.Update();
        }

        public static void HighScoreDraw(SpriteBatch spriteBatch) //Draws highscore
        {
            currentMenu.Draw(spriteBatch);
        }

        public static State SettingsUpdate()
        {
            return State.Settings;
        }

        public static void SettingsDraw(SpriteBatch spriteBatch)
        {

        }

        public static State NameChoosingUpdate(GameWindow window)
        {
            if (lastState != State.NameChoosing)
            {
                currentMenu = new NameChoosingMenu(window, publicPixel24pt, menuItemTextures, menuControlTextures, skyTexture, HighScore.RecentPlayers);
            }

            return currentMenu.Update();
        }

        public static void NameChoosingDraw(SpriteBatch spriteBatch)
        {
            currentMenu.Draw(spriteBatch);
        }

        public static State RunUpdate(GameWindow window, GameTime gameTime) //Updates run state
        {
            if(lastState != State.Run)
            {
                background = new Background(window, skyTexture, 9, 9);
                level = new Level(window, gameTime, tileTextures, hDivTextures, vDivTextures, endPortalTextures, timerTexture, publicPixel20pt, 5 + HighScore.CurrentScore.Points, 0.17, x_Sp_Player, y_Sp_Player); //TODO change speed to player speed
                player = new Player(playerTextures, gameTime, (window.ClientBounds.Width / 2) - (playerTextures[0].Width / 2), (window.ClientBounds.Height / 2) - (playerTextures[0].Height / 2), x_Sp_Player, y_Sp_Player); //Change texture
            }

            List<Level.Direction> directions = player.Update(window);
            level.Update(gameTime, directions, player); //Updates player first, then uses the enum list provided by method to update 
            background.Update(directions);

            if(level.EndPortal.CheckWin(player))
            {
                HighScore.CurrentScore.Points++;
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
                currentMenu = new ClearedMenu(window, publicPixel24pt, menuItemTextures, menuBannerTextures[0], greyedOut, HighScore.CurrentScore.Points);
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

        public static State FailedUpdate(GameWindow window) //Updates failed
        {
            if (lastState != State.Failed)
            {
                currentMenu = new FailedMenu(window, publicPixel24pt, menuItemTextures, menuBannerTextures[2], greyedOut, HighScore.CurrentScore.Points);
            }

            return currentMenu.Update();
        }

        public static void FailedDraw(SpriteBatch spriteBatch, GameWindow window) //Draws failed
        {
            background.Draw(spriteBatch);
            level.Draw(spriteBatch, window);
            level.EndPortal.DrawTop(spriteBatch);
            currentMenu.Draw(spriteBatch);
        }

        public static Level Level
        {
            get { return level; }   
        }

    }
}
