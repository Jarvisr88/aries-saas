namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;

    public abstract class XlsSourceCommandContentBase : XlsSourceCommandBase
    {
        protected XlsSourceCommandContentBase()
        {
        }

        protected abstract IXlsContent GetContent();
        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            IXlsContent content = this.GetContent();
            if (content != null)
            {
                content.Read(reader, base.Size);
            }
        }
    }
}

