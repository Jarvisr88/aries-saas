namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfLineAnnotation : PdfUnclosedPathAnnotation
    {
        internal const string Type = "Line";
        private const string leaderLinesLengthDictionaryKey = "LL";
        private const string leaderLineExtensionsLengthDictionaryKey = "LLE";
        private const string showCaptionDictionaryKey = "Cap";
        private const string leaderLineOffsetDictionaryKey = "LLO";
        private const string captionPositionDictionaryKey = "CP";
        private const string captionOffsetsDictionaryKey = "CO";
        private readonly double leaderLinesLength;
        private readonly double leaderLineExtensionsLength;
        private readonly bool showCaption;
        private readonly PdfLineAnnotationIntent lineIntent;
        private readonly double leaderLineOffset;
        private readonly PdfLineAnnotationCaptionPosition captionPosition;
        private readonly double horizontalCaptionOffset;
        private readonly double verticalCaptionOffset;

        internal PdfLineAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            double? number = dictionary.GetNumber("LL");
            this.leaderLinesLength = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("LLE");
            this.leaderLineExtensionsLength = (number != null) ? number.GetValueOrDefault() : 0.0;
            bool? boolean = dictionary.GetBoolean("Cap");
            this.showCaption = (boolean != null) ? boolean.GetValueOrDefault() : false;
            this.lineIntent = PdfEnumToStringConverter.Parse<PdfLineAnnotationIntent>(base.Intent, true);
            number = dictionary.GetNumber("LLO");
            this.leaderLineOffset = (number != null) ? number.GetValueOrDefault() : 0.0;
            IList<PdfPoint> vertices = base.Vertices;
            if (((vertices != null) && (vertices.Count != 2)) || (this.leaderLineOffset < 0.0))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.captionPosition = PdfEnumToStringConverter.Parse<PdfLineAnnotationCaptionPosition>(dictionary.GetName("CP"), true);
            IList<object> array = dictionary.GetArray("CO");
            if (array != null)
            {
                if (array.Count != 2)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.horizontalCaptionOffset = PdfDocumentReader.ConvertToDouble(array[0]);
                this.verticalCaptionOffset = PdfDocumentReader.ConvertToDouble(array[1]);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("LL", this.leaderLinesLength, 0.0);
            dictionary.Add("LLE", this.leaderLineExtensionsLength, 0.0);
            dictionary.Add("Cap", this.showCaption, false);
            dictionary.Add("LLO", this.leaderLineOffset, 0.0);
            dictionary.AddName("CP", PdfEnumToStringConverter.Convert<PdfLineAnnotationCaptionPosition>(this.captionPosition, true));
            if ((this.horizontalCaptionOffset != 0.0) || (this.verticalCaptionOffset != 0.0))
            {
                double[] numArray1 = new double[] { this.horizontalCaptionOffset, this.verticalCaptionOffset };
                dictionary.Add("CO", numArray1);
            }
            return dictionary;
        }

        public double LeaderLinesLength =>
            this.leaderLinesLength;

        public double LeaderLineExtensionsLength =>
            this.leaderLineExtensionsLength;

        public bool ShowCaption =>
            this.showCaption;

        public PdfLineAnnotationIntent LineIntent =>
            this.lineIntent;

        public double LeaderLineOffset =>
            this.leaderLineOffset;

        public PdfLineAnnotationCaptionPosition CaptionPosition =>
            this.captionPosition;

        public double HorizontalCaptionOffset =>
            this.horizontalCaptionOffset;

        public double VerticalCaptionOffset =>
            this.verticalCaptionOffset;

        protected override string AnnotationType =>
            "Line";

        protected override string VerticesDictionaryKey =>
            "L";
    }
}

