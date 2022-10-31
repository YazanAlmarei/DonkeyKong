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
        private Texture2D _texture;

        public Vector2 Position;
        Rectangle rectangle;

        public float Speed = 5f;

        public Player(Texture2D texture)
        {
            _texture = texture;
        }



        public void Update()
        {

            rectangle = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

            /*if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Position.Y -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position.Y += Speed;
            }*/
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X += Speed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }
    }
}