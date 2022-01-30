using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MazeGame
{
    /// <summary>
    /// Abstact menu class, contains variables common to all menus.
    /// </summary>
    abstract class Menu
    {
        protected SpriteFont font;
        protected readonly Background background;
        protected List<MenuItem> menuItems = new List<MenuItem>();

        /// <summary>
        /// Assigns values to common variables for all menu.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="font"></param>
        /// <param name="backgroundTexture"></param>
        public Menu(GameWindow window, SpriteFont font, Texture2D backgroundTexture)
        {
            background = new Background(window, backgroundTexture, 0, 0);
            this.font = font;
        }

        /// <summary>
        /// Abstract update method, all menus have to implement this.
        /// </summary>
        /// <returns></returns>
        abstract public GameElements.State Update();

        /// <summary>
        /// Draws background and all menuitems.
        /// </summary>
        /// <param name="spriteBatch"></param>
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
