using System;
using System.Collections.Generic;
using System.Text;
using MazeGame.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    static class GameElements
    {
        public enum State { Menu, HighScore, Reset, Run, Paused, Cleared, Failed, Quit };

        public static State currentState; //Current gamestate

        static int x_Sp_Player; //General speed for game
        static int y_Sp_Player; //General speed for game

        private static Background background;
        private static Level level; //Games level
        private static Player player; //Player

        private static Texture2D skyTexture;
        private static Texture2D[] tileTextures = new Texture2D[9];
        private static Texture2D[] hDivTextures = new Texture2D[4];
        private static Texture2D[] vDivTextures = new Texture2D[4];
        private static Texture2D[] playerTextures = new Texture2D[9];
        private static Texture2D[] endPortalTextures = new Texture2D[8];

        public static void Initialize()
        {
            x_Sp_Player = 11;
            y_Sp_Player = 11;
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            skyTexture = content.Load<Texture2D>("assets/background/sky");

            for(int i = 0; i < 9; i++) //Loads tiles
            {
                tileTextures[i] = content.Load<Texture2D>("assets/level/grassTexture" + i);
            }
            for(int i = 0; i < 4; i++) //Loads horizontal dividers
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

        public static State Reset(GameWindow window, GameTime gameTime) //Resets level then sets state to run
        {
            background = new Background(window, skyTexture, 9, 9);
            level = new Level(tileTextures, hDivTextures, vDivTextures,endPortalTextures, 7, 0.17, window, x_Sp_Player, y_Sp_Player); //TODO change speed to player speed
            player = new Player(playerTextures, gameTime, (window.ClientBounds.Width / 2) - (playerTextures[0].Width / 2), (window.ClientBounds.Height / 2) - (playerTextures[0].Height / 2), x_Sp_Player, y_Sp_Player); //Change texture
            return State.Run;
        }

        public static State RunUpdate(GameWindow window) //Updates run state
        {
            List<Level.Direction> directions = player.Update(window);
            level.Update(directions, player); //Updates player first, then uses the enum list provided by method to update 
            background.Update(directions);

            if(level.EndPortal.CheckWin(player))
            {
                return State.Cleared;
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
        }

        public static State PausedUpdate() //Updates paused
        {
            return State.Paused;
        }

        public static void PausedDraw(SpriteBatch spriteBatch) //Draws paused
        {

        }

        public static State ClearedUpdate() //Updates cleared
        {
            return State.Cleared;
        }

        public static void ClearedDraw(SpriteBatch spriteBatch) //Draws cleared
        {

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
