using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavatest
{
    static class DirectionVectors
    {
        public static OrderedDictionary Defaults;
        public static OrderedDictionary Clockwise = new OrderedDictionary();
        public static OrderedDictionary Counter = new OrderedDictionary();
        public static OrderedDictionary Clockwise45 = new OrderedDictionary();
        public static OrderedDictionary Counter45 = new OrderedDictionary();
        public static OrderedDictionary Bounce = new OrderedDictionary();
        public static OrderedDictionary KeyFromValue = new OrderedDictionary();
        static DirectionVectors() {
            Defaults = new OrderedDictionary() {
                { DirectionEnum.nw, new Vector2Int(-1, -1)},
                { DirectionEnum.w , new Vector2Int(-1, 0)},
                { DirectionEnum.sw, new Vector2Int(-1, 1)},
                { DirectionEnum.s , new Vector2Int(0, 1)},
                { DirectionEnum.se, new Vector2Int(1, 1)},
                { DirectionEnum.e , new Vector2Int(1, 0)},
                { DirectionEnum.ne, new Vector2Int(1, -1)},
                { DirectionEnum.n , new Vector2Int(0, -1)}
                };

            DirectionEnum[] keys = Defaults.Keys.Cast<DirectionEnum>().ToArray();
            Vector2Int[] vals = Defaults.Values.Cast<Vector2Int>().ToArray();
                int i = 0;
                foreach (DirectionEnum key in keys)
                {
                    //System.Console.WriteLine("DV value is {0},{1},{2}", i, key, vals.Length);

                    Clockwise.Add(key, vals[(i + 2) % vals.Length]);
                    Counter.Add(key, vals[(i + vals.Length - 2) % vals.Length]);
                    Clockwise45.Add(key, vals[(i + 1) % vals.Length]);
                    Counter45.Add(key, vals[(i + vals.Length - 1) % vals.Length]);
                    Bounce.Add(key, vals[(i + (vals.Length / 2)) % vals.Length]);
                KeyFromValue.Add(vals[i], key);
                    ++i;
                }
        }
    public static DirectionEnum key(Vector2Int value)
    {
            return (DirectionEnum) KeyFromValue[value];

        }
    }
}
