using System;

namespace DataStructures_DoublyLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            // implement doubly linked list
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

            Console.WriteLine(list.First());
            Console.WriteLine(list.Last());
            list.RemoveLast();
            list.RemoveLast();
            list.RemoveLast();
            list.RemoveLast();
            Console.WriteLine(list.ToString()); // [5]
        }
    }

    public class LinkedList<TItem>
    {
        private Node<TItem> _header;
        private Node<TItem> _trailer;

        public int Count { get; private set; }

        public LinkedList()
        {
            _header = new Node<TItem>(default, null, null);
            _trailer = new Node<TItem>(default, _header, null);
            _header.Next = _trailer;
            Count = 0;
        }

        public bool IsEmpty() => Count == 0;

        public TItem First()
        {
            if (IsEmpty())
                return default;
            return _header.Next.Value;
        }

        public TItem Last()
        {
            if (IsEmpty())
                return default;
            return _trailer.Prev.Value;
        }

        public void AddFirst(TItem item)
        {
            AddBetween(item, _header, _header.Next);
        }

        public void AddLast(TItem item)
        {
            AddBetween(item, _trailer.Prev, _trailer);
        }

        public TItem RemoveFirst()
        {
            if (IsEmpty())
                return default;
            return Remove(_header.Next);
        }

        public TItem RemoveLast()
        {
            if (IsEmpty())
                return default;
            return Remove(_trailer.Prev);
        }

        public override string ToString()
        {
            var retVal = "[";
            var nextPtr = _header.Next; // the head

            while (nextPtr != _trailer)
            {
                retVal += nextPtr.Value.ToString();
                if (nextPtr.Next != _trailer)
                    retVal += ",";
                nextPtr = nextPtr.Next;
            }
            return retVal += "]";
        }

        private void AddBetween(TItem item, Node<TItem> predecessor, Node<TItem> successor)
        {
            var node = new Node<TItem>(item, predecessor, successor);
            predecessor.Next = node;
            successor.Prev = node;
            Count++;
        }

        private TItem Remove(Node<TItem> node)
        {
            var predecessor = node.Prev;
            var successor = node.Next;
            predecessor.Next = successor;
            successor.Prev = predecessor;
            Count--;
            return node.Value;
        }

        private class Node<TItem>
        {
            public Node<TItem> Next { get; set; }
            public Node<TItem> Prev { get; set; }

            public TItem Value { get; set; }

            public Node(
                TItem value,
                Node<TItem> prev,
                Node<TItem> next)
            {
                Value = value;
                Next = next;
                Prev = prev;
            }
        }
    }
}
