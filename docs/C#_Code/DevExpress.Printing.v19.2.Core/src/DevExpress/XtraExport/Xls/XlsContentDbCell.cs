namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentDbCell : XlsContentBase
    {
        private const int fixedPartSize = 4;
        private const int variablePartItemSize = 2;
        private readonly List<int> streamOffsets = new List<int>();

        public override int GetSize() => 
            4 + (2 * this.streamOffsets.Count);

        public override void Read(XlReader reader, int size)
        {
            this.streamOffsets.Clear();
            this.FirstRowOffset = reader.ReadUInt32();
            int num = (size - 4) / 2;
            for (int i = 0; i < num; i++)
            {
                this.streamOffsets.Add(reader.ReadUInt16());
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((uint) this.FirstRowOffset);
            int count = this.streamOffsets.Count;
            for (int i = 0; i < count; i++)
            {
                writer.Write((ushort) this.streamOffsets[i]);
            }
        }

        public long FirstRowOffset { get; set; }

        public List<int> StreamOffsets =>
            this.streamOffsets;
    }
}

