using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class FiniteAnimation : Animation
    {
        bool hasEnded = false;

        public FiniteAnimation(GameObject gameObject, GameTime gameTime, Texture2D[] textures, int interval):base(gameObject, gameTime, textures, interval)
        {

        }

        /// <summary>
        /// Updates cycle if animation has not ended.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Update(SpriteBatch spriteBatch)
        {
            if(!hasEnded && lastGameTime > gameTime.TotalGameTime.TotalMilliseconds - 17 && !(elapsedFrames > interval && currentIndex >= textures.Length - 1))
            {
                base.Update(spriteBatch);
            }
            else if(lastGameTime == 0) //Exception when on first frame
            {
                base.Update(spriteBatch);
            }
            else
            {
                hasEnded = true;
            }
        }

        public void reset()
        {
            currentIndex = 0;
            hasEnded = false;
            lastGameTime = 0;
            elapsedFrames = 0;
        }
    }
}
