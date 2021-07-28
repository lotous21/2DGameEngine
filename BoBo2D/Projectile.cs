using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BoBo2D
{
    class Projectile : Componenet
    {
        public Projectile(Vector2 location, Texture2D image, Vector2 velocity, Color color)
        {
            this.Transform.Position = location;
            this.Image = image;
            this.Transform.Velocity = velocity;
            this.Bounds = new Rectangle(0, 0, 45, 10);
            this.ImageColor = color;
            this.Drawable = true;
        }

        public void Update(float elapsed)
        {
            this.Transform.Position += this.Transform.Velocity * elapsed;
            this.Bounds.X = (int)Transform.Position.X;
            this.Bounds.Y = (int)Transform.Position.Y;
        }
    }
}
