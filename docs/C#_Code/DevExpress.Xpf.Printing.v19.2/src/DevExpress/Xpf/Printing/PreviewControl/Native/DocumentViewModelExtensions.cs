namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.ReportServer.Printing;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.Xpf.Printing.PreviewControl.Native.Models;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Caching;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports;
    using System;
    using System.Runtime.CompilerServices;

    public static class DocumentViewModelExtensions
    {
        public static IBrickPaintService GetPaintService(this IDocumentViewModel document)
        {
            Func<DocumentViewModel, IBrickPaintService> evaluator = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<DocumentViewModel, IBrickPaintService> local1 = <>c.<>9__6_0;
                evaluator = <>c.<>9__6_0 = x => x.PaintService;
            }
            return (document as DocumentViewModel).With<DocumentViewModel, IBrickPaintService>(evaluator);
        }

        public static bool IsCachedReportSource(this IDocumentViewModel document)
        {
            Func<ReportDocumentViewModel, bool> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<ReportDocumentViewModel, bool> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = x => x.Report is CachedReportSource;
            }
            return (document as ReportDocumentViewModel).Return<ReportDocumentViewModel, bool>(evaluator, (<>c.<>9__4_1 ??= () => false));
        }

        public static bool IsLegacyLinkDocumentSource(this IDocumentViewModel document)
        {
            Func<DocumentViewModel, ILink> evaluator = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DocumentViewModel, ILink> local1 = <>c.<>9__3_0;
                evaluator = <>c.<>9__3_0 = x => x.Link;
            }
            return ((document as DocumentViewModel).With<DocumentViewModel, ILink>(evaluator) is LegacyPrintableComponentLink);
        }

        public static bool IsRemoteReportDocumentSource(this IDocumentViewModel document)
        {
            Func<ReportDocumentViewModel, IReport> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<ReportDocumentViewModel, IReport> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => x.Report;
            }
            return ((document as ReportDocumentViewModel).With<ReportDocumentViewModel, IReport>(evaluator) is RemoteDocumentSource);
        }

        public static bool IsReportDocumentSource(this IDocumentViewModel document) => 
            document is ReportDocumentViewModel;

        public static bool IsXpfLinkDocumentSource(this IDocumentViewModel document)
        {
            Func<DocumentViewModel, ILink> evaluator = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<DocumentViewModel, ILink> local1 = <>c.<>9__2_0;
                evaluator = <>c.<>9__2_0 = x => x.Link;
            }
            return ((document as DocumentViewModel).With<DocumentViewModel, ILink>(evaluator) is DevExpress.Xpf.Printing.LinkBase);
        }

        public static void SetCurrentPage(this IDocumentViewModel document, int pageIndex)
        {
            document.Do<IDocumentViewModel>(delegate (IDocumentViewModel x) {
                Action<PageViewModel> <>9__1;
                Action<PageViewModel> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<PageViewModel> local1 = <>9__1;
                    action = <>9__1 = p => p.IsSelected = p.PageIndex == pageIndex;
                }
                x.Pages.ForEach<PageViewModel>(action);
            });
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentViewModelExtensions.<>c <>9 = new DocumentViewModelExtensions.<>c();
            public static Func<ReportDocumentViewModel, IReport> <>9__1_0;
            public static Func<DocumentViewModel, ILink> <>9__2_0;
            public static Func<DocumentViewModel, ILink> <>9__3_0;
            public static Func<ReportDocumentViewModel, bool> <>9__4_0;
            public static Func<bool> <>9__4_1;
            public static Func<DocumentViewModel, IBrickPaintService> <>9__6_0;

            internal IBrickPaintService <GetPaintService>b__6_0(DocumentViewModel x) => 
                x.PaintService;

            internal bool <IsCachedReportSource>b__4_0(ReportDocumentViewModel x) => 
                x.Report is CachedReportSource;

            internal bool <IsCachedReportSource>b__4_1() => 
                false;

            internal ILink <IsLegacyLinkDocumentSource>b__3_0(DocumentViewModel x) => 
                x.Link;

            internal IReport <IsRemoteReportDocumentSource>b__1_0(ReportDocumentViewModel x) => 
                x.Report;

            internal ILink <IsXpfLinkDocumentSource>b__2_0(DocumentViewModel x) => 
                x.Link;
        }
    }
}

