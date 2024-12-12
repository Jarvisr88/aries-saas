namespace DevExpress.XtraPrinting.Caching
{
    using DevExpress.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Native;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    [ToolboxBitmap(typeof(ResFinder), "Bitmaps256.CachedReportSource.bmp")]
    public class CachedReportSource : CachedReportSourceBase, IReport, IDocumentSource, ILink, IComponent, IDisposable, IServiceProvider, IExtensionsProvider
    {
        private IReportPrintTool printTool;
        private Invoker invoker;
        private CachedReportSource.Counter documentChangedCounter;

        event EventHandler<ParametersRequestEventArgs> IReport.ParametersRequestSubmit;

        public CachedReportSource();
        public CachedReportSource(IContainer components);
        public CachedReportSource(IReport report, DocumentStorage storage);
        public CachedReportSource(Type reportType, DocumentStorage storage);
        [CompilerGenerated, DebuggerHidden]
        private void <>n__0(ContentChangedEventArgs ea);
        [CompilerGenerated, DebuggerHidden]
        private void <>n__1(object sender, EventArgs e);
        public override Task CreateDocumentAsync();
        void IReport.ApplyExportOptions(ExportOptions destinationOptions);
        void IReport.ApplyPageSettings(XtraPageSettingsBase destinationSettings);
        void IReport.CollectParameters(IList<Parameter> list, Predicate<Parameter> condition);
        void IReport.InitializeDocumentCreation();
        void IReport.RaiseParametersRequestBeforeShow(IList<ParameterInfo> parametersInfo);
        void IReport.RaiseParametersRequestSubmit(IList<ParameterInfo> parametersInfo, bool createDocument);
        void IReport.RaiseParametersRequestValueChanged(IList<ParameterInfo> parametersInfo, ParameterInfo changedParameterInfo);
        void IReport.ReleasePrintingSystem();
        void IReport.UpdatePageSettings(XtraPageSettingsBase sourceSettings, string paperName);
        protected override void InstantiatePrintingSystem();
        protected override void OnAfterBuildPages();
        protected override void OnAfterCreateDocument();
        protected override void OnBeforeBuildPages();
        protected override void OnContentChanged(ContentChangedEventArgs ea);
        protected override void OnDocumentChanged();
        protected override void OnProgressReflectorChanged(int position);
        protected override void OnProgressReflectorIncremented(object sender, EventArgs e);
        protected override void ReleasePrintingSystemCore();

        EventHandlerList IReport.Events { get; }

        Func<PrintingSystemBase, PrintingDocument> IReport.InstantiateDocument { get; set; }

        Watermark IReport.Watermark { get; }

        bool IReport.IsDisposed { get; }

        bool IReport.ShowPreviewMarginLines { get; }

        IReportPrintTool IReport.PrintTool { get; set; }

        bool IReport.RequestParameters { get; }

        private class Counter
        {
            public Counter();
            public void Increment();
            public void Memorize();
            public void Reset();

            public int Count { get; set; }

            public int MemorizedCount { get; private set; }
        }
    }
}

