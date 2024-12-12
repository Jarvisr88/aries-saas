namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfTextMarkupAnnotation : PdfMarkupAnnotation
    {
        internal const string HighlightType = "Highlight";
        internal const string UnderlineType = "Underline";
        internal const string SquigglyType = "Squiggly";
        internal const string StrikeOutType = "StrikeOut";
        private readonly IList<PdfQuadrilateral> quads;
        private PdfTextMarkupAnnotationType type;

        internal PdfTextMarkupAnnotation(PdfPage page, IPdfTextMarkupAnnotationBuilder builder) : base(page, builder)
        {
            this.type = builder.Style;
            this.quads = builder.Quads;
        }

        internal PdfTextMarkupAnnotation(PdfPage page, PdfTextMarkupAnnotationType type, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.type = type;
            this.quads = PdfQuadrilateral.ParseArray(dictionary);
        }

        protected internal override void Accept(IPdfAnnotationVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected internal override IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch) => 
            new PdfTextMarkupAnnotationAppearanceBuilder(this);

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            PdfQuadrilateral.Write(dictionary, this.quads);
            return dictionary;
        }

        public PdfTextMarkupAnnotationType Type
        {
            get => 
                this.type;
            internal set => 
                this.type = value;
        }

        public IList<PdfQuadrilateral> Quads =>
            this.quads;

        protected override string AnnotationType =>
            PdfEnumToStringConverter.Convert<PdfTextMarkupAnnotationType>(this.type, true);
    }
}

