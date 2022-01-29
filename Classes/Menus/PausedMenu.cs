using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class PausedMenu : Menu
    {
        public PausedMenu(GameWindow window, SpriteFont font, Texture2D[] menuItemTextures, Texture2D bannerTexture, Texture2D backgroundTexture) : base(window, font, backgroundTexture)
        {
            menuItems.Add(new MenuItem(bannerTexture, font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 4) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(menuItemTextures[0], font, "Continue", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[0].Width / 2), (int)menuItems[0].Y_Pos + 350));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Main menu", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 450));
        }

        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (menuItems[1].CheckPress(mouseState))
            {
                return GameElements.State.Run;
            }
            if (menuItems[2].CheckPress(mouseState))
            {
                HighScore.SaveScore();
                GameElements.SuperSpeedsLeft = 1;
                return GameElements.State.Menu;
            }

            return GameElements.State.Paused;
        }
    }
}

