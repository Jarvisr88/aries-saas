namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Data.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Office.Utils;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class XlsContentXF : XlsContentBase
    {
        private readonly MsoCrc32Compute crc32 = new MsoCrc32Compute();

        public override int GetSize() => 
            20;

        public override void Read(XlReader reader, int size)
        {
            int num = reader.ReadInt16();
            this.Crc32.Add((short) num);
            if (num == 4)
            {
                num = 0;
            }
            else if (num > 4)
            {
                num--;
            }
            this.FontId = num;
            this.NumberFormatId = reader.ReadInt16();
            this.Crc32.Add((short) this.NumberFormatId);
            int data = reader.ReadInt16();
            this.Crc32.Add((short) data);
            this.IsStyleFormat = Convert.ToBoolean((int) (data & 4));
            if (!this.IsStyleFormat)
            {
                this.QuotePrefix = Convert.ToBoolean((int) (data & 8));
                this.StyleId = (data & 0xfff0) >> 4;
            }
            this.IsLocked = Convert.ToBoolean((int) (data & 1));
            this.IsHidden = Convert.ToBoolean((int) (data & 2));
            data = reader.ReadUInt16();
            this.Crc32.Add((short) data);
            this.HorizontalAlignment = ((XlHorizontalAlignment) data) & XlHorizontalAlignment.Distributed;
            this.WrapText = Convert.ToBoolean((int) (data & 8));
            this.VerticalAlignment = (XlVerticalAlignment) ((data & 0x70) >> 4);
            this.TextRotation = (data & 0xff00) >> 8;
            data = reader.ReadUInt16();
            this.Crc32.Add((short) data);
            this.Indent = (byte) (data & 15);
            this.ShrinkToFit = Convert.ToBoolean((int) (data & 0x10));
            this.ReadingOrder = (XlReadingOrder) ((data & 0xc0) >> 6);
            if (this.IsStyleFormat)
            {
                this.ApplyNumberFormat = !Convert.ToBoolean((int) (data & 0x400));
                this.ApplyFont = !Convert.ToBoolean((int) (data & 0x800));
                this.ApplyAlignment = !Convert.ToBoolean((int) (data & 0x1000));
                this.ApplyBorder = !Convert.ToBoolean((int) (data & 0x2000));
                this.ApplyFill = !Convert.ToBoolean((int) (data & 0x4000));
                this.ApplyProtection = !Convert.ToBoolean((int) (data & 0x8000));
            }
            else
            {
                this.ApplyNumberFormat = Convert.ToBoolean((int) (data & 0x400));
                this.ApplyFont = Convert.ToBoolean((int) (data & 0x800));
                this.ApplyAlignment = Convert.ToBoolean((int) (data & 0x1000));
                this.ApplyBorder = Convert.ToBoolean((int) (data & 0x2000));
                this.ApplyFill = Convert.ToBoolean((int) (data & 0x4000));
                this.ApplyProtection = Convert.ToBoolean((int) (data & 0x8000));
            }
            data = reader.ReadUInt16();
            this.Crc32.Add((short) data);
            this.BorderLeftLineStyle = ((XlBorderLineStyle) data) & (XlBorderLineStyle.SlantDashDot | XlBorderLineStyle.Medium);
            this.BorderRightLineStyle = (XlBorderLineStyle) ((data & 240) >> 4);
            this.BorderTopLineStyle = (XlBorderLineStyle) ((data & 0xf00) >> 8);
            this.BorderBottomLineStyle = (XlBorderLineStyle) ((data & 0xf000) >> 12);
            data = reader.ReadUInt16();
            this.Crc32.Add((short) data);
            this.BorderLeftColorIndex = data & 0x7f;
            this.BorderRightColorIndex = (data & 0x3f80) >> 7;
            this.BorderDiagonalDown = Convert.ToBoolean((int) (data & 0x4000));
            this.BorderDiagonalUp = Convert.ToBoolean((int) (data & 0x8000));
            data = reader.ReadInt32();
            this.Crc32.Add(data);
            this.BorderTopColorIndex = data & 0x7f;
            this.BorderBottomColorIndex = (data & 0x3f80) >> 7;
            this.BorderDiagonalColorIndex = (data & 0x1fc000) >> 14;
            this.BorderDiagonalLineStyle = (XlBorderLineStyle) ((data & 0x1e00000) >> 0x15);
            this.HasExtension = Convert.ToBoolean((int) (data & 0x2000000));
            this.FillPatternType = (XlPatternType) ((data & 0xfc000000UL) >> 0x1a);
            data = reader.ReadUInt16();
            this.Crc32.Add((short) data);
            this.FillForeColorIndex = data & 0x7f;
            this.FillBackColorIndex = (data & 0x3f80) >> 7;
            if (!this.IsStyleFormat)
            {
                this.PivotButton = Convert.ToBoolean((int) (data & 0x4000));
            }
        }

        private int SetApplyProperties(int bitwiseField)
        {
            if (this.IsStyleFormat)
            {
                if (!this.ApplyNumberFormat)
                {
                    bitwiseField |= 0x400;
                }
                if (!this.ApplyFont)
                {
                    bitwiseField |= 0x800;
                }
                if (!this.ApplyAlignment)
                {
                    bitwiseField |= 0x1000;
                }
                if (!this.ApplyBorder)
                {
                    bitwiseField |= 0x2000;
                }
                if (!this.ApplyFill)
                {
                    bitwiseField |= 0x4000;
                }
                if (!this.ApplyProtection)
                {
                    bitwiseField |= 0x8000;
                }
            }
            else
            {
                if (this.ApplyNumberFormat)
                {
                    bitwiseField |= 0x400;
                }
                if (this.ApplyFont)
                {
                    bitwiseField |= 0x800;
                }
                if (this.ApplyAlignment)
                {
                    bitwiseField |= 0x1000;
                }
                if (this.ApplyBorder)
                {
                    bitwiseField |= 0x2000;
                }
                if (this.ApplyFill)
                {
                    bitwiseField |= 0x4000;
                }
                if (this.ApplyProtection)
                {
                    bitwiseField |= 0x8000;
                }
            }
            return bitwiseField;
        }

        public override void Write(BinaryWriter writer)
        {
            int fontId = this.FontId;
            if (fontId >= 4)
            {
                fontId++;
            }
            writer.Write((short) fontId);
            this.Crc32.Add((short) fontId);
            writer.Write((short) this.NumberFormatId);
            this.Crc32.Add((short) this.NumberFormatId);
            int bitwiseField = 0;
            if (this.IsLocked)
            {
                bitwiseField |= 1;
            }
            if (this.IsHidden)
            {
                bitwiseField |= 2;
            }
            if (this.IsStyleFormat)
            {
                bitwiseField |= 4;
            }
            if (this.IsStyleFormat)
            {
                bitwiseField |= 0xfff0;
            }
            else
            {
                if (this.QuotePrefix)
                {
                    bitwiseField |= 8;
                }
                bitwiseField |= (this.StyleId & 0xfff) << 4;
            }
            writer.Write((ushort) bitwiseField);
            this.Crc32.Add((short) bitwiseField);
            bitwiseField = (int) this.HorizontalAlignment;
            if (this.WrapText)
            {
                bitwiseField |= 8;
            }
            bitwiseField = (bitwiseField | (((int) this.VerticalAlignment) << 4)) | ((this.TextRotation & 0xff) << 8);
            writer.Write((ushort) bitwiseField);
            this.Crc32.Add((short) bitwiseField);
            bitwiseField = this.Indent & 15;
            if (this.ShrinkToFit)
            {
                bitwiseField |= 0x10;
            }
            bitwiseField |= ((int) this.ReadingOrder) << 6;
            bitwiseField = this.SetApplyProperties(bitwiseField);
            writer.Write((ushort) bitwiseField);
            this.Crc32.Add((short) bitwiseField);
            bitwiseField = ((((int) this.BorderLeftLineStyle) | (((int) this.BorderRightLineStyle) << 4)) | (((int) this.BorderTopLineStyle) << 8)) | (((int) this.BorderBottomLineStyle) << 12);
            writer.Write((ushort) bitwiseField);
            this.Crc32.Add((short) bitwiseField);
            bitwiseField = (this.BorderLeftColorIndex & 0x7f) | ((this.BorderRightColorIndex & 0x7f) << 7);
            if (this.BorderDiagonalDown)
            {
                bitwiseField |= 0x4000;
            }
            if (this.BorderDiagonalUp)
            {
                bitwiseField |= 0x8000;
            }
            writer.Write((ushort) bitwiseField);
            this.Crc32.Add((short) bitwiseField);
            bitwiseField = (((this.BorderTopColorIndex & 0x7f) | ((this.BorderBottomColorIndex & 0x7f) << 7)) | ((this.BorderDiagonalColorIndex & 0x7f) << 14)) | (((int) this.BorderDiagonalLineStyle) << 0x15);
            if (this.HasExtension && !this.IsStyleFormat)
            {
                bitwiseField |= 0x2000000;
            }
            bitwiseField |= ((int) this.FillPatternType) << 0x1a;
            writer.Write(bitwiseField);
            this.Crc32.Add(bitwiseField);
            bitwiseField = (this.FillForeColorIndex & 0x7f) | ((this.FillBackColorIndex & 0x7f) << 7);
            if (!this.IsStyleFormat && this.PivotButton)
            {
                bitwiseField |= 0x4000;
            }
            writer.Write((ushort) bitwiseField);
            this.Crc32.Add((short) bitwiseField);
        }

        public bool IsStyleFormat { get; set; }

        public int FontId { get; set; }

        public int NumberFormatId { get; set; }

        public bool IsLocked { get; set; }

        public bool IsHidden { get; set; }

        public bool QuotePrefix { get; set; }

        public int StyleId { get; set; }

        public XlHorizontalAlignment HorizontalAlignment { get; set; }

        public bool WrapText { get; set; }

        public XlVerticalAlignment VerticalAlignment { get; set; }

        public int TextRotation { get; set; }

        public byte Indent { get; set; }

        public bool ShrinkToFit { get; set; }

        public XlReadingOrder ReadingOrder { get; set; }

        public bool ApplyNumberFormat { get; set; }

        public bool ApplyFont { get; set; }

        public bool ApplyAlignment { get; set; }

        public bool ApplyBorder { get; set; }

        public bool ApplyFill { get; set; }

        public bool ApplyProtection { get; set; }

        public XlBorderLineStyle BorderLeftLineStyle { get; set; }

        public XlBorderLineStyle BorderRightLineStyle { get; set; }

        public XlBorderLineStyle BorderTopLineStyle { get; set; }

        public XlBorderLineStyle BorderBottomLineStyle { get; set; }

        public int BorderLeftColorIndex { get; set; }

        public int BorderRightColorIndex { get; set; }

        public bool BorderDiagonalDown { get; set; }

        public bool BorderDiagonalUp { get; set; }

        public int BorderTopColorIndex { get; set; }

        public int BorderBottomColorIndex { get; set; }

        public int BorderDiagonalColorIndex { get; set; }

        public XlBorderLineStyle BorderDiagonalLineStyle { get; set; }

        public bool HasExtension { get; set; }

        public XlPatternType FillPatternType { get; set; }

        public int FillForeColorIndex { get; set; }

        public int FillBackColorIndex { get; set; }

        public bool PivotButton { get; set; }

        public int CrcValue
        {
            get => 
                this.crc32.CrcValue;
            set => 
                this.crc32.CrcValue = value;
        }

        protected MsoCrc32Compute Crc32 =>
            this.crc32;
    }
}

