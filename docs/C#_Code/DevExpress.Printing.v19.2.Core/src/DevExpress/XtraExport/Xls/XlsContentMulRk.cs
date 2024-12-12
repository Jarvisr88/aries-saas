namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentMulRk : XlsContentBase
    {
        private const int fixedPartSize = 6;
        private const int variablePartElementSize = 6;
        private readonly List<XlsRkRec> rkRecords = new List<XlsRkRec>();

        public override int GetSize() => 
            6 + (this.RkRecords.Count * 6);

        public override void Read(XlReader reader, int size)
        {
            this.RowIndex = reader.ReadUInt16();
            this.FirstColumnIndex = reader.ReadUInt16();
            this.RkRecords.Clear();
            int num = (size - 6) / 6;
            for (int i = 0; i < num; i++)
            {
                this.RkRecords.Add(XlsRkRec.Read(reader));
            }
            reader.ReadUInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.RowIndex);
            writer.Write((ushort) this.FirstColumnIndex);
            int count = this.RkRecords.Count;
            for (int i = 0; i < count; i++)
            {
                this.RkRecords[i].Write(writer);
            }
            writer.Write((ushort) this.LastColumnIndex);
        }

        public int RowIndex { get; set; }

        public int FirstColumnIndex { get; set; }

        public int LastColumnIndex =>
            (this.FirstColumnIndex + this.RkRecords.Count) - 1;

        public List<XlsRkRec> RkRecords =>
            this.rkRecords;
    }
}

