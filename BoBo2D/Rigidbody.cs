using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
     class Rigidbody : Componenet
    {
        private bool UseGravity;
        private float GravityScale;

        public Rigidbody()
        {
            UseGravity = false;
            GravityScale = 0;
        }

        public bool GetUseGravity()
        {
            return UseGravity;
        }

        public void SetUseGravity(bool usegravity)
        {
            UseGravity = usegravity;
        }

        public float GetgravityScale()
        {
            return GravityScale;
        }

        public void SetGravity(float gravityscale)
        {
            GravityScale = gravityscale;
        }

        public void ApplyConstantForce(float meter, Vector2 direction)
        {

        }
    }
}
