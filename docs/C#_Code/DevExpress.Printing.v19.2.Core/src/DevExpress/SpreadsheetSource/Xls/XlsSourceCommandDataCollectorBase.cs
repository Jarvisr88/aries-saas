namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public abstract class XlsSourceCommandDataCollectorBase : XlsSourceCommandRecordBase, IXlsSourceDataCollector
    {
        protected XlsSourceCommandDataCollectorBase()
        {
        }

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            if (base.Data.Length != 0)
            {
                contentBuilder.PushDataCollector(this);
                this.PutData(base.Data, contentBuilder);
            }
        }

        protected virtual bool GetCompleted() => 
            false;

        public virtual void PutData(byte[] data, XlsSpreadsheetSource contentBuilder)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, false)))
            {
                using (XlReader reader2 = new XlReader(reader))
                {
                    this.ReadData(reader2, contentBuilder);
                }
            }
            if (this.GetCompleted())
            {
                contentBuilder.PopDataCollector();
            }
        }

        protected virtual void ReadData(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
        }
    }
}

