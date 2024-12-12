namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DirectWriteFont : DXFontFace
    {
        private readonly DirectWriteFontFamily fontFamily;
        private readonly DirectWriteFontMeasurer measurer;
        private readonly DWriteFont1 font;
        private readonly Lazy<DWriteFontFace> fontFace;

        public DirectWriteFont(DirectWriteFontFamily fontFamily, DirectWriteFontMeasurer measurer, DWriteFont1 font)
        {
            this.fontFamily = fontFamily;
            this.measurer = measurer;
            this.font = font;
            this.fontFace = new Lazy<DWriteFontFace>(() => font.CreateFontFace(), false);
        }

        public override DXCTLShaper CreateShaper() => 
            new DirectWriteShaper(this.measurer.TextAnalyzer, this);

        public DWriteTextFormat CreateTextFormat(float sizeInPoints) => 
            this.fontFamily.CreateTextFormat(this.font, sizeInPoints);

        public override void Dispose()
        {
            this.font.Dispose();
            if (this.fontFace.IsValueCreated)
            {
                this.fontFace.Value.Dispose();
            }
        }

        public override IList<DXCharRange> GetFontCharacterRanges()
        {
            Func<DWRITE_UNICODE_RANGE, DXCharRange> selector = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                Func<DWRITE_UNICODE_RANGE, DXCharRange> local1 = <>c.<>9__23_0;
                selector = <>c.<>9__23_0 = range => new DXCharRange(range.First, range.Last);
            }
            return this.font.GetUnicodeRanges().Select<DWRITE_UNICODE_RANGE, DXCharRange>(selector).ToArray<DXCharRange>();
        }

        public override DXFontFileInfo GetFontFile()
        {
            using (DWriteFontFace face = this.font.CreateFontFace())
            {
                DWriteFontFile[] files = face.GetFiles();
                return new DXFontFileInfo(face.GetIndex(), files[0].GetData());
            }
        }

        public override DXShapedGlyphInfo GetShapedGlyphsInfo(string text)
        {
            throw new NotImplementedException();
        }

        public override float MeasureCharacterWidth(char character, float fontSizeInPoints) => 
            this.measurer.MeasureCharacterWidth(character, this, fontSizeInPoints);

        public override DXSizeF MeasureString(string text, float fontSizeInPoints) => 
            this.measurer.MeasureString(text, this, fontSizeInPoints);

        public override string FamilyName =>
            this.fontFamily.Name;

        public override DXFontMetrics Metrics =>
            new DXFontMetrics(this.font.GetMetrics1());

        public override byte[] Panose =>
            this.font.GetPanose();

        public override DXFontSimulations Simulations =>
            (DXFontSimulations) this.font.GetSimulations();

        public override DXFontStretch Stretch =>
            (DXFontStretch) this.font.GetStretch();

        public override DXFontStyle Style =>
            (DXFontStyle) this.font.GetStyle();

        public override DXFontWeight Weight =>
            (DXFontWeight) this.font.GetWeight();

        public DWriteFontFace FontFace =>
            this.fontFace.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DirectWriteFont.<>c <>9 = new DirectWriteFont.<>c();
            public static Func<DWRITE_UNICODE_RANGE, DXCharRange> <>9__23_0;

            internal DXCharRange <GetFontCharacterRanges>b__23_0(DWRITE_UNICODE_RANGE range) => 
                new DXCharRange(range.First, range.Last);
        }
    }
}

