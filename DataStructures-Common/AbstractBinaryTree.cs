using System;
using System.Collections.Generic;

namespace DataStructures_Common
{
    public abstract class AbstractBinaryTree<TItem> : AbstractTree<TItem>, IBinaryTree<TItem>
    {
        public abstract IPosition<TItem> Left(IPosition<TItem> position);
        public abstract IPosition<TItem> Right(IPosition<TItem> position);

        public IPosition<TItem> Siblings(IPosition<TItem> position)
        {
            var parent = Parent(position);
            if (parent is null) return null;
            var leftParent = Left(parent);
            if (parent.Equals(leftParent))
                return Right(parent);
            else
                return Left(parent);
        }

        public override int NumChildren(IPosition<TItem> position)
        {
            var count = 0;
            if (Left(position) is not null)
                count++;
            if (Right(position) is not null)
                count++;
            return count;
        }

        public override IEnumerable<IPosition<TItem>> Children(IPosition<TItem> position)
        {
            var snapshot = new List<IPosition<TItem>>(2);
            var left = Left(position);
            var right = Right(position);
            if (left is not null)
                snapshot.Add(left);
            if (right is not null)
                snapshot.Add(right);
            return snapshot;
        }
    }
}
