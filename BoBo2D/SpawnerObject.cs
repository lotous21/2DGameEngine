using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace BoBo2D
{
    class SpawnerObject : Componenet
    {
        public SpawnerObject(Vector2 location, Texture2D image, int vel, Color color, Rectangle bounds)
        {
            this.Bounds = bounds;
            this.Transform.Position = location;
            this.Image = image;
            this.Transform.Velocity = new Vector2(-vel, 0);
            this.ImageColor = color;
            this.Enable();
            this.Drawable = true;
        }
        public void Update(float elapsed)
        {
            this.Transform.Position += this.Transform.Velocity * elapsed;
            this.Bounds.X = (int)this.Transform.Position.X;
            this.Bounds.Y = (int)this.Transform.Position.Y;
        }

    }
}
