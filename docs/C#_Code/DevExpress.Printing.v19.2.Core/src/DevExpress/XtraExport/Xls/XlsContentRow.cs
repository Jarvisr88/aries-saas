namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentRow : XlsContentBase
    {
        private const int minHeightInTwips = 0;
        private const int maxHeightInTwips = 0x2000;
        private int firstColumnIndex;
        private int lastColumnIndex;
        private int heightInTwips = 0xff;

        public override int GetSize() => 
            0x10;

        public override void Read(XlReader reader, int size)
        {
            this.Index = reader.ReadUInt16();
            int num = reader.ReadUInt16();
            int num2 = reader.ReadUInt16();
            if (num != num2)
            {
                this.FirstColumnIndex = num;
                this.LastColumnIndex = num2;
            }
            this.heightInTwips = reader.ReadUInt16() & 0x7fff;
            if (this.heightInTwips > 0x2000)
            {
                this.heightInTwips = 0x2000;
            }
            reader.Seek(4L, SeekOrigin.Current);
            byte num3 = reader.ReadByte();
            this.OutlineLevel = num3 & 7;
            this.IsCollapsed = Convert.ToBoolean((int) (num3 & 0x10));
            this.IsHidden = Convert.ToBoolean((int) (num3 & 0x20));
            this.IsCustomHeight = Convert.ToBoolean((int) (num3 & 0x40));
            this.HasFormatting = Convert.ToBoolean((int) (num3 & 0x80));
            reader.ReadByte();
            ushort num4 = reader.ReadUInt16();
            this.FormatIndex = num4 & 0xfff;
            this.HasThickBorder = Convert.ToBoolean((int) (num4 & 0x1000));
            this.HasMediumBorder = Convert.ToBoolean((int) (num4 & 0x2000));
            this.HasPhoneticGuide = Convert.ToBoolean((int) (num4 & 0x4000));
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((ushort) this.Index);
            writer.Write((ushort) this.FirstColumnIndex);
            writer.Write((ushort) this.LastColumnIndex);
            writer.Write((ushort) this.HeightInTwips);
            writer.BaseStream.Seek(4L, SeekOrigin.Current);
            byte outlineLevel = (byte) this.OutlineLevel;
            if (this.IsCollapsed)
            {
                outlineLevel = (byte) (outlineLevel | 0x10);
            }
            if (this.IsHidden)
            {
                outlineLevel = (byte) (outlineLevel | 0x20);
            }
            if (this.IsCustomHeight)
            {
                outlineLevel = (byte) (outlineLevel | 0x40);
            }
            if (this.HasFormatting)
            {
                outlineLevel = (byte) (outlineLevel | 0x80);
            }
            writer.Write(outlineLevel);
            writer.Write((byte) 1);
            ushort num2 = (ushort) (this.FormatIndex & 0xfff);
            if (this.HasThickBorder)
            {
                num2 = (ushort) (num2 | 0x1000);
            }
            if (this.HasMediumBorder)
            {
                num2 = (ushort) (num2 | 0x2000);
            }
            if (this.HasPhoneticGuide)
            {
                num2 = (ushort) (num2 | 0x4000);
            }
            writer.Write(num2);
        }

        public int HeightInTwips
        {
            get => 
                this.heightInTwips;
            set
            {
                this.heightInTwips = value;
                if (this.heightInTwips < 0)
                {
                    this.heightInTwips = 0;
                }
                if (this.heightInTwips > 0x2000)
                {
                    this.heightInTwips = 0x2000;
                }
            }
        }

        public int Index { get; set; }

        public int FirstColumnIndex
        {
            get => 
                this.firstColumnIndex;
            set
            {
                base.CheckValue(value, 0, 0xff, "FirstColumnIndex");
                this.firstColumnIndex = value;
            }
        }

        public int LastColumnIndex
        {
            get => 
                this.lastColumnIndex;
            set
            {
                base.CheckValue(value, 0, 0x100, "LastColumnIndex");
                this.lastColumnIndex = value;
            }
        }

        public int OutlineLevel { get; set; }

        public bool IsCollapsed { get; set; }

        public bool IsHidden { get; set; }

        public bool IsCustomHeight { get; set; }

        public bool HasFormatting { get; set; }

        public int FormatIndex { get; set; }

        public bool HasThickBorder { get; set; }

        public bool HasMediumBorder { get; set; }

        public bool HasPhoneticGuide { get; set; }
    }
}

