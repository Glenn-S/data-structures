using DataStructures_DoublyLinkedList;
using System;

namespace DataStructures_Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<int>();
            stack.Push(1);
            Console.WriteLine(stack.ToString());
            stack.Push(2);
            Console.WriteLine(stack.ToString());
            stack.Push(3);
            Console.WriteLine(stack.ToString());
            stack.Push(4);
            Console.WriteLine(stack.ToString());

            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
        }
    }

    public class Stack<TItem>
    {
        private LinkedList<TItem> _stack;

        public Stack()
        {
            _stack = new LinkedList<TItem>();
        }

        public void Push(TItem item)
        {
            _stack.AddFirst(item);
        }

        public TItem Pop()
        {
            return _stack.RemoveFirst();
        }

        public TItem Top()
        {
            return _stack.First();
        }

        public int Size() => _stack.Count;

        public bool IsEmpty() => _stack.IsEmpty();

        public override string ToString()
        {
            return _stack.ToString();
        }
    }
}
