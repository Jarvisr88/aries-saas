namespace DevExpress.SpreadsheetSource.Xls
{
    using System;

    public abstract class XlsSourceCommandCellBase : XlsSourceCommandContentBase
    {
        protected XlsSourceCommandCellBase()
        {
        }

        public abstract int RowIndex { get; }
    }
}

