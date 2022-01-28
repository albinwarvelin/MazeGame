using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class InfiniteAnimation : Animation
    {
        public InfiniteAnimation(GameObject gameObject, GameTime gameTime, Texture2D[] textures, int interval) : base(gameObject, gameTime, textures, interval)
        {

        }
        public InfiniteAnimation(GameTime gameTime, Texture2D[] textures, int interval) : base( gameTime, textures, interval)
        {

        }

        /// <summary>
        /// Updates and resets animation if interrupted.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Update()
        {
            if(lastGameTime < gameTime.TotalGameTime.TotalMilliseconds - 20 || (elapsedFrames > interval && currentIndex >= textures.Length - 1))
            {
                currentIndex = 0;
                
                elapsedFrames = 0;
            }

            base.Update();
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
