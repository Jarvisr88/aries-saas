namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandFormat : XlsSourceCommandContentBase
    {
        private XlsContentNumberFormat content = new XlsContentNumberFormat();

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.NumberFormatCodes[this.content.FormatId] = this.content.FormatCode;
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

