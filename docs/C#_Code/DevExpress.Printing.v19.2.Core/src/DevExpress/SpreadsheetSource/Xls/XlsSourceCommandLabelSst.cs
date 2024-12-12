namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandLabelSst : XlsSourceCommandSingleCellBase
    {
        private XlsContentLabelSst content = new XlsContentLabelSst();

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
                    dataReader.AddCell(new Cell(fieldIndex, dataReader.GetSharedString(this.content.StringIndex), this.content.ColumnIndex, this.content.FormatIndex));
                }
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;
    }
}

