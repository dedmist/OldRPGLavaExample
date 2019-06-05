using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavatest
{
    class EventQueue
    {
        static EventQueue eventQueue = new EventQueue();
        public static EventQueue getInstance ()
        {
            return eventQueue;
        }
        long tick = 1;
        long timeKeeper = 0;
        SortedDictionary<long, LinkedList<EventObject> > btree = new SortedDictionary<long, LinkedList<EventObject> >();
        EventQueue()
        {

        }

        public void enqueue(EventObject obj, int timeOffset = 0)
        {
            long timeVal = tick + timeOffset;
            if(!btree.ContainsKey(timeVal))
            {
                btree.Add(tick + timeOffset, new LinkedList<EventObject>());
            }
            LinkedList<EventObject> list = btree[timeVal];
            list.AddLast(obj);
            
        }
        public void nextTick()
        {
            //++tick;
            ++timeKeeper;
            //if(timeKeeper %2 == 1)
            //{
                ++tick;
            //}
            EventObject eo;
            while(!((eo = dequeue()) is EmptyEvent))
            {                
                eo.execute();
            }
        }
        public EventObject dequeue()
        {
            System.Console.WriteLine("dequeue {0}", tick);
            if (btree.Count > 0)
            {
                KeyValuePair<long, LinkedList<EventObject>> topPriority = btree.First();
                System.Console.WriteLine("topPriority {0}", topPriority.Key);
                if (topPriority.Key <= tick)
                {
                    System.Console.WriteLine("Executing tick {0}, priority {1}", tick, topPriority.Key);
                    EventObject eo = topPriority.Value.First();
                    if (eo != null)
                    {
                        topPriority.Value.RemoveFirst();
                    }
                    else
                    {
                        eo = new EmptyEvent();
                    }

                    if (topPriority.Value.Count == 0)
                    {
                        btree.Remove(topPriority.Key);
                    }
                    return eo;
                }
            }
            return new EmptyEvent();
        }
    }
}
