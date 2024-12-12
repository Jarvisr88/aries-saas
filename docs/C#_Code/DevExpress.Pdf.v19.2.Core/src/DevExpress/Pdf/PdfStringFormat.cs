namespace DevExpress.Pdf
{
    using System;

    public class PdfStringFormat : ICloneable
    {
        private static readonly PdfStringFormat genericDefault = new PdfStringFormat();
        private static readonly PdfStringFormat genericTypographic = new PdfStringFormat(PdfStringFormatFlags.NoClip | PdfStringFormatFlags.LineLimit);
        private double leadingMarginFactor;
        private double trailingMarginFactor;
        private PdfStringFormatFlags formatFlags;
        private PdfStringAlignment alignment;
        private PdfStringAlignment lineAlignment;
        private PdfStringTrimming trimming;
        private PdfHotkeyPrefix hotkeyPrefix;
        private double tabStopInterval;
        private bool directionRightToLeft;

        static PdfStringFormat()
        {
            genericTypographic.trimming = PdfStringTrimming.None;
            genericTypographic.leadingMarginFactor = 0.0;
            genericTypographic.trailingMarginFactor = 0.0;
        }

        public PdfStringFormat()
        {
            this.leadingMarginFactor = 0.16666666666666666;
            this.trailingMarginFactor = 0.16666666666666666;
            this.trimming = PdfStringTrimming.Character;
        }

        public PdfStringFormat(PdfStringFormat format)
        {
            this.leadingMarginFactor = 0.16666666666666666;
            this.trailingMarginFactor = 0.16666666666666666;
            this.trimming = PdfStringTrimming.Character;
            this.formatFlags = format.formatFlags;
            this.alignment = format.alignment;
            this.lineAlignment = format.lineAlignment;
            this.trimming = format.trimming;
            this.hotkeyPrefix = format.hotkeyPrefix;
            this.leadingMarginFactor = format.leadingMarginFactor;
            this.trailingMarginFactor = format.trailingMarginFactor;
            this.tabStopInterval = format.tabStopInterval;
            this.directionRightToLeft = format.directionRightToLeft;
        }

        public PdfStringFormat(PdfStringFormatFlags formatFlags)
        {
            this.leadingMarginFactor = 0.16666666666666666;
            this.trailingMarginFactor = 0.16666666666666666;
            this.trimming = PdfStringTrimming.Character;
            this.formatFlags = formatFlags;
        }

        public object Clone() => 
            new PdfStringFormat(this);

        public static PdfStringFormat GenericDefault =>
            new PdfStringFormat(genericDefault);

        public static PdfStringFormat GenericTypographic =>
            new PdfStringFormat(genericTypographic);

        public PdfStringFormatFlags FormatFlags
        {
            get => 
                this.formatFlags;
            set => 
                this.formatFlags = value;
        }

        public PdfStringAlignment Alignment
        {
            get => 
                this.alignment;
            set => 
                this.alignment = value;
        }

        public PdfStringAlignment LineAlignment
        {
            get => 
                this.lineAlignment;
            set => 
                this.lineAlignment = value;
        }

        public PdfStringTrimming Trimming
        {
            get => 
                this.trimming;
            set => 
                this.trimming = value;
        }

        public PdfHotkeyPrefix HotkeyPrefix
        {
            get => 
                this.hotkeyPrefix;
            set => 
                this.hotkeyPrefix = value;
        }

        public double LeadingMarginFactor
        {
            get => 
                this.leadingMarginFactor;
            set => 
                this.leadingMarginFactor = value;
        }

        public double TrailingMarginFactor
        {
            get => 
                this.trailingMarginFactor;
            set => 
                this.trailingMarginFactor = value;
        }

        internal double TabStopInterval
        {
            get => 
                this.tabStopInterval;
            set => 
                this.tabStopInterval = value;
        }

        internal bool DirectionRightToLeft
        {
            get => 
                this.directionRightToLeft;
            set => 
                this.directionRightToLeft = value;
        }
    }
}

