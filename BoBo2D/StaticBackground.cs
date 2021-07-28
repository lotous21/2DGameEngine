using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class StaticBackground : Componenet
    {

        public StaticBackground(Vector2 location, Vector2 velocity, Texture2D image, Color color)
        {
            this.Transform.Position = location;
            this.Transform.Velocity = velocity;
            this.Image = image;
            this.ImageColor = color;
            this.Drawable = true;

            this.Bounds = new Rectangle(0, 0, 1280, 720);

        }

        public void Update(float elapsed)
        {
            this.Transform.Position += this.Transform.Velocity * elapsed;
            this.Bounds.X = (int)Transform.Position.X;
            this.Bounds.Y = (int)Transform.Position.Y;

            if (this.Bounds.Right < 0)
            {
                this.Transform.Position.X = 1281;
            }
        }
    }
}
