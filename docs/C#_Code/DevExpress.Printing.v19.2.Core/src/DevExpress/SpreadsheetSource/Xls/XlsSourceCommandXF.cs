namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandXF : XlsSourceCommandContentBase
    {
        private XlsContentXF content = new XlsContentXF();

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.NumberFormatIds.Add(this.content.NumberFormatId);
        }

        protected override IXlsContent GetContent()
        {
            this.content = new XlsContentXF();
            return this.content;
        }
    }
}

