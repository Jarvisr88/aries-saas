namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;

    public class XlsContentGuts : XlsContentBase
    {
        private int rowGutterMaxOutlineLevel;
        private int columnGutterMaxOutlineLevel;

        private int DecodeOutlineLevel(int value) => 
            (value != 0) ? (value - 1) : 0;

        private int EncodeOutlineLevel(int value) => 
            (value != 0) ? (value + 1) : 0;

        public override int GetSize() => 
            8;

        public override void Read(XlReader reader, int size)
        {
            reader.ReadUInt16();
            reader.ReadUInt16();
            this.RowGutterMaxOutlineLevel = this.DecodeOutlineLevel(reader.ReadUInt16());
            this.ColumnGutterMaxOutlineLevel = this.DecodeOutlineLevel(reader.ReadUInt16());
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) 0);
            writer.Write((ushort) 0);
            writer.Write((ushort) this.EncodeOutlineLevel(this.RowGutterMaxOutlineLevel));
            writer.Write((ushort) this.EncodeOutlineLevel(this.ColumnGutterMaxOutlineLevel));
        }

        public int RowGutterMaxOutlineLevel
        {
            get => 
                this.rowGutterMaxOutlineLevel;
            set
            {
                base.CheckValue(value, 0, 7, "RowGutterMaxOutlineLevel");
                this.rowGutterMaxOutlineLevel = value;
            }
        }

        public int ColumnGutterMaxOutlineLevel
        {
            get => 
                this.columnGutterMaxOutlineLevel;
            set
            {
                base.CheckValue(value, 0, 7, "ColumnGutterMaxOutlineLevel");
                this.columnGutterMaxOutlineLevel = value;
            }
        }
    }
}

