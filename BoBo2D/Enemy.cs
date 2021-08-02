using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class Enemy : Componenet
    {
        public Enemy(Vector2 location, Texture2D image, int velocity, Color color)
        {
            this.ImageColor = color;
            this.Transform.Position = location;
            this.Image = image;
            this.Transform.Velocity = new Vector2(-velocity, 0);
            this.Bounds = new Rectangle(0, 0, 28, 28);
            this.Enable();
        }

        public override void Update(float elapsed)
        {
            this.Transform.Position += this.Transform.Velocity * elapsed;
            this.Bounds.X = (int)Transform.Position.X;
            this.Bounds.Y = (int)Transform.Position.Y;

            if (this.Transform.Position.X < -20)
            {
                this.Disable();
            }
        }
    }
}
