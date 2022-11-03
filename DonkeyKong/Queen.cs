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
    public class Queen
    {
        public Texture2D queenTex;
        public Vector2 queenPos;
        public Rectangle queenRect;

        public Queen(Texture2D queenTex, Vector2 queenPos)
        {
            this.queenTex = queenTex;
            this.queenPos = queenPos;
        }

        public void Update()
        {
            queenRect = new Rectangle((int)queenPos.X, (int)queenPos.Y, queenTex.Width, queenTex.Height);
        }

        public void Draw(SpriteBatch sb3)
        {
            sb3.Draw(queenTex, new Vector2(500, 15), Color.White);
        }
    }
}
