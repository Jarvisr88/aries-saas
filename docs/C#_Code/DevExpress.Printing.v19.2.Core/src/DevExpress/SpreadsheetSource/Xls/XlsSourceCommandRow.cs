namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandRow : XlsSourceCommandContentBase
    {
        private XlsContentRow content = new XlsContentRow();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            XlsRow row = new XlsRow(this.content.Index, this.content.FirstColumnIndex, this.content.LastColumnIndex, this.content.FormatIndex, this.content.IsHidden);
            dataReader.AddRow(row);
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

