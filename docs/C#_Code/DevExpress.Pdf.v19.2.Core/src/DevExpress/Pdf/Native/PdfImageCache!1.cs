namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class PdfImageCache<TValue> : IDisposable where TValue: IDisposable
    {
        private readonly IDictionary<PdfImage, ImageCacheItem<TValue>> objectStorage;
        private readonly LinkedList<PdfImage> recentImages;
        private long capacity;
        private long size;

        protected PdfImageCache(long capacity)
        {
            this.objectStorage = new Dictionary<PdfImage, ImageCacheItem<TValue>>();
            this.recentImages = new LinkedList<PdfImage>();
            this.capacity = capacity;
        }

        public void Clear()
        {
            foreach (ImageCacheItem<TValue> item in this.objectStorage.Values)
            {
                item.Dispose();
            }
            this.objectStorage.Clear();
            this.recentImages.Clear();
            this.size = 0L;
        }

        protected abstract ImageCacheItem<TValue> CreateValue(PdfImage image, PdfImageParameters imageParameters);
        public void Dispose()
        {
            this.Clear();
        }

        public PdfImageCacheItem<TValue> GetImage(PdfImage image, PdfImageParameters imageParameters)
        {
            ImageCacheItem<TValue> item;
            PdfImageParameters actualSize = image.GetActualSize(imageParameters);
            if (this.objectStorage.TryGetValue(image, out item) && !this.ShouldReplaceImage(actualSize, item.Parameters))
            {
                this.recentImages.Remove(item.Node);
                this.recentImages.AddFirst(item.Node);
                return new PdfImageCacheItem<TValue>(item.Value, false);
            }
            ImageCacheItem<TValue> item2 = this.CreateValue(image, actualSize);
            if (item2 == null)
            {
                return null;
            }
            if ((this.Capacity != 0) && (item2.Size > this.Capacity))
            {
                return new PdfImageCacheItem<TValue>(item2.Value, true);
            }
            if (item != null)
            {
                this.RemoveItem(image, item);
            }
            item = item2;
            this.size += item.Size;
            this.objectStorage.Add(image, item);
            this.recentImages.AddFirst(item.Node);
            this.TrimCache();
            return new PdfImageCacheItem<TValue>(item.Value, false);
        }

        private void RemoveItem(PdfImage image, ImageCacheItem<TValue> value)
        {
            this.size -= value.Size;
            this.recentImages.Remove(value.Node);
            this.objectStorage.Remove(image);
            value.Dispose();
        }

        protected virtual bool ShouldReplaceImage(PdfImageParameters imageParameters, PdfImageParameters oldParameters) => 
            (imageParameters.Width > oldParameters.Width) || (imageParameters.Height > oldParameters.Height);

        private void TrimCache()
        {
            if (this.capacity != 0)
            {
                while ((this.size > this.Capacity) && (this.size > 0L))
                {
                    PdfImage image = this.recentImages.Last.Value;
                    this.RemoveItem(image, this.objectStorage[image]);
                }
            }
        }

        public long Capacity
        {
            get => 
                this.capacity;
            set
            {
                this.capacity = Math.Max(value, 0L);
                this.TrimCache();
            }
        }

        protected class ImageCacheItem : IDisposable
        {
            public ImageCacheItem(TValue value, PdfImage image, PdfImageParameters parameters, long size)
            {
                this.<Value>k__BackingField = value;
                this.<Parameters>k__BackingField = parameters;
                this.<Size>k__BackingField = size;
                this.<Node>k__BackingField = new LinkedListNode<PdfImage>(image);
            }

            public void Dispose()
            {
                this.Value.Dispose();
            }

            public TValue Value { get; }

            public LinkedListNode<PdfImage> Node { get; }

            public PdfImageParameters Parameters { get; }

            public long Size { get; }
        }

        public class PdfImageCacheItem : IDisposable
        {
            private readonly bool shouldDispose;

            public PdfImageCacheItem(TValue data, bool shouldDispose)
            {
                this.<Data>k__BackingField = data;
                this.shouldDispose = shouldDispose;
            }

            public void Dispose()
            {
                if (this.shouldDispose)
                {
                    this.Data.Dispose();
                }
            }

            public TValue Data { get; }
        }
    }
}

