using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeGame
{
    /// <summary>
    /// Timer class holds countdown timer that has to be updated every frame.
    /// </summary>
    class Timer : MenuItem
    {
        private readonly long limit;
        private long currentTime = 0;
        private bool hasEnded;

        /// <summary>
        /// Creates a new timer with specified parameters.
        /// </summary>
        /// <param name="texture">Background texture of timer.</param>
        /// <param name="font">Spritefont of text in timer.</param>
        /// <param name="seconds">Length of countdown in seconds.</param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        public Timer(Texture2D texture, SpriteFont font, int seconds, int x_Pos, int y_Pos) : base(texture, font, "Time left:", Alignment.Mid, x_Pos, y_Pos)
        {
            limit = 60 * seconds;
        }

        /// <summary>
        /// Updates timer, if timer has ended object boolean is updated.
        /// </summary>
        public void Update()
        {
            currentTime++;

            if (currentTime > limit)
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
            spriteBatch.Draw(texture, position, Color.White);
            text = "Time left:" + (limit - currentTime) / 60;
            ReCenterText(); //Recenters text in textbox, called because text and textsize is changed.
            printText.Print(text, spriteBatch, Color.Black);
        }

        /// <summary>
        /// HasEnded property.
        /// </summary>
        public bool HasEnded
        {
            get { return hasEnded; }
        }


        /// <summary>
        /// Timeleft property, returns time left in seconds.
        /// </summary>
        public int TimeLeft
        {
            get { return (int)(limit - currentTime) / 60; }
        }
    }
}
