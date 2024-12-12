namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Printing.DataAwareExport.Export.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class AdvBandedSummaryExportHelper<TCol, TRow> : SummaryExportHelper<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private readonly AdvBandedExportInfo<TCol, TRow> advBandedExportInfo;

        public AdvBandedSummaryExportHelper(ExportInfo<TCol, TRow> exportInfo) : base(exportInfo)
        {
            this.advBandedExportInfo = exportInfo as AdvBandedExportInfo<TCol, TRow>;
        }

        private bool CanMerge(BandNodeDescriptor desc)
        {
            int num;
            return ((this.advBandedExportInfo.ItemsByNodes.Count <= 0) || (!this.advBandedExportInfo.ItemsByNodes.TryGetValue(desc, out num) || ((desc.RowCount <= 1) || (num <= 1))));
        }

        private bool CheckOnlyHorzMerging()
        {
            if (this.advBandedExportInfo.ItemsByNodes.Count <= 0)
            {
                return false;
            }
            Func<KeyValuePair<BandNodeDescriptor, int>, int> selector = <>c<TCol, TRow>.<>9__9_0;
            if (<>c<TCol, TRow>.<>9__9_0 == null)
            {
                Func<KeyValuePair<BandNodeDescriptor, int>, int> local1 = <>c<TCol, TRow>.<>9__9_0;
                selector = <>c<TCol, TRow>.<>9__9_0 = x => x.Value;
            }
            return (this.advBandedExportInfo.ItemsByNodes.Max<KeyValuePair<BandNodeDescriptor, int>>(selector) > 1);
        }

        protected override void CorrectExportRowIndex(bool correct)
        {
            ExportInfo<TCol, TRow> exportInfo = base.ExportInfo;
            exportInfo.ExportRowIndex--;
        }

        protected override void ExecuteCore(FooterAreaType areaType, int index, Action<ISummaryItemEx, TCol, IXlCell> action, List<ISummaryItemEx> tempCollection)
        {
            base.ExportInfo.Exporter.BeginRow();
            BandedAreaRowPattern patternByArea = this.GetPatternByArea(areaType);
            BandedRowInfo info = patternByArea[index];
            for (int i = 0; i < this.advBandedExportInfo.BandedColumnsCount; i++)
            {
                BandNodeDescriptor descriptorByCellPosition = info.GetDescriptorByCellPosition(i);
                IXlCell cell = base.ExportInfo.Exporter.BeginCell();
                if (descriptorByCellPosition.Column != null)
                {
                    ISummaryItemEx itemByKey = base.GetItemByKey(tempCollection, descriptorByCellPosition.Column.FieldName);
                    if (itemByKey != null)
                    {
                        action(itemByKey, (TCol) descriptorByCellPosition.Column, cell);
                    }
                    else if (base.ExportInfo.Options.CanRaiseCustomizeCellEvent)
                    {
                        CustomizeSummaryCellInfo<TCol, TRow> info2 = new CustomizeSummaryCellInfo<TCol, TRow> {
                            AreaType = (areaType == FooterAreaType.GroupFooter) ? SheetAreaType.GroupFooter : SheetAreaType.TotalFooter,
                            Column = (TCol) descriptorByCellPosition.Column,
                            Cell = cell,
                            View = base.ExportInfo.View,
                            ExportRowIndex = base.ExportInfo.ExportRowIndex,
                            Options = base.ExportInfo.Options,
                            Cellobj = new CellObject()
                        };
                        CustomizationEventsUtils<TCol, TRow>.RaiseCustomizeSummaryCellEvent(info2, base.ExportInfo);
                    }
                    tempCollection.Remove(itemByKey);
                    int row = (base.ExportInfo.ExportRowIndex - patternByArea.Count) + index;
                    if (this.CanMerge(descriptorByCellPosition))
                    {
                        MergingUtils.MergeCells(base.ExportInfo.Sheet, descriptorByCellPosition, i, row, true, this.CheckOnlyHorzMerging());
                    }
                }
                FormattingUtils.SetBorder(cell.Formatting, base.ExportInfo.AllowHorzLines, base.ExportInfo.AllowVertLines);
                base.ExportInfo.Exporter.EndCell();
            }
            base.ExportInfo.Exporter.EndRow();
        }

        private BandNodeDescriptor FindDescriptor(string fieldName) => 
            this.advBandedExportInfo.BandsLayoutInfo.Values.FirstOrDefault<BandNodeDescriptor>(x => x.Column.FieldName == fieldName);

        protected internal override int FooterRowsCount(IEnumerable<ISummaryItemEx> collection) => 
            ((AdvBandedExportInfo<TCol, TRow>) base.ExportInfo).ConstructAdvBandedFooterRowPattern(collection).Count;

        protected override int GetColumnPosition(string fieldName) => 
            !string.IsNullOrEmpty(fieldName) ? this.advBandedExportInfo.BandsLayoutInfo.Values.FirstOrDefault<BandNodeDescriptor>(x => (x.Column.FieldName == fieldName)).ColIndex : -3;

        protected override string GetCorrectFieldName(string itemFieldName, TCol gridColumn, bool isCount)
        {
            IColumn objA = this.FindDescriptor(itemFieldName).Column;
            return ((string.IsNullOrEmpty(itemFieldName) || (ReferenceEquals(objA, null) & isCount)) ? gridColumn.FieldName : itemFieldName);
        }

        protected override List<XlCellRange> GetFullSheetRange(int columnPosition, int endRangeRow, string fieldName) => 
            this.GetRangeList(columnPosition, fieldName, base.ExportInfo.GroupsList);

        private BandedAreaRowPattern GetPatternByArea(FooterAreaType areaType) => 
            (areaType == FooterAreaType.GroupFooter) ? this.advBandedExportInfo.GroupFooterRowPattern : ((areaType == FooterAreaType.TotalFooter) ? this.advBandedExportInfo.TotalFooterRowPattern : this.advBandedExportInfo.BandedRowPattern);

        protected override List<XlCellRange> GetRangeList(IList<Group> dataRanges, int columnPosition, string fieldName)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            BandNodeDescriptor descriptor = this.FindDescriptor(fieldName);
            if (descriptor.Column != null)
            {
                int num = this.advBandedExportInfo.BandedRowPattern.FindColumnRowIndexInTemplate(descriptor.Column.FieldName);
                int bandedRowPatternCount = this.advBandedExportInfo.BandedRowPatternCount;
                if (bandedRowPatternCount == 1)
                {
                    return base.GetRangeList(dataRanges, columnPosition, fieldName);
                }
                if (dataRanges != null)
                {
                    int num3 = 0;
                    while (num3 < dataRanges.Count)
                    {
                        int start = dataRanges[num3].Start;
                        while (true)
                        {
                            Group group = dataRanges[num3];
                            if (start >= (group.End - 1))
                            {
                                num3++;
                                break;
                            }
                            int row = start + num;
                            list.Add(new XlCellRange(new XlCellPosition(columnPosition, row), new XlCellPosition(columnPosition, row)));
                            start += bandedRowPatternCount;
                        }
                    }
                }
            }
            return list;
        }

        protected override List<XlCellRange> GetRangeList(int columnPosition, string fieldName, List<Group> groupsList)
        {
            List<XlCellRange> list = new List<XlCellRange>();
            BandNodeDescriptor descriptor = this.FindDescriptor(fieldName);
            if (descriptor.Column != null)
            {
                int num = this.advBandedExportInfo.BandedRowPattern.FindColumnRowIndexInTemplate(descriptor.Column.FieldName);
                int bandedRowPatternCount = this.advBandedExportInfo.BandedRowPatternCount;
                if (bandedRowPatternCount == 1)
                {
                    return base.GetRangeList(columnPosition, fieldName, groupsList);
                }
                int num3 = 0;
                while (num3 < groupsList.Count)
                {
                    Group objA = groupsList[num3];
                    int end = objA.End;
                    if (Equals(objA, base.ExportInfo.GroupsList.Last<Group>()))
                    {
                        end--;
                    }
                    int start = objA.Start;
                    while (true)
                    {
                        if (start >= end)
                        {
                            num3++;
                            break;
                        }
                        int row = start + num;
                        list.Add(new XlCellRange(new XlCellPosition(columnPosition, row), new XlCellPosition(columnPosition, row)));
                        start += bandedRowPatternCount;
                    }
                }
            }
            return list;
        }

        protected override void ModifyRowIndex(int totalFooterRowsCnt)
        {
            this.advBandedExportInfo.ExportRowIndex += totalFooterRowsCnt;
        }

        protected override void RemoveSkippedRowItems(FooterAreaType areaType, int ind, List<ISummaryItemEx> tempCollection)
        {
            BandedRowInfo info = this.GetPatternByArea(areaType)[ind];
            for (int i = 0; i < this.advBandedExportInfo.BandedColumnsCount; i++)
            {
                BandNodeDescriptor descriptorByCellPosition = info.GetDescriptorByCellPosition(i);
                if (descriptorByCellPosition.Column != null)
                {
                    ISummaryItemEx itemByKey = base.GetItemByKey(tempCollection, descriptorByCellPosition.Column.FieldName);
                    tempCollection.Remove(itemByKey);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AdvBandedSummaryExportHelper<TCol, TRow>.<>c <>9;
            public static Func<KeyValuePair<BandNodeDescriptor, int>, int> <>9__9_0;

            static <>c()
            {
                AdvBandedSummaryExportHelper<TCol, TRow>.<>c.<>9 = new AdvBandedSummaryExportHelper<TCol, TRow>.<>c();
            }

            internal int <CheckOnlyHorzMerging>b__9_0(KeyValuePair<BandNodeDescriptor, int> x) => 
                x.Value;
        }
    }
}

