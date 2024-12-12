namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public class IndicatorColumnHeaderDropTarget : ServiceColumnHeaderDropTarget
    {
        public IndicatorColumnHeaderDropTarget(GridColumnHeaderBase columnHeader) : base(columnHeader)
        {
        }

        protected override bool CanDropCore(int dropIndex, ColumnBase sourceColumn, HeaderPresenterType headerPresenterType)
        {
            if (base.AllowDragToServiceColumn)
            {
                if (base.IsFirstColumnOfItsType(sourceColumn))
                {
                    return false;
                }
                switch (sourceColumn.Fixed)
                {
                    case FixedStyle.None:
                        return (base.ThereIsNoFixedLeftColumns && ((base.ScrollInfo == null) || (base.ScrollInfo.Offset == 0.0)));

                    case FixedStyle.Left:
                        return true;

                    case FixedStyle.Right:
                        return (base.ThereIsNoFixedLeftColumns && base.ThereIsNoFixedNoneColumns);
                }
            }
            return false;
        }

        protected override int GetDropIndexFromDragSource(UIElement element, Point pt)
        {
            if (base.TableView.IsCheckBoxSelectorColumnVisible)
            {
                return -1;
            }
            if (base.UseLegacyColumnVisibleIndexes)
            {
                return 0;
            }
            IList<ColumnBase> visibleColumnsCollectionFromDragElement = base.GetVisibleColumnsCollectionFromDragElement(base.TableView.TableViewBehavior, element);
            return (((visibleColumnsCollectionFromDragElement == null) || (visibleColumnsCollectionFromDragElement.Count == 0)) ? 0 : visibleColumnsCollectionFromDragElement[0].VisibleIndex);
        }
    }
}

