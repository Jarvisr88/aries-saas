namespace DevExpress.Office.Model
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Office.History;
    using DevExpress.Office.Internal;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Utils;
    using DevExpress.Services.Internal;
    using DevExpress.Utils;
    using DevExpress.Utils.Zip;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.IO;
    using System.Text;

    public abstract class DocumentModelBase<TFormat> : DpiSupport, IDocumentModel, IServiceProvider, IOfficeServiceContainer, IDisposable, IBatchUpdateable, IBatchUpdateHandler, IServiceContainer
    {
        private static readonly float dpiX;
        private static readonly float dpiY;
        private static readonly float dpi;
        private bool isDisposed;
        private DevExpress.Utils.BatchUpdateHelper batchUpdateHelper;
        private DevExpress.Services.Internal.ServiceManager serviceManager;
        private DocumentHistory history;
        private DocumentModelUnitConverter unitConverter;
        private DocumentModelUnitToLayoutUnitConverter toDocumentLayoutUnitConverter;
        private DocumentLayoutUnitConverter layoutUnitConverter;
        private DocumentLayoutUnit layoutUnit;
        private DevExpress.Office.Drawing.FontCacheManager fontCacheManager;
        private DevExpress.Office.Drawing.FontCache fontCache;
        private ImageCacheBase imageCache;
        private IDrawingCache drawingCache;
        private DevExpress.Office.Utils.UriBasedImageReplaceQueue uriBasedImageReplaceQueue;
        private IOfficeTheme officeTheme;
        private EventHandler layoutUnitChanged;
        private EventHandler onThemeChanged;

        public event EventHandler LayoutUnitChanged
        {
            add
            {
                this.layoutUnitChanged += value;
            }
            remove
            {
                this.layoutUnitChanged -= value;
            }
        }

        public event EventHandler ThemeChanged
        {
            add
            {
                this.onThemeChanged += value;
            }
            remove
            {
                this.onThemeChanged -= value;
            }
        }

        static DocumentModelBase()
        {
            DocumentModelBase<TFormat>.dpiX = GraphicsDpi.Pixel;
            DocumentModelBase<TFormat>.dpiY = GraphicsDpi.Pixel;
            DocumentModelBase<TFormat>.dpi = DocumentModelBase<TFormat>.DpiX;
        }

        protected DocumentModelBase() : this(DocumentModelBase<TFormat>.DpiX, DocumentModelBase<TFormat>.DpiY)
        {
            this.layoutUnit = this.GetDefaultLayoutUnit();
        }

        protected DocumentModelBase(float screenDpiX, float screenDpiY) : base(screenDpiX, screenDpiY)
        {
            this.layoutUnit = this.GetDefaultLayoutUnit();
            this.batchUpdateHelper = new DevExpress.Utils.BatchUpdateHelper(this);
            this.serviceManager = this.CreateServiceManager();
            this.unitConverter = this.CreateDocumentModelUnitConverter();
            this.uriBasedImageReplaceQueue = new DevExpress.Office.Utils.UriBasedImageReplaceQueue(this);
            this.imageCache = this.CreateImageCache();
            this.UpdateLayoutUnitConverter();
            this.CreateOfficeTheme();
            this.drawingCache = new DrawingCache<TFormat>((DocumentModelBase<TFormat>) this);
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            if (this.serviceManager != null)
            {
                this.serviceManager.AddService(serviceType, callback);
            }
        }

        public void AddService(Type serviceType, object serviceInstance)
        {
            if (this.serviceManager != null)
            {
                this.serviceManager.AddService(serviceType, serviceInstance);
            }
        }

        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            if (this.serviceManager != null)
            {
                this.serviceManager.AddService(serviceType, callback, promote);
            }
        }

        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            if (this.serviceManager != null)
            {
                this.serviceManager.AddService(serviceType, serviceInstance, promote);
            }
        }

        private void ApplyTheme(IOfficeTheme officeTheme)
        {
            officeTheme ??= OfficeThemeBuilder<TFormat>.CreateTheme(OfficeThemePreset.Office);
            if (this.officeTheme != null)
            {
                this.officeTheme.Dispose();
            }
            this.officeTheme = officeTheme;
            this.RaiseThemeChanged();
        }

        public virtual TFormat AutodetectDocumentFormat(string fileName) => 
            this.AutodetectDocumentFormat(fileName, true);

        public virtual TFormat AutodetectDocumentFormat(string fileName, bool useFormatFallback)
        {
            ImportHelper<TFormat, bool> helper = this.CreateDocumentImportHelper();
            IImportManagerService<TFormat, bool> importManagerService = this.GetImportManagerService();
            if (importManagerService == null)
            {
                return helper.UndefinedFormat;
            }
            IImporter<TFormat, bool> importer = helper.AutodetectImporter(fileName, importManagerService, useFormatFallback);
            return ((importer != null) ? importer.Format : helper.UndefinedFormat);
        }

        public void BeginUpdate()
        {
            this.batchUpdateHelper.BeginUpdate();
        }

        public void CancelUpdate()
        {
            this.batchUpdateHelper.CancelUpdate();
        }

        public virtual void ClearCore()
        {
            this.ClearFontCache();
            if (this.history != null)
            {
                this.UnsubscribeHistoryEvents();
                this.history.Dispose();
                this.history = null;
            }
        }

        protected internal virtual void ClearFontCache()
        {
            if (this.fontCache != null)
            {
                if (this.fontCacheManager != null)
                {
                    this.fontCacheManager.ReleaseFontCache(this.fontCache);
                }
                this.fontCache = null;
            }
        }

        public abstract ExportHelper<TFormat, bool> CreateDocumentExportHelper(TFormat documentFormat);
        protected internal virtual DocumentHistory CreateDocumentHistory() => 
            new DocumentHistory(this);

        protected internal abstract ImportHelper<TFormat, bool> CreateDocumentImportHelper();
        protected virtual DocumentModelUnitConverter CreateDocumentModelUnitConverter() => 
            new DocumentModelUnitTwipsConverter(base.ScreenDpiX, base.ScreenDpiY);

        protected virtual DocumentHistory CreateEmptyHistory() => 
            new EmptyHistory(this);

        public OfficeReferenceImage CreateImage(MemoryStreamBasedImage image) => 
            new OfficeReferenceImage(this, OfficeImage.CreateImage(image));

        public OfficeReferenceImage CreateImage(Image image) => 
            new OfficeReferenceImage(this, OfficeImage.CreateImage(image));

        public OfficeReferenceImage CreateImage(Stream stream)
        {
            int readCheckSum;
            stream.Seek(0L, SeekOrigin.Begin);
            using (Crc32Stream stream2 = new Crc32Stream(stream))
            {
                stream2.ReadToEnd();
                readCheckSum = stream2.ReadCheckSum;
            }
            Crc32ImageId id = new Crc32ImageId(readCheckSum);
            OfficeReferenceImage imageById = this.ImageCache.GetImageById(id);
            if (imageById != null)
            {
                return imageById;
            }
            stream.Seek(0L, SeekOrigin.Begin);
            OfficeNativeImage image = OfficeImage.CreateImage(stream, id);
            return this.ImageCache.AddImage(image);
        }

        protected virtual ImageCacheBase CreateImageCache() => 
            new DevExpress.Office.Model.ImageCache(this);

        protected virtual void CreateOfficeTheme()
        {
            this.officeTheme = OfficeThemeBuilder<TFormat>.CreateTheme(OfficeThemePreset.Office);
        }

        protected internal virtual DevExpress.Services.Internal.ServiceManager CreateServiceManager() => 
            new DevExpress.Services.Internal.ServiceManager();

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.isDisposed)
            {
                if (disposing)
                {
                    this.DisposeCore();
                }
                this.isDisposed = true;
            }
        }

        protected internal virtual void DisposeCore()
        {
            this.ClearCore();
            if (this.serviceManager != null)
            {
                this.serviceManager.Dispose();
                this.serviceManager = null;
            }
            if (this.officeTheme != null)
            {
                this.officeTheme.Dispose();
                this.officeTheme = null;
            }
            this.fontCacheManager = null;
        }

        protected internal virtual void DisposeHistory()
        {
            if (this.History != null)
            {
                this.UnsubscribeHistoryEvents();
                this.History.Dispose();
            }
        }

        public void EndUpdate()
        {
            this.batchUpdateHelper.EndUpdate();
        }

        protected virtual DocumentLayoutUnit GetDefaultLayoutUnit() => 
            DocumentModelBase<TFormat>.DefaultLayoutUnit;

        protected internal abstract IExportManagerService<TFormat, bool> GetExportManagerService();
        public OfficeImage GetImageById(IUniqueImageId id)
        {
            OfficeReferenceImage imageById = this.ImageCache.GetImageById(id);
            return imageById?.NativeRootImage;
        }

        protected internal abstract IImportManagerService<TFormat, bool> GetImportManagerService();
        public virtual T GetService<T>() where T: class => 
            ServiceUtils.GetService<T>(this);

        public virtual object GetService(Type serviceType) => 
            this.serviceManager?.GetService(serviceType);

        public virtual void LoadDocument(Stream stream, TFormat documentFormat, string sourceUri)
        {
            this.LoadDocument(stream, documentFormat, sourceUri, true);
        }

        public virtual void LoadDocument(Stream stream, TFormat documentFormat, string sourceUri, bool leaveOpen)
        {
            this.LoadDocument(stream, documentFormat, sourceUri, null, leaveOpen);
        }

        public virtual void LoadDocument(Stream stream, TFormat documentFormat, string sourceUri, Encoding encoding)
        {
            this.LoadDocument(stream, documentFormat, sourceUri, encoding, true);
        }

        public virtual void LoadDocument(Stream stream, TFormat documentFormat, string sourceUri, Encoding encoding, bool leaveOpen)
        {
            ImportHelper<TFormat, bool> helper = this.CreateDocumentImportHelper();
            IImportManagerService<TFormat, bool> importManagerService = this.GetImportManagerService();
            if (importManagerService == null)
            {
                helper.ThrowUnsupportedFormatException();
            }
            helper.Import(stream, documentFormat, sourceUri, importManagerService, encoding, leaveOpen);
        }

        public abstract void OnBeginUpdate();
        public abstract void OnCancelUpdate();
        public abstract void OnEndUpdate();
        public abstract void OnFirstBeginUpdate();
        protected internal abstract void OnHistoryModifiedChanged(object sender, EventArgs e);
        protected internal abstract void OnHistoryOperationCompleted(object sender, EventArgs e);
        public abstract void OnLastCancelUpdate();
        public abstract void OnLastEndUpdate();
        protected internal virtual void OnLayoutUnitChanged()
        {
            this.UpdateLayoutUnitConverter();
            this.UpdateFontCache();
            this.RaiseLayoutUnitChanged();
        }

        public virtual void PreprocessContentBeforeExport(TFormat format)
        {
        }

        private void RaiseLayoutUnitChanged()
        {
            if (this.layoutUnitChanged != null)
            {
                this.layoutUnitChanged(this, EventArgs.Empty);
            }
        }

        protected internal virtual void RaiseThemeChanged()
        {
            if (this.onThemeChanged != null)
            {
                this.onThemeChanged(this, EventArgs.Empty);
            }
        }

        public virtual void Redo()
        {
            this.History.Redo();
        }

        public void RemoveService(Type serviceType)
        {
            if (this.serviceManager != null)
            {
                this.serviceManager.RemoveService(serviceType);
            }
        }

        public void RemoveService(Type serviceType, bool promote)
        {
            if (this.serviceManager != null)
            {
                this.serviceManager.RemoveService(serviceType, promote);
            }
        }

        public virtual DocumentHistory ReplaceHistory(DocumentHistory newHistory)
        {
            DocumentHistory history = this.history;
            this.UnsubscribeHistoryEvents();
            this.history = newHistory;
            this.ResetMerging();
            this.SubscribeHistoryEvents();
            return history;
        }

        public virtual T ReplaceService<T>(T newService) where T: class => 
            ServiceUtils.ReplaceService<T>(this, newService);

        public virtual void ResetMerging()
        {
        }

        public virtual void SaveDocument(Stream stream, ExportParameters<TFormat, bool> parameters)
        {
            this.SaveDocumentCore(stream, parameters);
        }

        public virtual void SaveDocument(Stream stream, TFormat documentFormat, string targetUri)
        {
            this.SaveDocument(stream, documentFormat, targetUri, null);
        }

        protected internal virtual void SaveDocument(Stream stream, TFormat documentFormat, string targetUri, Encoding encoding)
        {
            this.SaveDocumentCore(stream, new ExportParameters<TFormat, bool>(documentFormat, targetUri, encoding));
        }

        private void SaveDocumentCore(Stream stream, ExportParameters<TFormat, bool> parameters)
        {
            using (ExportHelper<TFormat, bool> helper = this.CreateDocumentExportHelper(parameters.DocumentFormat))
            {
                IExportManagerService<TFormat, bool> exportManagerService = this.GetExportManagerService();
                if (exportManagerService == null)
                {
                    helper.ThrowUnsupportedFormatException();
                }
                parameters.ExportManagerService = exportManagerService;
                helper.Export(stream, parameters);
            }
        }

        public virtual void SetFontCacheManager(DevExpress.Office.Drawing.FontCacheManager fontCacheManager)
        {
            Guard.ArgumentNotNull(fontCacheManager, "fontCacheManager");
            this.ClearFontCache();
            this.fontCacheManager = fontCacheManager;
            this.fontCache = fontCacheManager.CreateFontCache();
        }

        protected internal virtual void SubscribeHistoryEvents()
        {
            this.History.OperationCompleted += new EventHandler(this.OnHistoryOperationCompleted);
            this.History.ModifiedChanged += new EventHandler(this.OnHistoryModifiedChanged);
        }

        public virtual void SwitchToEmptyHistory(bool disposeHistory)
        {
            if (disposeHistory)
            {
                this.DisposeHistory();
            }
            this.history = this.CreateEmptyHistory();
            this.SubscribeHistoryEvents();
        }

        public virtual void SwitchToNormalHistory(bool disposeHistory)
        {
            if (disposeHistory)
            {
                this.DisposeHistory();
            }
            this.history = this.CreateDocumentHistory();
            this.ResetMerging();
            this.SubscribeHistoryEvents();
        }

        public virtual void Undo()
        {
            this.History.Undo();
        }

        protected internal virtual void UnsubscribeHistoryEvents()
        {
            this.History.OperationCompleted -= new EventHandler(this.OnHistoryOperationCompleted);
            this.History.ModifiedChanged -= new EventHandler(this.OnHistoryModifiedChanged);
        }

        protected internal void UpdateFontCache()
        {
            if (this.FontCacheManager != null)
            {
                this.FontCacheManager = this.FontCacheManager.Clone(this.LayoutUnitConverter, this.AllowCjkCorrection, this.UseSystemFontQuality);
            }
            else
            {
                this.FontCacheManager = DevExpress.Office.Drawing.FontCacheManager.CreateDefault(this.LayoutUnitConverter, this.AllowCjkCorrection, this.UseSystemFontQuality);
            }
        }

        protected internal void UpdateLayoutUnitConverter()
        {
            this.toDocumentLayoutUnitConverter = this.unitConverter.CreateConverterToLayoutUnits(this.LayoutUnit);
            this.layoutUnitConverter = DocumentLayoutUnitConverter.Create(this.LayoutUnit, base.ScreenDpi);
        }

        public static float Dpi =>
            DocumentModelBase<TFormat>.dpi;

        public static float DpiX =>
            DocumentModelBase<TFormat>.dpiX;

        public static float DpiY =>
            DocumentModelBase<TFormat>.dpiY;

        public static DocumentLayoutUnit DefaultLayoutUnit =>
            DocumentLayoutUnit.Document;

        public bool IsDisposed =>
            this.isDisposed;

        public virtual int MaxFieldSwitchLength =>
            2;

        public virtual bool EnableFieldNames =>
            false;

        public virtual bool AllowCjkCorrection =>
            false;

        public virtual bool UseSystemFontQuality =>
            true;

        public DevExpress.Services.Internal.ServiceManager ServiceManager =>
            this.serviceManager;

        public DevExpress.Utils.BatchUpdateHelper BatchUpdateHelper =>
            this.batchUpdateHelper;

        public DocumentHistory History =>
            this.history;

        public abstract IDocumentModelPart MainPart { get; }

        public DocumentModelUnitConverter UnitConverter =>
            this.unitConverter;

        public virtual DocumentModelUnitToLayoutUnitConverter ToDocumentLayoutUnitConverter =>
            this.toDocumentLayoutUnitConverter;

        public virtual DocumentLayoutUnitConverter LayoutUnitConverter =>
            this.layoutUnitConverter;

        public virtual DocumentLayoutUnit LayoutUnit
        {
            get => 
                this.layoutUnit;
            set
            {
                if (this.layoutUnit != value)
                {
                    this.layoutUnit = value;
                    this.OnLayoutUnitChanged();
                }
            }
        }

        public virtual DevExpress.Office.Drawing.FontCache FontCache =>
            this.fontCache;

        public ImageCacheBase ImageCache =>
            this.imageCache;

        public IDrawingCache DrawingCache =>
            this.drawingCache;

        public virtual IOfficeTheme OfficeTheme
        {
            get => 
                this.officeTheme;
            set => 
                this.ApplyTheme(value);
        }

        public DevExpress.Office.Drawing.FontCacheManager FontCacheManager
        {
            get => 
                this.fontCacheManager;
            set
            {
                if (!ReferenceEquals(this.fontCacheManager, value))
                {
                    this.SetFontCacheManager(value);
                }
            }
        }

        public DevExpress.Office.Utils.UriBasedImageReplaceQueue UriBasedImageReplaceQueue =>
            this.uriBasedImageReplaceQueue;

        DevExpress.Utils.BatchUpdateHelper IBatchUpdateable.BatchUpdateHelper =>
            this.batchUpdateHelper;

        public bool IsUpdateLocked =>
            this.batchUpdateHelper.IsUpdateLocked;

        public bool IsUpdateLockedOrOverlapped =>
            this.batchUpdateHelper.IsUpdateLocked || this.batchUpdateHelper.OverlappedTransaction;

        public virtual bool IsNormalHistory =>
            !(this.history is EmptyHistory);
    }
}

