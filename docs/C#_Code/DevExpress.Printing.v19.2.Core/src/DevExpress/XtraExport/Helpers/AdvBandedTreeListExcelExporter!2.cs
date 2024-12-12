namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using System;

    public class AdvBandedTreeListExcelExporter<TCol, TRow> : BandedTreeListExcelExporter<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public AdvBandedTreeListExcelExporter(IGridView<TCol, TRow> viewToExport) : base(viewToExport)
        {
        }

        public AdvBandedTreeListExcelExporter(IGridView<TCol, TRow> viewToExport, IDataAwareExportOptions options) : base(viewToExport, options)
        {
        }

        protected override void AddGroupNodeDataRange(XlGroup peekGroup, int startGroup, int endGroup, int groupLevel)
        {
            int count = ((TreeListAdvBandedExportInfo<TCol, TRow>) base.ExportInfo).BandedRowPattern.Count;
            Group item = new Group {
                Start = startGroup - count,
                End = endGroup,
                GroupLevel = groupLevel
            };
            peekGroup.DataRanges.Add(item);
        }

        protected override DataAwareExportContext<TCol, TRow> CreateContext(ExportInfo<TCol, TRow> exportInfo) => 
            new TreeListDataAwareAdvBandedExportContext<TCol, TRow>(exportInfo);

        internal override ExportInfo<TCol, TRow> CreateExportInfo() => 
            new TreeListAdvBandedExportInfo<TCol, TRow>(this);

        protected override void SetGroupNodeRange(int currentNodeLevel)
        {
            int count = ((TreeListAdvBandedExportInfo<TCol, TRow>) base.ExportInfo).BandedRowPattern.Count;
            this.AddGroupToList(base.ExportInfo.ExportRowIndex, base.ExportInfo.ExportRowIndex + count, currentNodeLevel);
        }
    }
}

