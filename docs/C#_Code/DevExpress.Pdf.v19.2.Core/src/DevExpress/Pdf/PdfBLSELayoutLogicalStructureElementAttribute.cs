namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBLSELayoutLogicalStructureElementAttribute : PdfLayoutLogicalStructureElementAttribute
    {
        private const string spaceBeforeKey = "SpaceBefore";
        private const string spaceAfterKey = "SpaceAfter";
        private const string startIndentKey = "StartIndent";
        private const string endIndentKey = "EndIndent";
        private const string textIndentKey = "TextIndent";
        private const string textAlignKey = "TextAlign";
        private const string bBoxKey = "BBox";
        private const string widthKey = "Width";
        private const string heightKey = "Height";
        private const string blockAlignKey = "BlockAlign";
        private const string inlineAlignKey = "InlineAlign";
        private const string tBorderStyleKey = "TBorderStyle";
        private const string tPaddingKey = "TPadding";
        internal static string[] Keys;
        private readonly double spaceBefore;
        private readonly double spaceAfter;
        private readonly double startIndent;
        private readonly double endIndent;
        private readonly double textIndent;
        private readonly PdfBLSELayoutLogicalStructureElementAttributeTextAlign textAlign;
        private readonly PdfRectangle bBox;
        private readonly double? width;
        private readonly double? height;
        private readonly PdfBLSELayoutLogicalStructureElementAttributeTableCellBlockAlign tableCellBlockAlign;
        private readonly PdfBLSELayoutLogicalStructureElementAttributeTableCellInlineAlign tableCellInlineAlign;

        static PdfBLSELayoutLogicalStructureElementAttribute()
        {
            string[] textArray1 = new string[13];
            textArray1[0] = "SpaceBefore";
            textArray1[1] = "SpaceAfter";
            textArray1[2] = "StartIndent";
            textArray1[3] = "EndIndent";
            textArray1[4] = "TextIndent";
            textArray1[5] = "TextAlign";
            textArray1[6] = "BBox";
            textArray1[7] = "Width";
            textArray1[8] = "Height";
            textArray1[9] = "BlockAlign";
            textArray1[10] = "InlineAlign";
            textArray1[11] = "TBorderStyle";
            textArray1[12] = "TPadding";
            Keys = textArray1;
        }

        internal PdfBLSELayoutLogicalStructureElementAttribute(PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            double? number = dictionary.GetNumber("SpaceBefore");
            this.spaceBefore = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("SpaceAfter");
            this.spaceAfter = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("StartIndent");
            this.startIndent = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("EndIndent");
            this.endIndent = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("TextIndent");
            this.textIndent = (number != null) ? number.GetValueOrDefault() : 0.0;
            this.textAlign = PdfEnumToStringConverter.Parse<PdfBLSELayoutLogicalStructureElementAttributeTextAlign>(dictionary.GetName("TextAlign"), true);
            this.bBox = dictionary.GetRectangle("BBox");
            if (dictionary.TryGetValue("Width", out obj2))
            {
                this.width = GetElementSize(obj2);
            }
            if (dictionary.TryGetValue("Height", out obj2))
            {
                this.height = GetElementSize(obj2);
            }
            this.tableCellBlockAlign = PdfEnumToStringConverter.Parse<PdfBLSELayoutLogicalStructureElementAttributeTableCellBlockAlign>(dictionary.GetName("BlockAlign"), true);
            this.tableCellInlineAlign = PdfEnumToStringConverter.Parse<PdfBLSELayoutLogicalStructureElementAttributeTableCellInlineAlign>(dictionary.GetName("InlineAlign"), true);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("SpaceBefore", this.spaceBefore);
            dictionary.Add("SpaceAfter", this.spaceAfter, 0.0);
            dictionary.Add("StartIndent", this.startIndent, 0.0);
            dictionary.Add("EndIndent", this.endIndent, 0.0);
            dictionary.Add("TextIndent", this.textIndent, 0.0);
            dictionary.AddName("TextAlign", PdfEnumToStringConverter.Convert<PdfBLSELayoutLogicalStructureElementAttributeTextAlign>(this.textAlign, true));
            dictionary.Add("BBox", this.bBox);
            dictionary.AddIfPresent("Width", this.width?.Value);
            dictionary.AddIfPresent("Height", this.height?.Value);
            dictionary.AddEnumName<PdfBLSELayoutLogicalStructureElementAttributeTableCellBlockAlign>("BlockAlign", this.tableCellBlockAlign);
            dictionary.AddEnumName<PdfBLSELayoutLogicalStructureElementAttributeTableCellInlineAlign>("InlineAlign", this.tableCellInlineAlign);
            return dictionary;
        }

        private static double? GetElementSize(object value)
        {
            if (value != null)
            {
                if (value is double)
                {
                    return new double?((double) value);
                }
                if (value is int)
                {
                    return new double?((double) ((int) value));
                }
                PdfName name = value as PdfName;
                if ((name == null) || (name.Name != "Auto"))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            return null;
        }

        public double SpaceBefore =>
            this.spaceBefore;

        public double SpaceAfter =>
            this.spaceAfter;

        public double StartIndent =>
            this.startIndent;

        public double EndIndent =>
            this.endIndent;

        public double TextIndent =>
            this.textIndent;

        public PdfBLSELayoutLogicalStructureElementAttributeTextAlign TextAlign =>
            this.textAlign;

        public PdfRectangle BBox =>
            this.bBox;

        public double? Width =>
            this.width;

        public double? Height =>
            this.height;

        public PdfBLSELayoutLogicalStructureElementAttributeTableCellBlockAlign TableCellBlockAlign =>
            this.tableCellBlockAlign;

        public PdfBLSELayoutLogicalStructureElementAttributeTableCellInlineAlign InlineAlign =>
            this.tableCellInlineAlign;
    }
}

