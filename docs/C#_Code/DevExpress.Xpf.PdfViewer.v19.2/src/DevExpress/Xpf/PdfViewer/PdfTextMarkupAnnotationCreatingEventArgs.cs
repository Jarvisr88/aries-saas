namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class PdfTextMarkupAnnotationCreatingEventArgs : RoutedEventArgs
    {
        private readonly string selectedText;
        private readonly PdfViewerTextMarkupAnnotationBuilder builder;

        internal PdfTextMarkupAnnotationCreatingEventArgs(string selectedText, PdfViewerTextMarkupAnnotationBuilder builder) : base(PdfViewerControl.TextMarkupAnnotationCreatingEvent)
        {
            this.selectedText = selectedText;
            this.builder = builder;
        }

        public IList<PdfQuadrilateral> Quads
        {
            get
            {
                Func<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>> evaluator = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>> local1 = <>c.<>9__3_0;
                    evaluator = <>c.<>9__3_0 = x => new ReadOnlyCollection<PdfQuadrilateral>(x);
                }
                return this.builder.Quads.With<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>>(evaluator);
            }
        }

        public string SelectedText =>
            this.selectedText;

        public PdfRectangle Bounds =>
            this.builder.Rect;

        public int PageNumber =>
            this.builder.PageNumber;

        public PdfTextMarkupAnnotationType Style
        {
            get => 
                this.builder.Style;
            set => 
                this.builder.Style = value;
        }

        public string Name
        {
            get => 
                this.builder.Name;
            set => 
                this.builder.Name = value;
        }

        public bool IsSelected
        {
            get => 
                this.builder.IsSelected;
            set => 
                this.builder.IsSelected = value;
        }

        public System.Windows.Media.Color Color
        {
            get
            {
                PdfRGBColor color = this.builder.Color;
                return System.Windows.Media.Color.FromArgb((byte) (this.builder.Opacity * 255.0), (byte) (color.R * 255.0), (byte) (color.G * 255.0), (byte) (color.B * 255.0));
            }
            set
            {
                this.builder.Opacity = ((double) value.A) / 255.0;
                this.builder.Color = new PdfRGBColor(((double) value.R) / 255.0, ((double) value.G) / 255.0, ((double) value.B) / 255.0);
            }
        }

        public DateTimeOffset? CreationDate
        {
            get => 
                this.builder.CreationDate;
            set => 
                this.builder.CreationDate = value;
        }

        public DateTimeOffset? ModificationDate
        {
            get => 
                this.builder.ModificationDate;
            set => 
                this.builder.ModificationDate = value;
        }

        public string Author
        {
            get => 
                this.builder.Title;
            set => 
                this.builder.Title = value;
        }

        public string Subject
        {
            get => 
                this.builder.Subject;
            set => 
                this.builder.Subject = value;
        }

        public string Comment
        {
            get => 
                this.builder.Contents;
            set => 
                this.builder.Contents = value;
        }

        public bool Cancel { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfTextMarkupAnnotationCreatingEventArgs.<>c <>9 = new PdfTextMarkupAnnotationCreatingEventArgs.<>c();
            public static Func<IList<PdfQuadrilateral>, ReadOnlyCollection<PdfQuadrilateral>> <>9__3_0;

            internal ReadOnlyCollection<PdfQuadrilateral> <get_Quads>b__3_0(IList<PdfQuadrilateral> x) => 
                new ReadOnlyCollection<PdfQuadrilateral>(x);
        }
    }
}

