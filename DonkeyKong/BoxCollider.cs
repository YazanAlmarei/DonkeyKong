using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyKong
{
    internal class BoxCollider
    {
        public Rectangle bounds;
        public float scale; // This variable is used to resize the bounds of the collider

        private Rectangle usedBounds; // Used in calculations

        public BoxCollider(Rectangle hitbox)
        {
            bounds = hitbox;
            usedBounds = bounds;
            scale = 1;
        }

        /// <summary>
        /// Check if the two colliders/hitboxes intersect, if yes return true, else return false
        /// </summary>
        /// <param name="anotherCollider"></param>
        /// <returns></returns>
        public bool CheckCollision(BoxCollider anotherCollider)
        {
            // Scale the bounds of the collider to the desired scale
            usedBounds.Width = (int)(bounds.Width * scale);
            usedBounds.Height = (int)(bounds.Height * scale);
            usedBounds.X = bounds.X;
            usedBounds.Y = bounds.Y;

            Rectangle otherCollider = new Rectangle();
            otherCollider.Location = anotherCollider.bounds.Location;
            otherCollider.Size = (anotherCollider.bounds.Size.ToVector2() * anotherCollider.scale).ToPoint();

            return usedBounds.Intersects(otherCollider);
        }
    }
}
