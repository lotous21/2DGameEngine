using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class Weapon : Componenet
    {
        public string WeaponName;
        public Projectile Bullet;
        public int TotalBullets;
        public int Damage;
        public bool IsSelected;
        public Keys KeyboardInput;
        public SoundEffect ShotSound;

        public Weapon(string Name, Projectile bullet, Keys keyboardInput, SoundEffect shot)
        {
            WeaponName = Name;
            Bounds = new Rectangle(0, 0, 45, 10);
            Bullet = bullet;
            KeyboardInput = keyboardInput;
            ShotSound = shot;
            this.Drawable = false;
        }

    }
}
