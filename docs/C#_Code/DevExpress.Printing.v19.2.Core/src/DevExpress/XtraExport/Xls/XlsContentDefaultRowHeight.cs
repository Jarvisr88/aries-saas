namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentDefaultRowHeight : XlsContentBase
    {
        private int defaultRowHeightInTwips;

        public override int GetSize() => 
            4;

        public override void Read(XlReader reader, int size)
        {
            short num = reader.ReadInt16();
            this.CustomHeight = Convert.ToBoolean((int) (num & 1));
            this.ZeroHeightOnEmptyRows = Convert.ToBoolean((int) (num & 2));
            this.ThickTopBorder = Convert.ToBoolean((int) (num & 4));
            this.ThickBottomBorder = Convert.ToBoolean((int) (num & 8));
            this.DefaultRowHeightInTwips = reader.ReadInt16();
        }

        public override void Write(BinaryWriter writer)
        {
            short num = 0;
            if (this.CustomHeight)
            {
                num = (short) (num | 1);
            }
            if (this.ZeroHeightOnEmptyRows)
            {
                num = (short) (num | 2);
            }
            if (this.ThickTopBorder)
            {
                num = (short) (num | 4);
            }
            if (this.ThickBottomBorder)
            {
                num = (short) (num | 8);
            }
            writer.Write(num);
            writer.Write((short) this.DefaultRowHeightInTwips);
        }

        public bool CustomHeight { get; set; }

        public bool ZeroHeightOnEmptyRows { get; set; }

        public bool ThickTopBorder { get; set; }

        public bool ThickBottomBorder { get; set; }

        public int DefaultRowHeightInTwips
        {
            get => 
                this.defaultRowHeightInTwips;
            set
            {
                if (value > 0x1ff3)
                {
                    value = 0x1ff3;
                }
                this.defaultRowHeightInTwips = value;
            }
        }
    }
}

