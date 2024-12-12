namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class FitColumnHeaderDropTarget : ServiceColumnHeaderDropTarget
    {
        public FitColumnHeaderDropTarget(GridColumnHeaderBase columnHeader) : base(columnHeader)
        {
        }

        protected override bool CanDropCore(int dropIndex, ColumnBase sourceColumn, HeaderPresenterType headerPresenterType)
        {
            if (base.AllowDragToServiceColumn)
            {
                if (base.IsLastColumnOfItsType(sourceColumn))
                {
                    return false;
                }
                switch (sourceColumn.Fixed)
                {
                    case FixedStyle.None:
                    {
                        ScrollInfoBase scrollInfo = base.ScrollInfo;
                        return (base.ThereIsNoFixedRightColumns && ((scrollInfo == null) || (sourceColumn.Owner.AutoWidth || ((scrollInfo.Extent <= scrollInfo.Viewport) || IsScrolledToEnd(scrollInfo)))));
                    }
                    case FixedStyle.Left:
                        return (base.ThereIsNoFixedRightColumns && base.ThereIsNoFixedNoneColumns);

                    case FixedStyle.Right:
                        return true;
                }
            }
            return false;
        }

        protected override int GetDropIndexFromDragSource(UIElement element, Point pt)
        {
            if (base.UseLegacyColumnVisibleIndexes)
            {
                return 0;
            }
            IList<ColumnBase> visibleColumnsCollectionFromDragElement = base.GetVisibleColumnsCollectionFromDragElement(base.TableView.TableViewBehavior, element);
            return (((visibleColumnsCollectionFromDragElement == null) || (visibleColumnsCollectionFromDragElement.Count == 0)) ? 0 : (visibleColumnsCollectionFromDragElement[visibleColumnsCollectionFromDragElement.Count - 1].VisibleIndex + 1));
        }

        private static bool IsScrolledToEnd(ScrollInfoBase scrollInfo) => 
            (scrollInfo.Extent - scrollInfo.Viewport) == scrollInfo.Offset;

        protected override void UpdateDragAdornerLocationCore(UIElement sourceElement, object headerAnchor)
        {
            if (!base.UseLegacyColumnVisibleIndexes)
            {
                headerAnchor = 0;
            }
            base.UpdateDragAdornerLocationCore(sourceElement, headerAnchor);
        }

        protected override int DropIndexCorrection =>
            (base.FixedLeftColumnsCount + base.FixedNoneColumnsCount) + base.FixedRightColumnsCount;
    }
}

