using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavatest
{
    class Vector3Int
    {
        public static Vector3Int operator + (Vector3Int a, Vector2Int b)
        {
            return new Vector3Int(a.X, a.Y + b.X, a.Z + b.Y);
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public Vector3Int (int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
