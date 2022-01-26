using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class Timer : GameObject
    {
        private readonly long limit;
        private long currentTime = 0;
        SpriteFont font;
        PrintText printText;
        private bool hasEnded;
        
        /// <summary>
        /// Creates new timer with texture, font, text and length in seconds.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="texture"></param>
        /// <param name="font"></param>
        /// <param name="seconds"></param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        public Timer(GameTime gameTime, Texture2D texture, SpriteFont font, int seconds, int x_Pos, int y_Pos) : base(texture, x_Pos , y_Pos)
        {
            limit = (long) gameTime.TotalGameTime.TotalSeconds + seconds;
            this.font = font;
            printText = new PrintText(font, (int) position.X + 10, (int) position.Y + 10);
        }

        /// <summary>
        /// Updates timer, returns true if timer has ended.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            currentTime = (long) gameTime.TotalGameTime.TotalSeconds;

            if(gameTime.TotalGameTime.TotalSeconds > limit)
            {
                hasEnded = true;
            }
            else
            {
                hasEnded = false;
            }
        }

        /// <summary>
        /// Draws timer.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), Color.White);
            printText.Print("Time left:" + (limit - currentTime), spriteBatch, Color.Black);
        }

        /// <summary>
        /// HasEnded property.
        /// </summary>
        public bool HasEnded
        {
            get { return hasEnded; }
        }
    }
}
