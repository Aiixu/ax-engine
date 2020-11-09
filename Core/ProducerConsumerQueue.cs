using System.Threading;
using System.Collections.Generic;
using System;

namespace Ax.Engine.Core
{
    internal sealed class ProducerConsumerQueue<T> where T: class
    {
        private readonly object listLock = new object();
        private readonly Queue<T> queue;

        private bool producingCompleted;

        public ProducerConsumerQueue()
        {
            queue = new Queue<T>();
        }

        public ProducerConsumerQueue(int capacity)
        {
            queue = new Queue<T>(capacity);
        }

        public void Produce(T item)
        {
            lock (listLock)
            {
                queue.Enqueue(item);

                Monitor.Pulse(listLock);
            }
        }

        public void Clear()
        {
            queue.Clear();
        }

        public bool Consume(out T item)
        {
            lock (listLock)
            {
                if(queue.Count == 0 && producingCompleted)
                {
                    item = null;
                    return false;
                }

                while (queue.Count == 0)
                {
                    Monitor.Wait(listLock);
                }

                item = queue.Dequeue();
                return true;
            }
        }

        public void EndProduction()
        {
            producingCompleted = true;
        }
    }
}
