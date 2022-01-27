using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class NameChoosingMenu : Menu
    {
        private bool isWriting = false;
        private int markerDelay = 20;
        private bool displayMarker = false;
        KeyboardState oldKeyboardstate = new KeyboardState();

        public NameChoosingMenu(GameWindow window, SpriteFont font, Texture2D[] menuItemTextures, Texture2D[] menuControlTextures, Texture2D backgroundTexture, List<string> recentNames) : base(window, font, backgroundTexture)
        {
            menuItems.Add(new MenuItem(menuItemTextures[6], font, "Create new player or choose recent player", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[6].Width / 2), (window.ClientBounds.Height / 6) - (menuItemTextures[5].Height / 2)));
            menuItems.Add(new MenuItem(menuItemTextures[1], font, "New player:", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[1].Width / 2), (int)menuItems[0].Y_Pos + 150));
            menuItems.Add(new MenuItem(menuItemTextures[3], font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[3].Width / 2), (int)menuItems[0].Y_Pos + 250));
            menuItems.Add(new MenuItem(menuControlTextures[0], font, "", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[3].Width / 2) + menuItemTextures[3].Width + 30, (int)menuItems[0].Y_Pos + 250));
            menuItems.Add(new MenuItem(menuItemTextures[3], font, "Recent players:", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[3].Width / 2), (int)menuItems[0].Y_Pos + 350));

            for (int i = 0; i < recentNames.Count; i++)
            {
                menuItems.Add(new MenuItem(menuItemTextures[3], font, recentNames[i], MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[3].Width / 2), (int)menuItems[0].Y_Pos + 450 + (i * 100)));
            }

            menuItems.Add(new MenuItem(menuItemTextures[0], font, "Back", MenuItem.Alignment.Mid, (window.ClientBounds.Width / 2) - (menuItemTextures[0].Width / 2), (int)menuItems[0].Y_Pos + 750));
        }

        public override GameElements.State Update()
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();
            char toAdd;

            /* Remove marker */
            if(menuItems[2].Text.Length != 0)
            {
                if (menuItems[2].Text[menuItems[2].Text.Length - 1] == '|')
                {
                    menuItems[2].Text = menuItems[2].Text.Remove(menuItems[2].Text.Length - 1);
                }
            }

            /* Check if textbox is pressed */
            if (menuItems[2].CheckPress(mouseState))
            {
                isWriting = !isWriting;
            }

            /* Update marker */
            if (isWriting)
            {
                if (markerDelay == 0)
                {
                    markerDelay = 20;
                    displayMarker = !displayMarker;
                }

                markerDelay--;
            }
            else
            {
                displayMarker = false;
            }

            /* Take text input */
            if (InputText.TryConvertKeyboardInput(keyboardState, oldKeyboardstate, out toAdd))
            {
                if(toAdd == 8)
                {
                    if(menuItems[2].Text.Length > 0)
                    {
                        menuItems[2].Text = menuItems[2].Text.Remove(menuItems[2].Text.Length - 1);
                    }
                }
                else
                {
                    if(menuItems[2].Text.Length < 18)
                    {
                        menuItems[2].Text += toAdd;
                    }
                }
            }
            oldKeyboardstate = keyboardState;

            /* Recalculate textposition of input field */
            menuItems[2].ReCenterText();

            /* Checks presses */
            if (menuItems[3].CheckPress(mouseState))
            {
                if(menuItems[2].Text != "" )
                {
                    HighScore.CurrentScore = new Score(menuItems[2].Text, 0, DateTime.Now);

                    return GameElements.State.Run;
                }
            }
            for (int i = 5; i < menuItems.Count - 1; i++)
            {
                if(menuItems[i].CheckPress(mouseState))
                {
                    HighScore.CurrentScore = new Score(menuItems[i].Text, 0, DateTime.Now);

                    return GameElements.State.Run;
                }
            }
            if (menuItems[menuItems.Count - 1].CheckPress(mouseState))
            {
                return GameElements.State.Menu;
            }

            /* Write marker */
            if (displayMarker)
            {
                menuItems[2].Text += "|";
            }

            return GameElements.State.NameChoosing;
        }
    }
}
