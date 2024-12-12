namespace DevExpress.XtraExport.Xls
{
    using System;
    using System.IO;

    public class XlsDxfNFlags
    {
        private const uint MaskAlignmentHorizontalNinch = 1;
        private const uint MaskAlignmentVerticalNinch = 2;
        private const uint MaskAlignmentWrapTextNinch = 4;
        private const uint MaskAlignmentTextRotationNinch = 8;
        private const uint MaskAlignmentJustifyLastLineNinch = 0x10;
        private const uint MaskAlignmentIndentNinch = 0x20;
        private const uint MaskAlignmentShrinkToFitNinch = 0x40;
        private const uint MaskMergeCellNinch = 0x80;
        private const uint MaskProtectionLockedNinch = 0x100;
        private const uint MaskProtectionHiddenNinch = 0x200;
        private const uint MaskBorderLeftNinch = 0x400;
        private const uint MaskBorderRightNinch = 0x800;
        private const uint MaskBorderTopNinch = 0x1000;
        private const uint MaskBorderBottomNinch = 0x2000;
        private const uint MaskBorderDiagonalDownNinch = 0x4000;
        private const uint MaskBorderDiagonalUpNinch = 0x8000;
        private const uint MaskFillPatternTypeNinch = 0x10000;
        private const uint MaskFillForegroundColorNinch = 0x20000;
        private const uint MaskFillBackgroundColorNinch = 0x40000;
        private const uint MaskNumberFormatNinch = 0x80000;
        private const uint MaskFontNinch = 0x100000;
        private const uint MaskIncludeNumberFormat = 0x2000000;
        private const uint MaskIncludeFont = 0x4000000;
        private const uint MaskIncludeAlignment = 0x8000000;
        private const uint MaskIncludeBorder = 0x10000000;
        private const uint MaskIncludeFill = 0x20000000;
        private const uint MaskIncludeProtection = 0x40000000;
        private const uint MaskAlignmentReadingOrderNinch = 0x80000000;
        private const ushort MaskUserDefinedNumberFormat = 1;
        private const ushort MaskNewBorder = 4;
        private const ushort MaskAlignmentReadingOrderZeroInited = 0x8000;
        private uint firstPackedValues = 0x801fffff;
        private ushort secondPackedValues;

        private bool GetBooleanValue(ushort mask) => 
            (this.secondPackedValues & mask) != 0;

        private bool GetBooleanValue(uint mask) => 
            (this.firstPackedValues & mask) != 0;

        public void Read(BinaryReader reader)
        {
            this.firstPackedValues = reader.ReadUInt32();
            this.secondPackedValues = reader.ReadUInt16();
        }

        private void SetBooleanValue(ushort mask, bool bitVal)
        {
            if (bitVal)
            {
                this.secondPackedValues = (ushort) (this.secondPackedValues | mask);
            }
            else
            {
                this.secondPackedValues = (ushort) (this.secondPackedValues & ~mask);
            }
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

        public void Write(BinaryWriter writer)
        {
            XlsChunkWriter writer2 = writer as XlsChunkWriter;
            if (writer2 != null)
            {
                writer2.BeginRecord(6);
            }
            writer.Write(this.firstPackedValues);
            writer.Write(this.secondPackedValues);
        }

        public bool AlignmentHorizontalNinch
        {
            get => 
                this.GetBooleanValue((uint) 1);
            set => 
                this.SetBooleanValue((uint) 1, value);
        }

        public bool AlignmentVerticalNinch
        {
            get => 
                this.GetBooleanValue((uint) 2);
            set => 
                this.SetBooleanValue((uint) 2, value);
        }

        public bool AlignmentWrapTextNinch
        {
            get => 
                this.GetBooleanValue((uint) 4);
            set => 
                this.SetBooleanValue((uint) 4, value);
        }

        public bool AlignmentTextRotationNinch
        {
            get => 
                this.GetBooleanValue((uint) 8);
            set => 
                this.SetBooleanValue((uint) 8, value);
        }

        public bool AlignmentJustifyLastLineNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x10);
            set => 
                this.SetBooleanValue((uint) 0x10, value);
        }

        public bool AlignmentIndentNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x20);
            set => 
                this.SetBooleanValue((uint) 0x20, value);
        }

        public bool AlignmentShrinkToFitNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x40);
            set => 
                this.SetBooleanValue((uint) 0x40, value);
        }

        public bool AlignmentReadingOrderNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x80000000);
            set => 
                this.SetBooleanValue((uint) 0x80000000, value);
        }

        public bool ProtectionLockedNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x100);
            set => 
                this.SetBooleanValue((uint) 0x100, value);
        }

        public bool ProtectionHiddenNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x200);
            set => 
                this.SetBooleanValue((uint) 0x200, value);
        }

        public bool BorderLeftNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x400);
            set => 
                this.SetBooleanValue((uint) 0x400, value);
        }

        public bool BorderRightNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x800);
            set => 
                this.SetBooleanValue((uint) 0x800, value);
        }

        public bool BorderTopNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x1000);
            set => 
                this.SetBooleanValue((uint) 0x1000, value);
        }

        public bool BorderBottomNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x2000);
            set => 
                this.SetBooleanValue((uint) 0x2000, value);
        }

        public bool BorderDiagonalDownNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x4000);
            set => 
                this.SetBooleanValue((uint) 0x4000, value);
        }

        public bool BorderDiagonalUpNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x8000);
            set => 
                this.SetBooleanValue((uint) 0x8000, value);
        }

        public bool FillPatternTypeNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x10000);
            set => 
                this.SetBooleanValue((uint) 0x10000, value);
        }

        public bool FillForegroundColorNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x20000);
            set => 
                this.SetBooleanValue((uint) 0x20000, value);
        }

        public bool FillBackgroundColorNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x40000);
            set => 
                this.SetBooleanValue((uint) 0x40000, value);
        }

        public bool NumberFormatNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x80000);
            set => 
                this.SetBooleanValue((uint) 0x80000, value);
        }

        public bool FontNinch
        {
            get => 
                this.GetBooleanValue((uint) 0x100000);
            set => 
                this.SetBooleanValue((uint) 0x100000, value);
        }

        public bool IncludeNumberFormat
        {
            get => 
                this.GetBooleanValue((uint) 0x2000000);
            set => 
                this.SetBooleanValue((uint) 0x2000000, value);
        }

        public bool IncludeFont
        {
            get => 
                this.GetBooleanValue((uint) 0x4000000);
            set => 
                this.SetBooleanValue((uint) 0x4000000, value);
        }

        public bool IncludeAlignment
        {
            get => 
                this.GetBooleanValue((uint) 0x8000000);
            set => 
                this.SetBooleanValue((uint) 0x8000000, value);
        }

        public bool IncludeBorder
        {
            get => 
                this.GetBooleanValue((uint) 0x10000000);
            set => 
                this.SetBooleanValue((uint) 0x10000000, value);
        }

        public bool IncludeFill
        {
            get => 
                this.GetBooleanValue((uint) 0x20000000);
            set => 
                this.SetBooleanValue((uint) 0x20000000, value);
        }

        public bool IncludeProtection
        {
            get => 
                this.GetBooleanValue((uint) 0x40000000);
            set => 
                this.SetBooleanValue((uint) 0x40000000, value);
        }

        public bool UserDefinedNumberFormat
        {
            get => 
                this.GetBooleanValue((ushort) 1);
            set => 
                this.SetBooleanValue((ushort) 1, value);
        }

        public bool NewBorder
        {
            get => 
                this.GetBooleanValue((ushort) 4);
            set => 
                this.SetBooleanValue((ushort) 4, value);
        }

        public bool AlignmentReadingOrderZeroInited
        {
            get => 
                this.GetBooleanValue((ushort) 0x8000);
            set => 
                this.SetBooleanValue((ushort) 0x8000, value);
        }
    }
}

