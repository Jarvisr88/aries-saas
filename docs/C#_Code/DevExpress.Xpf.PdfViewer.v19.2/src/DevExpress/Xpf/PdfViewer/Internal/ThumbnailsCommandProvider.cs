namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class ThumbnailsCommandProvider : CommandProvider
    {
        protected override void InitializeElements()
        {
            base.InitializeElements();
            CommandButton button1 = new CommandButton();
            button1.Caption = PdfViewerLocalizer.GetString(PdfViewerStringId.ThumbnailsViewerPrintPagesCaption);
            button1.Command = new CommandWrapper(() => this.PrintPagesCommandInternal);
            this.PrintPagesCommand = button1;
            PdfThumbnailsZoomItem item1 = new PdfThumbnailsZoomItem();
            item1.Command = new CommandWrapper(() => this.ZoomTrackCommandInternal);
            item1.MinValue = 1.0;
            item1.MaxValue = 3.0;
            item1.SmallStep = 0.25;
            item1.LargeStep = 1.0;
            this.ZoomTrackCommand = item1;
        }

        public ICommand PrintPagesCommand { get; private set; }

        public ICommand ZoomTrackCommand { get; private set; }

        protected virtual ICommand PrintPagesCommandInternal
        {
            get
            {
                Func<PdfThumbnailsViewerControl, ICommand> evaluator = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<PdfThumbnailsViewerControl, ICommand> local1 = <>c.<>9__9_0;
                    evaluator = <>c.<>9__9_0 = x => x.PrintPagesCommand;
                }
                return this.ActualDocumentViewer.With<PdfThumbnailsViewerControl, ICommand>(evaluator);
            }
        }

        protected virtual ICommand ZoomTrackCommandInternal
        {
            get
            {
                Func<PdfThumbnailsViewerControl, ICommand> evaluator = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<PdfThumbnailsViewerControl, ICommand> local1 = <>c.<>9__11_0;
                    evaluator = <>c.<>9__11_0 = x => x.SetZoomFactorCommand;
                }
                return this.ActualDocumentViewer.With<PdfThumbnailsViewerControl, ICommand>(evaluator);
            }
        }

        protected override ICommand ZoomInCommandInternal
        {
            get
            {
                Func<PdfThumbnailsViewerControl, ICommand> evaluator = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<PdfThumbnailsViewerControl, ICommand> local1 = <>c.<>9__13_0;
                    evaluator = <>c.<>9__13_0 = x => x.ZoomInCommand;
                }
                return this.ActualDocumentViewer.With<PdfThumbnailsViewerControl, ICommand>(evaluator);
            }
        }

        protected override ICommand ZoomOutCommandInternal
        {
            get
            {
                Func<PdfThumbnailsViewerControl, ICommand> evaluator = <>c.<>9__15_0;
                if (<>c.<>9__15_0 == null)
                {
                    Func<PdfThumbnailsViewerControl, ICommand> local1 = <>c.<>9__15_0;
                    evaluator = <>c.<>9__15_0 = x => x.ZoomOutCommand;
                }
                return this.ActualDocumentViewer.With<PdfThumbnailsViewerControl, ICommand>(evaluator);
            }
        }

        private PdfThumbnailsViewerControl ActualDocumentViewer =>
            base.DocumentViewer as PdfThumbnailsViewerControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsCommandProvider.<>c <>9 = new ThumbnailsCommandProvider.<>c();
            public static Func<PdfThumbnailsViewerControl, ICommand> <>9__9_0;
            public static Func<PdfThumbnailsViewerControl, ICommand> <>9__11_0;
            public static Func<PdfThumbnailsViewerControl, ICommand> <>9__13_0;
            public static Func<PdfThumbnailsViewerControl, ICommand> <>9__15_0;

            internal ICommand <get_PrintPagesCommandInternal>b__9_0(PdfThumbnailsViewerControl x) => 
                x.PrintPagesCommand;

            internal ICommand <get_ZoomInCommandInternal>b__13_0(PdfThumbnailsViewerControl x) => 
                x.ZoomInCommand;

            internal ICommand <get_ZoomOutCommandInternal>b__15_0(PdfThumbnailsViewerControl x) => 
                x.ZoomOutCommand;

            internal ICommand <get_ZoomTrackCommandInternal>b__11_0(PdfThumbnailsViewerControl x) => 
                x.SetZoomFactorCommand;
        }
    }
}

