namespace DevExpress.Office.Utils
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public abstract class OfficeNativeImage : OfficeImage
    {
        private int imageCacheKey;
        private IUniqueImageId id;

        public OfficeNativeImage(IUniqueImageId id)
        {
            Guard.ArgumentNotNull(id, "id");
            this.id = id;
        }

        public OfficeNativeImage Clone(IDocumentModel targetModel) => 
            (OfficeNativeImage) base.Clone(targetModel);

        protected internal virtual void SetImageCacheKey(int imageCacheKey)
        {
            this.imageCacheKey = imageCacheKey;
        }

        protected internal IUniqueImageId Id =>
            this.id;

        public override int ImageCacheKey =>
            this.imageCacheKey;

        public override OfficeNativeImage EncapsulatedOfficeNativeImage =>
            this;
    }
}

