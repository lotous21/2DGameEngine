using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    interface IComponents
    {
        public bool IsEnable();
        public void Enable();
        public void Disable();
    }
}
