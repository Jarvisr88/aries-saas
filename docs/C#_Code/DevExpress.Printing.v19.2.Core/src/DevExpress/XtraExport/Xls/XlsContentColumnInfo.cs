namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentColumnInfo : XlsContentBase
    {
        private int firstColumn;
        private int lastColumn;
        private int columnWidth;
        private int outlineLevel;

        public override int GetSize() => 
            12;

        public override void Read(XlReader reader, int size)
        {
            this.FirstColumn = reader.ReadUInt16();
            this.LastColumn = reader.ReadUInt16();
            this.ColumnWidth = reader.ReadUInt16();
            this.FormatIndex = reader.ReadUInt16();
            ushort num = reader.ReadUInt16();
            this.Hidden = Convert.ToBoolean((int) (num & 1));
            this.CustomWidth = Convert.ToBoolean((int) (num & 2));
            this.BestFit = Convert.ToBoolean((int) (num & 4));
            this.ShowPhoneticInfo = Convert.ToBoolean((int) (num & 8));
            this.OutlineLevel = (num & 0x700) >> 8;
            this.Collapsed = Convert.ToBoolean((int) (num & 0x1000));
            int num2 = size - 10;
            if (num2 > 0)
            {
                reader.Seek((long) num2, SeekOrigin.Current);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.FirstColumn);
            writer.Write((ushort) this.LastColumn);
            writer.Write((ushort) this.ColumnWidth);
            writer.Write((ushort) this.FormatIndex);
            ushort num = (ushort) ((this.OutlineLevel & 7) << 8);
            if (this.Hidden)
            {
                num = (ushort) (num | 1);
            }
            if (this.CustomWidth)
            {
                num = (ushort) (num | 2);
            }
            if (this.BestFit)
            {
                num = (ushort) (num | 4);
            }
            if (this.ShowPhoneticInfo)
            {
                num = (ushort) (num | 8);
            }
            if (this.Collapsed)
            {
                num = (ushort) (num | 0x1000);
            }
            writer.Write(num);
            writer.Write((ushort) 0);
        }

        public int FirstColumn
        {
            get => 
                this.firstColumn;
            set
            {
                base.CheckValue(value, 0, 0x100, "FirstColumn");
                this.firstColumn = value;
            }
        }

        public int LastColumn
        {
            get => 
                this.lastColumn;
            set
            {
                base.CheckValue(value, 0, 0x100, "LastColumn");
                this.lastColumn = value;
            }
        }

        public int ColumnWidth
        {
            get => 
                this.columnWidth;
            set
            {
                base.CheckValue(value, 0, 0xffff, "ColumnWidth");
                this.columnWidth = value;
            }
        }

        public int FormatIndex { get; set; }

        public bool Hidden { get; set; }

        public bool CustomWidth { get; set; }

        public bool BestFit { get; set; }

        public bool ShowPhoneticInfo { get; set; }

        public int OutlineLevel
        {
            get => 
                this.outlineLevel;
            set
            {
                base.CheckValue(value, 0, 7, "OutlineLevel");
                this.outlineLevel = value;
            }
        }

        public bool Collapsed { get; set; }
    }
}

