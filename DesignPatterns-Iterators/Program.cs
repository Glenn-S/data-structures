using System;
using DataStructures_Common;
using DS = DataStructures_List;

namespace DesignPatterns_Iterators
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class ArrayList<TItem> : DS.List<TItem>
    {
        public IIterator<TItem> Iterator => new ArrayIterator(this);

        private class ArrayIterator : IIterator<TItem>
        {
            private int j = 0;
            private bool removable = false;
            private ArrayList<TItem> _self;

            public ArrayIterator(ArrayList<TItem> self)
            {
                _self = self;
            }

            public bool HasNext() => j < _self.Count;

            public TItem Next()
            {
                if (j == _self.Count) throw new IndexOutOfRangeException();
                removable = true;
                return _self[j++];
            }

            public void Remove()
            {
                if (!removable) throw new InvalidOperationException();
                _self.Remove(j-- - 1);
                removable = false;
            }
        }
    }
}
