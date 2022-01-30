using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    /// <summary>
    /// Failed menu, shows up when player failes level.
    /// </summary>
    class FailedMenu : Menu
    {
        /// <summary>
        /// Creates failed menu, with menuitems.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="font"></param>
        /// <param name="menuItemTextures"></param>
        /// <param name="bannerTexture"></param>
        /// <param name="backgroundTexture"></param>
        /// <param name="score"></param>
        public FailedMenu(GameWindow window, SpriteFont font, Texture2D[] menuItemTextures, Texture2D bannerTexture, Texture2D backgroundTexture, int score) : base(window, font, backgroundTexture)
        {
            menuItems.Add(new MenuItem(bannerTexture, font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 4) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(menuItemTextures[2], font, "Final score:" + score, MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[2].Width / 2), (int)menuItems[0].Y_Pos + 280));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Main menu", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 370));
        }

        /// <summary>
        /// Updates failed menu, checks for button presses.
        /// </summary>
        /// <returns></returns>
        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (menuItems[2].CheckPress(mouseState))
            {
                HighScore.SaveScore();
                GameElements.SuperSpeedsLeft = 1;
                return GameElements.State.Menu;
            }

            return GameElements.State.Failed;
        }
    }
}

