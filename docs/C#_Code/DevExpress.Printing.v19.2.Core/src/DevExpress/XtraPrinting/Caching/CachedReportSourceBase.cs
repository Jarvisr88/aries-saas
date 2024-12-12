namespace DevExpress.XtraPrinting.Caching
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Caching;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    [ToolboxItem(false)]
    public class CachedReportSourceBase : Component, IDocumentSource, ILink, IExtensionsProvider, IServiceProvider, IDocumentModificationService
    {
        private static object locker;
        private const int stopBuildingWaitMilliseconds = 0x7d0;
        private object report;
        private IReport reportCore;
        private DocumentStorage storage;
        private PrintingSystemBase printingSystem;
        private PartiallyDeserializedDocument document;
        private System.Threading.Tasks.Task task;
        private IDictionary<string, string> extensions;
        private bool disposed;
        private CachedDocumentUpdater updater;

        public event EventHandler AfterBuildPages;

        public event EventHandler BeforeBuildPages;

        static CachedReportSourceBase();
        public CachedReportSourceBase();
        public CachedReportSourceBase(IReport report, DocumentStorage storage);
        public CachedReportSourceBase(Type reportType, DocumentStorage storage);
        protected void AddReportServices();
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method has become obsolete"), DXHelpExclude(true)]
        public void BeginUpdatePages();
        protected virtual void ClearStorage(DocumentStorage storage);
        public void CreateDocument();
        public virtual System.Threading.Tasks.Task CreateDocumentAsync();
        private void CreateDocumentCore();
        protected System.Threading.Tasks.Task CreateDocumentCoreAsync();
        void ILink.CreateDocument(bool buildForInstantPreview);
        protected override void Dispose(bool disposing);
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This method has become obsolete"), DXHelpExclude(true)]
        public void EndUpdatePages();
        protected virtual IBuildTaskFactory GetBuildTaskFactory();
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the ModifyDocument method instead"), DXHelpExclude(true)]
        public void InsertPage(int index, Page page);
        protected void InstantiateDocument();
        protected virtual void InstantiatePrintingSystem();
        public void ModifyDocument(Action<IDocumentModifier> callback);
        protected virtual void OnAfterBuildPages();
        protected virtual void OnAfterCreateDocument();
        protected virtual void OnBeforeBuildPages();
        protected virtual void OnContentChanged(ContentChangedEventArgs ea);
        protected virtual void OnDocumentChanged();
        private void OnDocumentChanged(object s, EventArgs e);
        private void OnPageBuildingStopped(object sender, EventArgs e);
        private void OnPageSettingsChanged(object sender, EventArgs e);
        protected virtual void OnProgressReflectorChanged(int position);
        private void OnProgressReflectorChanged(object sender, EventArgs e);
        protected virtual void OnProgressReflectorIncremented(object sender, EventArgs e);
        private void OnScaleFactorChanged(object sender, EventArgs e);
        protected virtual void ReleasePrintingSystemCore();
        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the ModifyDocument method instead"), DXHelpExclude(true)]
        public void RemoveAtPage(int index);
        protected void ResetPrintingSystem();
        public void StopPageBuilding();
        object IServiceProvider.GetService(Type serviceType);
        private static bool TryCreateInstance(Type type, out IReport instance);

        private CachedDocumentUpdater Updater { get; }

        protected System.Threading.Tasks.Task Task { get; }

        protected IReport ReportCore { get; }

        protected PartiallyDeserializedDocument Document { get; }

        [DefaultValue((string) null), Category("Behavior")]
        public DocumentStorage Storage { get; set; }

        [TypeConverter("DevExpress.XtraPrinting.Design.DocumentSourceConvertor,DevExpress.XtraPrinting.v19.2.Design"), Editor("DevExpress.XtraPrinting.Design.ReportSourceEditor,DevExpress.XtraPrinting.v19.2.Design", typeof(UITypeEditor)), DefaultValue((string) null), Category("Behavior")]
        public object Report { get; set; }

        [DefaultValue(true), Category("Behavior")]
        public bool AllowSingleFileExport { get; set; }

        internal bool AllowMultiThreading { get; set; }

        [Browsable(false)]
        public PrintingSystemBase PrintingSystem { get; }

        [Browsable(false)]
        public PageList Pages { get; }

        IPrintingSystem ILink.PrintingSystem { get; }

        PrintingSystemBase IDocumentSource.PrintingSystemBase { get; }

        IDictionary<string, string> IExtensionsProvider.Extensions { get; }
    }
}

