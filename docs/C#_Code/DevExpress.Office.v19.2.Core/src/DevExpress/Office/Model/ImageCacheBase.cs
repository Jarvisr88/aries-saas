namespace DevExpress.Office.Model
{
    using DevExpress.Office;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class ImageCacheBase : IEnumerable<KeyValuePair<int, OfficeNativeImage>>, IEnumerable
    {
        private readonly IDocumentModel owner;
        private int nextImageId = 1;

        protected ImageCacheBase(IDocumentModel owner)
        {
            Guard.ArgumentNotNull(owner, "owner");
            this.owner = owner;
        }

        public OfficeReferenceImage AddImage(OfficeNativeImage image)
        {
            this.RegisterImage(image);
            return new OfficeReferenceImage(this.owner, image);
        }

        protected abstract void AddImage(OfficeNativeImage image, int key);
        public virtual void Clear()
        {
            this.nextImageId = 1;
        }

        public IEnumerator<KeyValuePair<int, OfficeNativeImage>> GetEnumerator() => 
            this.GetEnumeratorByIds();

        public abstract IEnumerator<KeyValuePair<int, OfficeNativeImage>> GetEnumeratorByIds();
        public OfficeReferenceImage GetImageById(IUniqueImageId id)
        {
            OfficeNativeImage nativeImageById = this.GetNativeImageById(id);
            return ((nativeImageById == null) ? null : new OfficeReferenceImage(this.owner, nativeImageById));
        }

        public OfficeReferenceImage GetImageByKey(int imageId)
        {
            OfficeNativeImage nativeImageByKey = this.GetNativeImageByKey(imageId);
            return ((nativeImageByKey != null) ? new OfficeReferenceImage(this.owner, nativeImageByKey) : null);
        }

        protected abstract OfficeNativeImage GetNativeImageById(IUniqueImageId id);
        protected abstract OfficeNativeImage GetNativeImageByKey(int key);
        public void RegisterImage(OfficeNativeImage image)
        {
            if (image.ImageCacheKey <= 0)
            {
                OfficeImage nativeImageById = this.GetNativeImageById(image.Id);
                if (nativeImageById != null)
                {
                    image.SetImageCacheKey(nativeImageById.ImageCacheKey);
                }
                else
                {
                    image.SetImageCacheKey(this.nextImageId);
                    this.AddImage(image, this.nextImageId);
                    this.nextImageId++;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();
    }
}

