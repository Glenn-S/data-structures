using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures_Common;

namespace DataStructures_BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public enum TraverseOrder
    {
        Unknown = 0,
        PreOrder = 1,
        InOrder = 2,
        PostOrder = 3,
        BreadthFirst = 4
    }

    public class BinaryTree<TItem> : AbstractBinaryTree<TItem>
    {
        private int _size = 0;
        protected Node<TItem> _root = null;

        public override IEnumerable<TItem> Iterator => throw new NotImplementedException();

        public override IEnumerable<IPosition<TItem>> Positions => throw new NotImplementedException();

        public IEnumerable<IPosition<TItem>> Traverse(TraverseOrder traverse)
        {
            IEnumerable<IPosition<TItem>> snapshot = new List<IPosition<TItem>>();
            switch (traverse)
            {
                case TraverseOrder.PreOrder:
                    PreOrderTraverse(_root, ref snapshot);
                    break;
                case TraverseOrder.InOrder:
                    InOrderTraverse(_root, ref snapshot);
                    break;
                case TraverseOrder.PostOrder:
                    PostOrderTraverse(_root, ref snapshot);
                    break;
                case TraverseOrder.BreadthFirst:
                    BreadthFirstTraverse(_root, ref snapshot);
                    break;
                case TraverseOrder.Unknown:
                    break;
                default:
                    throw new InvalidOperationException();
            }

            return snapshot;
        }

        private void PreOrderTraverse(IPosition<TItem> position, ref IEnumerable<IPosition<TItem>> snapshot)
        {
            var children = Children(position);
            snapshot.Append(position);
            foreach (var child in children)
            {
                PreOrderTraverse(child, ref snapshot);
            }
        }

        private void PostOrderTraverse(IPosition<TItem> position, ref IEnumerable<IPosition<TItem>> snapshot)
        {
            var children = Children(position);
            foreach (var child in children)
            {
                PostOrderTraverse(child, ref snapshot);
            }
            snapshot.Append(position);
        }

        private void InOrderTraverse(IPosition<TItem> position, ref IEnumerable<IPosition<TItem>> snapshot)
        {
            var node = Validate(position);
            if (node.Left is not null)
                InOrderTraverse(node.Left, ref snapshot);
            snapshot.Append(position);
            if (node.Right is not null)
                InOrderTraverse(node.Right, ref snapshot);
        }

        public void BreadthFirstTraverse(IPosition<TItem> position, ref IEnumerable<IPosition<TItem>> snapshot)
        {
            var queue = new Queue<IPosition<TItem>>();
            queue.Enqueue(position);

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                snapshot.Append(node);
                var children = Children(node);
                foreach (var child in children)
                    queue.Enqueue(child);
            }
        }

        public override IPosition<TItem> Left(IPosition<TItem> position) => Validate(position).Left;

        public override IPosition<TItem> Right(IPosition<TItem> position) => Validate(position).Right;

        public override IPosition<TItem> Parent(IPosition<TItem> position) => Validate(position).Parent;

        public override IPosition<TItem> Root() => _root;

        public override int Size() => _size;

        public int Depth(IPosition<TItem> position)
        {
            int height = 0;

            foreach (var child in Children(position))
                height = Math.Max(height, 1 + Depth(child));

            return height;
        }

        public int Height() => Depth(_root);

        public IPosition<TItem> AddRoot(TItem item)
        {
            if (!IsEmpty()) throw new InvalidOperationException();
            _root = CreateNode(item, null, null, null);
            _size = 1;
            return _root;
        }

        public IPosition<TItem> AddLeft(IPosition<TItem> position, TItem item)
        {
            var parent = Validate(position);
            if (parent.Left is not null) throw new InvalidOperationException();
            var child = CreateNode(item, parent, null, null);
            parent.Left = child;
            _size++;
            return child;
        }

        public IPosition<TItem> AddRight(IPosition<TItem> position, TItem item)
        {
            var parent = Validate(position);
            if (parent.Right is not null) throw new InvalidOperationException();
            var child = CreateNode(item, parent, null, null);
            parent.Right = child;
            _size++;
            return child;
        }

        public TItem Set(IPosition<TItem> position, TItem item)
        {
            var node = Validate(position);
            var temp = node.Item;
            node.Item = item;
            return temp;
        }

        public void Attach(
            IPosition<TItem> position,
            BinaryTree<TItem> leftTree,
            BinaryTree<TItem> rightTree)
        {
            var node = Validate(position);
            if (IsInternal(position)) throw new InvalidOperationException();
            _size += leftTree.Size() + rightTree.Size();
            if (!leftTree.IsEmpty())
            {
                leftTree._root.Parent = node;
                node.Left = leftTree._root;
                leftTree._root = null;
                leftTree._size = 0;
            }
            if (!rightTree.IsEmpty())
            {
                rightTree._root.Parent = node;
                node.Right = rightTree._root;
                rightTree._root = null;
                rightTree._size = 0;
            }
        }

        public TItem Remove(IPosition<TItem> position)
        {
            var node = Validate(position);
            if (NumChildren(position) == 2) throw new InvalidOperationException();
            var child = node.Left is not null ? node.Left : node.Right;
            if (child is not null)
                child.Parent = node.Parent;
            if (node == _root)
                _root = child;
            // Determine which side to attach the child
            else
            {
                var parent = node.Parent;
                if (node == parent.Left)
                    parent.Left = child;
                else
                    parent.Right = child;
            }

            _size--;
            var temp = node.Item;
            // Help the garbage collector
            node.Item = default;
            node.Left = default;
            node.Right = default;
            node.Parent = node;
            return temp;
        }

        protected Node<TItem> CreateNode(
            TItem item,
            Node<TItem> parent,
            Node<TItem> left,
            Node<TItem> right) => new Node<TItem>(item, parent, left, right);

        protected Node<TItem> Validate(IPosition<TItem> position)
        {
            if (position is not Node<TItem>) throw new ArgumentException();
            var node = position as Node<TItem>;
            if (node.Parent == node) throw new IndexOutOfRangeException();
            return node;
        }

        protected class Node<TItem> : IPosition<TItem>
        {
            public TItem Item { get; set; }
            public Node<TItem> Parent { get; set; }
            public Node<TItem> Left { get; set; }
            public Node<TItem> Right { get; set; }

            public Node(
                TItem item,
                Node<TItem> parent,
                Node<TItem> left,
                Node<TItem> right)
            {
                Item = item;
                Parent = parent;
                Left = left;
                Right = right;
            }
        }
    }
}
