using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class SpaceShip : Componenet
    {
        public int BulletCount;
        public int HP;
        public int Shield;
        int HpRegen;
        int ShieldRegen;

        public SpaceShip(Vector2 location, Texture2D image, Color color)
        {
            this.Transform.Position = location;
            this.Image = image;
            this.ImageColor = color;
            this.Bounds = new Rectangle(0, 0, 128, 60);
            this.BulletCount = 5;
            this.Drawable = true;
            this.HP = 100;
            this.Shield = 100;
        }

        public void Update(float elapsed)
        {
            this.Transform.Position += this.Transform.Velocity * elapsed;
            this.Bounds.X = (int)Transform.Position.X;
            this.Bounds.Y = (int)Transform.Position.Y;

            if (this.Bounds.Top < 0)
            {
                this.Transform.Position.Y = 0;
            }
            if (this.Bounds.Bottom > 720)
            {
                this.Transform.Position.Y = 720 - this.Bounds.Height;
            }

            if (HP > 100)
            {
                HP = 100;
            }
            if (Shield > 100)
            {
                Shield = 100;
            }
        }
    }
}
