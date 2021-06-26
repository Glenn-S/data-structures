using System;

namespace DataStructures_Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new Array<int>(4);
            arr.AddFirst(1);
            Console.WriteLine(arr.ToString());
            arr.AddFirst(2);
            Console.WriteLine(arr.ToString());
            arr.AddFirst(3);
            Console.WriteLine(arr.ToString());
            arr.AddFirst(4);
            Console.WriteLine(arr.ToString());
            arr.AddFirst(5);
            Console.WriteLine(arr.ToString());
        }
    }

    public class Array<TItem>
    {
        private int _count;
        private int _size;
        private readonly TItem[] _collection;

        public Array(int size)
        {
            _collection = new TItem[size];
            _count = 0;
            _size = size;
        }

        public void AddFirst(TItem item)
        {
            TItem itemToMove;
            TItem temp = item;

            if (_count == 0)
            {
                _collection[0] = item;
                _count++;
            }
            else
            {
                if (_count < _size)
                    _count++;

                // if count is equal to size, the final item must be dropped.
                for (int i = 0; i < _count; i++)
                {
                    itemToMove = _collection[i];
                    _collection[i] = temp;
                    temp = itemToMove;
                }
            }
        }

        public string ToString()
        {
            var retVal = "[";

            for (int i = 0; i < _count; i++)
            {
                retVal += _collection[i].ToString();
                if (i != _count - 1)
                    retVal += ",";
            }
            return retVal += "]";
        }
    }
}
