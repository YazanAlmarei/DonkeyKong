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
    public class Monkey
    {
        public Texture2D monkeyTex;
        public Vector2 monkeyPos;
        public Rectangle monkeyRect;

        public Monkey(Texture2D monkeyTex, Vector2 monkeyPos)
        {
            this.monkeyTex = monkeyTex;
            this.monkeyPos = monkeyPos;
        }

        public void Update()
        {
            monkeyRect = new Rectangle((int)monkeyPos.X, (int)monkeyPos.Y, monkeyTex.Width, monkeyTex.Height);
        }

        public void Draw(SpriteBatch sb2)
        {
            sb2.Draw(monkeyTex, new Vector2(400, 100), Color.White);
        }

    }
}
