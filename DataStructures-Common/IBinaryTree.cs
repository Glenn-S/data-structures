using System;
namespace DataStructures_Common
{
    public interface IBinaryTree<TItem> : ITree<TItem>
    {
        IPosition<TItem> Left(IPosition<TItem> position);
        IPosition<TItem> Right(IPosition<TItem> position);
        IPosition<TItem> Siblings(IPosition<TItem> position);
    }
}
