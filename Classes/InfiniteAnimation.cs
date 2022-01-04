using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class InfiniteAnimation : Animation
    {
        public InfiniteAnimation(GameObject gameObject, GameTime gameTime, Texture2D[] textures, int interval) : base(gameObject, gameTime, textures, interval)
        {

        }

        /// <summary>
        /// Updates and resets animation if interrupted.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Update(SpriteBatch spriteBatch)
        {
            if(lastGameTime < gameTime.TotalGameTime.TotalMilliseconds - 17 || (elapsedFrames > interval && currentIndex >= textures.Length - 1))
            {
                currentIndex = 0;
                
                elapsedFrames = 0;
            }

            base.Update(spriteBatch);
        }
    }
}
