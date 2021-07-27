using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class SpaceShip : GameObjects
    {
        public int BulletCount;
        public Vector2 Velocity;
        public Rectangle BoundsMove;
        public Vector2 SpacePosition;

        public SpaceShip(Vector2 location, Texture2D image, Color color)
        {
            this.SpacePosition = location;
            this.Image = image;
            this.ImageColor = color;
            this.BoundsMove = new Rectangle(0, 0, 128, 60);
            this.BulletCount = 5;
            this.Position = SpacePosition;
            this.Bounds = BoundsMove;
        }

        public void Update(float elapsed)
        {
            this.Position = SpacePosition;
            this.Bounds = BoundsMove;

            this.SpacePosition += this.Velocity * elapsed;
            this.BoundsMove.X = (int)SpacePosition.X;
            this.BoundsMove.Y = (int)SpacePosition.Y;

            if (this.BoundsMove.Top < 0)
            {
                this.SpacePosition.Y = 0;
            }
            if (this.BoundsMove.Bottom > 720)
            {
                this.SpacePosition.Y = 720 - this.BoundsMove.Height;
            }
        }
    }
}
