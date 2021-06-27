using System;

namespace DataStructures_Common
{
    public interface IIterator<TIterable>
    {
        bool HasNext();
        TIterable Next();
        void Remove();
    }
}
