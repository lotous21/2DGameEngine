using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    class Input
    {
        public Keys UpKey;
        public Keys DownKey;
        public Keys RighyKey;
        public Keys LeftKey;
        public Keys FireKey;

        SpaceShip player;

        public Input (Keys up, Keys down, Keys right, Keys left, Keys fire, SpaceShip _player)
        {
            this.player = _player;
            this.UpKey = up;
            this.DownKey = down;
            this.RighyKey = right;
            this.LeftKey = left;
            this.FireKey = fire;
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(DownKey))
            {
                player.Transform.Velocity.Y = 500;
            }
            if (Keyboard.GetState().IsKeyDown(UpKey))
            {
                player.Transform.Velocity.Y = -500;
            }
            if (Keyboard.GetState().IsKeyDown(RighyKey))
            {
                player.Transform.Velocity.X = 500;
            }
            if (Keyboard.GetState().IsKeyDown(LeftKey))
            {
                player.Transform.Velocity.X = -500;
            }
        }
    }
}
