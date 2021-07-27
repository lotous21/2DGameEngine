using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBo2D
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Bobo2D())
                game.Run();


        }
    }
}
