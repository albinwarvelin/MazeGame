using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class EndPortal : TileDivider
    {
        private readonly Texture2D topTexture;
        public enum Type { Top, Left, Right, Bottom };
        private readonly Type type;

        public EndPortal(Texture2D[] textures, Type type, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(textures[0], x_Pos, y_Pos, x_Speed, y_Speed)
        {
            topTexture = textures[1];
            this.type = type;
        }

        /// <summary>
        /// Checks if player has won.
        /// </summary>
        public bool CheckWin(Player player)
        {
            return false;
        }

        /// <summary>
        /// Draws toptexture, bottom texture is drawn by base-method. Used to create effect of player walking into tunnel.
        /// </summary>
        public void DrawTop(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(topTexture, position, Color.White);
        }

        public Type PortalType
        {
            get { return type; }
        }
    }
}
