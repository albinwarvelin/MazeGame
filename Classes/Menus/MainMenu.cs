using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    /// <summary>
    /// Main menu class.
    /// </summary>
    class MainMenu : Menu
    {
        /// <summary>
        /// Creates main menu with menuitems to all options.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="font"></param>
        /// <param name="menuItemTextures"></param>
        /// <param name="bannerTexture"></param>
        /// <param name="backgroundTexture"></param>
        public MainMenu(GameWindow window, SpriteFont font, Texture2D[] menuItemTextures, Texture2D bannerTexture, Texture2D backgroundTexture) : base(window, font, backgroundTexture)
        {
            menuItems.Add(new MenuItem(bannerTexture, font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 4) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Play", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 280));
            menuItems.Add(new MenuItem(menuItemTextures[3], font, "View highscores", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[3].Width / 2), (int)menuItems[0].Y_Pos + 370));
            menuItems.Add(new MenuItem(menuItemTextures[2], font, "How to play", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[2].Width / 2), (int)menuItems[0].Y_Pos + 460));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Quit game", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 550));
        }

        /// <summary>
        /// Updates main menu, checks for buttonpresses.
        /// </summary>
        /// <returns></returns>
        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (menuItems[1].CheckPress(mouseState))
            {
                return GameElements.State.NameChoosing;
            }
            if (menuItems[2].CheckPress(mouseState))
            {
                return GameElements.State.HighScore;
            }
            if (menuItems[3].CheckPress(mouseState))
            {
                return GameElements.State.HowTo;
            }
            if (menuItems[4].CheckPress(mouseState))
            {
                return GameElements.State.Quit;
            }

            return GameElements.State.Menu;
        }
    }
}
