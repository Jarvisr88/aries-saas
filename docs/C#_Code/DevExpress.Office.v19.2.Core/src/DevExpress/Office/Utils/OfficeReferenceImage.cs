namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class OfficeReferenceImage : OfficeReferenceImageBase<OfficeNativeImage>
    {
        private readonly OfficeNativeImage baseImage;
        private readonly IDocumentModel owner;

        public OfficeReferenceImage(IDocumentModel owner, OfficeNativeImage baseImage)
        {
            Guard.ArgumentNotNull(baseImage, "baseImage");
            this.baseImage = baseImage;
            this.owner = owner;
            this.Uri = baseImage.Uri;
        }

        public OfficeReferenceImage Clone(IDocumentModel targetModel) => 
            (OfficeReferenceImage) base.Clone(targetModel);

        protected override OfficeImage CreateClone(IDocumentModel targetModel)
        {
            if (ReferenceEquals(targetModel, this.owner) && (this.owner != null))
            {
                return new OfficeReferenceImage(this.owner, this.baseImage);
            }
            OfficeNativeImage imageById = targetModel.GetImageById(this.baseImage.Id) as OfficeNativeImage;
            if (imageById != null)
            {
                return new OfficeReferenceImage(this.owner, imageById);
            }
            OfficeNativeImage image = this.baseImage.Clone(targetModel);
            return targetModel.ImageCache.AddImage(image);
        }

        protected internal override OfficeNativeImage InnerImage =>
            this.baseImage;

        public override OfficeImage RootImage =>
            this.InnerImage.RootImage;

        public OfficeNativeImage NativeRootImage =>
            this.InnerImage;
    }
}

