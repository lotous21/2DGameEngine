using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BoBo2D
{
    static class Physics
    {

    public static bool AABB(Rectangle boxA, Rectangle boxB)
    {
        return boxA.Left < boxB.Right &&
                boxA.Right > boxB.Left &&
                boxA.Top < boxB.Bottom &&
                boxA.Bottom > boxB.Top;
    }
}
}
