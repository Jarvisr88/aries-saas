namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PdfTextMarkupAnnotationData : PdfMarkupAnnotationData
    {
        private readonly PdfTextMarkupAnnotationState annotationState;
        private readonly ReadOnlyCollection<PdfQuadrilateral> quads;

        internal PdfTextMarkupAnnotationData(PdfTextMarkupAnnotationState annotationState)
        {
            this.annotationState = annotationState;
            IList<PdfQuadrilateral> quads = annotationState.MarkupAnnotation.Quads;
            this.quads = (quads != null) ? new ReadOnlyCollection<PdfQuadrilateral>(quads) : null;
        }

        internal static string GetSubject(PdfTextMarkupAnnotationType type)
        {
            switch (type)
            {
                case PdfTextMarkupAnnotationType.Underline:
                case PdfTextMarkupAnnotationType.Squiggly:
                    return PdfCoreLocalizer.GetString(PdfCoreStringId.TextUnderlineDefaultSubject);

                case PdfTextMarkupAnnotationType.StrikeOut:
                    return PdfCoreLocalizer.GetString(PdfCoreStringId.TextStrikethroughDefaultSubject);
            }
            return PdfCoreLocalizer.GetString(PdfCoreStringId.TextHighlightDefaultSubject);
        }

        internal static PdfColor UnderlineDefaultColor =>
            new PdfColor(new double[] { 0.4157, 0.851, 0.157 });

        internal static PdfColor StrikeOutDefaultColor =>
            new PdfColor(new double[] { 0.898, 0.1333, 0.2157 });

        internal static PdfColor HighlightDefaultColor
        {
            get
            {
                double[] components = new double[3];
                components[0] = 1.0;
                components[1] = 0.8196;
                return new PdfColor(components);
            }
        }

        public PdfTextMarkupAnnotationType MarkupType
        {
            get => 
                this.annotationState.MarkupAnnotation.Type;
            set
            {
                if ((this.Annotation.Page.DocumentCatalog.CreationOptions.Compatibility == PdfCompatibility.PdfA1b) && (value == PdfTextMarkupAnnotationType.Highlight))
                {
                    throw new NotSupportedException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnsupportedAnnotationType));
                }
                PdfTextMarkupAnnotation annotation = this.annotationState.MarkupAnnotation;
                base.SetPropertyValue<PdfTextMarkupAnnotationType>(annotation.Type, value, () => annotation.Type = value);
                base.RebuildAppearance();
            }
        }

        public IList<PdfQuadrilateral> Quads =>
            this.quads;

        protected override PdfMarkupAnnotation Annotation =>
            this.annotationState.MarkupAnnotation;

        protected internal override PdfMarkupAnnotationState AnnotationState =>
            this.annotationState;
    }
}

