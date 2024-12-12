namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class DropTarget : DropTargetBase
    {
        public DropTarget(Panel panel) : base(panel)
        {
        }

        protected override int GetDragIndex(int dropIndex, Point pt) => 
            !base.UseLegacyColumnVisibleIndexes ? this.GetDragIndexCore(dropIndex, pt) : base.GetDragIndex(dropIndex, pt);

        protected int GetDragIndexCore(int dropIndex, Point pt)
        {
            bool isFarCorner = false;
            int num = base.GetChildHeaderIndexByPoint(pt, true, out isFarCorner);
            if (isFarCorner)
            {
                num++;
            }
            return (num - this.GetFirstActualCollectionIndex());
        }

        protected virtual int GetFirstActualCollectionIndex() => 
            0;
    }
}

