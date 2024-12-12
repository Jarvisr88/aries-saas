namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfILSELayoutLogicalStructureElementAttribute : PdfLayoutLogicalStructureElementAttribute
    {
        private const string baselineShiftKey = "BaselineShift";
        private const string lineHeightKey = "LineHeight";
        private const string textDecorationColorKey = "TextDecorationColor";
        private const string textDecorationThicknessKey = "TextDecorationThickness";
        private const string textDecorationTypeKey = "TextDecorationType";
        private const string rubyAlignKey = "RubyAlign";
        private const string rubyPositionKey = "RubyPosition";
        private const string glyphOrientationVerticalKey = "GlyphOrientationVertical";
        internal static string[] Keys = new string[] { "BaselineShift", "LineHeight", "TextDecorationColor", "TextDecorationThickness", "TextDecorationType", "RubyAlign", "RubyPosition", "GlyphOrientationVertical" };
        private readonly double baselineShift;
        private readonly double? lineHeight;
        private readonly PdfColor textDecorationColor;
        private readonly double? textDecorationThickness;
        private readonly PdfILSELayoutLogicalStructureElementAttributeTextDecorationType textDecorationType;
        private readonly PdfILSELayoutLogicalStructureElementAttributeRubyAlign rubyAlign;
        private readonly PdfILSELayoutLogicalStructureElementAttributeRubyPosition rubyPosition;
        private readonly object glyphOrientationVertical;

        internal PdfILSELayoutLogicalStructureElementAttribute(PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            this.rubyAlign = PdfILSELayoutLogicalStructureElementAttributeRubyAlign.Distribute;
            double? number = dictionary.GetNumber("BaselineShift");
            this.baselineShift = (number != null) ? number.GetValueOrDefault() : 0.0;
            this.lineHeight = dictionary.GetNumber("LineHeight");
            this.textDecorationColor = base.ConvertToColor(dictionary.GetDoubleArray("TextDecorationColor"));
            this.textDecorationThickness = dictionary.GetNumber("TextDecorationThickness");
            if ((this.textDecorationThickness != null) && (this.textDecorationThickness.Value < 0.0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.textDecorationType = PdfEnumToStringConverter.Parse<PdfILSELayoutLogicalStructureElementAttributeTextDecorationType>(dictionary.GetName("TextDecorationType"), true);
            this.rubyAlign = PdfEnumToStringConverter.Parse<PdfILSELayoutLogicalStructureElementAttributeRubyAlign>(dictionary.GetName("RubyAlign"), true);
            this.rubyPosition = PdfEnumToStringConverter.Parse<PdfILSELayoutLogicalStructureElementAttributeRubyPosition>(dictionary.GetName("RubyPosition"), true);
            if (dictionary.TryGetValue("GlyphOrientationVertical", out obj2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("BaselineShift", this.baselineShift);
            if (this.lineHeight != null)
            {
                dictionary.Add("LineHeight", this.lineHeight.Value);
            }
            dictionary.Add("TextDecorationColor", this.textDecorationColor);
            if (this.textDecorationThickness != null)
            {
                dictionary.Add("TextDecorationThickness", this.textDecorationThickness);
            }
            dictionary.AddEnumName<PdfILSELayoutLogicalStructureElementAttributeTextDecorationType>("TextDecorationType", this.textDecorationType);
            dictionary.AddEnumName<PdfILSELayoutLogicalStructureElementAttributeRubyAlign>("RubyAlign", this.rubyAlign);
            dictionary.AddEnumName<PdfILSELayoutLogicalStructureElementAttributeRubyPosition>("RubyPosition", this.rubyPosition);
            return dictionary;
        }

        public double BaselineShift =>
            this.baselineShift;

        public double? LineHeight =>
            this.lineHeight;

        public object TextDecorationColor =>
            this.textDecorationColor;

        public double? TextDecorationThickness =>
            this.textDecorationThickness;

        public PdfILSELayoutLogicalStructureElementAttributeTextDecorationType TextDecorationType =>
            this.textDecorationType;

        public PdfILSELayoutLogicalStructureElementAttributeRubyAlign RubyAlign =>
            this.rubyAlign;

        public PdfILSELayoutLogicalStructureElementAttributeRubyPosition RubyPosition =>
            this.rubyPosition;

        public object GlyphOrientationVertical =>
            this.glyphOrientationVertical;
    }
}

