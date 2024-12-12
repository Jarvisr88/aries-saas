namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandColumnInfo : XlsSourceCommandContentBase
    {
        private XlsContentColumnInfo content = new XlsContentColumnInfo();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            ColumnInfo item = new ColumnInfo(this.content.FirstColumn, this.content.LastColumn, this.content.Hidden, this.content.FormatIndex);
            dataReader.Columns.Add(item);
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

