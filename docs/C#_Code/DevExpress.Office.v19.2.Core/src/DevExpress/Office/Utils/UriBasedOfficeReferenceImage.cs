namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class UriBasedOfficeReferenceImage : UriBasedOfficeImageBase
    {
        private readonly UriBasedOfficeImage baseImage;
        private bool suppressStorePlaceholder;

        public UriBasedOfficeReferenceImage(UriBasedOfficeImage baseImage, int pixelTargetWidth, int pixelTargetHeight) : base(pixelTargetWidth, pixelTargetHeight)
        {
            Guard.ArgumentNotNull(baseImage, "baseImage");
            this.Uri = baseImage.Uri;
            this.baseImage = baseImage;
            base.CreatePlaceHolder();
            this.baseImage.Loaded += new EventHandler(this.OnBaseImageLoaded);
        }

        protected override void CopyFrom(OfficeImage officeImage)
        {
            base.CopyFrom(officeImage);
            UriBasedOfficeImage image = officeImage as UriBasedOfficeImage;
            if (image != null)
            {
                this.SuppressStorePlaceholder = image.SuppressStorePlaceholder;
            }
        }

        protected override OfficeImage CreateClone(IDocumentModel targetModel) => 
            this.baseImage.Clone(targetModel);

        protected internal override void EnsureLoadComplete(TimeSpan timeout)
        {
            this.baseImage.EnsureLoadComplete(timeout);
        }

        protected internal override void LoadActualImageSynchronous()
        {
        }

        private void OnBaseImageLoaded(object sender, EventArgs e)
        {
            this.ReplaceInnerImage(this.baseImage.InnerImage.Clone(this.baseImage.DocumentModel));
            this.baseImage.Loaded -= new EventHandler(this.OnBaseImageLoaded);
        }

        public override bool IsLoaded =>
            this.baseImage.IsLoaded;

        protected internal override OfficeReferenceImage InnerImage =>
            this.IsLoaded ? this.baseImage.InnerImage : base.InnerImage;

        protected internal override IDocumentModel DocumentModel =>
            this.baseImage.DocumentModel;

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
    }
}

