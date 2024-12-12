namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandMulRk : XlsSourceCommandCellBase
    {
        private XlsContentMulRk content = new XlsContentMulRk();

        public override void Execute(XlsSourceDataReader dataReader)
        {
            if (dataReader.Stage == XlsSourceReaderStage.Index)
            {
                dataReader.RegisterCell(this.RowIndex, this.content.FirstColumnIndex, this.content.LastColumnIndex);
            }
            else
            {
                int count = this.content.RkRecords.Count;
                for (int i = 0; i < count; i++)
                {
                    XlsRkRec rec = this.content.RkRecords[i];
                    int columnIndex = this.content.FirstColumnIndex + i;
                    int fieldIndex = dataReader.TranslateColumnIndex(columnIndex);
                    if (fieldIndex >= 0)
                    {
                        int formatIndex = rec.FormatIndex;
                        dataReader.AddCell(new Cell(fieldIndex, dataReader.GetDateTimeOrNumericValue(rec.Rk.Value, formatIndex), columnIndex, formatIndex));
                    }
                }
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;

        public override int RowIndex =>
            this.content.RowIndex;
    }
}

