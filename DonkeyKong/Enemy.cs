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
    internal class Enemy
    {

        public Texture2D enemyTex;
        public Vector2 enemyPos;
        public Vector2 enemySpeed;
        public Rectangle enemyRect;
        public bool alive;

        public Enemy(Texture2D enemyTex, Vector2 enemySpeed, Vector2 enemyPos)
        {
            this.enemyTex = enemyTex;
            this.enemyPos = enemyPos;
            this.enemySpeed = enemySpeed;
            alive = true;
        }

        public void Update()
        {
            enemyPos = enemyPos + enemySpeed;
            enemyRect = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, enemyTex.Width, enemyTex.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTex, enemyPos, Color.White);
        }

        public bool IsTouched(int x, int y)
        {
            bool isTouched = false;
            Rectangle rect = new Rectangle((int)enemyPos.X, (int)enemyPos.Y, enemyTex.Width, enemyTex.Height);

            if (rect.Contains(x, y))
            {
                isTouched = true;
                alive = false;
            }

            return isTouched;

        }
    }

}
