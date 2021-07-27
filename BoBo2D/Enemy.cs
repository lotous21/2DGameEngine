using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class Enemy : Componenet
    {
        public Vector2 Location;
        public Vector2 Velocity;
        private Texture2D image;
        public Rectangle Bounds; // x,y,w,h
        public bool IsVisible;

        public Enemy(Vector2 location, Texture2D image, int velocity)
        {
            this.Transform.Position = location;
            this.image = image;
            this.IsVisible = true;
            this.Velocity = new Vector2(-velocity, 0);
            this.Bounds = new Rectangle(0, 0, 28, 28);

        }

        public void Update(float elapsed)
        {
            this.Transform.Position += this.Velocity * elapsed;
            this.Bounds.X = (int)Location.X;
            this.Bounds.Y = (int)Location.Y;

            if (this.Location.X < -20)
            {
                this.IsVisible = false;
            }
        }
    }
}
