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
        public Rectangle playerRec;

        Vector2 destination;
        Vector2 direction;
        float speed = 100.0f;
        bool moving = false;

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
            playerRec = new Rectangle((int)(position.X), (int)(position.Y), texture.Width, texture.Height);

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