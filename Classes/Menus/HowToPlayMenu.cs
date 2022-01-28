using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MazeGame
{
    class HowToPlayMenu : Menu
    {
        public HowToPlayMenu(GameWindow window, SpriteFont fontBig, SpriteFont fontSmall, Texture2D[] menuControlTextures, Texture2D[] textBoxTextures, Texture2D bannerTexture, Texture2D backgroundTexture) : base(window, fontBig, backgroundTexture)
        {
            menuItems.Add(new MenuItem(bannerTexture, fontBig, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (bannerTexture.Width / 2), (window.ClientBounds.Height / 8) - (bannerTexture.Height / 2)));
            menuItems.Add(new MenuItem(menuControlTextures[1], fontSmall, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (textBoxTextures[0].Width / 2), (int)menuItems[0].Y_Pos + bannerTexture.Height / 2 - menuControlTextures[1].Height / 2));

            string[] rows = new string[9];
            rows[0] = "Goal: finish as many levels as ";
            rows[1] = "possible.";
            rows[2] = "Walk around with WASD or Arrow keys.";
            rows[3] = "Activate superspeed by pressing space.";
            rows[4] = "Levels are finished by finding the";
            rows[5] = "end portal before time is up.";
            rows[6] = "Each level gets increasingly more";
            rows[7] = "difficult as size increases";
            rows[8] = "exponentially.";

            menuItems.Add(new MenuItem(textBoxTextures[0], fontSmall, rows[0], MenuItem.Alignment.Left, (window.ClientBounds.Width / 2) - (textBoxTextures[0].Width / 2), (int)menuItems[0].Y_Pos + 250));
            
            for(int i = 1; i < rows.Length - 1; i++)
            {
                menuItems.Add(new MenuItem(textBoxTextures[1], fontSmall, rows[i], MenuItem.Alignment.Left, (window.ClientBounds.Width / 2) - (textBoxTextures[2].Width / 2), (int)menuItems[2].Y_Pos + i * 70));
            }

            menuItems.Add(new MenuItem(textBoxTextures[2], fontSmall, rows[rows.Length - 1], MenuItem.Alignment.Left, (window.ClientBounds.Width / 2) - (textBoxTextures[2].Width / 2), (int)menuItems[2].Y_Pos + (rows.Length - 1) * 70 ));
        }

        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();

            if (menuItems[1].CheckPress(mouseState))
            {
                return GameElements.State.Menu;
            }
            
            return GameElements.State.HowTo;
        }
    }
}
