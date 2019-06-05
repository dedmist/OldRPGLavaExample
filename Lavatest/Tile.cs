using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavatest
{
    class Tile
    {
        public Tile(TileEnum tileEnum, DirectionEnum direction=DirectionEnum.none)
        {
            this.tileEnum = tileEnum;
            this.direction = direction;
        }
        public TileEnum tileEnum { get; set; }
        public DirectionEnum direction { get; set; }
    }
}
