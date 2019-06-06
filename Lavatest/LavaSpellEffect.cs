using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavatest
{
    delegate void WorldFunc();
    class LavaSpellEffect : EventObject
    {
        Vector2Int direction;
        int lifespan;
        int originCount;
        WorldFunc worldFunc;
        Vector3Int origTile;
        int age = 0;

        public LavaSpellEffect(Vector3Int origTile, DirectionEnum _direction, int lifespan, int originCount) :
            this(origTile, (Vector2Int)DirectionVectors.Defaults[_direction], lifespan, originCount)
        {
        }
        public LavaSpellEffect(Vector3Int origTile, Vector2Int direction, int lifespan, int originCount)
        {
            this.origTile = origTile;
            this.lifespan = lifespan;
            this.originCount = originCount;
            this.direction = direction;
            this.worldFunc = new WorldFunc(this.firstExec);
        }

        public void execute()
        {
            worldFunc();
        }

        public void firstExec()
        {
            GameMap gameMap = GameMap.getInstance();
            if (originCount > 0)
            {                
                while (gameMap.isAir(origTile))
                {
                    origTile = gameMap.getDropTile(origTile);
                }
                if (gameMap.isWater(origTile) || gameMap.isObstructed(origTile) || gameMap.isLava(origTile))
                {
                    return;
                }

                LinkedList<Tuple<Vector3Int, Vector2Int>> openTiles = findOpenTiles(gameMap);
                if (openTiles.Count > 0)
                {
                    direction = openTiles.First().Item2;
                    DirectionEnum directionKey = DirectionVectors.key(direction);
                    gameMap.setDirection(origTile, directionKey);
                    foreach(Tuple<Vector3Int,Vector2Int> openTile in openTiles)
                    {
                        EventQueue.getInstance().enqueue(new LavaSpellEffect(openTile.Item1, 
                            //direction, 
                            openTile.Item2, 
                            lifespan, originCount - 1), 1);
                    }
                }
                /*
                ArrayList nextObjects = DirectionEnumFunc.isCardinal(DirectionVectors.key(direction)) ?
                    determineNextObjectsCardinal(gameMap) :
                    determineNextObjectsNonCardinal(gameMap);
                DirectionEnum directionKey = DirectionVectors.key(direction);
                gameMap.setDirection(origTile, directionKey);
                foreach (Vector2Int nextDirection in nextObjects) {
                    Vector3Int nextTile = origTile + nextDirection;
                    if (!gameMap.isObstructed(nextTile))
                    {
                        EventQueue.getInstance().enqueue(new LavaSpellEffect(nextTile, direction, lifespan, originCount - 1),1);
                    }
                }*/
            }
            gameMap.setLava(origTile);
            ++age;
            this.worldFunc = new WorldFunc(this.nextExec);
            EventQueue.getInstance().enqueue(this, 1);
        }

        private LinkedList<Tuple<Vector3Int,Vector2Int> > findOpenTiles(GameMap gameMap)
        {
            DirectionEnum[] keys = DirectionVectors.Defaults.Keys.Cast<DirectionEnum>().ToArray();
            LinkedList<Tuple<Vector3Int, Vector2Int>> nextTile = new LinkedList<Tuple<Vector3Int, Vector2Int>>();
            Vector2Int[] vals = DirectionVectors.Defaults.Values.Cast<Vector2Int>().ToArray();
            DirectionEnum directionKey = DirectionVectors.key(direction);
            Random random = new Random();
            LinkedList<Vector2Int> testDirections = new LinkedList<Vector2Int>();
            int[,] index = new int[,] {
                {
                    ((int) directionKey + 1) % keys.Length,
                    ((int) directionKey - 1 + keys.Length) % keys.Length },
                {
                    ((int) directionKey + 2) % keys.Length,
                    ((int) directionKey - 2 + keys.Length) % keys.Length },
                {
                    ((int) directionKey + 3) % keys.Length,
                    ((int) directionKey - 3 + keys.Length) % keys.Length }
            };
            for (int i = 0; i< 3; ++i)
            {              
                int num = random.Next(2);
                for(int j = 0; j < 2; ++j)
                {
                    testDirections.AddLast((Vector2Int)DirectionVectors.Defaults[keys[index[i,num]]]);
                    num = (num + 1) % 2;
                }
                if(i==2)
                {
                    testDirections.AddFirst((Vector2Int)DirectionVectors.Defaults[directionKey]);
                    testDirections.AddLast((Vector2Int)DirectionVectors.Defaults[((int)directionKey + 4) % keys.Length]);
                }
            }
            int count = testDirections.Count;
            for(int i = 0; i < count && nextTile.Count < 3; ++i)
            {
                Vector2Int testDirection = testDirections.First();
                Vector3Int tile = origTile + testDirection;
                testDirections.RemoveFirst();
                if(!gameMap.isObstructed(tile))
                {
                    nextTile.AddLast(new Tuple<Vector3Int,Vector2Int>(tile, testDirection));
                    System.Console.WriteLine("Step {0} , {1}, {2}", i, direction, testDirection);
                }
            }
            return nextTile;
        }
        /*
        private ArrayList determineNextObjectsCardinal(GameMap gameMap)
        {
            DirectionEnum directionKey = DirectionVectors.key(direction);
            ArrayList nextObjects = new ArrayList{direction,
                    DirectionVectors.Clockwise45[directionKey],
                    DirectionVectors.Counter45[directionKey]};
            Vector3Int forwardTile = origTile + direction;
            Vector3Int backTile = origTile + (Vector2Int)DirectionVectors.Bounce[directionKey];
            Vector3Int clockwiseTile = origTile + (Vector2Int)DirectionVectors.Clockwise[directionKey];
            Vector3Int counterTile = origTile + (Vector2Int)DirectionVectors.Counter[directionKey];
            Vector3Int clockwise45Tile = origTile + (Vector2Int)DirectionVectors.Clockwise45[directionKey];
            Vector3Int counter45Tile = origTile + (Vector2Int)DirectionVectors.Counter45[directionKey];
            //prefer first open, then bounce, random left or right
            //states: impassible, unobstructed, water, lava, open
            //change direction if not open
            //drop down level 

            if (gameMap.isObstructed(forwardTile) || gameMap.isLava(forwardTile))
            {
                direction = (Vector2Int)DirectionVectors.Bounce[directionKey];
                if (gameMap.isObstructed(backTile) || gameMap.isLava(backTile))
                {
                    if (!(gameMap.isObstructed(clockwiseTile) || gameMap.isLava(clockwiseTile)))
                    {
                        direction = (Vector2Int)DirectionVectors.Clockwise[directionKey];
                    }
                    if (!(gameMap.isObstructed(counterTile) || gameMap.isLava(counterTile)))
                    {
                        direction = (Vector2Int)DirectionVectors.Counter[directionKey];
                    }
                }
                DirectionEnum newDirectionKey = DirectionVectors.key(direction);
                nextObjects = new ArrayList{
                            direction,
                            DirectionVectors.Clockwise[newDirectionKey],
                            DirectionVectors.Counter[newDirectionKey],
                        };
            }
            return nextObjects;
        }

        private ArrayList determineNextObjectsNonCardinal(GameMap gameMap)
        {
            DirectionEnum directionKey = DirectionVectors.key(direction);
            ArrayList nextObjects = new ArrayList{direction,
                    DirectionVectors.Clockwise45[directionKey],
                    DirectionVectors.Counter45[directionKey]};
            Vector3Int forwardTile = origTile + direction;
            Vector3Int backTile = origTile + (Vector2Int)DirectionVectors.Bounce[directionKey];
            Vector3Int clockwiseTile = origTile + (Vector2Int)DirectionVectors.Clockwise[directionKey];
            Vector3Int counterTile = origTile + (Vector2Int)DirectionVectors.Counter[directionKey];
            Vector3Int clockwise45Tile = origTile + (Vector2Int)DirectionVectors.Clockwise45[directionKey];
            Vector3Int counter45Tile = origTile + (Vector2Int)DirectionVectors.Counter45[directionKey];

            //prefer first open, reflect, then bounce
            //states: impassible, unobstructed, water, lava, open
            //change direction if not open
            //drop down level 

            if (gameMap.isObstructed(forwardTile) || gameMap.isLava(forwardTile))
            {
               
                    if (!(gameMap.isObstructed(clockwise45Tile) || gameMap.isLava(clockwise45Tile)))
                    {
                        direction = (Vector2Int)DirectionVectors.Clockwise[directionKey];
                    }
                    if (!(gameMap.isObstructed(counter45Tile) || gameMap.isLava(counter45Tile)))
                    {
                        direction = (Vector2Int)DirectionVectors.Counter[directionKey];
                    }
                DirectionEnum newDirectionKey = DirectionVectors.key(direction);
                nextObjects = new ArrayList{
                            direction,
                            DirectionVectors.Clockwise45[newDirectionKey],
                            DirectionVectors.Counter45[newDirectionKey],
                        };
            }
            return nextObjects;
        }
        */
        public void nextExec()
        {
            System.Console.WriteLine("nextExec");
            if (age <= lifespan)
            {
                ++age;
                //push in direction
                EventQueue.getInstance().enqueue(this, 1);
            }
            else
            {
                GameMap.getInstance().unsetLava(origTile);
                GameMap.getInstance().setDirection(origTile, DirectionEnum.none);
            }
        }
    }
}
