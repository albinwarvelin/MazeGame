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

        static Level level; //Games level

        static Texture2D tileTexture;
        static Texture2D hDivTexture;
        static Texture2D vDivTexture;

        public static void Initialize()
        {

        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            tileTexture = content.Load<Texture2D>("assets/level/grassTexture");
            hDivTexture = content.Load<Texture2D>("assets/level/horizontalHedge");
            vDivTexture = content.Load<Texture2D>("assets/level/verticalHedge");
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
            level = new Level(tileTexture, hDivTexture, vDivTexture, 100, window, 10, 10); //TODO change speed to player speed

            return State.Run;
        }

        public static State RunUpdate() //Updates run state
        {
            level.Update();
            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch) //Draws run
        {
            level.Draw(spriteBatch);
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
