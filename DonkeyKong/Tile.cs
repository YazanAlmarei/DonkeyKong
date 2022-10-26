using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    public class Tile
    {
        public Vector2 bridgePos;
        public Texture2D bridgeTileTex;
        public bool bridgee;

        public Tile(Texture2D bridgeTileTex, Vector2 bridgePos, bool bridgee)
        {
            this.bridgeTileTex = bridgeTileTex;
            this.bridgePos = bridgePos;
            this.bridgee = bridgee;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bridgeTileTex, bridgePos, Color.White);
        }


    }
}
