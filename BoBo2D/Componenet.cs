using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    class Componenet : IComponents
    {
        public Transform transform;

        public Componenet()
        {
            transform = new Transform();
        }
    }
}
