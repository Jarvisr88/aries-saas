namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Utils;
    using System;

    public class InlineStringDestination : SharedStringDestination
    {
        private readonly WorksheetCellDestination cellDestination;

        public InlineStringDestination(XlsxSpreadsheetSourceImporter importer, WorksheetCellDestination cellDestination) : base(importer)
        {
            Guard.ArgumentNotNull(cellDestination, "cellDestination");
            this.cellDestination = cellDestination;
        }

        protected override void ApplyText(string item)
        {
            this.cellDestination.Value = item;
        }
    }
}

