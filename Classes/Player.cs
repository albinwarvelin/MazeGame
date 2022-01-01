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
        private Texture2D[] textures;
        private int textureSwitchTimer = 10;
        private PlayerDirection currentDir = PlayerDirection.Right; //Default, overwritten in first frame.
        private PlayerDirection lastDir = PlayerDirection.Right; //Default, overwritten in first frame.
        
        private List<TileDivider> surroundingDividers; //Updated every frame to contain current surrounding dividers

        public Player(Texture2D[] textures, double x_Pos, double y_Pos, double x_Speed, double y_Speed) : base(textures[0], x_Pos, y_Pos, x_Speed, y_Speed)
        {
            this.textures = textures;
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
                    position.Y = (float)(collider.Y_Pos + collider.Height);
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
                    position.Y = (float)(collider.Y_Pos - texture.Height);
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
                    position.X = (float)(collider.X_Pos + collider.Width); //Reverse effect of position change
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
                    position.X = (float)(collider.X_Pos - texture.Width); //Reverse effect of position change
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
        
        /// <summary>
        /// Updates texture before drawing.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            switch(currentDir)
            {
                case PlayerDirection.Left: //Uses textures 0 and 3
                    if(texture == textures[3])
                    {
                        if(textureSwitchTimer == 0)
                        {
                            texture = textures[0];
                            textureSwitchTimer = 10;
                        }
                    }
                    else if(texture == textures[0])
                    {
                        if (textureSwitchTimer == 0)
                        {
                            texture = textures[3];
                            textureSwitchTimer = 10;
                        }
                    }
                    else
                    {
                        texture = textures[3];
                        textureSwitchTimer = 10;
                    }

                    textureSwitchTimer--;
                    break;
                case PlayerDirection.Right: //Uses textires 1 and 2
                    if(texture == textures[2])
                    {
                        if (textureSwitchTimer == 0)
                        {
                            texture = textures[1];
                            textureSwitchTimer = 10;
                        }
                    }
                    else if (texture == textures[1])
                    {
                        if (textureSwitchTimer == 0)
                        {
                            texture = textures[2];
                            textureSwitchTimer = 10;
                        }
                    }
                    else
                    {
                        texture = textures[2];
                        textureSwitchTimer = 10;
                    }

                    textureSwitchTimer--;
                    break;
                case PlayerDirection.Up:
                    textureSwitchTimer = 10; //Resets timer
                    break;
                case PlayerDirection.Down:
                    textureSwitchTimer = 10; //Resets timer
                    break;
                case PlayerDirection.Still:
                    textureSwitchTimer = 10; //Resets timer
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
