using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class ClearedMenu : Menu
    {
        public ClearedMenu(GameWindow window, SpriteFont font, Texture2D[] menuItemTextures, Texture2D bannerTexture, Texture2D backgroundTexture, int score):base(window, font, backgroundTexture)
        {
            menuItems.Add(new MenuItem(bannerTexture, font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 4) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(menuItemTextures[0], font, "Score:" + score, MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[0].Width / 2), (int)menuItems[0].Y_Pos + 280));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Continue", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 370));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "Main menu", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 460));
        }

        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (menuItems[2].CheckPress(mouseState))
            {
                GameElements.RemainingTime = GameElements.Level.Timer.TimeLeft;
                return GameElements.State.Run;
            }
            if(menuItems[3].CheckPress(mouseState))
            {
                HighScore.SaveScore();
                return GameElements.State.Menu;
            }

            return GameElements.State.Cleared;
        }
    }
}
