namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class FontInfo : IDisposable
    {
        private const string StringToMeasureAverageCharWidth = "ABCDEFGWabcdefgw1234567890";
        private const string StringToMeasureMaxCharWidth = "W";
        protected const int InitialCharCount = 0x400;
        private bool isDisposed;
        private int ascent;
        private int descent;
        private int free;
        private int lineSpacing;
        private int calculatedLineSpacing;
        private int textRotation;
        private int spaceWidth;
        private int pilcrowSignWidth;
        private int nonBreakingSpaceWidth;
        private int underscoreWidth;
        private int middleDotWidth;
        private int dotWidth;
        private int dashWidth;
        private int equalSignWidth;
        private float maxDigitWidth;
        private int underlineThickness;
        private int underlinePosition;
        private int strikeoutThickness;
        private int strikeoutPosition;
        private System.Drawing.Size subscriptSize;
        private Point subscriptOffset;
        private System.Drawing.Size superscriptSize;
        private Point superscriptOffset;
        private FontInfo baselineFontInfo;
        private float sizeInPoints;
        private int charset;
        private char currentLastChar;

        protected FontInfo()
        {
        }

        protected FontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, bool useSystemFontQuality, FontInfo baselineFontInfo) : this(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, 0, useSystemFontQuality, baselineFontInfo)
        {
        }

        protected FontInfo(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, int textRotation, bool useSystemFontQuality, FontInfo baselineFontInfo)
        {
            this.Initialize(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline, allowCjkCorrection, textRotation, useSystemFontQuality, baselineFontInfo);
        }

        protected internal virtual void AdjustFontParameters()
        {
            this.free = (this.lineSpacing - this.ascent) - this.descent;
            if (this.free < 0)
            {
                this.descent += this.free;
                if (this.descent < 0)
                {
                    this.ascent += this.descent;
                    this.descent = 0;
                }
                this.free = 0;
            }
        }

        protected internal abstract int CalculateFontCharset(FontInfoMeasurer measurer);
        protected virtual void CalculateFontHeightMetrics(FontInfoMeasurer measurer)
        {
            this.CalculateMetricFromFont(null);
        }

        protected internal virtual void CalculateFontParameters(FontInfoMeasurer measurer, bool allowCjkCorrection)
        {
            this.CalculateFontVerticalParameters(measurer, allowCjkCorrection);
            this.sizeInPoints = this.CalculateFontSizeInPoints();
            this.AdjustFontParameters();
            this.spaceWidth = (int) Math.Round((double) measurer.MeasureCharacterWidthF(' ', this));
            this.pilcrowSignWidth = measurer.MeasureCharacterWidth('\x00b6', this);
            this.nonBreakingSpaceWidth = measurer.MeasureCharacterWidth('\x00a0', this);
            this.underscoreWidth = measurer.MeasureCharacterWidth('_', this);
            this.middleDotWidth = measurer.MeasureCharacterWidth('\x00b7', this);
            this.dotWidth = measurer.MeasureCharacterWidth('.', this);
            this.dashWidth = measurer.MeasureCharacterWidth('-', this);
            this.equalSignWidth = measurer.MeasureCharacterWidth('=', this);
            this.charset = this.CalculateFontCharset(measurer);
            this.maxDigitWidth = this.CalculateMaxDigitWidth(measurer);
            this.CalculateUnderlineAndStrikeoutParameters(measurer);
            this.CalculateFontHeightMetrics(measurer);
        }

        protected internal abstract float CalculateFontSizeInPoints();
        public FontStyle CalculateFontStyle()
        {
            FontStyle regular = FontStyle.Regular;
            if (this.Bold)
            {
                regular |= FontStyle.Bold;
            }
            if (this.Italic)
            {
                regular |= FontStyle.Italic;
            }
            if (this.Strikeout)
            {
                regular |= FontStyle.Strikeout;
            }
            if (this.Underline)
            {
                regular |= FontStyle.Underline;
            }
            return regular;
        }

        protected internal abstract void CalculateFontVerticalParameters(FontInfoMeasurer measurer, bool allowCjkCorrection);
        private float CalculateMaxDigitWidth(FontInfoMeasurer measurer) => 
            measurer.MeasureMaxDigitWidthF(this);

        protected virtual void CalculateMetricFromFont(Graphics graphics)
        {
            this.FontHeightMetrics = new DevExpress.Office.Drawing.FontHeightMetrics();
            this.FontHeightMetrics.FirstChar = '\0';
            this.FontHeightMetrics.LastChar = '\0';
            this.FontHeightMetrics.Height = this.Font.Height;
            if (graphics != null)
            {
                this.FontHeightMetrics.AverageCharWidth = (int) (graphics.MeasureString("ABCDEFGWabcdefgw1234567890", this.Font).Width / ((float) "ABCDEFGWabcdefgw1234567890".Length));
                this.FontHeightMetrics.MaxCharWidth = (int) (graphics.MeasureString("W", this.Font).Width / ((float) "W".Length));
            }
        }

        protected internal abstract void CalculateUnderlineAndStrikeoutParameters(FontInfoMeasurer measurer);
        protected internal abstract void CreateFont(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline);
        public virtual void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.baselineFontInfo = null;
            }
            this.isDisposed = true;
        }

        protected internal abstract void Initialize(FontInfoMeasurer measurer, bool useSystemFontQuality);
        protected void Initialize(FontInfoMeasurer measurer, string fontName, int doubleFontSize, bool fontBold, bool fontItalic, bool fontStrikeout, bool fontUnderline, bool allowCjkCorrection, int textRotation, bool useSystemFontQuality, FontInfo baselineFontInfo)
        {
            this.textRotation = textRotation;
            FontInfo info1 = baselineFontInfo;
            if (baselineFontInfo == null)
            {
                FontInfo local1 = baselineFontInfo;
                info1 = this;
            }
            this.baselineFontInfo = info1;
            this.CreateFont(measurer, fontName, doubleFontSize, fontBold, fontItalic, fontStrikeout, fontUnderline);
            this.Initialize(measurer, useSystemFontQuality);
            this.CalculateFontParameters(measurer, allowCjkCorrection);
        }

        public bool IsFontChar(char ch) => 
            (ch >= this.FontHeightMetrics.FirstChar) && (ch <= this.FontHeightMetrics.LastChar);

        public static bool IsNewLine(char ch) => 
            ch == NewLineChar;

        public static bool IsReturn(char ch) => 
            ch == ReturnChar;

        public static bool IsSpace(char ch) => 
            ch == SpaceChar;

        public static bool IsTabStop(char ch) => 
            ch == TabStopChar;

        public static char TabStopChar =>
            '\t';

        public static char NewLineChar =>
            '\n';

        public static char ReturnChar =>
            '\r';

        public static char SpaceChar =>
            ' ';

        public abstract System.Drawing.Font Font { get; }

        public bool IsDisposed =>
            this.isDisposed;

        public abstract bool Bold { get; }

        public abstract bool Italic { get; }

        public abstract bool Underline { get; }

        public abstract bool Strikeout { get; }

        public abstract float Size { get; }

        public abstract string Name { get; }

        public abstract string FontFamilyName { get; }

        public int Ascent
        {
            get => 
                this.ascent;
            protected internal set => 
                this.ascent = value;
        }

        public int AscentAndFree =>
            this.ascent + this.free;

        public int Descent
        {
            get => 
                this.descent;
            protected internal set => 
                this.descent = value;
        }

        public int Free
        {
            get => 
                this.free;
            protected internal set => 
                this.free = value;
        }

        public int LineSpacing
        {
            get => 
                this.lineSpacing;
            protected internal set => 
                this.lineSpacing = value;
        }

        public int CalculatedLineSpacing
        {
            get => 
                this.calculatedLineSpacing;
            protected internal set => 
                this.calculatedLineSpacing = value;
        }

        public int TextRotation
        {
            get => 
                this.textRotation;
            protected internal set => 
                this.textRotation = value;
        }

        public int SpaceWidth
        {
            get => 
                this.spaceWidth;
            protected internal set => 
                this.spaceWidth = value;
        }

        public int PilcrowSignWidth
        {
            get => 
                this.pilcrowSignWidth;
            protected internal set => 
                this.pilcrowSignWidth = value;
        }

        public int NonBreakingSpaceWidth
        {
            get => 
                this.nonBreakingSpaceWidth;
            set => 
                this.nonBreakingSpaceWidth = value;
        }

        public int UnderscoreWidth
        {
            get => 
                this.underscoreWidth;
            set => 
                this.underscoreWidth = value;
        }

        public int MiddleDotWidth
        {
            get => 
                this.middleDotWidth;
            set => 
                this.middleDotWidth = value;
        }

        public int DotWidth
        {
            get => 
                this.dotWidth;
            set => 
                this.dotWidth = value;
        }

        public int DashWidth
        {
            get => 
                this.dashWidth;
            set => 
                this.dashWidth = value;
        }

        public int EqualSignWidth
        {
            get => 
                this.equalSignWidth;
            protected internal set => 
                this.equalSignWidth = value;
        }

        public int UnderlineThickness
        {
            get => 
                this.underlineThickness;
            protected internal set => 
                this.underlineThickness = value;
        }

        public int UnderlinePosition
        {
            get => 
                this.underlinePosition;
            protected internal set => 
                this.underlinePosition = value;
        }

        public int StrikeoutThickness
        {
            get => 
                this.strikeoutThickness;
            protected internal set => 
                this.strikeoutThickness = value;
        }

        public int StrikeoutPosition
        {
            get => 
                this.strikeoutPosition;
            protected internal set => 
                this.strikeoutPosition = value;
        }

        public System.Drawing.Size SubscriptSize
        {
            get => 
                this.subscriptSize;
            protected internal set => 
                this.subscriptSize = value;
        }

        public Point SubscriptOffset
        {
            get => 
                this.subscriptOffset;
            protected internal set => 
                this.subscriptOffset = value;
        }

        public System.Drawing.Size SuperscriptSize
        {
            get => 
                this.superscriptSize;
            protected internal set => 
                this.superscriptSize = value;
        }

        public Point SuperscriptOffset
        {
            get => 
                this.superscriptOffset;
            protected internal set => 
                this.superscriptOffset = value;
        }

        public FontInfo BaselineFontInfo =>
            this.baselineFontInfo;

        public float SizeInPoints
        {
            get => 
                this.sizeInPoints;
            protected internal set => 
                this.sizeInPoints = value;
        }

        public int Charset
        {
            get => 
                this.charset;
            protected internal set => 
                this.charset = value;
        }

        public float MaxDigitWidth
        {
            get => 
                this.maxDigitWidth;
            protected internal set => 
                this.maxDigitWidth = value;
        }

        public int DrawingOffset { get; set; }

        public bool UseTypoMetrics { get; protected internal set; }

        public DevExpress.Office.Drawing.FontHeightMetrics FontHeightMetrics { get; set; }

        public char CurrentLastChar
        {
            get => 
                this.currentLastChar;
            protected set => 
                this.currentLastChar = value;
        }
    }
}

