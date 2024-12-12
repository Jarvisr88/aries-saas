namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.IO;

    public abstract class UriBasedOfficeImageBase : OfficeReferenceImageBase<OfficeReferenceImage>, IDesiredSizeSupport
    {
        private OfficeReferenceImage innerImage;
        private readonly int pixelTargetWidth;
        private readonly int pixelTargetHeight;
        private bool isLoaded;
        private bool loadFailed;

        protected UriBasedOfficeImageBase(int pixelTargetWidth, int pixelTargetHeight)
        {
            this.pixelTargetWidth = pixelTargetWidth;
            this.pixelTargetHeight = pixelTargetHeight;
        }

        private void AfterSetInnerImage()
        {
            this.RaiseNativeImageChanged(this.GetDesiredSizeInTwips());
        }

        protected void CreatePlaceHolder()
        {
            this.innerImage = CreatePlaceHolder(this.DocumentModel, this.pixelTargetWidth, this.pixelTargetHeight);
        }

        public static OfficeReferenceImage CreatePlaceHolder(IDocumentModel documentModel, int pixelTargetWidth, int pixelTargetHeight)
        {
            Image image = CommandResourceImageLoader.CreateBitmapFromResources("DevExpress.Office.Images.ImagePlaceHolder.png", typeof(UriBasedOfficeImageBase).Assembly);
            int width = pixelTargetWidth;
            if (width <= 0)
            {
                width = image.Width;
            }
            int height = pixelTargetHeight;
            if (height <= 0)
            {
                height = image.Height;
            }
            if ((width == image.Width) && (height == image.Height))
            {
                return ((documentModel == null) ? new OfficeReferenceImage(null, CreateImage(new MemoryStreamBasedImage(image, null))) : documentModel.CreateImage(new MemoryStreamBasedImage(image, null)));
            }
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(image, new Rectangle(Point.Empty, image.Size));
                graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, bitmap.Width - 1, bitmap.Height - 1));
            }
            return ((documentModel == null) ? new OfficeReferenceImage(null, CreateImage(new MemoryStreamBasedImage(bitmap, null))) : documentModel.CreateImage(new MemoryStreamBasedImage(bitmap, null)));
        }

        protected internal override void EnsureLoadComplete(TimeSpan timeout)
        {
            UriBasedOfficeImageBase base2 = this;
            lock (base2)
            {
                this.LoadActualImageSynchronous();
            }
        }

        private Size GetDesiredSizeInTwips()
        {
            DocumentModelUnitConverter unitConverter = this.DocumentModel.UnitConverter;
            return Units.PixelsToTwips(new Size(this.PixelTargetWidth, this.PixelTargetHeight), unitConverter.ScreenDpiX, unitConverter.ScreenDpiY);
        }

        protected internal abstract void LoadActualImageSynchronous();
        protected internal void RaiseEventsWhenImageLoaded()
        {
            this.RaiseNativeImageChanging();
            this.AfterSetInnerImage();
        }

        protected internal virtual void ReplaceInnerImage(OfficeReferenceImage image)
        {
            this.RaiseNativeImageChanging();
            this.innerImage.Dispose();
            this.innerImage = image;
            this.AfterSetInnerImage();
        }

        protected internal virtual void ReplaceInnerImage(Stream imageStream)
        {
            OfficeReferenceImage image;
            try
            {
                image = this.DocumentModel.CreateImage(imageStream);
            }
            catch
            {
                image = null;
            }
            if (image == null)
            {
                this.loadFailed = true;
            }
            else
            {
                this.isLoaded = true;
                image.Uri = this.Uri;
                this.ReplaceInnerImage(image);
            }
        }

        protected internal override OfficeReferenceImage InnerImage =>
            this.innerImage;

        public override bool IsLoaded =>
            this.isLoaded;

        public bool LoadFailed
        {
            get => 
                this.loadFailed;
            protected set => 
                this.loadFailed = value;
        }

        public int PixelTargetWidth =>
            this.pixelTargetWidth;

        public int PixelTargetHeight =>
            this.pixelTargetHeight;

        protected internal abstract IDocumentModel DocumentModel { get; }

        Size IDesiredSizeSupport.DesiredSize =>
            this.GetDesiredSizeInTwips();
    }
}

