using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace BoBo2D
{
    class SpaceShip : Componenet
    {
        public int BulletCount { get; set; }
        public int HP { get; set; }
        public int Shield { get; set; }
        public int MaxBullet;
        public int MovingSpeed;

        Timer hpRegen;
        Timer totalTimer;
        Levels level;

        public int HpRegenSpeed;
        public int ShieldRegenSpeed { get; set; }

        int totalTimerSpeed;

        bool ActivateTimer;
        public bool ShieldDestroyed { get; set; }


        public SpaceShip(Vector2 location, int velocity, Texture2D image, Color color, Levels _level)
        {
            this.Transform.Position = location;
            this.MovingSpeed = velocity;
            this.level = _level;
            this.Image = image;
            this.ImageColor = color;
            this.Bounds = new Rectangle(0, 0, 128, 60);
            this.BulletCount = 5;
            this.Drawable = true;
            this.HP = 100;
            this.Shield = 100;
            this.ActivateTimer = true;
            this.MaxBullet = 5;
            hpRegen = new Timer();
            totalTimer = new Timer();
            this.HpRegenSpeed = 1000;
            this.ShieldDestroyed = false;
            hpRegen.Elapsed += delegate
            {
                HP++;
            };
            totalTimer.Elapsed += delegate
            {
                ActivateTimer = true;
            };
        }

        public override void Update(float elapsed)
        {
            this.Transform.Position += this.Transform.Velocity * elapsed;
            this.Bounds.X = (int)Transform.Position.X;
            this.Bounds.Y = (int)Transform.Position.Y;
            totalTimerSpeed = level.LevelTime;

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
            if (Shield <= 0)
            {
                Shield = 0;
                ShieldDestroyed = true;
            }

            if (level.Level == 1 && ActivateTimer)
            {
                hpRegen.Interval = HpRegenSpeed;
                hpRegen.Enabled = true;
                totalTimer.Interval = totalTimerSpeed;
                totalTimer.Enabled = true;
                ActivateTimer = false;
            }
            else if (level.levelChanged && ActivateTimer)
            {
                HpRegenSpeed += 200;
                hpRegen.Interval = HpRegenSpeed;
                hpRegen.Enabled = true;
                totalTimer.Interval = totalTimerSpeed;
                totalTimer.Enabled = true;
                ActivateTimer = false;
            }

            if (BulletCount >= MaxBullet)
            {
                BulletCount = MaxBullet;
            }

        }
    }
}
