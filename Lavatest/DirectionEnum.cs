using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavatest
{
    enum DirectionEnum
    {
        nw,w,sw,s,se,e,ne,n,none
    }
    static class DirectionEnumFunc
    {
        static public bool isCardinal(DirectionEnum d)
        {
            return d == DirectionEnum.n || d == DirectionEnum.s || d == DirectionEnum.e || d == DirectionEnum.w;
        }
    }
}
