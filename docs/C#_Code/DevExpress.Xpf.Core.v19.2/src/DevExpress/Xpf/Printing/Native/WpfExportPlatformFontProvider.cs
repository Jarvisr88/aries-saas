namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;
    using System.Globalization;
    using System.Windows.Media;

    internal class WpfExportPlatformFontProvider : IPdfExportPlatformFontProvider
    {
        private readonly GlyphTypeface typeface;
        private readonly DXFontDescriptor descriptor;

        public WpfExportPlatformFontProvider(GlyphTypeface typeface)
        {
            this.typeface = typeface;
            this.descriptor = this.CreateDescriptor(typeface);
        }

        private DXFontDescriptor CreateDescriptor(GlyphTypeface typeface) => 
            new DXFontDescriptor(typeface.FamilyNames[new CultureInfo("en-US")], typeface.Weight.ToPdfFontWeight(), typeface.Style.ToPdfFontStyle(), typeface.Stretch.ToPdfFontStretch());

        public IPdfExportPlatformFont GetPlatformFont() => 
            new WpfExportPlatformFont(this.typeface, this.descriptor);

        public object Key =>
            this.descriptor;
    }
}

