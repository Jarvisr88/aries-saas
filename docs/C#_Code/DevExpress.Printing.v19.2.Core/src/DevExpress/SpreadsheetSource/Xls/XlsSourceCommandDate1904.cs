namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsSourceCommandDate1904 : XlsSourceCommandBase
    {
        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.IsDate1904 = this.IsDate1904;
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.IsDate1904 = reader.ReadInt16() == 1;
        }

        public bool IsDate1904 { get; private set; }
    }
}

