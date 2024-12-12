namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.SpreadsheetSource.Implementation;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsSourceCommandRichString5 : XlsSourceCommandCellBase
    {
        private int rowIndex;

        public override void Execute(XlsSourceDataReader dataReader)
        {
            if (dataReader.Stage == XlsSourceReaderStage.Index)
            {
                dataReader.RegisterCell(this.RowIndex, this.ColumnIndex, this.ColumnIndex);
            }
            else
            {
                int fieldIndex = dataReader.TranslateColumnIndex(this.ColumnIndex);
                if (fieldIndex >= 0)
                {
                    dataReader.AddCell(new Cell(fieldIndex, this.Value, this.ColumnIndex, this.FormatIndex));
                }
            }
        }

        protected override IXlsContent GetContent() => 
            null;

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.rowIndex = reader.ReadUInt16();
            this.ColumnIndex = reader.ReadUInt16();
            this.FormatIndex = reader.ReadUInt16();
            this.Value = contentBuilder.ReadString2(reader);
            int count = reader.ReadByte() * 2;
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }

        public override int RowIndex =>
            this.rowIndex;

        public int ColumnIndex { get; private set; }

        public int FormatIndex { get; private set; }

        public string Value { get; private set; }
    }
}

