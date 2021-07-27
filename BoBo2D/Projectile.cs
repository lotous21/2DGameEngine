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
        public Vector2 Velocity;
        public Rectangle BoundsMove;

        public Projectile(Vector2 location, Texture2D image, Vector2 velocity, Color color)
        {
            this.Transform.Position = location;
            this.Image = image;
            this.Velocity = velocity;
            this.BoundsMove = new Rectangle(0, 0, 45, 10);
            this.ImageColor = color;
        }

        public void Update(float elapsed)
        {
            this.Transform.Position += this.Velocity * elapsed;
            this.BoundsMove.X = (int)Transform.Position.X;
            this.BoundsMove.Y = (int)Transform.Position.Y;
        }
    }
}
