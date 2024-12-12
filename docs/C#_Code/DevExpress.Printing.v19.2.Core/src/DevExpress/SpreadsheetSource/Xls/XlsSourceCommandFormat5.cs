namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsSourceCommandFormat5 : XlsSourceCommandBase
    {
        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.NumberFormatCodes[this.FormatId] = this.FormatCode;
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.FormatId = reader.ReadUInt16();
            this.FormatCode = contentBuilder.ReadString(reader);
        }

        public int FormatId { get; private set; }

        public string FormatCode { get; private set; }
    }
}

