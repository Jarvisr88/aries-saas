namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XlsDxfBorder
    {
        private static Dictionary<uint, int> maskToPositionTranslationTable = CreateMaskToPositionTranslationTable();
        private const uint MaskLeftLineStyle = 15;
        private const uint MaskRightLineStyle = 240;
        private const uint MaskTopLineStyle = 0xf00;
        private const uint MaskBottomLineStyle = 0xf000;
        private const uint MaskLeftColorIndex = 0x7f0000;
        private const uint MaskRightColorIndex = 0x3f800000;
        private const uint MaskDiagonalDown = 0x40000000;
        private const uint MaskDiagonalUp = 0x80000000;
        private const uint MaskTopColorIndex = 0x7f;
        private const uint MaskBottomColorIndex = 0x3f80;
        private const uint MaskDiagonalColorIndex = 0x1fc000;
        private const uint MaskDiagonalLineStyle = 0x1e00000;
        private const int LeftLineStylePosition = 0;
        private const int RightLineStylePosition = 4;
        private const int TopLineStylePosition = 8;
        private const int BottomLineStylePosition = 12;
        private const int LeftColorIndexPosition = 0x10;
        private const int RightColorIndexPosition = 0x17;
        private const int TopColorIndexPosition = 0;
        private const int BottomColorIndexPosition = 7;
        private const int DiagonalColorIndexPosition = 14;
        private const int DiagonalLineStylePosition = 0x15;
        public const short Size = 8;
        private uint firstPackedValues = 0x20400000;
        private uint secondPackedValues = 0x2040;

        private static Dictionary<uint, int> CreateMaskToPositionTranslationTable() => 
            new Dictionary<uint, int> { 
                { 
                    15,
                    0
                },
                { 
                    240,
                    4
                },
                { 
                    0xf00,
                    8
                },
                { 
                    0xf000,
                    12
                },
                { 
                    0x7f0000,
                    0x10
                },
                { 
                    0x3f800000,
                    0x17
                },
                { 
                    0x7f,
                    0
                },
                { 
                    0x3f80,
                    7
                },
                { 
                    0x1fc000,
                    14
                },
                { 
                    0x1e00000,
                    0x15
                }
            };

        private bool GetBooleanValue(uint mask) => 
            (this.firstPackedValues & mask) != 0;

        private int GetBorderColorIndex(uint packedValues, uint mask, int position) => 
            (int) this.GetUIntValue(packedValues, mask, position);

        private XlBorderLineStyle GetBorderLineStyle(uint packedValues, uint mask, int position) => 
            (XlBorderLineStyle) this.GetUIntValue(packedValues, mask, position);

        private uint GetPackedValue(uint mask, int position, uint value) => 
            (value << (position & 0x1f)) & mask;

        private uint GetUIntValue(uint packedValues, uint mask, int position) => 
            (packedValues & mask) >> (position & 0x1f);

        public void Read(BinaryReader reader)
        {
            this.firstPackedValues = reader.ReadUInt32();
            this.secondPackedValues = reader.ReadUInt32();
        }

        private void SetBooleanValue(uint mask, bool bitVal)
        {
            if (bitVal)
            {
                this.firstPackedValues |= mask;
            }
            else
            {
                this.firstPackedValues &= ~mask;
            }
        }

        private uint SetBorderColorIndex(uint packedValues, uint mask, int value)
        {
            int position = maskToPositionTranslationTable[mask];
            return this.SetUIntValue(packedValues, mask, position, (uint) value);
        }

        private uint SetBorderLineStyle(uint packedValues, uint mask, XlBorderLineStyle value)
        {
            int position = maskToPositionTranslationTable[mask];
            return this.SetUIntValue(packedValues, mask, position, (uint) value);
        }

        private uint SetUIntValue(uint packedValues, uint mask, int position, uint value)
        {
            packedValues &= ~mask;
            packedValues |= this.GetPackedValue(mask, position, value);
            return packedValues;
        }

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(8);
            }
            writer.Write(this.firstPackedValues);
            writer.Write(this.secondPackedValues);
        }

        public XlBorderLineStyle LeftLineStyle
        {
            get => 
                this.GetBorderLineStyle(this.firstPackedValues, 15, 0);
            set => 
                this.firstPackedValues = this.SetBorderLineStyle(this.firstPackedValues, 15, value);
        }

        public XlBorderLineStyle RightLineStyle
        {
            get => 
                this.GetBorderLineStyle(this.firstPackedValues, 240, 4);
            set => 
                this.firstPackedValues = this.SetBorderLineStyle(this.firstPackedValues, 240, value);
        }

        public XlBorderLineStyle TopLineStyle
        {
            get => 
                this.GetBorderLineStyle(this.firstPackedValues, 0xf00, 8);
            set => 
                this.firstPackedValues = this.SetBorderLineStyle(this.firstPackedValues, 0xf00, value);
        }

        public XlBorderLineStyle BottomLineStyle
        {
            get => 
                this.GetBorderLineStyle(this.firstPackedValues, 0xf000, 12);
            set => 
                this.firstPackedValues = this.SetBorderLineStyle(this.firstPackedValues, 0xf000, value);
        }

        public int LeftColorIndex
        {
            get => 
                this.GetBorderColorIndex(this.firstPackedValues, 0x7f0000, 0x10);
            set => 
                this.firstPackedValues = this.SetBorderColorIndex(this.firstPackedValues, 0x7f0000, value);
        }

        public int RightColorIndex
        {
            get => 
                this.GetBorderColorIndex(this.firstPackedValues, 0x3f800000, 0x17);
            set => 
                this.firstPackedValues = this.SetBorderColorIndex(this.firstPackedValues, 0x3f800000, value);
        }

        public bool DiagonalDown
        {
            get => 
                this.GetBooleanValue(0x40000000);
            set => 
                this.SetBooleanValue(0x40000000, value);
        }

        public bool DiagonalUp
        {
            get => 
                this.GetBooleanValue(0x80000000);
            set => 
                this.SetBooleanValue(0x80000000, value);
        }

        public int TopColorIndex
        {
            get => 
                this.GetBorderColorIndex(this.secondPackedValues, 0x7f, 0);
            set => 
                this.secondPackedValues = this.SetBorderColorIndex(this.secondPackedValues, 0x7f, value);
        }

        public int BottomColorIndex
        {
            get => 
                this.GetBorderColorIndex(this.secondPackedValues, 0x3f80, 7);
            set => 
                this.secondPackedValues = this.SetBorderColorIndex(this.secondPackedValues, 0x3f80, value);
        }

        public int DiagonalColorIndex
        {
            get => 
                this.GetBorderColorIndex(this.secondPackedValues, 0x1fc000, 14);
            set => 
                this.secondPackedValues = this.SetBorderColorIndex(this.secondPackedValues, 0x1fc000, value);
        }

        public XlBorderLineStyle DiagonalLineStyle
        {
            get => 
                this.GetBorderLineStyle(this.secondPackedValues, 0x1e00000, 0x15);
            set => 
                this.secondPackedValues = this.SetBorderLineStyle(this.secondPackedValues, 0x1e00000, value);
        }
    }
}

