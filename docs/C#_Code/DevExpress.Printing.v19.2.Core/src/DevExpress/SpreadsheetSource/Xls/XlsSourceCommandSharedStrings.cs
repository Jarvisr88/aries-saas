namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;

    public class XlsSourceCommandSharedStrings : XlsSourceCommandDataCollectorBase
    {
        private int count;
        private int stringsToRead;
        private bool readingStrings;
        private XLUnicodeRichExtendedString sharedString = new XLUnicodeRichExtendedString();

        protected override bool GetCompleted() => 
            this.count == this.stringsToRead;

        protected override void ReadData(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            if (!this.readingStrings)
            {
                reader.ReadInt32();
                this.stringsToRead = reader.ReadInt32();
                this.readingStrings = true;
            }
            while (this.count < this.stringsToRead)
            {
                if (!this.ReadSharedString(reader))
                {
                    return;
                }
                contentBuilder.SharedStrings.Add(this.sharedString.Value);
                this.count++;
            }
        }

        private bool ReadSharedString(XlReader reader)
        {
            if (reader.Position == reader.StreamLength)
            {
                return false;
            }
            this.sharedString.ReadData(reader);
            return this.sharedString.Complete;
        }
    }
}

