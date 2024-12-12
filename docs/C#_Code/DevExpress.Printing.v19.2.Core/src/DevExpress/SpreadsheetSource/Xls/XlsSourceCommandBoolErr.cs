namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandBoolErr : XlsSourceCommandSingleCellBase
    {
        private XlsContentBoolErr content = new XlsContentBoolErr();

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
                    if (this.IsError)
                    {
                        dataReader.AddCell(new Cell(fieldIndex, this.ErrorValue, this.content.ColumnIndex, this.content.FormatIndex));
                    }
                    else
                    {
                        dataReader.AddCell(new Cell(fieldIndex, this.BoolValue, this.content.ColumnIndex, this.content.FormatIndex));
                    }
                }
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;

        private bool BoolValue =>
            this.content.Value != 0;

        private XlVariantValue ErrorValue =>
            !this.IsError ? XlVariantValue.Empty : XlCellErrorFactory.CreateError((XlCellErrorType) this.content.Value);

        private bool IsError =>
            this.content.IsError;
    }
}

