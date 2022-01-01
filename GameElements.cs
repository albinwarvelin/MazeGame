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

        static Level level; //Games level
        static Player player; //Player

        static Texture2D[] tileTextures = new Texture2D[9];
        static Texture2D[] hDivTextures = new Texture2D[4];
        static Texture2D[] vDivTextures = new Texture2D[4];
        static Texture2D[] playerTextures = new Texture2D[4];

        public static void Initialize()
        {
            x_Sp_Player = 11;
            y_Sp_Player = 11;
        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            for(int i = 0; i < 9; i++) //Loads tiles
            {
                tileTextures[i] = content.Load<Texture2D>("assets/level/grassTexture" + (i + 1));
            }
            for(int i = 0; i < 4; i++) //Loads horizontal dividers
            {
                hDivTextures[i] = content.Load<Texture2D>("assets/level/horizontalHedge" + (i + 1));
            }
            for(int i = 0; i < 4; i++) //Loads vertical dividers
            {
                vDivTextures[i] = content.Load<Texture2D>("assets/level/verticalHedge" + (i + 1));
            }
            for(int i = 0; i < 4; i++)
            {
                playerTextures[i] = content.Load<Texture2D>("assets/player/player" + (i + 1));
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

        public static State Reset(GameWindow window) //Resets level then sets state to run
        {
            level = new Level(tileTextures, hDivTextures, vDivTextures, 10, window, x_Sp_Player, y_Sp_Player); //TODO change speed to player speed
            player = new Player(playerTextures, (window.ClientBounds.Width / 2) - (playerTextures[0].Width / 2), (window.ClientBounds.Height / 2) - (playerTextures[0].Height / 2), x_Sp_Player, y_Sp_Player); //Change texture
            return State.Run;
        }

        public static State RunUpdate(GameWindow window) //Updates run state
        {
            level.Update(player.Update(window), player); //Updates player first, then uses the enum list provided by method to update 
            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch, GameWindow window) //Draws run
        {
            level.Draw(spriteBatch, window);
            player.Draw(spriteBatch);
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
    }
}
