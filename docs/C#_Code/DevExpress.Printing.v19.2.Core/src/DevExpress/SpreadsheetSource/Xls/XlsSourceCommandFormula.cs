namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandFormula : XlsSourceCommandSingleCellBase
    {
        private XlsContentFormula content = new XlsContentFormula();
        private string stringValue;
        private bool hasStringValue;

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
                    XlVariantValue value2 = this.GetValue(dataReader, formatIndex, columnIndex);
                    if (!value2.IsEmpty)
                    {
                        dataReader.AddCell(new Cell(fieldIndex, value2, columnIndex, formatIndex));
                    }
                }
            }
        }

        protected override IXlsContent GetContent() => 
            this.content;

        private XlVariantValue GetValue(XlsSourceDataReader dataReader, int formatIndex, int columnIndex)
        {
            XlsFormulaValue value2 = this.content.Value;
            return (!value2.IsBoolean ? (!value2.IsError ? (!value2.IsNumeric ? ((!value2.IsString || !this.hasStringValue) ? (!value2.IsBlankString ? XlVariantValue.Empty : string.Empty) : this.stringValue) : dataReader.GetDateTimeOrNumericValue(value2.NumericValue, formatIndex)) : XlCellErrorFactory.CreateError((XlCellErrorType) value2.ErrorCode)) : value2.BooleanValue);
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.stringValue = null;
            this.hasStringValue = false;
            base.ReadCore(reader, contentBuilder);
        }

        internal string StringValue
        {
            get => 
                this.stringValue;
            set
            {
                this.stringValue = value;
                this.hasStringValue = true;
            }
        }
    }
}

