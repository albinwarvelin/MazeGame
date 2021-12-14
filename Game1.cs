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
            _graphics.PreferredBackBufferWidth = 1280; //Window width
            _graphics.PreferredBackBufferHeight = 720; //Window height
            _graphics.ApplyChanges();

            GameElements.currentState = GameElements.State.Reset;
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

            // TODO: Add your update logic here

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

            switch (GameElements.currentState)
            {
                case GameElements.State.Menu:
                    GameElements.MenuDraw(_spriteBatch);
                    break;
                case GameElements.State.HighScore:
                    GameElements.HighScoreDraw(_spriteBatch);
                    break;
                case GameElements.State.Reset:
                    GameElements.MenuDraw(_spriteBatch);
                    break;
                case GameElements.State.Run:
                    GameElements.RunDraw(_spriteBatch);
                    break;
                case GameElements.State.Paused:
                    GameElements.PausedDraw(_spriteBatch);
                    break;
                case GameElements.State.Cleared:
                    GameElements.ClearedDraw(_spriteBatch);
                    break;
                case GameElements.State.Failed:
                    GameElements.FailedDraw(_spriteBatch);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
