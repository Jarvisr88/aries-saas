namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.IO;

    public class XlsDxfFill
    {
        private const uint MaskPatternType = 0xfc00;
        private const uint MaskForeColorIndex = 0x7f0000;
        private const uint MaskBackColorIndex = 0x3f800000;
        public const short Size = 4;
        private uint packedValues;

        private int GetColorIndex(uint mask, int position) => 
            (int) this.GetUIntValue(mask, position);

        private uint GetPackedValue(uint mask, int position, uint value) => 
            (value << (position & 0x1f)) & mask;

        private XlPatternType GetPatternType() => 
            (XlPatternType) this.GetUIntValue(0xfc00, 10);

        private uint GetUIntValue(uint mask, int position) => 
            (this.packedValues & mask) >> (position & 0x1f);

        public void Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
        }

        private void SetColorIndex(uint mask, int position, int value)
        {
            this.SetUIntValue(mask, position, (uint) value);
        }

        private void SetPatternType(XlPatternType value)
        {
            this.SetUIntValue(0xfc00, 10, (uint) value);
        }

        private void SetUIntValue(uint mask, int position, uint value)
        {
            this.packedValues &= ~mask;
            this.packedValues |= this.GetPackedValue(mask, position, value);
        }

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(4);
            }
            writer.Write(this.packedValues);
        }

        public XlPatternType PatternType
        {
            get => 
                this.GetPatternType();
            set => 
                this.SetPatternType(value);
        }

        public int BackColorIndex
        {
            get => 
                this.GetColorIndex(0x3f800000, 0x17);
            set => 
                this.SetColorIndex(0x3f800000, 0x17, value);
        }

        public int ForeColorIndex
        {
            get => 
                this.GetColorIndex(0x7f0000, 0x10);
            set => 
                this.SetColorIndex(0x7f0000, 0x10, value);
        }
    }
}

