namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class TreeListSummaryExportHelper<TCol, TRow> : SummaryExportHelper<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public TreeListSummaryExportHelper(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
        }

        protected override List<XlCellRange> CalcSummaryRange(SheetAreaType areaType, IList<Group> gRanges, int colPosition, string fieldName, XlSummary summaryType, bool isRecursive, bool alignByColumnInFooter)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            if (areaType == SheetAreaType.GroupFooter)
            {
                list = this.GetRangeList(gRanges, colPosition, fieldName);
            }
            if (areaType == SheetAreaType.TotalFooter)
            {
                if (isRecursive)
                {
                    list = this.GetFullSheetRange(colPosition, base.ExportInfo.ExportRowIndex, fieldName);
                }
                else
                {
                    Func<Group, bool> predicate = <>c<TCol, TRow>.<>9__3_0;
                    if (<>c<TCol, TRow>.<>9__3_0 == null)
                    {
                        Func<Group, bool> local1 = <>c<TCol, TRow>.<>9__3_0;
                        predicate = <>c<TCol, TRow>.<>9__3_0 = x => x.GroupLevel == 0;
                    }
                    List<Group> groupsList = base.ExportInfo.GroupsList.Where<Group>(predicate).ToList<Group>();
                    if ((groupsList.Count == 0) && (base.ExportInfo.GroupsList.Count == 1))
                    {
                        list.Add(new XlCellRange(new XlCellPosition(colPosition, base.ExportInfo.GroupsList[0].Start), new XlCellPosition(colPosition, base.ExportInfo.GroupsList[0].End - 1)));
                    }
                    else
                    {
                        list = this.GetRangeList(colPosition, fieldName, groupsList);
                    }
                }
            }
            return list;
        }

        protected override bool CheckRecursive(ISummaryItemEx item)
        {
            ITreeListNodeSummaryItem item2 = item as ITreeListNodeSummaryItem;
            return ((item2 == null) || item2.IsRecursive);
        }

        public override void ExportTotalSummary()
        {
            if (base.AllowExportTotalSummary && (base.ExportInfo.ExportRowIndex != this.GetStartRangePosition()))
            {
                List<ISummaryItemEx> totalSummaryItems = base.ExportInfo.View.GridTotalSummaryItemCollection.Where<ISummaryItemEx>(this.SelectNodeItemsPredicate()).ToList<ISummaryItemEx>();
                base.ExportTotalSummaryFooterCore(totalSummaryItems);
                List<ISummaryItemEx> list2 = base.ExportInfo.View.GridTotalSummaryItemCollection.Where<ISummaryItemEx>(this.SelectTotalItemsPredicate()).ToList<ISummaryItemEx>();
                base.ExportTotalSummaryFooterCore(list2);
            }
        }

        protected override IList<Group> GetGroupSummaryDataRanges(XlGroup group, ISummaryItemEx item)
        {
            ITreeListNodeSummaryItem item2 = item as ITreeListNodeSummaryItem;
            return ((item2 == null) ? base.GetGroupSummaryDataRanges(group, item) : (!item2.IsRecursive ? (from x in group.DataRanges
                where x.GroupLevel == group.Group.OutlineLevel
                select x).ToList<Group>() : (from x in group.DataRanges
                where x.GroupLevel >= group.Group.OutlineLevel
                select x).ToList<Group>()));
        }

        protected override List<XlCellRange> GetRangeList(int columnPosition, string fieldName, List<Group> groupsList)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            for (int i = 0; i < groupsList.Count; i++)
            {
                Group objA = groupsList[i];
                int end = objA.End;
                if (Equals(objA, base.ExportInfo.GroupsList.Last<Group>()))
                {
                    end--;
                }
                list.Add(new XlCellRange(new XlCellPosition(columnPosition, objA.Start), new XlCellPosition(columnPosition, end)));
            }
            return list;
        }

        protected override int GetStartRangePosition() => 
            (base.ExportInfo.Options.ShowColumnHeaders != DefaultBoolean.True) ? 0 : 1;

        private Func<ISummaryItemEx, bool> SelectNodeItemsPredicate() => 
            <>c<TCol, TRow>.<>9__7_0 ??= x => ((ITreeListNodeSummaryItem) x).IsNodeSummaryItem;

        private Func<ISummaryItemEx, bool> SelectTotalItemsPredicate() => 
            <>c<TCol, TRow>.<>9__8_0 ??= x => !((ITreeListNodeSummaryItem) x).IsNodeSummaryItem;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TreeListSummaryExportHelper<TCol, TRow>.<>c <>9;
            public static Func<Group, bool> <>9__3_0;
            public static Func<ISummaryItemEx, bool> <>9__7_0;
            public static Func<ISummaryItemEx, bool> <>9__8_0;

            static <>c()
            {
                TreeListSummaryExportHelper<TCol, TRow>.<>c.<>9 = new TreeListSummaryExportHelper<TCol, TRow>.<>c();
            }

            internal bool <CalcSummaryRange>b__3_0(Group x) => 
                x.GroupLevel == 0;

            internal bool <SelectNodeItemsPredicate>b__7_0(ISummaryItemEx x) => 
                ((ITreeListNodeSummaryItem) x).IsNodeSummaryItem;

            internal bool <SelectTotalItemsPredicate>b__8_0(ISummaryItemEx x) => 
                !((ITreeListNodeSummaryItem) x).IsNodeSummaryItem;
        }
    }
}

