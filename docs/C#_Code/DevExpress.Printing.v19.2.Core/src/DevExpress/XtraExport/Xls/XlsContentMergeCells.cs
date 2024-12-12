namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XlsContentMergeCells : XlsContentBase
    {
        private readonly List<XlsRef8> mergeCells = new List<XlsRef8>();

        public override int GetSize() => 
            (Math.Min(this.mergeCells.Count, 0x402) * 8) + 2;

        public override void Read(XlReader reader, int size)
        {
            this.mergeCells.Clear();
            int num = reader.ReadUInt16();
            for (int i = 0; i < num; i++)
            {
                this.mergeCells.Add(XlsRef8.FromStream(reader));
            }
        }

        public override void Write(BinaryWriter writer)
        {
            int num = Math.Min(this.mergeCells.Count, 0x402);
            writer.Write((ushort) num);
            for (int i = 0; i < num; i++)
            {
                this.mergeCells[i].Write(writer);
            }
        }

        public IList<XlsRef8> MergeCells =>
            this.mergeCells;
    }
}

