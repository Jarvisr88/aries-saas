namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfCourierFontFamily : PdfStandardFontFamily
    {
        private const string family = "Courier";
        private static readonly PdfStandardFontFamily.PdfMonospacedGlyphWidthProvider courierWidths = new PdfStandardFontFamily.PdfMonospacedGlyphWidthProvider(600);
        private static readonly WidthsProvider widthsProvider = new WidthsProvider();
        private static readonly DescriptorProvider descriptorProvider = new DescriptorProvider();

        protected override PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider> GlyphWidthProvider =>
            widthsProvider;

        protected override PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<PdfFontDescriptorData> FontDescriptorProvider =>
            descriptorProvider;

        private class DescriptorProvider : PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<PdfFontDescriptorData>
        {
            protected override PdfFontDescriptorData Regular
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Courier";
                    data1.Flags = PdfFontFlags.SmallCap | PdfFontFlags.Nonsymbolic | PdfFontFlags.Serif | PdfFontFlags.FixedPitch;
                    data1.BBox = new PdfRectangle(-23.0, -250.0, 715.0, 805.0);
                    data1.Ascent = 629.0;
                    data1.Descent = -157.0;
                    data1.CapHeight = 562.0;
                    data1.XHeight = 426.0;
                    data1.StemV = 51.0;
                    data1.StemH = 51.0;
                    return data1;
                }
            }

            protected override PdfFontDescriptorData Bold
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Courier";
                    data1.Flags = PdfFontFlags.ForceBold | PdfFontFlags.SmallCap | PdfFontFlags.Nonsymbolic | PdfFontFlags.Serif | PdfFontFlags.FixedPitch;
                    data1.BBox = new PdfRectangle(-113.0, -250.0, 749.0, 801.0);
                    data1.Ascent = 629.0;
                    data1.Descent = -157.0;
                    data1.CapHeight = 562.0;
                    data1.XHeight = 439.0;
                    data1.StemV = 106.0;
                    data1.StemH = 84.0;
                    return data1;
                }
            }

            protected override PdfFontDescriptorData Italic
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Courier";
                    data1.Flags = PdfFontFlags.SmallCap | PdfFontFlags.Italic | PdfFontFlags.Nonsymbolic | PdfFontFlags.Serif | PdfFontFlags.FixedPitch;
                    data1.BBox = new PdfRectangle(-27.0, -250.0, 849.0, 805.0);
                    data1.ItalicAngle = -12.0;
                    data1.Ascent = 629.0;
                    data1.Descent = -157.0;
                    data1.CapHeight = 562.0;
                    data1.XHeight = 426.0;
                    data1.StemH = 51.0;
                    data1.StemV = 51.0;
                    return data1;
                }
            }

            protected override PdfFontDescriptorData BoldItalic
            {
                get
                {
                    PdfFontDescriptorData data1 = new PdfFontDescriptorData();
                    data1.FontFamily = "Courier";
                    data1.Flags = PdfFontFlags.ForceBold | PdfFontFlags.SmallCap | PdfFontFlags.Italic | PdfFontFlags.Nonsymbolic | PdfFontFlags.Serif | PdfFontFlags.FixedPitch;
                    data1.BBox = new PdfRectangle(-57.0, -250.0, 869.0, 801.0);
                    data1.ItalicAngle = -12.0;
                    data1.Ascent = 629.0;
                    data1.Descent = -157.0;
                    data1.CapHeight = 562.0;
                    data1.XHeight = 439.0;
                    data1.StemH = 84.0;
                    data1.StemV = 106.0;
                    return data1;
                }
            }
        }

        private class WidthsProvider : PdfStandardFontFamily.PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider>
        {
            protected override IPdfGlyphWidthProvider Regular =>
                PdfCourierFontFamily.courierWidths;
        }
    }
}

