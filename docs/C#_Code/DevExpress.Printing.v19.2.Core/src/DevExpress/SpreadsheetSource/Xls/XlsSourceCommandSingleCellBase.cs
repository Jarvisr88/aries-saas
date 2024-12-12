namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public abstract class XlsSourceCommandSingleCellBase : XlsSourceCommandCellBase
    {
        protected XlsSourceCommandSingleCellBase()
        {
        }

        public override int RowIndex =>
            this.Content.RowIndex;

        private XlsContentCellBase Content =>
            this.GetContent() as XlsContentCellBase;
    }
}

