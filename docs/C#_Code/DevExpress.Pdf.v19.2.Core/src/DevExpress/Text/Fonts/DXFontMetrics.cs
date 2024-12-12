namespace DevExpress.Text.Fonts
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class DXFontMetrics
    {
        internal DXFontMetrics(DWRITE_FONT_METRICS1 metrics)
        {
            this.<UnitsPerEm>k__BackingField = metrics.DesignUnitsPerEm;
            this.<EmAscent>k__BackingField = metrics.Ascent;
            this.<EmDescent>k__BackingField = metrics.Descent;
            this.<EmLineSpacing>k__BackingField = (this.EmAscent + this.EmDescent) + metrics.LineGap;
            this.<UnderlinePosition>k__BackingField = metrics.UnderlinePosition;
            this.<UnderlineSize>k__BackingField = metrics.UnderlineThickness;
            this.<StrikeoutPosition>k__BackingField = metrics.StrikethroughPosition;
            this.<StrikeoutSize>k__BackingField = metrics.StrikethroughThickness;
            this.<SubscriptSize>k__BackingField = new DXSize(metrics.SubscriptSizeX, metrics.SubscriptSizeY);
            this.<SuperscriptOffset>k__BackingField = new DXPoint(metrics.SuperscriptPositionX, metrics.SuperscriptPositionY);
            this.<SubscriptOffset>k__BackingField = new DXPoint(metrics.SubscriptPositionX, metrics.SubscriptPositionY);
            this.<SuperscriptSize>k__BackingField = new DXSize(metrics.SuperscriptSizeX, metrics.SuperscriptSizeY);
            this.<HasTypoMetrics>k__BackingField = metrics.HasTypographicMetrics;
        }

        public PdfFontMetrics ToPdfMetrics() => 
            new PdfFontMetrics((double) this.EmAscent, (double) this.EmDescent, (double) this.EmLineSpacing, (double) this.UnitsPerEm);

        public int UnitsPerEm { get; }

        public int EmAscent { get; }

        public int EmDescent { get; }

        public int EmLineSpacing { get; }

        public int UnderlinePosition { get; }

        public int UnderlineSize { get; }

        public int StrikeoutPosition { get; }

        public int StrikeoutSize { get; }

        public DXSize SubscriptSize { get; }

        public DXPoint SuperscriptOffset { get; }

        public DXPoint SubscriptOffset { get; }

        public DXSize SuperscriptSize { get; }

        public bool HasTypoMetrics { get; }
    }
}

