using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavatest
{
    class Vector2Int
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vector2Int (int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return base.ToString() + ": (" + X + "," + Y + ")";
        }
    }
}
