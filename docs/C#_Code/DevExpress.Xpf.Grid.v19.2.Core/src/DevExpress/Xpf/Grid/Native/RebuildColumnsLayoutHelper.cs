namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class RebuildColumnsLayoutHelper : RebuildColumnsLayoutHelperBase
    {
        public RebuildColumnsLayoutHelper(DataViewBase view) : base(view)
        {
        }

        protected override void ApplyColumnVisibleIndexCore(ColumnBase source, int visibleIndex, int oldVisibleIndex)
        {
            BaseColumn.SetVisibleIndex(source, visibleIndex);
            int actualVisibleIndex = ApplyVisibleIndexInBounds(base.Columns, source, oldVisibleIndex);
            if (source.Visible)
            {
                this.UpdateVisibleColumnsList(source, actualVisibleIndex);
            }
        }

        internal static int ApplyVisibleIndexInBounds(IList parentCollection, BaseColumn source, int oldVisibleIndex)
        {
            int actualVisibleIndex = -1;
            ApplyVisibleIndexInBounds(parentCollection, source, oldVisibleIndex, delegate (BaseColumn column, bool isMoveToRight) {
                if (source.Visible && column.Visible)
                {
                    if (!isMoveToRight)
                    {
                        if ((actualVisibleIndex == -1) || (actualVisibleIndex > column.ActualVisibleIndex))
                        {
                            actualVisibleIndex = column.ActualVisibleIndex;
                        }
                    }
                    else if (actualVisibleIndex < column.ActualVisibleIndex)
                    {
                        actualVisibleIndex = column.ActualVisibleIndex;
                    }
                }
            });
            if ((actualVisibleIndex == -1) && (oldVisibleIndex != -1))
            {
                actualVisibleIndex = source.ActualVisibleIndex;
            }
            return actualVisibleIndex;
        }

        internal static void ApplyVisibleIndexInBounds(IList parentCollection, BaseColumn source, int oldVisibleIndex, Action<BaseColumn, bool> columnAction)
        {
            oldVisibleIndex = (oldVisibleIndex > -1) ? oldVisibleIndex : 0x7fffffff;
            int visibleIndex = source.VisibleIndex;
            int leftBound = Math.Min(oldVisibleIndex, visibleIndex);
            int rightBound = Math.Max(oldVisibleIndex, visibleIndex);
            bool isMoveToRight = visibleIndex > oldVisibleIndex;
            for (int i = 0; i < parentCollection.Count; i++)
            {
                BaseColumn objA = (BaseColumn) parentCollection[i];
                if (!ReferenceEquals(objA, source) && IsInBounds(objA, isMoveToRight, leftBound, rightBound))
                {
                    columnAction(objA, isMoveToRight);
                    BaseColumn.SetVisibleIndex(objA, isMoveToRight ? (objA.VisibleIndex - 1) : (objA.VisibleIndex + 1));
                }
            }
        }

        public override Tuple<ColumnBase, BandedViewDropPlace> GetColumnDropTarget(ColumnBase source, int targetVisibleIndex, HeaderPresenterType moveFrom)
        {
            int num = base.View.IsCheckBoxSelectorColumnVisible ? (base.Columns.Count + 1) : base.Columns.Count;
            return ((num != 0) ? ((num > targetVisibleIndex) ? ((targetVisibleIndex >= 0) ? new Tuple<ColumnBase, BandedViewDropPlace>(this.GetColumnFromVisibleIndex(targetVisibleIndex), BandedViewDropPlace.Left) : new Tuple<ColumnBase, BandedViewDropPlace>(this.GetColumnFromVisibleIndex(base.View.IsCheckBoxSelectorColumnVisible ? 1 : 0), BandedViewDropPlace.Left)) : new Tuple<ColumnBase, BandedViewDropPlace>(this.GetColumnFromVisibleIndex(num - 1), BandedViewDropPlace.Right)) : new Tuple<ColumnBase, BandedViewDropPlace>(source, BandedViewDropPlace.Left));
        }

        private ColumnBase GetColumnFromVisibleIndex(int targetVisibleIndex)
        {
            for (int i = 0; i < base.Columns.Count; i++)
            {
                ColumnBase base2 = base.Columns[i];
                if (base2.VisibleIndex == targetVisibleIndex)
                {
                    return base2;
                }
            }
            return null;
        }

        public override int GetFixedLeftColumnsCount(TableViewBehavior tableViewBehavior) => 
            tableViewBehavior.FixedLeftColumnsCount;

        public override int GetFixedNoneColumnsCount(TableViewBehavior tableViewBehavior) => 
            tableViewBehavior.FixedNoneColumnsCount;

        public override int GetFixedRightColumnsCount(TableViewBehavior tableViewBehavior) => 
            tableViewBehavior.FixedRightColumnsCount;

        internal static bool IsInBounds(BaseColumn column, bool isMoveToRight, int leftBound, int rightBound)
        {
            int visibleIndex = column.VisibleIndex;
            return (!isMoveToRight ? ((visibleIndex >= leftBound) && (visibleIndex < rightBound)) : ((visibleIndex > leftBound) && (visibleIndex <= rightBound)));
        }

        protected override IList<ColumnBase> RebuildVisibleColumnsCore()
        {
            bool hasFixedLeftColumns = false;
            List<ColumnBase> columns = new List<ColumnBase>(base.Columns as IEnumerable<ColumnBase>);
            int fixedLeftColumnsCount = 0;
            int fixedNoneColumnsCount = 0;
            int fixedRightColumnsCount = 0;
            for (int i = 0; i < columns.Count; i++)
            {
                ColumnBase base2 = columns[i];
                base2.index = i;
                FixedStyle @fixed = base2.Fixed;
                switch (@fixed)
                {
                    case FixedStyle.None:
                        fixedNoneColumnsCount++;
                        break;

                    case FixedStyle.Left:
                        fixedLeftColumnsCount++;
                        hasFixedLeftColumns |= base2.Visible;
                        break;

                    case FixedStyle.Right:
                        fixedRightColumnsCount++;
                        break;

                    default:
                        break;
                }
            }
            base.View.ViewBehavior.UpdateFixedAreaColumnsCount(fixedLeftColumnsCount, fixedNoneColumnsCount, fixedRightColumnsCount);
            columns.Sort(new Comparison<ColumnBase>(base.View.VisibleComparison));
            base.PatchColumns(columns, hasFixedLeftColumns);
            List<ColumnBase> visibleColumnsList = new List<ColumnBase>();
            for (int j = 0; j < columns.Count; j++)
            {
                ColumnBase element = columns[j];
                BaseColumn.SetVisibleIndex(element, j);
                if (base.IsColumnVisible(element))
                {
                    visibleColumnsList.Add(element);
                }
            }
            base.UpdateVisibleColumnsPositions(visibleColumnsList);
            return new ObservableCollection<ColumnBase>(visibleColumnsList);
        }

        private void UpdateVisibleColumnsList(ColumnBase source, int actualVisibleIndex)
        {
            ObservableCollection<ColumnBase> visibleColumns = new ObservableCollection<ColumnBase>(base.View.VisibleColumnsCore);
            visibleColumns.Remove(source);
            visibleColumns.Insert((actualVisibleIndex == -1) ? 0 : actualVisibleIndex, source);
            base.UpdateColumnsPositions(visibleColumns);
        }

        protected override bool ApplyVisibleIndex =>
            false;
    }
}

