using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    abstract class Menu
    {
        protected SpriteFont font;
        protected readonly Background background;
        protected List<MenuItem> menuItems = new List<MenuItem>();

        public Menu(GameWindow window, SpriteFont font, Texture2D backgroundTexture)
        {
            background = new Background(window, backgroundTexture, 0, 0);
            this.font = font;
        }

        abstract public GameElements.State Update();

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            foreach (MenuItem item in menuItems)
            {
                item.Draw(spriteBatch);
            }
        }
    }
}
