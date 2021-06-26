using System;

namespace DataStructures_CircularlyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new LinkedList<int>();
            list.AddFirst(1);
            Console.WriteLine(list.ToString());
            list.AddFirst(2);
            Console.WriteLine(list.ToString());
            list.AddFirst(3);
            Console.WriteLine(list.ToString());
            list.AddFirst(4);
            Console.WriteLine(list.ToString());
            list.AddFirst(5);
            Console.WriteLine(list.ToString());
        }
    }

    public class LinkedList<TItem>
    {
        private Node<TItem> _tail;

        public int Count { get; private set; }

        public LinkedList()
        {
            _tail = null;
            Count = 0;
        }

        public bool IsEmpty() => Count == 0;

        public TItem First()
        {
            if (IsEmpty())
                return default;
            return _tail.Next.Value;
        }

        public TItem Last()
        {
            if (IsEmpty())
                return default;
            return _tail.Value;
        }

        public void Rotate()
        {
            if (_tail != null)
                _tail = _tail.Next;
        }

        public void AddFirst(TItem item)
        {
            var node = new Node<TItem>(item);
            if (IsEmpty())
            {
                _tail = node;
                _tail.Next = _tail; // link to itself
            }
            else
            {
                node.Next = _tail.Next;
                _tail.Next = node;
            }
            Count++;
        }

        public void AddLast(TItem item)
        {
            AddFirst(item);
            Rotate();
        }

        public TItem RemoveFirst()
        {
            if (!IsEmpty())
            {
                var head = _tail.Next;
                if (head == _tail)
                    _tail = null;
                else
                    _tail = head.Next; // removes the head
                Count--;
                return head.Value;
            }
            return default;
        }

        public override string ToString()
        {
            var retVal = "[";
            var tail = _tail;
            var nextPtr = _tail.Next; // the head

            while (nextPtr != tail)
            {
                retVal += nextPtr.Value.ToString();
                retVal += ",";
                nextPtr = nextPtr.Next;
            }
            retVal += nextPtr.Value.ToString();

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
