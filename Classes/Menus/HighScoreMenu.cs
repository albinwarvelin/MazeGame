using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MazeGame
{
    /// <summary>
    /// Highscore menu class, used to display highscores in separate menu.
    /// </summary>
    class HighScoreMenu : Menu
    {
        /// <summary>
        /// Creates new highscore menu, lists all current highscores.
        /// </summary>
        /// <param name="window">Gamewindow.</param>
        /// <param name="fontBig">Big font, used in menuitems.</param>
        /// <param name="fontSmall">Small font, used in list.</param>
        /// <param name="menuItemTextures"></param>
        /// <param name="listTextures"></param>
        /// <param name="menuControlTextures"></param>
        /// <param name="bannerTexture"></param>
        /// <param name="backgroundTexture"></param>
        public HighScoreMenu(GameWindow window, SpriteFont fontBig, SpriteFont fontSmall, Texture2D[] menuItemTextures, Texture2D[] listTextures, Texture2D[] menuControlTextures, Texture2D bannerTexture, Texture2D backgroundTexture) : base(window, fontBig, backgroundTexture)
        {
            List<string> list = HighScore.HighscoreList;

            menuItems.Add(new MenuItem(bannerTexture, fontBig, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 10) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(listTextures[0], fontSmall, list[0], MenuItem.Alignment.Left, (window.ClientBounds.Width / 2) - (listTextures[0].Width / 2), (int)menuItems[0].Y_Pos + 320));
            menuItems.Add(new MenuItem(menuItemTextures[0], fontBig, "Name", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (listTextures[0].Width / 2), (int)menuItems[0].Y_Pos + 220));
            menuItems.Add(new MenuItem(menuItemTextures[0], fontBig, "Score", MenuItem.Alignment.Mid, (int)menuItems[2].X_Pos + 450, (int)menuItems[0].Y_Pos + 220));
            menuItems.Add(new MenuItem(menuItemTextures[2], fontBig, "Date and time", MenuItem.Alignment.Mid, (int)menuItems[2].X_Pos + 900, (int)menuItems[0].Y_Pos + 220));

            for (int i = 0; i < 8; i++)
            {
                menuItems.Add(new MenuItem(listTextures[1], fontSmall, list[i + 1], MenuItem.Alignment.Left, (window.ClientBounds.Width / 2) - (listTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 390 + i * 65));
            }

            menuItems.Add(new MenuItem(listTextures[2], fontSmall, list[9], MenuItem.Alignment.Left, (window.ClientBounds.Width / 2) - (listTextures[0].Width / 2), (int)menuItems[12].Y_Pos + 65));
            menuItems.Add(new MenuItem(menuControlTextures[1], fontSmall, "", MenuItem.Alignment.Mid, (int)menuItems[1].X_Pos, (int)menuItems[0].Y_Pos + bannerTexture.Height / 2 - menuControlTextures[1].Height / 2));
        }

        /// <summary>
        /// Updates highscore menu, checks for menuitem presses.
        /// </summary>
        /// <returns></returns>
        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (menuItems[14].CheckPress(mouseState))
            {
                return GameElements.State.Menu;
            }

            return GameElements.State.HighScore;
        }
    }
}
