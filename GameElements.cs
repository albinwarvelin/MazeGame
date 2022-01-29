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
        public enum State { Startup, Menu, NameChoosing, HowTo, HighScore, Run, Paused, Cleared, Failed, Quit };

        public static State currentState; //Current gamestate
        public static State lastState = State.Startup; //Last gamestate, used when switching states, if not equal to current state methods will most likely initialize the new state
        private static KeyboardState oldKeyboardstate = new KeyboardState();

        private static Menu currentMenu;

        private static readonly int x_Sp_Player = 11; //General speed for game
        private static readonly int y_Sp_Player = 11; //General speed for game

        private static Background background;
        private static Level level; //Games level
        private static int remainingTime = 0;
        private static Player player; //Player
        private static int superSpeedsLeft = 1; // How many superspeeds player has left.

        private static Texture2D skyTexture;
        private static readonly Texture2D[] tileTextures = new Texture2D[9];
        private static readonly Texture2D[] hDivTextures = new Texture2D[4];
        private static readonly Texture2D[] vDivTextures = new Texture2D[4];
        private static readonly Texture2D[] playerTextures = new Texture2D[9];
        private static readonly Texture2D[] fireTextures = new Texture2D[26];
        private static readonly Texture2D[] endPortalTextures = new Texture2D[8];
        private static readonly Texture2D[] menuItemTextures = new Texture2D[7];
        private static readonly Texture2D[] menuControlTextures = new Texture2D[2];
        private static readonly Texture2D[] menuBannerTextures = new Texture2D[6];
        private static readonly Texture2D[] listTextures = new Texture2D[3];
        private static readonly Texture2D[] textBoxTextures = new Texture2D[3];
        private static Texture2D greyedOut;
        private static SpriteFont publicPixel20pt;
        private static SpriteFont publicPixel24pt;

        public static void Initialize()
        {
            HighScore.LoadHighScores();
        }

        public static void LoadContent(ContentManager content)
        {
            skyTexture = content.Load<Texture2D>("assets/background/sky");
            publicPixel20pt = content.Load<SpriteFont>("assets/fonts/publicpixel20pt");
            publicPixel24pt = content.Load<SpriteFont>("assets/fonts/publicpixel24pt");
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
            menuBannerTextures[4] = content.Load<Texture2D>("assets/menus/howtoplay");
            menuBannerTextures[5] = content.Load<Texture2D>("assets/menus/paused");
            listTextures[0] = content.Load<Texture2D>("assets/menus/highscorelisttop");
            listTextures[1] = content.Load<Texture2D>("assets/menus/highscorelistmid");
            listTextures[2] = content.Load<Texture2D>("assets/menus/highscorelistbottom");
            textBoxTextures[0] = content.Load<Texture2D>("assets/menus/textboxtop");
            textBoxTextures[1] = content.Load<Texture2D>("assets/menus/textboxmid");
            textBoxTextures[2] = content.Load<Texture2D>("assets/menus/textboxbottom");
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
            for (int i = 0; i < 26; i++)
            {
                fireTextures[i] = content.Load<Texture2D>("assets/player/fire" + i);
            }
        }

        public static State MenuUpdate(GameWindow window) //Updates menu state
        {
            if (lastState != State.Menu)
            {
                remainingTime = 0;
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

        public static State HowToPlayUpdate(GameWindow window)
        {
            if (lastState != State.HowTo)
            {
                currentMenu = new HowToPlayMenu(window, publicPixel24pt, publicPixel20pt, menuControlTextures, textBoxTextures, menuBannerTextures[4], skyTexture);
            }

            return currentMenu.Update();
        }

        public static void HowToPlayDraw(SpriteBatch spriteBatch)
        {
            currentMenu.Draw(spriteBatch);
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
                level = new Level(window, tileTextures, hDivTextures, vDivTextures, endPortalTextures, menuItemTextures[2], publicPixel24pt, remainingTime / 60, 5 + HighScore.CurrentScore.Points, 0.17, x_Sp_Player, y_Sp_Player); //TODO change speed to player speed
                player = new Player(playerTextures, fireTextures, gameTime, (window.ClientBounds.Width / 2) - (playerTextures[0].Width / 2), (window.ClientBounds.Height / 2) - (playerTextures[0].Height / 2), x_Sp_Player, y_Sp_Player); //Change texture

                level.Update(new List<Level.Direction>(), player); //Runs single levelupdate. To prevent bug where player update tries to get dividers from level that have not yet been initialized.
            }

            List<Level.Direction> directions = player.Update(window);
            level.Update(directions, player); //Updates player first, then uses the enum list provided by method to update 
            background.Update(directions);
            

            InputText.TryConvertKeyboardInput(Keyboard.GetState(), oldKeyboardstate, out char key);
            oldKeyboardstate = Keyboard.GetState();

            if (key == (char)27) //Escape key
            {
                return State.Paused;
            }
            else if(level.EndPortal.CheckWin(player))
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
            level.DrawOverlay(spriteBatch);
        }

        public static State PausedUpdate(GameWindow window) //Updates paused
        {
            if (lastState != State.Paused)
            {
                currentMenu = new PausedMenu(window, publicPixel24pt, menuItemTextures, menuBannerTextures[5], greyedOut);
            }

            return currentMenu.Update();
        }

        public static void PausedDraw(SpriteBatch spriteBatch, GameWindow window) //Draws paused
        {
            background.Draw(spriteBatch);
            level.Draw(spriteBatch, window);
            spriteBatch.Draw(player.Texture, new Vector2((float) player.X_Pos, (float) player.Y_Pos), Color.White); //Draws player frozen
            currentMenu.Draw(spriteBatch);
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

        public static int RemainingTime
        {
            set { remainingTime = value; }
        }

        public static int SuperSpeedsLeft
        {
            set { superSpeedsLeft = value; }
            get { return superSpeedsLeft; }
        }

        public static int Y_Sp_Player
        {
            get { return y_Sp_Player;}
        }

        public static int X_Sp_Player
        {
            get { return x_Sp_Player; }
        }
    }
}
