using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MazeGame
{
    /// <summary>
    /// Player class, creates player that can move around level and check collissions with dividers. Holds superspeeds.
    /// </summary>
    class Player : PhysicalObject, ISetSpeed
    {
        public enum Direction { Up, Down, Left, Right, Still }; //Used to determine texture

        private List<TileDivider> surroundingDividers; //Updated by level every frame to contain current surrounding dividers, used to optimize collission checking to minimize lag in bigger levels.
        private const int colliderMargin = 15; //How much player should be allowed to overlap dividers, makes collission rectangle smaller.

        /* Player animations */
        private readonly Animation up, down, left, right, superspeedUp, superspeedDown, superspeedLeft, superspeedRight, superspeedStill;
        private readonly Texture2D stillTexture;
        private Direction currentDir = Direction.Right; //Default, overwritten in first frame.

        /* Superspeed animations */
        private bool superSpeed = false;
        private int superSpeedTimer = 0;
        private Texture2D currentSuperSpeedTexture;

        /// <summary>
        /// Creates new player with given parameters. Assigns new animations.
        /// </summary>
        /// <param name="textures">Player textures for animation.</param>
        /// <param name="superSpeedTextures">Superpeed textures for animation.</param>
        /// <param name="gameTime"></param>
        /// <param name="x_Pos"></param>
        /// <param name="y_Pos"></param>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public Player(Texture2D[] textures, Texture2D[] superSpeedTextures, GameTime gameTime, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(textures[0], x_Pos, y_Pos, x_Speed, y_Speed)
        {
            up = new Animation(this, gameTime, new Texture2D[] { textures[4], textures[5] }, 10);
            right = new Animation(this, gameTime, new Texture2D[] { textures[1], textures[2] }, 10);
            left = new Animation(this, gameTime, new Texture2D[] { textures[0], textures[3] }, 10);
            down = new Animation(this, gameTime, new Texture2D[] { textures[7], textures[8] }, 10);
            stillTexture = textures[6];

            currentSuperSpeedTexture = superSpeedTextures[0];

            superspeedUp = new Animation(gameTime, new Texture2D[] { superSpeedTextures[21], superSpeedTextures[22], superSpeedTextures[23], superSpeedTextures[24], superSpeedTextures[25] }, 5);
            superspeedRight = new Animation(gameTime, new Texture2D[] { superSpeedTextures[7], superSpeedTextures[8], superSpeedTextures[9], superSpeedTextures[10], superSpeedTextures[11], superSpeedTextures[12], superSpeedTextures[13] }, 5);
            superspeedLeft = new Animation(gameTime, new Texture2D[] { superSpeedTextures[0], superSpeedTextures[1], superSpeedTextures[2], superSpeedTextures[3], superSpeedTextures[4], superSpeedTextures[5], superSpeedTextures[6] }, 5);
            superspeedDown = new Animation(gameTime, new Texture2D[] { superSpeedTextures[14], superSpeedTextures[15], superSpeedTextures[16], superSpeedTextures[17], superSpeedTextures[18], superSpeedTextures[19], superSpeedTextures[20] }, 5);
            superspeedStill = new Animation(gameTime, new Texture2D[] { superSpeedTextures[14], superSpeedTextures[15], superSpeedTextures[16], superSpeedTextures[17], superSpeedTextures[18], superSpeedTextures[19], superSpeedTextures[20] }, 5);
        }

        /// <summary>
        /// Updates player by checking keyboard input. 
        /// Checks if player collides with any nearby dividers and stops player if there's collission, if player moves outside of specified window area
        /// Level-Directions are returned for level to process.
        /// Current direction is updated for Draw method to utilize in texture updating.
        /// If superspeed input is given superspeed bool is updated and speed of player and level is updated.
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public List<Level.Direction> Update(GameWindow window)
        {
            List<Level.Direction> directions = new List<Level.Direction>();

            currentDir = Direction.Still; //Direction is set to still if there's no movement.

            KeyboardState keyboardInput = Keyboard.GetState();
            if (keyboardInput.IsKeyDown(Keys.Space) && GameElements.SuperSpeedsLeft != 0 && !superSpeed)
            {
                superSpeedTimer = 7 * 60;
                superSpeed = true;
                GameElements.SuperSpeedsLeft--;
            }
            if (superSpeedTimer <= 0)
            {
                superSpeed = false;
                SetSpeed(GameElements.MainXSpeed, GameElements.MainYSpeed);
                GameElements.Level.SetSpeed(GameElements.MainXSpeed, GameElements.MainYSpeed);
            }
            else
            {
                superSpeedTimer--;
                SetSpeed(GameElements.MainXSpeed + 7, GameElements.MainYSpeed + 7);
                GameElements.Level.SetSpeed(GameElements.MainXSpeed + 7, GameElements.MainYSpeed + 7);
            }
            if (keyboardInput.IsKeyDown(Keys.W) && !keyboardInput.IsKeyDown(Keys.S) && !keyboardInput.IsKeyDown(Keys.Down) || keyboardInput.IsKeyDown(Keys.Up) && !keyboardInput.IsKeyDown(Keys.Down) && !keyboardInput.IsKeyDown(Keys.S))
            {
                position.Y -= speed.Y;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (CheckCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider == GameElements.Level.EndPortal) //Easier solution but not the most elegant. Player tends not not end up exactly 15 pixels within portal.
                {
                    position.Y += speed.Y;
                }
                else if (collider != null)
                {
                    position.Y = (float)(collider.Y_Pos + collider.Height - colliderMargin);
                }
                else
                {
                    if (position.Y < window.ClientBounds.Height * 0.30)
                    {
                        position.Y += speed.Y;
                        directions.Add(Level.Direction.Down); //Direction that level should move, inverted to player movement
                    }

                    currentDir = Direction.Up;
                }
            }
            if (keyboardInput.IsKeyDown(Keys.S) && !keyboardInput.IsKeyDown(Keys.W) && !keyboardInput.IsKeyDown(Keys.Up) || keyboardInput.IsKeyDown(Keys.Down) && !keyboardInput.IsKeyDown(Keys.Up) && !keyboardInput.IsKeyDown(Keys.W))
            {
                position.Y += speed.Y;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (CheckCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider == GameElements.Level.EndPortal) //Easier solution but not the most elegant. Player tends not not end up exactly 15 pixels within portal.
                {
                    position.Y -= speed.Y;
                }
                else if (collider != null)
                {
                    position.Y = (float)(collider.Y_Pos - texture.Height + colliderMargin);
                }
                else
                {
                    if (position.Y > window.ClientBounds.Height * 0.70 - texture.Width)
                    {
                        position.Y -= speed.Y;
                        directions.Add(Level.Direction.Up); //Direction that level should move, inverted to player movement
                    }

                    currentDir = Direction.Down;
                }
            }
            if (keyboardInput.IsKeyDown(Keys.A) && !keyboardInput.IsKeyDown(Keys.D) && !keyboardInput.IsKeyDown(Keys.Right) || keyboardInput.IsKeyDown(Keys.Left) && !keyboardInput.IsKeyDown(Keys.Right) && !keyboardInput.IsKeyDown(Keys.D))
            {
                position.X -= speed.X;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (CheckCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider == GameElements.Level.EndPortal) //Easier solution but not the most elegant. Player tends to not end up exactly 15 pixels within portal.
                {
                    position.X += speed.X;
                }
                else if (collider != null)
                {
                    position.X = (float)(collider.X_Pos + collider.Width - colliderMargin); //Reverse effect of position change
                }
                else
                {
                    if (position.X < window.ClientBounds.Width * 0.20)
                    {
                        position.X += speed.X;
                        directions.Add(Level.Direction.Right); //Direction that level should move, inverted to player movement
                    }

                    currentDir = Direction.Left;
                }
            }
            if (keyboardInput.IsKeyDown(Keys.D) && !keyboardInput.IsKeyDown(Keys.A) && !keyboardInput.IsKeyDown(Keys.Left) || keyboardInput.IsKeyDown(Keys.Right) && !keyboardInput.IsKeyDown(Keys.Left) && !keyboardInput.IsKeyDown(Keys.A))
            {
                position.X += speed.X;
                TileDivider collider = null;

                foreach (TileDivider tileDivider in surroundingDividers)
                {
                    if (CheckCollision(tileDivider))
                    {
                        collider = tileDivider;
                        break;
                    }
                }
                if (collider == GameElements.Level.EndPortal) //Easier solution but not the most elegant. Player tends not not end up exactly 15 pixels within portal.
                {
                    position.X -= speed.X;
                }
                else if (collider != null)
                {
                    position.X = (float)(collider.X_Pos - texture.Width + colliderMargin); //Reverse effect of position change
                }
                else
                {
                    if (position.X > window.ClientBounds.Width * 0.80 - texture.Height)
                    {
                        position.X -= speed.X;
                        directions.Add(Level.Direction.Left); //Direction that level should move, inverted to player movement
                    }

                    currentDir = Direction.Right;
                }
            }


            return directions;
        }

        /// <summary>
        /// Sets speed of player.
        /// </summary>
        /// <param name="x_Speed"></param>
        /// <param name="y_Speed"></param>
        public void SetSpeed(double x_Speed, double y_Speed)
        {
            speed.X = (float)x_Speed;
            speed.Y = (float)y_Speed;
        }

        /// <summary>
        /// Checks collission with any other PhysicalObject, returns bool.
        /// </summary>
        /// <param name="other">PhysicalObject to check collission with.</param>
        /// <returns></returns>
        public override bool CheckCollision(PhysicalObject other)
        {
            Rectangle myRect = new Rectangle(Convert.ToInt32(position.X + colliderMargin), Convert.ToInt32(position.Y + colliderMargin), Convert.ToInt32(texture.Width - (colliderMargin * 2)), Convert.ToInt32(texture.Height - (colliderMargin * 2))); //Own object, with room on each side.
            if (other == GameElements.Level.EndPortal)
            {
                Rectangle otherRect1 = new Rectangle();
                Rectangle otherRect2 = new Rectangle();
                switch (GameElements.Level.EndPortal.PortalType)
                {
                    case EndPortal.Type.Top:
                        otherRect1 = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos), Convert.ToInt32(60), Convert.ToInt32(other.Height)); //Other object
                        otherRect2 = new Rectangle(Convert.ToInt32(other.X_Pos + 240), Convert.ToInt32(other.Y_Pos), Convert.ToInt32(60), Convert.ToInt32(other.Height)); //Other object
                        break;

                    case EndPortal.Type.Right:
                        otherRect1 = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos + 100), Convert.ToInt32(other.Width), Convert.ToInt32(60)); //Other object
                        otherRect2 = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos + 340), Convert.ToInt32(other.Width), Convert.ToInt32(60)); //Other object
                        break;

                    case EndPortal.Type.Left:
                        otherRect1 = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos + 100), Convert.ToInt32(other.Width), Convert.ToInt32(60)); //Other object
                        otherRect2 = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos + 340), Convert.ToInt32(other.Width), Convert.ToInt32(60)); //Other object
                        break;

                    case EndPortal.Type.Bottom:
                        otherRect1 = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos), Convert.ToInt32(60), Convert.ToInt32(other.Height)); //Other object
                        otherRect2 = new Rectangle(Convert.ToInt32(other.X_Pos + 240), Convert.ToInt32(other.Y_Pos), Convert.ToInt32(60), Convert.ToInt32(other.Height)); //Other object
                        break;
                }
                return myRect.Intersects(otherRect1) || myRect.Intersects(otherRect2);
            }
            else
            {
                Rectangle otherRect = new Rectangle(Convert.ToInt32(other.X_Pos), Convert.ToInt32(other.Y_Pos), Convert.ToInt32(other.Width), Convert.ToInt32(other.Height)); //Other object
                return myRect.Intersects(otherRect);
            }
        }

        /// <summary>
        /// Runs animation updates before drawing playertexture and superspeedtextures.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (currentDir)
            {
                case Direction.Left: { left.Update(); } break; //Uses textures 0 and 3
                case Direction.Right: { right.Update(); } break; //Uses textires 1 and 2
                case Direction.Up: { up.Update(); } break;
                case Direction.Down: { down.Update(); } break;
                case Direction.Still: { texture = stillTexture; break; }
            }

            base.Draw(spriteBatch);

            if (superSpeed)
            {
                Vector2 superSpeedPos = new Vector2();

                switch (currentDir)
                {
                    case Direction.Left: { superspeedLeft.Update(out currentSuperSpeedTexture); superSpeedPos = new Vector2(position.X, position.Y + 110); } break; //Uses textures 0 and 3
                    case Direction.Right: { superspeedRight.Update(out currentSuperSpeedTexture); superSpeedPos = new Vector2(position.X - 35, position.Y + 110); } break; //Uses textires 1 and 2
                    case Direction.Up: { superspeedUp.Update(out currentSuperSpeedTexture); superSpeedPos = new Vector2(position.X - 5, position.Y + 110); } break;
                    case Direction.Down: { superspeedDown.Update(out currentSuperSpeedTexture); superSpeedPos = new Vector2(position.X - 5, position.Y + 110); } break;
                    case Direction.Still: { superspeedStill.Update(out currentSuperSpeedTexture); superSpeedPos = new Vector2(position.X - 5, position.Y + 110); } break;
                }

                spriteBatch.Draw(currentSuperSpeedTexture, superSpeedPos, Color.White);
            }
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
