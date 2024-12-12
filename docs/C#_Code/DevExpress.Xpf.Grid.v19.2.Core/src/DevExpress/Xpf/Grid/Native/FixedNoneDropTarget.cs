namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class FixedNoneDropTarget : FixedDropTargetEx
    {
        public FixedNoneDropTarget(Panel panel) : base(panel)
        {
        }

        protected override bool CanDropCore(int dropIndex, ColumnBase sourceColumn, HeaderPresenterType headerPresenterType)
        {
            if ((sourceColumn.Fixed != FixedStyle.Left) && (sourceColumn.Fixed != FixedStyle.Right))
            {
                return true;
            }
            int num = (sourceColumn.Fixed == FixedStyle.Left) ? base.FixedLeftVisibleColumnsCount : base.FixedRightVisibleColumnsCount;
            return (((headerPresenterType == HeaderPresenterType.GroupPanel) || (headerPresenterType == HeaderPresenterType.ColumnChooser)) ? (num == 0) : false);
        }

        protected override int CorrectDropIndex(int dropIndex) => 
            (!base.TableView.IsCheckBoxSelectorColumnVisible || ((dropIndex != 0) || (base.FixedLeftColumnsCount != 0))) ? dropIndex : -1;

        protected override IList<ColumnBase> GetColumnsList() => 
            base.TableView.TableViewBehavior.FixedNoneVisibleColumns;

        protected override int GetDragIndex(int dropIndex, Point pt) => 
            !base.UseLegacyColumnVisibleIndexes ? base.GetDragIndex(dropIndex, pt) : ((dropIndex - base.GetFirstVisibleIndex()) + this.DropIndexCorrection);

        protected override int GetRelativeVisibleIndex(UIElement element) => 
            base.UseLegacyColumnVisibleIndexes ? ((base.GetRelativeVisibleIndex(element) + base.GetFirstVisibleIndex()) - this.DropIndexCorrection) : base.GetRelativeVisibleIndex(element);

        protected override int DropIndexCorrection =>
            base.FixedLeftColumnsCount;
    }
}

