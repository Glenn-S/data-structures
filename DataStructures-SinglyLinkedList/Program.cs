using System;

namespace DataStructures_SinglyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new LinkedList<int>();
            list.AddLast(1); // [1]
            Console.WriteLine(list.ToString());
            list.AddLast(2); // [1,2]
            Console.WriteLine(list.ToString());
            //Console.WriteLine(list.ToString());
            //list.Add(1, 0); // [1]
            //Console.WriteLine(list.ToString());
            //list.Add(2, 0); // [2,1]
            //Console.WriteLine(list.ToString());
            //list.Add(3, 1); // [2,3,1]
            //Console.WriteLine(list.ToString());
            //list.Add(4, 0); // [4,2,3,1]
            //Console.WriteLine(list.ToString());

            //Console.WriteLine(list.Get(0));
            //Console.WriteLine(list.Get(1));
            //Console.WriteLine(list.Get(2));
            //Console.WriteLine(list.Get(3));

            //try
            //{
            //    Console.WriteLine(list.Get(4));
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //try
            //{
            //    Console.WriteLine(list.Get(-1));
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //list.RemoveFirst(); // [2,3,1]
            //Console.WriteLine(list.ToString());
        }
    }

    public class LinkedList<TItem>
    {
        private Node<TItem> _head;
        private Node<TItem> _tail;

        public int Count { get; private set; }

        public LinkedList()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public void AddFirst(TItem item)
        {
            var node = new Node<TItem>(item);
            if (IsEmpty())
                _tail = node;
            node.Next = _head;
            _head = node;
            Count++;
        }

        public void AddLast(TItem item)
        {
            var node = new Node<TItem>(item);
            if (IsEmpty())
                _head = node;
            else
                _tail.Next = node;
            _tail = node;

            Count++;
        }

        public void Add(TItem item, int index)
        {
            var node = new Node<TItem>(item);
            var nextPtr = _head;
            var prevPtr = _head;

            if (IsEmpty() || index == 0)
            {
                AddFirst(item);
                return;
            }

            if (index >= Count)
                throw new ArgumentOutOfRangeException();

            var counter = 0;
            while (nextPtr is not null)
            {
                if (counter == index)
                    break;
                prevPtr = nextPtr;
                nextPtr = nextPtr.Next;
                counter++;
            }

            prevPtr.Next = node;
            node.Next = nextPtr;
            Count++;
        }

        public TItem RemoveFirst()
        {
            if (Count > 0)
            {
                var item = _head.Value;
                _head = _head.Next;
                Count--;
                return item;
            }
            throw new ArgumentOutOfRangeException();
        }

        public TItem First()
        {
            if (IsEmpty())
                throw new ArgumentOutOfRangeException();
            return _head.Value;
        }

        public TItem Last()
        {
            if (IsEmpty())
                throw new ArgumentOutOfRangeException();
            return _tail.Value;
        }

        // No different than Count though since count is a readonly property publically
        public int Size()
        {
            return Count;
        }

        public TItem Get(int index)
        {
            var nextPtr = _head;

            if (IsEmpty() || index >= Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is out of range.");

            var counter = 0;
            while (nextPtr is not null)
            {
                if (counter == index)
                    break;
                nextPtr = nextPtr.Next;
                counter++;
            }

            return nextPtr.Value;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public override string ToString()
        {
            var retVal = "[";
            var nextPtr = _head;

            while (nextPtr is not null)
            {
                retVal += nextPtr.Value.ToString();
                if (nextPtr.Next is not null)
                    retVal += ",";
                nextPtr = nextPtr.Next;
            }

            return retVal += "]";
        }

        private class Node<TItem>
        {
            public Node<TItem> Next { get; set; }
            public TItem Value { get; set; }

            public Node(TItem value)
            {
                Value = value;
            }
        }
    }
}
