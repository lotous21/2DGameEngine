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
        public bool IsSelected;

        public Projectile Bullet;
        public SoundEffect ShotSound;

        Keys keyboardInput;
        List<Weapon> weapons;
        KeyboardState prevState;

        public Weapon(string Name, Projectile bullet, Keys _keyboardInput, SoundEffect shot, List<Weapon> _weapons)
        {
            WeaponName = Name;
            Bounds = new Rectangle(0, 0, 45, 10);
            Bullet = bullet;
            keyboardInput = _keyboardInput;
            ShotSound = shot;
            weapons = _weapons;
            this.Drawable = false;
        }

        public override void Update(float elapsed)
        {
            if (Keyboard.GetState().IsKeyDown(keyboardInput) && prevState.IsKeyUp(keyboardInput))
            {
                foreach (Weapon w in weapons)
                {
                    w.IsSelected = false;
                }

                this.IsSelected = true;
            }

            prevState = Keyboard.GetState();

            base.Update(elapsed);
        }

    }
}
