namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsSourceCommandEmpty : XlsSourceCommandBase
    {
        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            reader.Seek((long) base.Size, SeekOrigin.Current);
        }
    }
}

