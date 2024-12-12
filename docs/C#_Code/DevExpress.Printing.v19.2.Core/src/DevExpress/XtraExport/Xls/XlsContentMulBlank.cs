namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentMulBlank : XlsContentBase
    {
        private const short fixedPartSize = 6;
        private const short varPartItemSize = 2;
        private readonly List<int> formatIndices = new List<int>();

        public override int GetSize() => 
            6 + (this.formatIndices.Count * 2);

        public override void Read(XlReader reader, int size)
        {
            this.RowIndex = reader.ReadUInt16();
            this.FirstColumnIndex = reader.ReadUInt16();
            this.formatIndices.Clear();
            int num = (size - 6) / 2;
            for (int i = 0; i < num; i++)
            {
                this.formatIndices.Add(reader.ReadUInt16());
            }
            reader.ReadUInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.RowIndex);
            writer.Write((ushort) this.FirstColumnIndex);
            int count = this.formatIndices.Count;
            for (int i = 0; i < count; i++)
            {
                writer.Write((ushort) this.formatIndices[i]);
            }
            writer.Write((ushort) this.LastColumnIndex);
        }

        public int RowIndex { get; set; }

        public int FirstColumnIndex { get; set; }

        public int LastColumnIndex =>
            (this.FirstColumnIndex + this.formatIndices.Count) - 1;

        public IList<int> FormatIndices =>
            this.formatIndices;
    }
}

