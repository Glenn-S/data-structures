using DataStructures_DoublyLinkedList;
using System;

namespace DataStructures_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new Queue<int>();
            queue.Enqueue(1);
            Console.WriteLine(queue.ToString());
            queue.Enqueue(2);
            Console.WriteLine(queue.ToString());
            queue.Enqueue(3);
            Console.WriteLine(queue.ToString());
            queue.Enqueue(4);
            Console.WriteLine(queue.ToString());

            queue.Dequeue();
            Console.WriteLine(queue.ToString());
            queue.Dequeue();
            Console.WriteLine(queue.ToString());
            queue.Dequeue();
            Console.WriteLine(queue.ToString());
            queue.Dequeue();
            Console.WriteLine(queue.ToString());
        }
    }

    public class Queue<TItem>
    {
        private LinkedList<TItem> _queue;

        public Queue()
        {
            _queue = new LinkedList<TItem>();
        }

        public void Enqueue(TItem item)
        {
            _queue.AddFirst(item);
        }

        public TItem Dequeue()
        {
            return _queue.RemoveLast();
        }

        public TItem First()
        {
            return _queue.First();
        }

        public int Size() => _queue.Count;

        public bool IsEmpty() => _queue.IsEmpty();

        public override string ToString()
        {
            return _queue.ToString();
        }
    }
}
