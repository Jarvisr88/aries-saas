namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;

    public class XlsDxfAlign
    {
        private const uint MaskHorizontalAlignment = 7;
        private const uint MaskWrapText = 8;
        private const uint MaskVerticalAlignment = 0x70;
        private const uint MaskJustifyLastLine = 0x80;
        private const uint MaskTextRotation = 0xff00;
        private const uint MaskIndent = 0xf0000;
        private const uint MaskShrinkToFit = 0x100000;
        private const uint MaskMergeCell = 0x200000;
        private const uint MaskReadingOrder = 0xc00000;
        public const short Size = 8;
        public const int DefaultRelativeIndent = 0xff;
        private uint packedValues;
        private int relativeIndent = 0xff;

        private bool GetBooleanValue(uint mask) => 
            (this.packedValues & mask) != 0;

        public void Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.RelativeIndent = reader.ReadInt32();
        }

        private void SetBooleanValue(uint mask, bool bitVal)
        {
            if (bitVal)
            {
                this.packedValues |= mask;
            }
            else
            {
                this.packedValues &= ~mask;
            }
        }

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(8);
            }
            writer.Write(this.packedValues);
            writer.Write(this.RelativeIndent);
        }

        public XlHorizontalAlignment HorizontalAlignment
        {
            get => 
                ((XlHorizontalAlignment) this.packedValues) & XlHorizontalAlignment.Distributed;
            set
            {
                this.packedValues &= (uint) (-8);
                this.packedValues = (uint) (((XlHorizontalAlignment) this.packedValues) | (value & XlHorizontalAlignment.Distributed));
            }
        }

        public XlVerticalAlignment VerticalAlignment
        {
            get => 
                (XlVerticalAlignment) ((this.packedValues & 0x70) >> 4);
            set
            {
                this.packedValues &= (uint) (-113);
                this.packedValues |= (uint) ((((int) value) << 4) & 0x70);
            }
        }

        public int TextRotation
        {
            get => 
                (int) ((this.packedValues & 0xff00) >> 8);
            set
            {
                this.packedValues &= 0xffff00ff;
                this.packedValues |= (uint) ((value << 8) & 0xff00);
            }
        }

        public byte Indent
        {
            get => 
                (byte) ((this.packedValues & 0xf0000) >> 0x10);
            set
            {
                this.packedValues &= 0xfff0ffff;
                this.packedValues |= (uint) ((value << 0x10) & 0xf0000);
            }
        }

        public XlReadingOrder ReadingOrder
        {
            get => 
                (XlReadingOrder) ((this.packedValues & 0xc00000) >> 0x16);
            set
            {
                this.packedValues &= 0xff3fffff;
                this.packedValues |= (uint) ((((int) value) << 0x16) & 0xc00000);
            }
        }

        public bool WrapText
        {
            get => 
                this.GetBooleanValue(8);
            set => 
                this.SetBooleanValue(8, value);
        }

        public bool JustifyLastLine
        {
            get => 
                this.GetBooleanValue(0x80);
            set => 
                this.SetBooleanValue(0x80, value);
        }

        public bool ShrinkToFit
        {
            get => 
                this.GetBooleanValue(0x100000);
            set => 
                this.SetBooleanValue(0x100000, value);
        }

        public bool MergeCell
        {
            get => 
                this.GetBooleanValue(0x200000);
            set => 
                this.SetBooleanValue(0x200000, value);
        }

        public int RelativeIndent
        {
            get => 
                this.relativeIndent;
            set => 
                this.relativeIndent = value;
        }
    }
}

