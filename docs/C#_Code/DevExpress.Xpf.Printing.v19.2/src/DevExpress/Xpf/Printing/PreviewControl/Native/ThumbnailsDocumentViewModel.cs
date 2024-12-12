namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ThumbnailsDocumentViewModel : IDocument
    {
        private readonly ThumbnailPageCollection pages = new ThumbnailPageCollection();
        private bool synchronizationEnabled;

        internal ThumbnailsDocumentViewModel(IDocumentViewModel document)
        {
            this.OwnerDocument = (DocumentViewModel) document;
            this.AllowSynchronization(true);
        }

        internal void AllowSynchronization(bool enable)
        {
            if ((enable != this.synchronizationEnabled) && (this.OwnerDocument != null))
            {
                if (!enable)
                {
                    this.OwnerDocument.Pages.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnDocumentCollectionChanged);
                    this.pages.RemoveAll();
                    this.synchronizationEnabled = false;
                }
                else
                {
                    this.pages.RemoveAll();
                    Func<PageViewModel, ThumbnailPageViewModel> selector = <>c.<>9__19_0;
                    if (<>c.<>9__19_0 == null)
                    {
                        Func<PageViewModel, ThumbnailPageViewModel> local1 = <>c.<>9__19_0;
                        selector = <>c.<>9__19_0 = x => ThumbnailPageViewModel.Create(x.Page);
                    }
                    this.pages.AddRange(this.OwnerDocument.Pages.Select<PageViewModel, ThumbnailPageViewModel>(selector));
                    this.OwnerDocument.Pages.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnDocumentCollectionChanged);
                    this.OwnerDocument.Pages.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnDocumentCollectionChanged);
                    this.synchronizationEnabled = true;
                }
            }
        }

        private void OnDocumentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.pages.RemoveAll();
            Func<PageViewModel, bool> predicate = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<PageViewModel, bool> local1 = <>c.<>9__10_0;
                predicate = <>c.<>9__10_0 = x => x.PageList.Count != 0;
            }
            if (this.OwnerDocument.Pages.All<PageViewModel>(predicate))
            {
                Func<PageViewModel, ThumbnailPageViewModel> selector = <>c.<>9__10_1;
                if (<>c.<>9__10_1 == null)
                {
                    Func<PageViewModel, ThumbnailPageViewModel> local2 = <>c.<>9__10_1;
                    selector = <>c.<>9__10_1 = delegate (PageViewModel x) {
                        ThumbnailPageViewModel model = ThumbnailPageViewModel.Create(x.Page);
                        model.ForceInvalidate = true;
                        return model;
                    };
                }
                this.pages.AddRange(this.OwnerDocument.Pages.Select<PageViewModel, ThumbnailPageViewModel>(selector));
            }
        }

        public void SetCurrentPage(int pageIndex)
        {
            this.CurrentPageIndex = pageIndex;
            Func<ObservableCollection<ThumbnailPageViewModel>, bool> evaluator = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<ObservableCollection<ThumbnailPageViewModel>, bool> local1 = <>c.<>9__17_0;
                evaluator = <>c.<>9__17_0 = x => x.Any<ThumbnailPageViewModel>();
            }
            if (this.Pages.Return<ObservableCollection<ThumbnailPageViewModel>, bool>(evaluator, <>c.<>9__17_1 ??= () => false))
            {
                foreach (ThumbnailPageViewModel model in this.Pages)
                {
                    model.IsSelected = model.PageIndex == pageIndex;
                }
            }
        }

        internal DocumentViewModel OwnerDocument { get; private set; }

        private int CurrentPageIndex { get; set; }

        public bool IsLoaded
        {
            get
            {
                Func<DocumentViewModel, bool> evaluator = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<DocumentViewModel, bool> local1 = <>c.<>9__12_0;
                    evaluator = <>c.<>9__12_0 = x => x.IsLoaded;
                }
                return this.OwnerDocument.Return<DocumentViewModel, bool>(evaluator, (<>c.<>9__12_1 ??= () => false));
            }
        }

        IEnumerable<IPage> IDocument.Pages =>
            (IEnumerable<IPage>) this.Pages;

        public ObservableCollection<ThumbnailPageViewModel> Pages =>
            this.pages;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThumbnailsDocumentViewModel.<>c <>9 = new ThumbnailsDocumentViewModel.<>c();
            public static Func<PageViewModel, bool> <>9__10_0;
            public static Func<PageViewModel, ThumbnailPageViewModel> <>9__10_1;
            public static Func<DocumentViewModel, bool> <>9__12_0;
            public static Func<bool> <>9__12_1;
            public static Func<ObservableCollection<ThumbnailPageViewModel>, bool> <>9__17_0;
            public static Func<bool> <>9__17_1;
            public static Func<PageViewModel, ThumbnailPageViewModel> <>9__19_0;

            internal ThumbnailPageViewModel <AllowSynchronization>b__19_0(PageViewModel x) => 
                ThumbnailPageViewModel.Create(x.Page);

            internal bool <get_IsLoaded>b__12_0(DocumentViewModel x) => 
                x.IsLoaded;

            internal bool <get_IsLoaded>b__12_1() => 
                false;

            internal bool <OnDocumentCollectionChanged>b__10_0(PageViewModel x) => 
                x.PageList.Count != 0;

            internal ThumbnailPageViewModel <OnDocumentCollectionChanged>b__10_1(PageViewModel x)
            {
                ThumbnailPageViewModel model = ThumbnailPageViewModel.Create(x.Page);
                model.ForceInvalidate = true;
                return model;
            }

            internal bool <SetCurrentPage>b__17_0(ObservableCollection<ThumbnailPageViewModel> x) => 
                x.Any<ThumbnailPageViewModel>();

            internal bool <SetCurrentPage>b__17_1() => 
                false;
        }
    }
}

