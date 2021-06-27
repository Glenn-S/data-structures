using System;
using System.Collections.Generic;

namespace DataStructures_Common
{
    public interface ITree<TItem>
    {
        IPosition<TItem> Root();
        IPosition<TItem> Parent(IPosition<TItem> position);
        IEnumerable<IPosition<TItem>> Children(IPosition<TItem> position);
        int NumChildren(IPosition<TItem> position);
        bool IsInternal(IPosition<TItem> position);
        bool IsExternal(IPosition<TItem> position);
        bool IsRoot(IPosition<TItem> position);
        int Size();
        bool IsEmpty();
        IEnumerable<TItem> Iterator { get; }
        IEnumerable<IPosition<TItem>> Positions { get; }
    }
}
