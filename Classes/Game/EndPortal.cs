using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeGame
{
    /// <summary>
    /// Endportal is used as finish in every level.
    /// </summary>
    class EndPortal : TileDivider
    {
        private readonly Texture2D topTexture;
        public enum Type { Top, Left, Right, Bottom };
        private readonly Type type;

        /// <summary>
        /// Creates new endportal.
        /// </summary>
        /// <param name="textures">All endportal textures, both bottom and top textures.</param>
        /// <param name="type">What side portal is on.</param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public EndPortal(Texture2D[] textures, Type type, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(textures[0], x_Pos, y_Pos, x_Speed, y_Speed)
        {
            topTexture = textures[1];
            this.type = type;
        }

        /// <summary>
        /// Checks if player has won by checking if player intersects portal.
        /// </summary>
        /// <param name="player">Player to check win of.</param>
        /// <returns></returns>
        public bool CheckWin(Player player)
        {
            Rectangle myRect = new Rectangle();
            Rectangle otherRect = new Rectangle((int)player.X_Pos, (int)player.Y_Pos, (int)player.Width, (int)player.Height);

            switch (type)
            {
                case Type.Top:
                    myRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height - 250);
                    break;
                case Type.Right:
                    myRect = new Rectangle((int)position.X + 190, (int)position.Y, texture.Width - 190, texture.Height);
                    break;
                case Type.Left:
                    myRect = new Rectangle((int)position.X, (int)position.Y, texture.Width - 190, texture.Height);
                    break;
                case Type.Bottom:
                    myRect = new Rectangle((int)position.X, (int)position.Y + 255, texture.Width, texture.Height - 255);
                    break;
            }

            return myRect.Intersects(otherRect);
        }

        /// <summary>
        /// Draws toptexture, bottom texture is drawn by base-method. Used to create effect of player walking into tunnel.
        /// </summary>
        public void DrawTop(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(topTexture, position, Color.White);
        }

        /// <summary>
        /// Portaltype property.
        /// </summary>
        public Type PortalType
        {
            get { return type; }
        }
    }
}
