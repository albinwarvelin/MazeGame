using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    /// <summary>
    /// Cleared menu shows up when player has cleared a level.
    /// </summary>
    class ClearedMenu : Menu
    {
        /// <summary>
        /// Creates new menu for when player clears level, shows current scoreprovides clickable buttons for:
        /// Continue
        /// Main menu
        /// </summary>
        /// <param name="window">GameWindow.</param>
        /// <param name="font">Spritefont for text.</param>
        /// <param name="menuItemTextures">All menu items.</param>
        /// <param name="bannerTexture">Specified banner texture.</param>
        /// <param name="backgroundTexture">Background texture.</param>
        /// <param name="score"></param>
        public ClearedMenu(GameWindow window, SpriteFont font, Texture2D[] menuItemTextures, Texture2D bannerTexture, Texture2D backgroundTexture, int score) : base(window, font, backgroundTexture)
        {
            menuItems.Add(new MenuItem(bannerTexture, font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 4) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(menuItemTextures[0], font, "Score:" + score, MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[0].Width / 2), (int)menuItems[0].Y_Pos + 280));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Continue", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 370));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Main menu", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 460));
        }

        /// <summary>
        /// Updates Cleared menu, checks for button presses and returns state.
        /// </summary>
        /// <returns></returns>
        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (menuItems[2].CheckPress(mouseState))
            {
                GameElements.RemainingTime = GameElements.Level.Timer.TimeLeft;
                GameElements.SuperSpeedsLeft++;
                return GameElements.State.Run;
            }
            if (menuItems[3].CheckPress(mouseState))
            {
                HighScore.SaveScore();
                GameElements.SuperSpeedsLeft = 1;
                return GameElements.State.Menu;
            }

            return GameElements.State.Cleared;
        }
    }
}
