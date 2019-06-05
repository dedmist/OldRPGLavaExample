using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Lavatest
{
    class GameMap
    {
        static GameMap gameMap = new GameMap();
        public static GameMap getInstance()
        {
            return gameMap;
        }
        //public enum tileEnum { floor, lava, air, water, wall };
        Rectangle[] tiles = {
            new Rectangle(45, 45, 55, 55),
            new Rectangle(1045, 45, 55, 55),
            new Rectangle(845,45,55,55),
            new Rectangle(1045,145,55,55),
            new Rectangle(745,145,55,55) };
        SortedDictionary<DirectionEnum, Rectangle> directions = new SortedDictionary<DirectionEnum, Rectangle>
        {
            { DirectionEnum.n, new Rectangle(55,0,55,55) },
            { DirectionEnum.ne, new Rectangle(110,0,55,55) },
            { DirectionEnum.e, new Rectangle(110,55,55,55) },
            { DirectionEnum.se, new Rectangle(110,110,55,55) },
            { DirectionEnum.s, new Rectangle(55,110,55,55) },
            { DirectionEnum.sw, new Rectangle(0,110,55,55) },
            { DirectionEnum.w, new Rectangle(0,55,55,55) },
            { DirectionEnum.nw, new Rectangle(0,0,55,55) },
            { DirectionEnum.none, new Rectangle(55,55,55,55) }
        };
        Tile[,,] gridMap; 
        private GameMap()
        {
            gridMap = new Tile[startVals.GetLength(0), startVals.GetLength(1), startVals.GetLength(2)];
            for (int i = 0; i < gridMap.GetLength(0); i++)
                for (int k = 0; k < gridMap.GetLength(1); k++)
                    for (int l = 0; l < gridMap.GetLength(2); l++)
                    {
                        /*
                        System.Console.WriteLine("{0}..{1}/{2},{3}..{4}/{5},{6}..{7}/{8}", 
                            i, gridMap.GetLength(0),startVals.GetLength(0),
                            k, gridMap.GetLength(1), startVals.GetLength(1),
                            l, gridMap.GetLength(2), startVals.GetLength(2));*/
                        gridMap[i, k, l] = new Tile((TileEnum)startVals[i, k, l]);
                    }
        }
        public DirectionEnum getDirection(int x, int y, int z)
        {
            return gridMap[x, y, z].direction;
        }
        public void setDirection(Vector3Int tile, DirectionEnum direction)
        {
            gridMap[tile.X, tile.Y, tile.Z].direction = direction;
        }
        public Rectangle getDirectionRect(int x, int y, int z)
        {
            return directions[getDirection(x, y, z)];
        }
        public int getDimensionSize(int dimension)
        {
            return gridMap.GetLength(dimension);
        }
        public Rectangle getRect(int x, int y, int z)
        {
            return tiles[(int)gridMap[x, y, z].tileEnum];
        }
        public bool isAir(Vector3Int tile)
        {
            return gridMap[tile.X, tile.Y, tile.Z].tileEnum == TileEnum.air;
        }
        public bool isObstructed(Vector3Int tile)
        {
            return gridMap[tile.X, tile.Y, tile.Z].tileEnum == TileEnum.wall;
        }
        public bool isWater(Vector3Int tile)
        {
            return gridMap[tile.X, tile.Y, tile.Z].tileEnum == TileEnum.water;
        }
        public bool isLava(Vector3Int tile)
        {
            return gridMap[tile.X, tile.Y, tile.Z].tileEnum == TileEnum.lava;
        }
        public void setLava(Vector3Int tile)
        {
            gridMap[tile.X, tile.Y, tile.Z].tileEnum = TileEnum.lava;
        }
        public void unsetLava(Vector3Int tile)
        {
            gridMap[tile.X, tile.Y, tile.Z].tileEnum = TileEnum.floor;
        }
        public Vector3Int getDropTile(Vector3Int tile)
        {
            tile.X++;
            return tile;
        }
        int[,,] startVals = {
        {
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,4,4,4,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,4,0,4,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,4,0,0,4,0,4,0,4,4,4,4,0,4},
            {4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,4,0,4,4,4,4,0,4,4,4},
            {4,0,0,0,0,0,0,0,0,0,4,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
        },
        {
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,3,0,0,0,0,0,0,0,0,3,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,3,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,3,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
        }/*,
        {
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,4},
            {4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4},
        }*/
    };


    }
}

