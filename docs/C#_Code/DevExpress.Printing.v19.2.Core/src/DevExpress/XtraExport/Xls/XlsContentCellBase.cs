namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public abstract class XlsContentCellBase : XlsContentBase
    {
        protected XlsContentCellBase()
        {
        }

        public override int GetSize() => 
            6;

        public override void Read(XlReader reader, int size)
        {
            this.RowIndex = reader.ReadUInt16();
            this.ColumnIndex = reader.ReadUInt16();
            this.FormatIndex = reader.ReadUInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.RowIndex);
            writer.Write((ushort) this.ColumnIndex);
            writer.Write((ushort) this.FormatIndex);
        }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public int FormatIndex { get; set; }
    }
}

