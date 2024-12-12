namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraExport.Xls;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XlsSourceCommandExternSheet : XlsSourceCommandRecordBase
    {
        private short[] typeCodes = new short[] { 60 };
        private readonly List<XlsXTI> items = new List<XlsXTI>();

        protected override void CheckPosition(XlReader reader, long initialPosition, long expectedPosition)
        {
        }

        public override void Execute(XlsSpreadsheetSource contentBuilder)
        {
            contentBuilder.ExternSheets.AddRange(this.items);
            this.items.Clear();
        }

        protected override void ReadCore(XlReader reader, XlsSpreadsheetSource contentBuilder)
        {
            this.items.Clear();
            if (contentBuilder.ContentType != XlsContentType.WorkbookGlobals)
            {
                base.ReadCore(reader, contentBuilder);
            }
            else
            {
                using (XlsCommandStream stream = new XlsCommandStream(reader, this.typeCodes, base.Size))
                {
                    using (BinaryReader reader2 = new BinaryReader(stream))
                    {
                        int num = reader2.ReadUInt16();
                        for (int i = 0; i < num; i++)
                        {
                            XlsXTI item = XlsXTI.FromStream(reader2);
                            this.items.Add(item);
                        }
                    }
                }
            }
        }
    }
}

