namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Services;
    using DevExpress.Office.Services.Implementation;
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class UriBasedOfficeImage : UriBasedOfficeImageBase
    {
        private readonly IDocumentModel documentModel;
        private readonly IUriStreamService service;
        private readonly IThreadSyncService threadSyncService;
        private readonly string[] uriList;
        private readonly bool originalAsyncImageLoading;
        private bool suppressStorePlaceholder;
        private EventHandler onLoaded;

        public event EventHandler Loaded
        {
            add
            {
                this.onLoaded += value;
            }
            remove
            {
                this.onLoaded -= value;
            }
        }

        public UriBasedOfficeImage(string uri, int pixelTargetWidth, int pixelTargetHeight, IDocumentModel documentModel, bool asyncImageLoading, bool suppressLoading = false) : this(textArray1, pixelTargetWidth, pixelTargetHeight, documentModel, asyncImageLoading, suppressLoading)
        {
            string[] textArray1 = new string[] { uri };
        }

        public UriBasedOfficeImage(string[] uriList, int pixelTargetWidth, int pixelTargetHeight, IDocumentModel documentModel, bool asyncImageLoading, bool suppressLoading = false) : base(pixelTargetWidth, pixelTargetHeight)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            Guard.ArgumentNotNull(uriList, "uriList");
            Guard.ArgumentPositive(uriList.Length, "uriList.Count");
            this.uriList = uriList;
            this.documentModel = documentModel;
            this.Uri = uriList[0];
            this.originalAsyncImageLoading = asyncImageLoading;
            base.CreatePlaceHolder();
            if (suppressLoading)
            {
                base.LoadFailed = suppressLoading;
            }
            else
            {
                this.threadSyncService = documentModel.GetService<IThreadSyncService>();
                if (this.threadSyncService == null)
                {
                    asyncImageLoading = false;
                }
                this.service = documentModel.GetService<IUriStreamService>();
                if (this.service != null)
                {
                    if (asyncImageLoading)
                    {
                        IThreadPoolService service = documentModel.GetService<IThreadPoolService>();
                        if (service != null)
                        {
                            service.QueueJob(new WaitCallback(this.LoadActualImage));
                        }
                        else
                        {
                            this.LoadActualImage(asyncImageLoading);
                        }
                    }
                    else
                    {
                        this.LoadActualImage(asyncImageLoading);
                    }
                }
            }
        }

        protected override OfficeImage CreateClone(IDocumentModel targetModel)
        {
            Guard.ArgumentNotNull(targetModel, "targetModel");
            return (!this.IsLoaded ? (!ReferenceEquals(targetModel, this.DocumentModel) ? ((OfficeImage) new UriBasedOfficeImage(this.Uri, base.PixelTargetWidth, base.PixelTargetHeight, targetModel, this.originalAsyncImageLoading, base.LoadFailed)) : ((OfficeImage) new UriBasedOfficeReferenceImage(this, base.PixelTargetWidth, base.PixelTargetHeight))) : ((OfficeImage) this.InnerImage.Clone(targetModel)));
        }

        protected internal virtual void LoadActualImage(object state)
        {
            int length = this.uriList.Length;
            for (int i = 0; i < length; i++)
            {
                this.Uri = this.uriList[i];
                Stream imageStream = this.service.GetStream(this.Uri);
                if (imageStream != null)
                {
                    if (imageStream.CanSeek)
                    {
                        imageStream.Seek(0L, SeekOrigin.Begin);
                    }
                    if (!((state as bool) ? ((bool) state) : true))
                    {
                        this.ReplaceInnerImage(imageStream);
                        return;
                    }
                    this.DocumentModel.UriBasedImageReplaceQueue.Add(this, imageStream);
                    this.threadSyncService.EnqueueInvokeInUIThread(() => this.ProcessRegisteredInnerImageReplacements());
                    return;
                }
                base.LoadFailed = true;
            }
        }

        protected internal override void LoadActualImageSynchronous()
        {
            UriBasedOfficeImage image = this;
            lock (image)
            {
                if ((!this.IsLoaded && !base.LoadFailed) && !this.DocumentModel.UriBasedImageReplaceQueue.ForceImageProcess(this))
                {
                    Stream imageStream = this.service.GetStream(this.Uri);
                    if (imageStream != null)
                    {
                        if (imageStream.CanSeek)
                        {
                            imageStream.Seek(0L, SeekOrigin.Begin);
                        }
                        this.ReplaceInnerImage(imageStream);
                    }
                }
            }
        }

        protected internal virtual void ProcessRegisteredInnerImageReplacements()
        {
            if (!this.DocumentModel.IsDisposed)
            {
                this.DocumentModel.UriBasedImageReplaceQueue.ProcessRegisteredImages();
            }
        }

        protected internal virtual void RaiseLoaded()
        {
            if (this.onLoaded != null)
            {
                this.onLoaded(this, EventArgs.Empty);
            }
        }

        protected internal override void ReplaceInnerImage(Stream imageStream)
        {
            UriBasedOfficeImage image = this;
            lock (image)
            {
                if (!this.IsLoaded)
                {
                    base.ReplaceInnerImage(imageStream);
                    if (this.IsLoaded)
                    {
                        this.RaiseLoaded();
                    }
                }
            }
        }

        protected internal bool SuppressStorePlaceholder
        {
            get => 
                this.suppressStorePlaceholder;
            set => 
                this.suppressStorePlaceholder = value;
        }

        protected internal override bool SuppressStore
        {
            get => 
                base.SuppressStore || (this.SuppressStorePlaceholder && !this.IsLoaded);
            set => 
                base.SuppressStore = value;
        }

        protected internal override IDocumentModel DocumentModel =>
            this.documentModel;
    }
}

