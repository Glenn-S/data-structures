using System;

namespace DataStructures_PositionalList
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public interface IPosition<TItem>
    {
        /// <exception cref="IndexOutOfRangeException"></exception>
        TItem Item { get; set; }
    }

    public interface IPositionalList<TItem>
    {
        int Size();
        bool IsEmpty();
        IPosition<TItem> First();
        IPosition<TItem> Last();
        /// <exception cref="IndexOutOfRangeException"></exception>
        IPosition<TItem> Before(IPosition<TItem> position);
        /// <exception cref="IndexOutOfRangeException"></exception>
        IPosition<TItem> After(IPosition<TItem> position);
        IPosition<TItem> AddFirst(TItem item);
        IPosition<TItem> AddLast(TItem item);
        /// <exception cref="IndexOutOfRangeException"></exception>
        IPosition<TItem> AddBefore(IPosition<TItem> position, TItem item);
        /// <exception cref="IndexOutOfRangeException"></exception>
        IPosition<TItem> AddAfter(IPosition<TItem> position, TItem item);
        /// <exception cref="IndexOutOfRangeException"></exception>
        TItem Set(IPosition<TItem> position, TItem item);
        /// <exception cref="IndexOutOfRangeException"></exception>
        TItem Remove(IPosition<TItem> position);
    }

    public class PositionalList<TItem> : IPositionalList<TItem>
    {
        private Node<TItem> header;
        private Node<TItem> trailer;
        private int _size = 0;

        public PositionalList()
        {
            header = new Node<TItem>(default, null, null);
            trailer = new Node<TItem>(default, header, null);
            header.Next = trailer;
        }

        public IPosition<TItem> After(IPosition<TItem> position) => Position(Validate(position).Next);

        public IPosition<TItem> Before(IPosition<TItem> position) => Position(Validate(position).Prev);

        public IPosition<TItem> First() => Position(header.Next);

        public IPosition<TItem> Last() => Position(trailer.Prev);

        public IPosition<TItem> AddAfter(IPosition<TItem> position, TItem item)
        {
            var node = Validate(position);
            return AddBetween(item, node, node.Next);
        }

        public IPosition<TItem> AddBefore(IPosition<TItem> position, TItem item)
        {
            var node = Validate(position);
            return AddBetween(item, node.Prev, node);
        }

        public IPosition<TItem> AddFirst(TItem item) => AddBetween(item, header, header.Next);

        public IPosition<TItem> AddLast(TItem item) => AddBetween(item, trailer.Prev, trailer);

        public TItem Remove(IPosition<TItem> position)
        {
            var node = Validate(position);
            var pred = node.Prev;
            var succ = node.Next;
            pred.Next = succ;
            succ.Prev = pred;
            _size--;
            var currentValue = node.Item;
            node.Clear();
            return currentValue;
        }

        public TItem Set(IPosition<TItem> position, TItem item)
        {
            var node = Validate(position);
            var currentValue = node.Item;
            node.Item = item;
            return currentValue;
        }

        public bool IsEmpty() => _size == 0;

        public int Size() => _size;

        private static Node<TItem> Validate(IPosition<TItem> position)
        {
            if (position is not Node<TItem>) throw new InvalidCastException();
            var node = position as Node<TItem>;
            if (node.Next is null)
                throw new IndexOutOfRangeException();
            return node;
        }

        private IPosition<TItem> Position(Node<TItem> node)
        {
            // Check if sentinel
            if (node == header || node == trailer)
                return null;
            return node;
        }

        private IPosition<TItem> AddBetween(TItem item, Node<TItem> pred, Node<TItem> succ)
        {
            var newest = new Node<TItem>(item, pred, succ);
            pred.Next = newest;
            succ.Prev = newest;
            _size++;
            return newest;
        }

        private class Node<TItem> : IPosition<TItem>
        {
            private TItem _item;
            public TItem Item
            {
                get
                {
                    if (Next is null)
                        throw new IndexOutOfRangeException();
                    return _item;
                }
                set
                {
                    _item = value;
                }
            }
            public Node<TItem> Prev { get; set; }
            public Node<TItem> Next { get; set; }

            public Node(TItem item, Node<TItem> prev, Node<TItem> next)
            {
                Item = item;
                Prev = prev;
                Next = next;
            }

            public void Clear()
            {
                _item = default;
                Prev = null;
                Next = null;
            }
        }
    }
}
