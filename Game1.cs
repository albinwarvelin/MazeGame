using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initialize method. Used for logic initialization. Run once on startup.
        /// </summary>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1920; //Window width
            _graphics.PreferredBackBufferHeight = 1080; //Window height
            _graphics.ApplyChanges();

            GameElements.currentState = GameElements.State.Run;
            GameElements.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent method. Used for initialization of logic that uses textures. Run once on startup
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameElements.LoadContent(Content, Window);
        }

        /// <summary>
        /// Game update method, updates all entities
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameElements.State temp;

            switch (GameElements.currentState)
            {
                case GameElements.State.Menu:
                    temp = GameElements.MenuUpdate();
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.HighScore:
                    temp = GameElements.HighScoreUpdate();
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.Run:
                    temp = GameElements.RunUpdate(Window, gameTime);
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.Paused:
                    temp = GameElements.PausedUpdate();
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.Cleared:
                    temp = GameElements.ClearedUpdate(Window);
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.Failed:
                    temp = GameElements.FailedUpdate();
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws all entities
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (GameElements.lastState)
            {
                case GameElements.State.Menu:
                    GameElements.MenuDraw(_spriteBatch);
                    break;
                case GameElements.State.HighScore:
                    GameElements.HighScoreDraw(_spriteBatch);
                    break;
                case GameElements.State.Run:
                    GameElements.RunDraw(_spriteBatch, Window);
                    break;
                case GameElements.State.Paused:
                    GameElements.PausedDraw(_spriteBatch);
                    break;
                case GameElements.State.Cleared:
                    GameElements.ClearedDraw(_spriteBatch, Window);
                    break;
                case GameElements.State.Failed:
                    GameElements.FailedDraw(_spriteBatch);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
