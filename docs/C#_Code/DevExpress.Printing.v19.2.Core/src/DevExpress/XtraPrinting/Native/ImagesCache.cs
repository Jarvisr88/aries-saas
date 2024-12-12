namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;

    public class ImagesCache : IImagesContainer, IObjectContainer, IEnumerable<KeyValuePair<object, System.Drawing.Image>>, IEnumerable
    {
        private const int limit = 100;
        private Dictionary<object, System.Drawing.Image> cachedItems = new Dictionary<object, System.Drawing.Image>();
        private UniqueQueue queue = new UniqueQueue();

        public object Add(object key, System.Drawing.Image image)
        {
            if (image == null)
            {
                return null;
            }
            key ??= new DevExpress.XtraPrinting.Native.ImageInfo(image);
            this.cachedItems.Add(key, image);
            this.queue.Push(key);
            this.CheckCapacity();
            return key;
        }

        private void CheckCapacity()
        {
            while (this.cachedItems.Count > 100)
            {
                System.Drawing.Image image;
                object key = this.queue.Pop();
                this.cachedItems.TryGetValue(key, out image);
                this.cachedItems.Remove(key);
                this.OnRemoveImage(image);
            }
        }

        public void Clear()
        {
            this.cachedItems.Clear();
            this.queue.Clear();
        }

        public bool ContainsImage(object key) => 
            this.cachedItems.ContainsKey(key);

        public IEnumerator<KeyValuePair<object, System.Drawing.Image>> GetEnumerator() => 
            this.cachedItems.GetEnumerator();

        public System.Drawing.Image GetImage(System.Drawing.Image image) => 
            this.GetImage(null, image);

        public System.Drawing.Image GetImage(object key, System.Drawing.Image image)
        {
            System.Drawing.Image image2;
            if (image == null)
            {
                return null;
            }
            key ??= new DevExpress.XtraPrinting.Native.ImageInfo(image);
            if (!this.cachedItems.TryGetValue(key, out image2))
            {
                this.cachedItems[key] = image;
                this.queue.Push(key);
                this.CheckCapacity();
                return image;
            }
            if (!ReferenceEquals(image2, image))
            {
                image.Dispose();
            }
            this.queue.Push(key);
            return image2;
        }

        public System.Drawing.Image GetImageByKey(object key)
        {
            System.Drawing.Image image;
            if (key == null)
            {
                return null;
            }
            if (!this.cachedItems.TryGetValue(key, out image))
            {
                return null;
            }
            this.queue.Push(key);
            return image;
        }

        protected virtual void OnRemoveImage(System.Drawing.Image image)
        {
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.cachedItems.GetEnumerator();

        public int Count =>
            this.cachedItems.Count;
    }
}

