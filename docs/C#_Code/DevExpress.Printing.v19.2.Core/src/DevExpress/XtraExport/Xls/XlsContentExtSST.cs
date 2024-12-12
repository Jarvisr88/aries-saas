namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XlsContentExtSST : XlsContentBase
    {
        private int stringsInBucket = 8;
        private readonly List<XlsSSTInfo> items = new List<XlsSSTInfo>();

        public override int GetSize() => 
            2 + (this.items.Count * 8);

        public override void Read(XlReader reader, int size)
        {
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.StringsInBucket);
            foreach (XlsSSTInfo info in this.items)
            {
                writer.Write(info.StreamPosition);
                writer.Write((ushort) info.Offset);
                writer.Write((ushort) 0);
            }
        }

        public int StringsInBucket
        {
            get => 
                this.stringsInBucket;
            set
            {
                Guard.ArgumentNonNegative(value, "StringsInBucket value");
                this.stringsInBucket = value;
            }
        }

        public List<XlsSSTInfo> Items =>
            this.items;
    }
}

