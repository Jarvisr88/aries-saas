namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandBlank : XlsSourceCommandSingleCellBase
    {
        private XlsContentBlank content = new XlsContentBlank();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            if (dataReader.Stage == XlsSourceReaderStage.Index)
            {
                dataReader.RegisterCell(this.RowIndex, this.content.ColumnIndex, this.content.ColumnIndex);
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

