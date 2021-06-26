using System;

namespace DataStructures_List
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new ArrayList<int>();
            list.Add(0, 1); // [1]
            Console.WriteLine(list.ToString());
            list.Add(0, 2); // [2,1]
            Console.WriteLine(list.ToString());
            list.Add(1, 3); // [2,3,1]
            Console.WriteLine(list.ToString());

            var list2 = new ArrayList<int>();
            list2[0] = 1; // [1]
            Console.WriteLine(list2.ToString());
            list2[0] = 2; // [2,1]
            Console.WriteLine(list2.ToString());
            list2[1] = 3; // [2,3,1]
            Console.WriteLine(list2.ToString());
        }
    }

    public class ArrayList<TItem>
    {
        private TItem[] _buffer;
        private int _capacity;
        public int Count { get; private set; }

        public ArrayList(int initialCapacity = 16)
        {
            _capacity = initialCapacity;
            _buffer = new TItem[initialCapacity];
        }

        public bool IsEmpty() => Count == 0;

        public TItem Get(int i)
        {
            CheckIndex(i, Count);
            return _buffer[i];
        }

        public TItem Set(int i, TItem value)
        {
            CheckIndex(i, Count);
            var oldItem = _buffer[i];
            _buffer[i] = value;
            return oldItem;
        }

        public TItem this[int key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, value);
            }
        }

        public void Add(int i, TItem value)
        {
            CheckIndex(i, Count + 1);
            if (Count == _capacity)
                Grow();
            // descending, shift all items to the right by 1
            for (int index = Count-1; index >= i; index--)
            {
                _buffer[index + 1] = _buffer[index]; 
            }
            _buffer[i] = value;
            Count++;
        }

        public TItem Remove(int i)
        {
            CheckIndex(i, Count);
            var itemToReturn = _buffer[i];
            for (int index = i; i < Count - 1; i++)
                _buffer[index] = _buffer[index + 1];
            Count--;
            return itemToReturn;
        }

        private void Grow()
        {
            var newBuffer = new TItem[_capacity * 2];
            for (int i = 0; i < Count; i++)
            {
                newBuffer[i] = _buffer[i];
            }
            _buffer = newBuffer;
        }

        private static void CheckIndex(int i, int n)
        {
            if (i < 0 || i >= n) throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            var retVal = "[";

            for (int i = 0; i < Count; i++)
            {
                retVal += _buffer[i].ToString();
                if (i < Count - 1)
                    retVal += ",";
            }
            return retVal += "]";
        }
    }
}
