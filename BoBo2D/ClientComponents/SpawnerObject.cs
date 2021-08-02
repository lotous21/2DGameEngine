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
        Timer fireTimer;

        public bool IsFire;
        public bool IsDamage;
        public bool IsReload;
        public bool IsShield;

        public Texture2D BulletImage;
        public List <Projectile> ProjectilesList = new List<Projectile>();

        public SpawnerObject(Vector2 location, Texture2D image, int vel, Color color, Rectangle bounds, bool fire, bool damage, bool realod, bool shield)
        {
            this.IsFire = fire;
            this.IsDamage = damage;
            this.IsReload = realod;
            this.IsShield = shield;
            this.Bounds = bounds;
            this.Transform.Position = location;
            this.Image = image;
            this.Transform.Velocity = new Vector2(-vel, 0);
            this.ImageColor = color;
            this.Enable();
            this.Drawable = true;
            fireTimer = new Timer(1000);
            if (IsFire)
            {
                this.Bounds = new Rectangle(0, 0, 128, 60);
                fireTimer.Elapsed += delegate
                {
                   if (this.IsEnable())
                    {
                        Projectile p = new Projectile(new Vector2(-50, -50), BulletImage, new Vector2(-500, 0), Color.White);
                        p.Transform.Position = new Vector2(this.Transform.Position.X, this.Transform.Position.Y + 15);
                        ProjectilesList.Add(p);
                        p.Enable();
                    }
                };
            }
        }
        public override void Update(float elapsed)
        {
            this.Transform.Position += this.Transform.Velocity * elapsed;
            this.Bounds.X = (int)this.Transform.Position.X;
            this.Bounds.Y = (int)this.Transform.Position.Y;

            if (this.IsEnable())
            {
                fireTimer.Enabled = true;
            }
            else fireTimer.Enabled = false;
        }

    }
}
