namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class PdfDocumentSelectionResults : IPdfDocumentSelectionResults
    {
        private readonly PdfDocumentViewModel document;
        private Dictionary<int, Geometry> selectionGeometry;

        public PdfDocumentSelectionResults(PdfDocumentViewModel document)
        {
            this.document = document;
            PdfSelection selection = document.DocumentState.SelectionState.Selection;
            this.Selection = selection;
            this.ContentType = selection.ContentType;
        }

        private Dictionary<int, Geometry> GenerateGeometry()
        {
            Dictionary<int, Geometry> dictionary = new Dictionary<int, Geometry>();
            foreach (PdfHighlight highlight in this.Selection.Highlights)
            {
                CombinedGeometry geometry1 = new CombinedGeometry();
                geometry1.GeometryCombineMode = GeometryCombineMode.Union;
                CombinedGeometry geometry = geometry1;
                IList<PdfOrientedRectangle> rectangles = highlight.Rectangles;
                foreach (PdfOrientedRectangle rectangle in rectangles)
                {
                    Geometry rectangleGeometry = this.GetRectangleGeometry(rectangle);
                    geometry = new CombinedGeometry(GeometryCombineMode.Union, geometry, rectangleGeometry);
                }
                if (dictionary.ContainsKey(highlight.PageIndex))
                {
                    dictionary[highlight.PageIndex] = new CombinedGeometry(GeometryCombineMode.Union, geometry, dictionary[highlight.PageIndex]);
                }
                else
                {
                    dictionary.Add(highlight.PageIndex, geometry);
                }
            }
            return dictionary;
        }

        public Rect GetBoundBox(double zoomFactor, double angle) => 
            this.GetBoundBox(this.PageIndex, zoomFactor, angle);

        public Rect GetBoundBox(int pageIndex, double zoomFactor, double angle)
        {
            PdfPageViewModel model = this.document.Pages[pageIndex];
            System.Windows.Point topLeft = this.SelectionGeometry[pageIndex].Bounds.TopLeft;
            System.Windows.Point bottomRight = this.SelectionGeometry[pageIndex].Bounds.BottomRight;
            return new Rect(model.GetPoint(new PdfPoint(topLeft.X, topLeft.Y), zoomFactor, angle), model.GetPoint(new PdfPoint(bottomRight.X, bottomRight.Y), zoomFactor, angle));
        }

        public BitmapSource GetImage(int rotationAngle)
        {
            using (Bitmap bitmap = this.document.ViewerBackend.CreateImageSelectionBitmap())
            {
                return ((bitmap != null) ? Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()) : null);
            }
        }

        private Geometry GetRectangleGeometry(PdfOrientedRectangle rectangle)
        {
            IList<PdfPoint> vertices = rectangle.Vertices;
            PdfPoint point = vertices[0];
            List<LineSegment> list2 = new List<LineSegment>();
            for (int i = 1; i < vertices.Count; i++)
            {
                PdfPoint point2 = vertices[i];
                list2.Add(new LineSegment(new System.Windows.Point(point2.X, point2.Y), true));
            }
            PathFigure figure = new PathFigure(new System.Windows.Point(point.X, point.Y), (IEnumerable<PathSegment>) list2, true);
            return new PathGeometry { Figures = { figure } };
        }

        public PdfSelection Selection { get; private set; }

        public string Text
        {
            get
            {
                Func<PdfTextSelection, string> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<PdfTextSelection, string> local1 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = x => x.Text;
                }
                return (this.Selection as PdfTextSelection).Return<PdfTextSelection, string>(evaluator, (<>c.<>9__7_1 ??= () => string.Empty));
            }
        }

        public bool HasSelection { get; private set; }

        public int PageIndex
        {
            get
            {
                Func<KeyValuePair<int, Geometry>, int> selector = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<KeyValuePair<int, Geometry>, int> local1 = <>c.<>9__13_0;
                    selector = <>c.<>9__13_0 = x => x.Key;
                }
                return this.SelectionGeometry.Min<KeyValuePair<int, Geometry>>(selector);
            }
        }

        private Dictionary<int, Geometry> SelectionGeometry
        {
            get
            {
                this.selectionGeometry ??= this.GenerateGeometry();
                return this.selectionGeometry;
            }
        }

        public PdfDocumentContentType ContentType { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfDocumentSelectionResults.<>c <>9 = new PdfDocumentSelectionResults.<>c();
            public static Func<PdfTextSelection, string> <>9__7_0;
            public static Func<string> <>9__7_1;
            public static Func<KeyValuePair<int, Geometry>, int> <>9__13_0;

            internal int <get_PageIndex>b__13_0(KeyValuePair<int, Geometry> x) => 
                x.Key;

            internal string <get_Text>b__7_0(PdfTextSelection x) => 
                x.Text;

            internal string <get_Text>b__7_1() => 
                string.Empty;
        }
    }
}

