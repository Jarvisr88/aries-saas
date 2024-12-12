namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class FixedDropTargetEx : FixedDropTarget
    {
        protected FixedDropTargetEx(Panel panel) : base(panel)
        {
        }

        protected abstract int CorrectDropIndex(int dropIndex);
        protected abstract IList<ColumnBase> GetColumnsList();
        protected sealed override int GetDropIndexFromDragSource(UIElement element, Point pt)
        {
            int dropIndexFromDragSource = base.GetDropIndexFromDragSource(element, pt);
            dropIndexFromDragSource = this.CorrectDropIndex(dropIndexFromDragSource);
            if (base.UseLegacyColumnVisibleIndexes || (dropIndexFromDragSource == -1))
            {
                return dropIndexFromDragSource;
            }
            IList<ColumnBase> columnsList = this.GetColumnsList();
            if (dropIndexFromDragSource < columnsList.Count)
            {
                return this.GetVisibleIndex(columnsList[dropIndexFromDragSource]);
            }
            ColumnBase base2 = columnsList[columnsList.Count - 1];
            return (this.GetVisibleIndex(base2) + 1);
        }

        private int GetVisibleIndex(DependencyObject obj) => 
            !base.UseLegacyColumnVisibleIndexes ? BaseColumn.GetVisibleIndex(obj) : (BaseColumn.GetActualVisibleIndex(obj) - this.DropIndexCorrection);

        protected override int GetVisibleIndex(DependencyObject obj, bool useVisibleIndexCorrection) => 
            !base.UseLegacyColumnVisibleIndexes ? BaseColumn.GetActualCollectionIndex(obj) : (BaseColumn.GetActualVisibleIndex(obj) - this.DropIndexCorrection);
    }
}

