using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    abstract class Animation
    {
        protected readonly GameObject gameObject;

        protected readonly GameTime gameTime;
        protected double lastGameTime; //To verify animation was played last frame.

        protected readonly Texture2D[] textures;
        protected int currentIndex = 0;

        protected readonly int interval;
        protected int elapsedFrames = 0;
        
        /// <summary>
        /// Base animation class, cycles through textures with specified interval by calling update(). Only changes texture, does not draw texture..
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="gameTime"></param>
        /// <param name="textures"></param>
        /// <param name="interval"></param>
        protected Animation(GameObject gameObject, GameTime gameTime, Texture2D[] textures, int interval)
        {
            this.gameObject = gameObject;
            this.gameTime = gameTime;
            this.textures = textures;
            this.interval = interval;
        }

        /// <summary>
        /// Updates cycle.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Update(SpriteBatch spriteBatch)
        {
            if (elapsedFrames > interval)
            {
                currentIndex++;

                elapsedFrames = 0;
            }

            gameObject.Texture = textures[currentIndex];
            elapsedFrames++;
            lastGameTime = gameTime.TotalGameTime.TotalMilliseconds;
        }
    }
}
