namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Printing.ExportHelpers;
    using DevExpress.Utils;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class CustomizeSummaryCellInfo<TCol, TRow> : CustomizeCellInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public override CustomizeCellEventArgsExtended CreateEventArgs()
        {
            CustomizeCellEventArgsExtended extended = base.CreateEventArgs();
            extended.SummaryItem = this.ItemEx;
            extended.Value = (base.CellValue == null) ? this.SummaryValue : base.CellValue;
            return extended;
        }

        protected override CellObject GetExportedCellObject(TCol gridColumn)
        {
            XlFormattingObject obj2 = new XlFormattingObject();
            obj2.CopyFrom(gridColumn.Appearance, FormatType.Custom);
            CellObject obj1 = new CellObject();
            obj1.Formatting = obj2;
            return obj1;
        }

        public ISummaryItemEx Item { get; set; }

        public ExportSummaryItem ItemEx { get; set; }

        public object SummaryValue { get; set; }

        public IList<Group> GroupRanges { get; set; }
    }
}

