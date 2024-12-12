namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Pdf.Drawing;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.PdfViewer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ThumbnailsDocumentViewModel : IDocument, IThumbnailsDocument
    {
        public ThumbnailsDocumentViewModel()
        {
            this.Pages = Enumerable.Empty<IPage>();
        }

        public void Initialize(IDocument doc)
        {
            this.Document = doc as PdfDocumentViewModel;
            if (this.Document == null)
            {
                this.Pages = (IEnumerable<IPage>) Enumerable.Empty<ThumbnailPageViewModel>();
            }
            else
            {
                Func<PdfPageViewModel, ThumbnailPageViewModel> selector = <>c.<>9__21_0;
                if (<>c.<>9__21_0 == null)
                {
                    Func<PdfPageViewModel, ThumbnailPageViewModel> local1 = <>c.<>9__21_0;
                    selector = <>c.<>9__21_0 = x => new ThumbnailPageViewModel(x.Page, x.PageIndex);
                }
                this.Pages = (IEnumerable<IPage>) doc.Pages.Cast<PdfPageViewModel>().Select<PdfPageViewModel, ThumbnailPageViewModel>(selector).ToList<ThumbnailPageViewModel>();
                foreach (ThumbnailPageViewModel page in this.Pages)
                {
                    page.IsSelected = page.PageIndex == this.CurrentPageNumber;
                    Func<bool> fallback = <>c.<>9__21_2;
                    if (<>c.<>9__21_2 == null)
                    {
                        Func<bool> local2 = <>c.<>9__21_2;
                        fallback = <>c.<>9__21_2 = () => false;
                    }
                    page.IsHighlighted = this.HighlightedPages.Return<IEnumerable<int>, bool>(x => x.Contains<int>(page.PageIndex + 1), fallback);
                }
            }
        }

        internal void SetCurrentPage(int index)
        {
            this.CurrentPageNumber = index;
            Func<IEnumerable<IPage>, bool> evaluator = <>c.<>9__22_0;
            if (<>c.<>9__22_0 == null)
            {
                Func<IEnumerable<IPage>, bool> local1 = <>c.<>9__22_0;
                evaluator = <>c.<>9__22_0 = x => x.Any<IPage>();
            }
            if (this.Pages.Return<IEnumerable<IPage>, bool>(evaluator, <>c.<>9__22_1 ??= () => false))
            {
                foreach (ThumbnailPageViewModel model in this.Pages)
                {
                    model.IsSelected = model.PageIndex == index;
                }
            }
        }

        internal void SetHighlightedPages(IEnumerable<int> pages)
        {
            this.HighlightedPages = pages;
            Func<IEnumerable<IPage>, bool> evaluator = <>c.<>9__23_0;
            if (<>c.<>9__23_0 == null)
            {
                Func<IEnumerable<IPage>, bool> local1 = <>c.<>9__23_0;
                evaluator = <>c.<>9__23_0 = x => x.Any<IPage>();
            }
            if (this.Pages.Return<IEnumerable<IPage>, bool>(evaluator, <>c.<>9__23_1 ??= () => false))
            {
                foreach (ThumbnailPageViewModel model in this.Pages)
                {
                    model.IsHighlighted = pages.Contains<int>(model.PageIndex + 1);
                }
            }
        }

        private PdfDocumentViewModel Document { get; set; }

        private int CurrentPageNumber { get; set; }

        private IEnumerable<int> HighlightedPages { get; set; }

        public IEnumerable<IPage> Pages { get; private set; }

        internal PdfDocumentState DocumentState
        {
            get
            {
                Func<PdfDocumentViewModel, PdfDocumentState> evaluator = <>c.<>9__17_0;
                if (<>c.<>9__17_0 == null)
                {
                    Func<PdfDocumentViewModel, PdfDocumentState> local1 = <>c.<>9__17_0;
                    evaluator = <>c.<>9__17_0 = x => x.DocumentState;
                }
                return this.Document.With<PdfDocumentViewModel, PdfDocumentState>(evaluator);
            }
        }

        public bool IsLoaded
        {
            get
            {
                Func<PdfDocumentViewModel, bool> evaluator = <>c.<>9__20_0;
                if (<>c.<>9__20_0 == null)
                {
                    Func<PdfDocumentViewModel, bool> local1 = <>c.<>9__20_0;
                    evaluator = <>c.<>9__20_0 = x => x.IsLoaded;
                }
                return this.Document.Return<PdfDocumentViewModel, bool>(evaluator, (<>c.<>9__20_1 ??= () => false));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsDocumentViewModel.<>c <>9 = new ThumbnailsDocumentViewModel.<>c();
            public static Func<PdfDocumentViewModel, PdfDocumentState> <>9__17_0;
            public static Func<PdfDocumentViewModel, bool> <>9__20_0;
            public static Func<bool> <>9__20_1;
            public static Func<PdfPageViewModel, ThumbnailPageViewModel> <>9__21_0;
            public static Func<bool> <>9__21_2;
            public static Func<IEnumerable<IPage>, bool> <>9__22_0;
            public static Func<bool> <>9__22_1;
            public static Func<IEnumerable<IPage>, bool> <>9__23_0;
            public static Func<bool> <>9__23_1;

            internal PdfDocumentState <get_DocumentState>b__17_0(PdfDocumentViewModel x) => 
                x.DocumentState;

            internal bool <get_IsLoaded>b__20_0(PdfDocumentViewModel x) => 
                x.IsLoaded;

            internal bool <get_IsLoaded>b__20_1() => 
                false;

            internal ThumbnailPageViewModel <Initialize>b__21_0(PdfPageViewModel x) => 
                new ThumbnailPageViewModel(x.Page, x.PageIndex);

            internal bool <Initialize>b__21_2() => 
                false;

            internal bool <SetCurrentPage>b__22_0(IEnumerable<IPage> x) => 
                x.Any<IPage>();

            internal bool <SetCurrentPage>b__22_1() => 
                false;

            internal bool <SetHighlightedPages>b__23_0(IEnumerable<IPage> x) => 
                x.Any<IPage>();

            internal bool <SetHighlightedPages>b__23_1() => 
                false;
        }
    }
}

