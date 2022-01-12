using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame.Classes
{
    class Player : PhysicalObject
    {
        public enum LevelDirection { Up, Down, Left, Right }; //Used to return what direction level should move
        public enum PlayerDirection { Up, Down, Left, Right, Still }; //Used to determine texture

        private List<TileDivider> surroundingDividers; //Updated every frame to contain current surrounding dividers
        private const int colliderMargin = 15; //How much player should be allowed to overlap dividers, makes collission rectangle smaller.

        private readonly InfiniteAnimation up, down, left, right;
        private readonly Texture2D stillTexture;
        private PlayerDirection currentDir = PlayerDirection.Right; //Default, overwritten in first frame.
        
        public Player(Texture2D[] textures, GameTime gameTime, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(textures[0], x_Pos, y_Pos, x_Speed, y_Speed)
        {
            up = new InfiniteAnimation(this, gameTime, new Texture2D[] { textures[4], textures[5] }, 10);
            right = new InfiniteAnimation(this, gameTime, new Texture2D[] { textures[1], textures[2] }, 10);
            left = new InfiniteAnimation(this, gameTime, new Texture2D[] { textures[0], textures[3] }, 10);
            down = new InfiniteAnimation(this, gameTime, new Texture2D[] { textures[7], textures[8] }, 10);
            stillTexture = textures[6];
        }
         
        /// <summary>
        /// Updates player. Checks if player collides with any dividers and stops player if there's collission, if
        /// player moves outside of specified window area level is given new direction to move. If player moves moveDir
        /// direction is set a new direction which draw-method later uses to determine texture.
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public List<LevelDirection> Update(GameWindow window)
        {
            List<LevelDirection> directions = new List<LevelDirection>();

            currentDir = PlayerDirection.Still; //Direction is set to still if there's no movement.

            KeyboardState keyboardInput = Keyboard.GetState();
            if (keyboardInput.IsKeyDown(Keys.W) && !keyboardInput.IsKeyDown(Keys.S))
            {
                position.Y -= speed.Y;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider != null)
                {
                    position.Y = (float)(collider.Y_Pos + collider.Height - colliderMargin);
                }
                else
                {
                    if (position.Y < window.ClientBounds.Height * 0.30)
                    {
                        position.Y += speed.Y;
                        directions.Add(LevelDirection.Down); //Direction that level should move, inverted to player movement
                    }

                    currentDir = PlayerDirection.Up;
                }
            }
            if (keyboardInput.IsKeyDown(Keys.S) && !keyboardInput.IsKeyDown(Keys.W))
            {
                position.Y += speed.Y;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider != null)
                {
                    position.Y = (float)(collider.Y_Pos - texture.Height + colliderMargin);
                }
                else
                {
                    if (position.Y > window.ClientBounds.Height * 0.70 - texture.Width)
                    {
                        position.Y -= speed.Y;
                        directions.Add(LevelDirection.Up); //Direction that level should move, inverted to player movement
                    }

                    currentDir = PlayerDirection.Down;
                }
            }
            if (keyboardInput.IsKeyDown(Keys.A) && !keyboardInput.IsKeyDown(Keys.D))
            {
                position.X -= speed.X;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider != null)
                {
                    position.X = (float)(collider.X_Pos + collider.Width - colliderMargin); //Reverse effect of position change
                }
                else
                {
                    if (position.X < window.ClientBounds.Width * 0.20)
                    {
                        position.X += speed.X;
                        directions.Add(LevelDirection.Right); //Direction that level should move, inverted to player movement
                    }

                    currentDir = PlayerDirection.Left;
                }
            }
            if (keyboardInput.IsKeyDown(Keys.D) && !keyboardInput.IsKeyDown(Keys.A))
            {
                position.X += speed.X;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (checkCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider != null)
                {
                    position.X = (float)(collider.X_Pos - texture.Width + colliderMargin); //Reverse effect of position change
                }
                else
                {
                    if (position.X > window.ClientBounds.Width * 0.80 - texture.Height)
                    {
                        position.X -= speed.X;
                        directions.Add(LevelDirection.Left); //Direction that level should move, inverted to player movement
                    }

                    currentDir = PlayerDirection.Right;
                }
            }

            return directions;
        }

        public override bool checkCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(position.X + colliderMargin) , Convert.ToInt32(position.Y + colliderMargin), Convert.ToInt32(texture.Width - (colliderMargin * 2)), Convert.ToInt32(texture.Height - (colliderMargin * 2))); //Own object, with 10 pixel room on each side.
            Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height)); //Other object

            return myRect.Intersects(otherRect);
        }

        /// <summary>
        /// Updates texture before drawing.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            switch(currentDir)
            {
                case PlayerDirection.Left: //Uses textures 0 and 3
                    left.Update(spriteBatch);
                    break;
                case PlayerDirection.Right: //Uses textires 1 and 2
                    right.Update(spriteBatch);
                    break;
                case PlayerDirection.Up:
                    up.Update(spriteBatch);
                    break;
                case PlayerDirection.Down:
                    down.Update(spriteBatch);
                    break;
                case PlayerDirection.Still:
                    texture = stillTexture;
                    break;
            }

            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Property for surrounding dividers, used to optimally check if player collides with barrier
        /// </summary>
        public List<TileDivider> SurroundingDividers
        {
            set { surroundingDividers = value; }
        }
    }
}
