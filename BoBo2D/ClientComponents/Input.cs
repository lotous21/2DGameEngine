using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class Input: Componenet
    {
        public Keys UpKey { get; set; }
        public Keys DownKey { get; set; }
        public Keys RighyKey { get; set; }
        public Keys LeftKey { get; set; }
        public Keys FireKey { get; set; }

        SpaceShip player;

        List<Weapon> weapons;
        List<Projectile> bullets;

        KeyboardState prevState;

        public Input(Keys up, Keys down, Keys right, Keys left, Keys fire, SpaceShip _player, List<Weapon> _weapons, List<Projectile> _bullets)
        {
            this.bullets = _bullets;
            this.weapons = _weapons;
            this.player = _player;
            this.UpKey = up;
            this.DownKey = down;
            this.RighyKey = right;
            this.LeftKey = left;
            this.FireKey = fire;
        }
        public Input(Keys fire, SpaceShip _player, List<Weapon> _weapons, List<Projectile> _bullets)
        {
            this.bullets = _bullets;
            this.weapons = _weapons;
            this.FireKey = fire;
            this.player = _player;
        }
        public override void Update(float elapsed)
        {
            if (Keyboard.GetState().IsKeyDown(DownKey))
            {
                player.Transform.Velocity.Y = player.MovingSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(UpKey))
            {
                player.Transform.Velocity.Y = -player.MovingSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(RighyKey))
            {
                player.Transform.Velocity.X = player.MovingSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(LeftKey))
            {
                player.Transform.Velocity.X = -player.MovingSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(FireKey) && prevState.IsKeyUp(FireKey) && player.BulletCount > 0)
            {
                Projectile p = new Projectile(GetWeaponSelected().Bullet.Transform.Position, GetWeaponSelected().Bullet.Image, GetWeaponSelected().Bullet.Transform.Velocity, Color.White);
                bullets.Add(p);
                p.Enable();
                p.Transform.Position = new Vector2(player.Transform.Position.X, player.Transform.Position.Y + 15);
                GetWeaponSelected().ShotSound.Play();
                player.BulletCount--;
            }

            prevState = Keyboard.GetState();
        }
        public void WASD()
        {
            UpKey = Keys.W;
            DownKey = Keys.S;
            RighyKey = Keys.D;
            LeftKey = Keys.A;
        }
        public void Arrows()
        {
            UpKey = Keys.Up;
            DownKey = Keys.Down;
            RighyKey = Keys.Right;
            LeftKey = Keys.Left;
        }

        Weapon GetWeaponSelected()
        {
            foreach (Weapon w in weapons)
            {
                if (w.IsSelected)
                {
                    return w;
                }
            }
            return null;
        }
    }
}
