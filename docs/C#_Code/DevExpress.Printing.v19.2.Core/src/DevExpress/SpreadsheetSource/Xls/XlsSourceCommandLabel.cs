namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandLabel : XlsSourceCommandSingleCellBase
    {
        private XlsContentLabel content = new XlsContentLabel();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            if (dataReader.Stage == XlsSourceReaderStage.Index)
            {
                dataReader.RegisterCell(this.RowIndex, this.content.ColumnIndex, this.content.ColumnIndex);
            }
            else
            {
                int fieldIndex = dataReader.TranslateColumnIndex(this.content.ColumnIndex);
                if (fieldIndex >= 0)
                {
                    dataReader.AddCell(new Cell(fieldIndex, this.content.Value, this.content.ColumnIndex, this.content.FormatIndex));
                }
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;

        protected XlsContentLabel InnerContent =>
            this.content;
    }
}

