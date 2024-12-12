namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Text;

    public class EmfPlusStringFormat
    {
        private uint formatFlags;
        public int Language;
        public StringAlignment Alignment;
        public StringAlignment LineAlignment;
        public int DigitSubstitution;
        public int DigitLanguage;
        public float FirstTabOffset;
        public System.Drawing.Text.HotkeyPrefix HotkeyPrefix;
        private float LeadingMargin;
        private float TrailingMargin;
        private float Tracking;
        public StringTrimming Trimming;
        public int TabStopCount;
        public int RangeCount;
        public float[] TabStops;
        public CharacterRange[] CharRange;

        public EmfPlusStringFormat(MetaReader reader)
        {
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            this.formatFlags = reader.ReadUInt32();
            this.Language = reader.ReadInt32();
            this.Alignment = (StringAlignment) reader.ReadUInt32();
            this.LineAlignment = (StringAlignment) reader.ReadUInt32();
            this.DigitSubstitution = reader.ReadInt32();
            this.DigitLanguage = reader.ReadInt32();
            this.FirstTabOffset = reader.ReadSingle();
            this.HotkeyPrefix = (System.Drawing.Text.HotkeyPrefix) reader.ReadUInt32();
            this.LeadingMargin = reader.ReadSingle();
            this.TrailingMargin = reader.ReadSingle();
            this.Tracking = reader.ReadSingle();
            this.Trimming = (StringTrimming) reader.ReadUInt32();
            this.TabStopCount = reader.ReadInt32();
            this.RangeCount = reader.ReadInt32();
            this.TabStops = reader.ReadSingleArray(this.TabStopCount);
            this.CharRange = ReadCharacterRanges(reader, this.RangeCount);
        }

        private static CharacterRange[] ReadCharacterRanges(MetaReader reader, int rangeCount)
        {
            CharacterRange[] rangeArray = new CharacterRange[rangeCount];
            for (int i = 0; i < rangeCount; i++)
            {
                rangeArray[i] = new CharacterRange(reader.ReadInt32(), reader.ReadInt32());
            }
            return rangeArray;
        }

        public StringFormatFlags FormatFlags =>
            ((StringFormatFlags) this.formatFlags) & ((StringFormatFlags) 0x7ffff);

        public bool IsGenericDefault =>
            (this.LeadingMargin == 0.1666667f) && ((this.TrailingMargin == 0.1666667f) && (this.Tracking == 1.03f));

        public bool IsGenericTypographic =>
            (this.LeadingMargin == 0f) && ((this.TrailingMargin == 0f) && (this.Tracking == 1f));
    }
}

