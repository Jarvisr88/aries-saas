namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandEncoding : XlsSourceCommandContentBase
    {
        private XlsContentEncoding content = new XlsContentEncoding();

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.WorkbookEncoding = this.content.Value;
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

