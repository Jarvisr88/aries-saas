namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandBOF : XlsSourceCommandContentBase
    {
        private XlsContentBeginOfSubstream content = new XlsContentBeginOfSubstream();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            dataReader.StartContent(this.content.SubstreamType);
        }

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.StartContent(this.content.SubstreamType);
        }

        protected override IXlsContent GetContent() => 
            this.content;

        public override bool IsSubstreamBound =>
            true;
    }
}

