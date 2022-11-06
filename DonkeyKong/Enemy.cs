using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.Cryptography.X509Certificates;

namespace DonkeyKong
{
    internal class Enemy
    {
        public Texture2D texture;
        public Vector2 position;
        float speed;
        Vector2 direction;
        Random rand;

        //Player player;
        

        public Enemy(Texture2D texture, Vector2 position, float speed)
        {
            this.texture = texture;
            this.position = position;

            direction = new Vector2(1, 0);

            rand = new Random();
            this.speed = rand.Next(1, 3);

        }

        public void Update(GameTime gameTime)
        {
            position = position + speed * direction;
            ChangeDirection(direction);

        }

        public void ChangeDirection(Vector2 dir)
        {
            direction = dir;
            Vector2 newDestination = position + direction * 40.0f;

            if (Game1.GetTileAtPosition(newDestination))
            {
                direction = direction * -1;
            }


        }

        public bool CollideEnemy(Rectangle rec)
        {
            bool Collide = false;
            Rectangle enemyRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (enemyRect.Intersects(Player.playerRec))
            {
                Collide = true;
            }
            return Collide;
    }

    public void Draw(SpriteBatch sbEnemy)
        {
            sbEnemy.Draw(texture, position, Color.White);
        }
    }
}


