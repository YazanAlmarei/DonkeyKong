using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class Player
    {
        public Vector2 position;
        public Texture2D texture;
        //Rectangle rectangle;

        int screenWidth = 1000;
        int screenHeight = 640;

        Vector2 destination;
        Vector2 direction;
        float speed = 100.0f;
        bool moving = false;

        bool climbUp = false;
        bool climbDown = false;

        public Player(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * 40.0f;

            
            if (!Game1.GetTileAtPosition(newDestination))
            {
                destination = newDestination;
                moving = true;
            }
        }

        public void Update(GameTime gameTime)
        {

            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (!moving)
            {
                
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    ChangeDirection(new Vector2(-1, 0));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    ChangeDirection(new Vector2(1, 0));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    ChangeDirection(new Vector2(0, -1));
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    ChangeDirection(new Vector2(0, 1));
                }

                /*if(rectangle.X <= 0)
                {
                    rectangle.X = 0;
                }

                if(rectangle.X + texture.Width >= screenWidth)
                {
                    rectangle.X = screenWidth - texture.Width;
                }

                if(rectangle.Y <= 0)
                {
                    rectangle.Y = 0;
                }
                if (rectangle.Y + texture.Height >= screenHeight)
                {
                    rectangle.Y = screenHeight - texture.Height;
                }*/


            }
            else
            {
                position += direction * speed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;

                
                if (Vector2.Distance(position, destination) < 1)
                {
                    position = destination;
                    moving = false;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}