using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    class Transform
    {
        private Vector2 position;

        public Transform()
        {

        }
        public Vector2 GetPosition()
        {
            return position;
        }
        
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}
