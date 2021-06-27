using System;
using System.Collections.Generic;

namespace DataStructures_Common
{
    public abstract class AbstractTree<TItem>
        : ITree<TItem>
    {
        public abstract IEnumerable<TItem> Iterator { get; }
        public abstract IEnumerable<IPosition<TItem>> Positions { get; }
        public abstract IEnumerable<IPosition<TItem>> Children(IPosition<TItem> position);
        public abstract int NumChildren(IPosition<TItem> position);
        public abstract IPosition<TItem> Parent(IPosition<TItem> position);
        public abstract IPosition<TItem> Root();
        public abstract int Size();

        public bool IsEmpty() =>
            Size() == 0;

        public bool IsExternal(IPosition<TItem> position) =>
            NumChildren(position) > 0;

        public bool IsInternal(IPosition<TItem> position) =>
            NumChildren(position) == 0;

        public bool IsRoot(IPosition<TItem> position) =>
            position.Equals(Root());
    }
}
