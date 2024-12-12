namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ThumbnailsKeyboardAndMouseController : KeyboardAndMouseController
    {
        public ThumbnailsKeyboardAndMouseController(DocumentPresenterControl presenter) : base(presenter)
        {
            this.SelectedPageNumber = -1;
        }

        public override void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.ProcessMouseLeftButtonUp(e);
            PdfThumbnailsViewerControl actualDocumentViewer = base.presenter.ActualDocumentViewer as PdfThumbnailsViewerControl;
            Point position = e.GetPosition(base.presenter);
            Func<double, double> getPageHorizontalOffsetHandler = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<double, double> local1 = <>c.<>9__5_0;
                getPageHorizontalOffsetHandler = <>c.<>9__5_0 = width => 0.0;
            }
            int num = base.presenter.NavigationStrategy.PositionCalculator.GetPageIndex(position.Y + base.presenter.ScrollViewer.VerticalOffset, position.X + base.presenter.ScrollViewer.HorizontalOffset, getPageHorizontalOffsetHandler);
            if (num == -1)
            {
                actualDocumentViewer.HighlightedPageNumbers.Clear();
                this.SelectedPageNumber = -1;
            }
            else
            {
                int pageNumber = num + 1;
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
                {
                    if (this.SelectedPageNumber == -1)
                    {
                        this.SelectedPageNumber = pageNumber;
                    }
                    int num2 = Math.Min(this.SelectedPageNumber, pageNumber);
                    int num3 = Math.Max(this.SelectedPageNumber, pageNumber);
                    actualDocumentViewer.HighlightedPageNumbers.Clear();
                    for (int i = num2; i <= num3; i++)
                    {
                        actualDocumentViewer.HighlightedPageNumbers.Add(i);
                    }
                }
                else if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    actualDocumentViewer.HighlightedPageNumbers.Clear();
                    actualDocumentViewer.HighlightedPageNumbers.Add(pageNumber);
                    this.SelectedPageNumber = pageNumber;
                }
                else
                {
                    if (!actualDocumentViewer.HighlightedPageNumbers.Contains(pageNumber))
                    {
                        actualDocumentViewer.HighlightedPageNumbers.Add(pageNumber);
                    }
                    else
                    {
                        actualDocumentViewer.HighlightedPageNumbers.Remove(pageNumber);
                    }
                    this.SelectedPageNumber = pageNumber;
                }
                actualDocumentViewer.Do<PdfThumbnailsViewerControl>(x => x.SelectedPageNumber = pageNumber);
            }
        }

        public override void ProcessMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.ProcessMouseRightButtonDown(e);
            PdfThumbnailsViewerControl actualDocumentViewer = base.presenter.ActualDocumentViewer as PdfThumbnailsViewerControl;
            Point position = e.GetPosition(base.presenter);
            Func<double, double> getPageHorizontalOffsetHandler = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<double, double> local1 = <>c.<>9__6_0;
                getPageHorizontalOffsetHandler = <>c.<>9__6_0 = width => 0.0;
            }
            int num = base.presenter.NavigationStrategy.PositionCalculator.GetPageIndex(position.Y + base.presenter.ScrollViewer.VerticalOffset, position.X + base.presenter.ScrollViewer.HorizontalOffset, getPageHorizontalOffsetHandler);
            if (num != -1)
            {
                int item = num + 1;
                if (!actualDocumentViewer.HighlightedPageNumbers.Contains(item))
                {
                    actualDocumentViewer.HighlightedPageNumbers.Clear();
                    actualDocumentViewer.HighlightedPageNumbers.Add(item);
                }
            }
        }

        private int SelectedPageNumber { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsKeyboardAndMouseController.<>c <>9 = new ThumbnailsKeyboardAndMouseController.<>c();
            public static Func<double, double> <>9__5_0;
            public static Func<double, double> <>9__6_0;

            internal double <ProcessMouseLeftButtonUp>b__5_0(double width) => 
                0.0;

            internal double <ProcessMouseRightButtonDown>b__6_0(double width) => 
                0.0;
        }
    }
}

