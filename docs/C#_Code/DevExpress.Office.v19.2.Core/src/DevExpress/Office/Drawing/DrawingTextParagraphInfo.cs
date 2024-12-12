namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingTextParagraphInfo : ICloneable<DrawingTextParagraphInfo>, ISupportsCopyFrom<DrawingTextParagraphInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        public const float DefaultLeftMargin = 547.5f;
        public const float DefaultIndent = -540f;
        private const int MaxTextIndentLevelType = 8;
        private const uint MaskTextAlignment = 7;
        private const uint MaskEastAsianLineBreak = 8;
        private const uint MaskFontAlignment = 0x70;
        private const uint MaskHangingPunctuation = 0x80;
        private const uint MaskTextIndentLevel = 0xf00;
        private const uint MaskRightToLeft = 0x1000;
        private const uint MaskLatinLineBreak = 0x2000;
        private const uint MaskHasDefaultTabSize = 0x4000;
        private const uint MaskHasTextIndentLevel = 0x8000;
        private const uint MaskHasTextAlignment = 0x10000;
        private const uint MaskHasEastAsianLineBreak = 0x20000;
        private const uint MaskHasFontAlignment = 0x40000;
        private const uint MaskHasHangingPunctuation = 0x80000;
        private const uint MaskHasRightToLeft = 0x100000;
        private const uint MaskHasLatinLineBreak = 0x200000;
        private const uint MaskHasIndent = 0x400000;
        private const uint MaskHasLeftMargin = 0x800000;
        private const uint MaskHasRightMargin = 0x1000000;
        private const uint MaskApplyDefaultCharacterProperties = 0x2000000;
        private uint packedValues = 0x2238;
        private float defaultTabSize;
        private float indent = -540f;
        private float leftMargin = 547.5f;
        private float rightMargin;

        public DrawingTextParagraphInfo Clone()
        {
            DrawingTextParagraphInfo info = new DrawingTextParagraphInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingTextParagraphInfo value)
        {
            this.packedValues = value.packedValues;
            this.defaultTabSize = value.defaultTabSize;
            this.indent = value.indent;
            this.leftMargin = value.leftMargin;
            this.rightMargin = value.rightMargin;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.defaultTabSize = reader.ReadInt32();
            this.indent = reader.ReadInt32();
            this.leftMargin = reader.ReadInt32();
            this.rightMargin = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
            writer.Write(this.defaultTabSize);
            writer.Write(this.indent);
            writer.Write(this.leftMargin);
            writer.Write(this.rightMargin);
        }

        public override bool Equals(object obj)
        {
            DrawingTextParagraphInfo info = obj as DrawingTextParagraphInfo;
            return ((info != null) ? ((this.packedValues == info.packedValues) && ((this.defaultTabSize == info.defaultTabSize) && ((this.indent == info.indent) && ((this.leftMargin == info.leftMargin) && (this.rightMargin == info.rightMargin))))) : false);
        }

        public override int GetHashCode() => 
            ((int) (((((((this.packedValues * 0x18d) ^ this.defaultTabSize.GetHashCode()) * 0x18d) ^ this.indent.GetHashCode()) * 0x18d) ^ this.leftMargin.GetHashCode()) * 0x18d)) ^ this.rightMargin.GetHashCode();

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public static DrawingTextParagraphInfo DefaultInfo =>
            new DrawingTextParagraphInfo();

        public float DefaultTabSize
        {
            get => 
                this.defaultTabSize;
            set => 
                this.defaultTabSize = value;
        }

        public float Indent
        {
            get => 
                this.indent;
            set => 
                this.indent = value;
        }

        public float LeftMargin
        {
            get => 
                this.leftMargin;
            set => 
                this.leftMargin = value;
        }

        public float RightMargin
        {
            get => 
                this.rightMargin;
            set => 
                this.rightMargin = value;
        }

        public DrawingTextAlignmentType TextAlignment
        {
            get => 
                (DrawingTextAlignmentType) PackedValues.GetIntBitValue(this.packedValues, 7);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 7, (int) value);
        }

        public DrawingFontAlignmentType FontAlignment
        {
            get => 
                (DrawingFontAlignmentType) PackedValues.GetIntBitValue(this.packedValues, 0x70, 4);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x70, 4, (int) value);
        }

        public int TextIndentLevel
        {
            get => 
                PackedValues.GetIntBitValue(this.packedValues, 0xf00, 8) - 2;
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0xf00, 8, value + 2);
        }

        public bool EastAsianLineBreak
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 8);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 8, value);
        }

        public bool LatinLineBreak
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x2000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x2000, value);
        }

        public bool HangingPunctuation
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x80);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x80, value);
        }

        public bool RightToLeft
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x1000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x1000, value);
        }

        public bool HasDefaultTabSize
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x4000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x4000, value);
        }

        public bool HasTextIndentLevel
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x8000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x8000, value);
        }

        public bool HasTextAlignment
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x10000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x10000, value);
        }

        public bool HasEastAsianLineBreak
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x20000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x20000, value);
        }

        public bool HasFontAlignment
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x40000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x40000, value);
        }

        public bool HasHangingPunctuation
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x80000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x80000, value);
        }

        public bool HasRightToLeft
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x100000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x100000, value);
        }

        public bool HasLatinLineBreak
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x200000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x200000, value);
        }

        public bool HasIndent
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x400000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x400000, value);
        }

        public bool HasLeftMargin
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x800000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x800000, value);
        }

        public bool HasRightMargin
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x1000000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x1000000, value);
        }

        public bool ApplyDefaultCharacterProperties
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x2000000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x2000000, value);
        }
    }
}

