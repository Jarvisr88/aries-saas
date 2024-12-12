namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.IO;

    public class DrawingTextCharacterInfo : ICloneable<DrawingTextCharacterInfo>, ISupportsCopyFrom<DrawingTextCharacterInfo>, ISupportsSizeOf, ISupportsBinaryReadWrite
    {
        private static readonly DrawingTextCharacterInfo defaultInfo = new DrawingTextCharacterInfo();
        private const int offsetUnderline = 4;
        private const int offsetStrikethrough = 9;
        private const int offsetCaps = 11;
        private const uint maskKumimoji = 1;
        private const uint maskBold = 2;
        private const uint maskItalic = 4;
        private const uint maskApplyFontSize = 8;
        private const uint maskUnderline = 0x1f0;
        private const uint maskStrikethrough = 0x600;
        private const uint maskCaps = 0x1800;
        private const uint maskNormalizeHeight = 0x2000;
        private const uint maskNoProofing = 0x4000;
        private const uint maskDirty = 0x8000;
        private const uint maskSpellingError = 0x10000;
        private const uint maskSmartTagClean = 0x20000;
        private const uint maskHasKumimoji = 1;
        private const uint maskHasFontSize = 2;
        private const uint maskHasBold = 4;
        private const uint maskHasItalic = 8;
        private const uint maskHasUnderline = 0x10;
        private const uint maskHasStrikethrough = 0x20;
        private const uint maskHasKerning = 0x40;
        private const uint maskHasCaps = 0x80;
        private const uint maskHasSpacing = 0x100;
        private const uint maskHasNormalizeHeight = 0x200;
        private const uint maskHasBaseline = 0x400;
        private const uint maskHasNoProofing = 0x800;
        private uint packedValues = 0x28000;
        private uint packedOptionsValues;
        private int fontSize = 0x2710;
        private int kerning;
        private int spacing;
        private int baseline;
        private int smartTagId;

        public DrawingTextCharacterInfo Clone()
        {
            DrawingTextCharacterInfo info = new DrawingTextCharacterInfo();
            info.CopyFrom(this);
            return info;
        }

        public void CopyFrom(DrawingTextCharacterInfo value)
        {
            Guard.ArgumentNotNull(value, "value");
            this.packedValues = value.packedValues;
            this.packedOptionsValues = value.packedOptionsValues;
            this.fontSize = value.fontSize;
            this.kerning = value.kerning;
            this.spacing = value.spacing;
            this.baseline = value.baseline;
            this.smartTagId = value.smartTagId;
        }

        void ISupportsBinaryReadWrite.Read(BinaryReader reader)
        {
            this.packedValues = reader.ReadUInt32();
            this.packedOptionsValues = reader.ReadUInt32();
            this.fontSize = reader.ReadInt32();
            this.kerning = reader.ReadInt32();
            this.spacing = reader.ReadInt32();
            this.baseline = reader.ReadInt32();
            this.smartTagId = reader.ReadInt32();
        }

        void ISupportsBinaryReadWrite.Write(BinaryWriter writer)
        {
            writer.Write(this.packedValues);
            writer.Write(this.packedOptionsValues);
            writer.Write(this.fontSize);
            writer.Write(this.kerning);
            writer.Write(this.spacing);
            writer.Write(this.baseline);
            writer.Write(this.smartTagId);
        }

        public override bool Equals(object obj)
        {
            DrawingTextCharacterInfo info = obj as DrawingTextCharacterInfo;
            return ((info != null) ? ((this.packedValues == info.packedValues) && ((this.packedOptionsValues == info.packedOptionsValues) && ((this.fontSize == info.fontSize) && ((this.kerning == info.kerning) && ((this.spacing == info.spacing) && ((this.baseline == info.baseline) && (this.smartTagId == info.smartTagId))))))) : false);
        }

        public override int GetHashCode() => 
            ((((((int) (this.packedValues ^ this.packedOptionsValues)) ^ this.fontSize) ^ this.kerning) ^ this.spacing) ^ this.baseline) ^ this.smartTagId;

        public int SizeOf() => 
            DXMarshal.SizeOf(base.GetType());

        public static DrawingTextCharacterInfo DefaultInfo =>
            defaultInfo;

        public bool Kumimoji
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 1);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 1, value);
        }

        public bool Bold
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 2);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 2, value);
        }

        public bool Italic
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 4);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 4, value);
        }

        public bool ApplyFontSize
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 8);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 8, value);
        }

        public int FontSize
        {
            get => 
                this.fontSize;
            set
            {
                ValueChecker.CheckValue(value, 100, 0x61a80, "FontSize");
                this.fontSize = value;
            }
        }

        public DrawingTextUnderlineType Underline
        {
            get => 
                (DrawingTextUnderlineType) PackedValues.GetIntBitValue(this.packedValues, 0x1f0, 4);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x1f0, 4, (int) value);
        }

        public DrawingTextStrikeType Strikethrough
        {
            get => 
                (DrawingTextStrikeType) PackedValues.GetIntBitValue(this.packedValues, 0x600, 9);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x600, 9, (int) value);
        }

        public int Kerning
        {
            get => 
                this.kerning;
            set
            {
                ValueChecker.CheckValue(value, 0, 0x61a80, "Kerning");
                this.kerning = value;
            }
        }

        public DrawingTextCapsType Caps
        {
            get => 
                (DrawingTextCapsType) PackedValues.GetIntBitValue(this.packedValues, 0x1800, 11);
            set => 
                PackedValues.SetIntBitValue(ref this.packedValues, 0x1800, 11, (int) value);
        }

        public int Spacing
        {
            get => 
                this.spacing;
            set
            {
                ValueChecker.CheckValue(value, -400000, 0x61a80, "Spacing");
                this.spacing = value;
            }
        }

        public bool NormalizeHeight
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x2000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x2000, value);
        }

        public int Baseline
        {
            get => 
                this.baseline;
            set => 
                this.baseline = value;
        }

        public bool NoProofing
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x4000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x4000, value);
        }

        public bool Dirty
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x8000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x8000, value);
        }

        public bool SpellingError
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x10000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x10000, value);
        }

        public bool SmartTagClean
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedValues, 0x20000);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedValues, 0x20000, value);
        }

        public int SmartTagId
        {
            get => 
                this.smartTagId;
            set
            {
                Guard.ArgumentNonNegative(value, "SmartTagId");
                this.smartTagId = value;
            }
        }

        public bool HasKumimoji
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 1);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 1, value);
        }

        public bool HasFontSize
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 2);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 2, value);
        }

        public bool HasBold
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 4);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 4, value);
        }

        public bool HasItalic
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 8);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 8, value);
        }

        public bool HasUnderline
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x10);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x10, value);
        }

        public bool HasStrikethrough
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x20);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x20, value);
        }

        public bool HasKerning
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x40);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x40, value);
        }

        public bool HasCaps
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x80);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x80, value);
        }

        public bool HasSpacing
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x100);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x100, value);
        }

        public bool HasNormalizeHeight
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x200);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x200, value);
        }

        public bool HasBaseline
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x400);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x400, value);
        }

        public bool HasNoProofing
        {
            get => 
                PackedValues.GetBoolBitValue(this.packedOptionsValues, 0x800);
            set => 
                PackedValues.SetBoolBitValue(ref this.packedOptionsValues, 0x800, value);
        }
    }
}

