namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public class GlyphControlViewInfo : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider);

        public int Size { get; set; }

        [Obsolete]
        public int Padding { get; set; }

        public Thickness GlyphLeftPadding { get; set; }

        public Thickness GlyphRightPadding { get; set; }

        public Thickness GlyphUpPadding { get; set; }

        public Thickness GlyphDownPadding { get; set; }

        public Thickness GlyphPlusPadding { get; set; }

        public Thickness GlyphClosePadding { get; set; }
    }
}

