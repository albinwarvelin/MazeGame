﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    class PrintText
    {
        private readonly SpriteFont font;
        private Vector2 position;

        public PrintText(SpriteFont font, int x_Pos, int y_Pos)
        {
            this.font = font;
            position = new Vector2(x_Pos, y_Pos);
        }

        public void Print(string text, SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawString(font, text, position, color);
        }

        public int X_Pos
        {
            get { return (int)position.X; }
            set { position.X = value; }
        }
    }
}