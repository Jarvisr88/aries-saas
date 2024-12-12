namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PdfTextMarkupAnnotationChangedEventArgs : RoutedEventArgs
    {
        internal PdfTextMarkupAnnotationChangedEventArgs(PdfTextMarkupAnnotationState annotationState) : base(PdfViewerControl.TextMarkupAnnotationChangedEvent)
        {
            PdfTextMarkupAnnotation markupAnnotation = annotationState.MarkupAnnotation;
            this.PageNumber = annotationState.PageNumber;
            this.Style = markupAnnotation.Type;
            this.Bounds = markupAnnotation.Rect;
            this.Name = markupAnnotation.Name;
            this.Color = this.FromPdfColor(markupAnnotation.Color);
            this.CreationDate = markupAnnotation.CreationDate;
            this.ModificationDate = markupAnnotation.Modified;
            this.Author = markupAnnotation.Title;
            this.Subject = markupAnnotation.Subject;
            this.Comment = markupAnnotation.Contents;
            Func<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>> evaluator = <>c.<>9__44_0;
            if (<>c.<>9__44_0 == null)
            {
                Func<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>> local1 = <>c.<>9__44_0;
                evaluator = <>c.<>9__44_0 = x => new ReadOnlyCollection<PdfQuadrilateral>(x);
            }
            this.Quads = markupAnnotation.Quads.With<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>>(evaluator);
        }

        private System.Windows.Media.Color FromPdfColor(PdfColor color)
        {
            if (color == null)
            {
                return Colors.Transparent;
            }
            PdfRGBColorData data = new PdfRGBColorData(color);
            return System.Windows.Media.Color.FromArgb(0xff, Convert.ToByte((double) (data.R * 255.0)), Convert.ToByte((double) (data.G * 255.0)), Convert.ToByte((double) (data.B * 255.0)));
        }

        public PdfRectangle Bounds { get; private set; }

        public int PageNumber { get; private set; }

        public PdfTextMarkupAnnotationType Style { get; private set; }

        public string Name { get; private set; }

        public System.Windows.Media.Color Color { get; private set; }

        public IList<PdfQuadrilateral> Quads { get; private set; }

        public DateTimeOffset? CreationDate { get; private set; }

        public DateTimeOffset? ModificationDate { get; private set; }

        public string Author { get; private set; }

        public string Subject { get; private set; }

        public string Comment { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfTextMarkupAnnotationChangedEventArgs.<>c <>9 = new PdfTextMarkupAnnotationChangedEventArgs.<>c();
            public static Func<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>> <>9__44_0;

            internal ReadOnlyCollection<PdfQuadrilateral> <.ctor>b__44_0(IList<PdfQuadrilateral> x) => 
                new ReadOnlyCollection<PdfQuadrilateral>(x);
        }
    }
}

