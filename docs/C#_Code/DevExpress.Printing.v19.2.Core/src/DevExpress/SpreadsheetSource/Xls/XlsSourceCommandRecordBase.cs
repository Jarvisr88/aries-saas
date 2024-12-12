namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;

    public abstract class XlsSourceCommandRecordBase : XlsSourceCommandBase
    {
        private byte[] data = new byte[0];

        protected XlsSourceCommandRecordBase()
        {
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.Data = reader.ReadBytes(base.Size);
        }

        public byte[] Data
        {
            get => 
                this.data;
            private set
            {
                if (value != null)
                {
                    this.data = value;
                }
                else
                {
                    this.data = new byte[0];
                }
            }
        }
    }
}

