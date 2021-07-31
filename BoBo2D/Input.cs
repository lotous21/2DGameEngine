using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoBo2D
{
    public class Input
    {
        public Keys UpKey;
        public Keys DownKey;
        public Keys RighyKey;
        public Keys LeftKey;
        public Keys FireKey;

        public Input (Keys up, Keys down, Keys right, Keys left, Keys fire)
        {
            this.UpKey = up;
            this.DownKey = down;
            this.RighyKey = right;
            this.LeftKey = left;
            this.FireKey = fire;
        }
    }
}
