using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class MainMenu : Menu
    {
        public MainMenu(GameWindow window, SpriteFont font, Texture2D[] menuItemTextures, Texture2D bannerTexture, Texture2D backgroundTexture) : base(window, font, backgroundTexture)
        {
            menuItems.Add(new MenuItem(bannerTexture, font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 4) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Play", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 280));
            menuItems.Add(new MenuItem(menuItemTextures[3], font, "View highscores", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[3].Width / 2), (int)menuItems[0].Y_Pos + 370));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Settings", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 460));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Quit game", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 550));
        }

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
                return GameElements.State.Settings;
            }
            if (menuItems[4].CheckPress(mouseState))
            {
                return GameElements.State.Quit;
            }

            return GameElements.State.Menu;
        }
    }
}
