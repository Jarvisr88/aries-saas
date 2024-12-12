namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandNumber : XlsSourceCommandSingleCellBase
    {
        private XlsContentNumber content = new XlsContentNumber();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            if (dataReader.Stage == XlsSourceReaderStage.Index)
            {
                dataReader.RegisterCell(this.RowIndex, this.content.ColumnIndex, this.content.ColumnIndex);
            }
            else
            {
                int columnIndex = this.content.ColumnIndex;
                int fieldIndex = dataReader.TranslateColumnIndex(columnIndex);
                if (fieldIndex >= 0)
                {
                    int formatIndex = this.content.FormatIndex;
                    dataReader.AddCell(new Cell(fieldIndex, dataReader.GetDateTimeOrNumericValue(this.content.Value, formatIndex), columnIndex, formatIndex));
                }
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

