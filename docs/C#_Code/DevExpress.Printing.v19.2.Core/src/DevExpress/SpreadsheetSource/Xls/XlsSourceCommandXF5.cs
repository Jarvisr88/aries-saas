namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;

    public class XlsSourceCommandXF5 : XlsSourceCommandBase
    {
        private int numberFormatId;

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.NumberFormatIds.Add(this.numberFormatId);
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            reader.ReadInt16();
            this.numberFormatId = reader.ReadInt16();
            int count = base.Size - 4;
            if (count > 0)
            {
                reader.ReadBytes(count);
            }
        }
    }
}

