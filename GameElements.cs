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

        static Texture2D[] tileTextures = new Texture2D[9];
        static Texture2D[] hDivTextures = new Texture2D[4];
        static Texture2D[] vDivTextures = new Texture2D[4];

        public static void Initialize()
        {

        }

        public static void LoadContent(ContentManager content, GameWindow window)
        {
            tileTextures[0] = content.Load<Texture2D>("assets/level/grassTexture1");
            tileTextures[1] = content.Load<Texture2D>("assets/level/grassTexture2");
            tileTextures[2] = content.Load<Texture2D>("assets/level/grassTexture3");
            tileTextures[3] = content.Load<Texture2D>("assets/level/grassTexture4");
            tileTextures[4] = content.Load<Texture2D>("assets/level/grassTexture5"); //Tree tile
            tileTextures[5] = content.Load<Texture2D>("assets/level/grassTexture6");
            tileTextures[6] = content.Load<Texture2D>("assets/level/grassTexture7");
            tileTextures[7] = content.Load<Texture2D>("assets/level/grassTexture8");
            tileTextures[8] = content.Load<Texture2D>("assets/level/grassTexture9");
            hDivTextures[0] = content.Load<Texture2D>("assets/level/horizontalHedge1");
            hDivTextures[1] = content.Load<Texture2D>("assets/level/horizontalHedge2");
            hDivTextures[2] = content.Load<Texture2D>("assets/level/horizontalHedge3");
            hDivTextures[3] = content.Load<Texture2D>("assets/level/horizontalHedge4");
            vDivTextures[0] = content.Load<Texture2D>("assets/level/verticalHedge1");
            vDivTextures[1] = content.Load<Texture2D>("assets/level/verticalHedge2");
            vDivTextures[2] = content.Load<Texture2D>("assets/level/verticalHedge3");
            vDivTextures[3] = content.Load<Texture2D>("assets/level/verticalHedge4");
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
            level = new Level(tileTextures, hDivTextures, vDivTextures, 10, window, 10, 10); //TODO change speed to player speed

            return State.Run;
        }

        public static State RunUpdate() //Updates run state
        {
            level.Update();
            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch, GameWindow window) //Draws run
        {
            level.Draw(spriteBatch, window);
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
