using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class Animation
    {
        protected readonly GameObject gameObject;

        protected readonly GameTime gameTime;
        protected double lastGameTime; //To verify animation was played last frame.

        protected readonly Texture2D[] textures;
        protected int currentIndex = 0;

        protected readonly int interval;
        protected int elapsedFrames = 0;

        public Animation(GameObject gameObject, GameTime gameTime, Texture2D[] textures, int interval)
        {
            this.gameObject = gameObject;
            this.gameTime = gameTime;
            this.textures = textures;
            this.interval = interval;
        }
        public Animation(GameTime gameTime, Texture2D[] textures, int interval)
        {
            this.gameTime = gameTime;
            this.textures = textures;
            this.interval = interval;
        }

        /// <summary>
        /// Updates and resets animation if interrupted.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Update()
        {
            /* Reset */
            if(lastGameTime < gameTime.TotalGameTime.TotalMilliseconds - 20 || (elapsedFrames > interval && currentIndex >= textures.Length - 1))
            {
                currentIndex = 0;

                elapsedFrames = 0;
            }

            /* Update */
            if (elapsedFrames > interval)
            {
                currentIndex++;

                elapsedFrames = 0;
            }

            gameObject.Texture = textures[currentIndex];

            elapsedFrames++;
            lastGameTime = gameTime.TotalGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Returns texture that switches to random texture after interval length.
        /// </summary>
        /// <param name="outTexture"></param>
        public void Update(out Texture2D outTexture)
        {
            Random rnd = new Random();

            if (elapsedFrames > interval)
            {
                currentIndex = rnd.Next(textures.Length);
                elapsedFrames = 0;
            }

            outTexture = textures[currentIndex];

            elapsedFrames++;
            lastGameTime = gameTime.TotalGameTime.TotalMilliseconds;
        }
    }
}
