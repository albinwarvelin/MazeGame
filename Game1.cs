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
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width; //Window width
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height; //Window height
            Window.IsBorderless = true;
            _graphics.ApplyChanges();

            Window.TextInput += InputText.ProcessTextInput;

            GameElements.currentState = GameElements.State.Menu;
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
                    temp = GameElements.MenuUpdate(Window);
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.HighScore:
                    temp = GameElements.HighScoreUpdate(Window);
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.Settings:
                    temp = GameElements.SettingsUpdate();
                    GameElements.lastState = GameElements.currentState;
                    GameElements.currentState = temp;
                    break;
                case GameElements.State.NameChoosing:
                    temp = GameElements.NameChoosingUpdate(Window);
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
                    temp = GameElements.FailedUpdate(Window);
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
        /// Draws all entities.
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
                case GameElements.State.Settings:
                    GameElements.SettingsDraw(_spriteBatch);
                    break;
                case GameElements.State.NameChoosing:
                    GameElements.NameChoosingDraw(_spriteBatch);
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
                    GameElements.FailedDraw(_spriteBatch, Window);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Method to run on exit. Saves highscores at end of game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected override void OnExiting(Object sender, EventArgs args)
        {
            HighScore.SaveHighScores();

            base.OnExiting(sender, args);
        }
    }
}
