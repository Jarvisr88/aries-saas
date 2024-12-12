namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.NativeBricks;
    using System;

    [SerializationContext(typeof(StreamingSerializationContext))]
    public class StreamingSerializationRootObject : IXtraRootSerializationObject, ISerializationCacheProvider
    {
        private readonly ObjectCache styleCache;
        private readonly ImagesCache imageCache;
        private readonly IStoredIDProvider storedIdProvider;

        public StreamingSerializationRootObject()
        {
            this.styleCache = new ObjectCache();
            this.imageCache = new ImagesCache();
        }

        internal StreamingSerializationRootObject(IStoredIDProvider storedIdProvider)
        {
            this.styleCache = new ObjectCache();
            this.imageCache = new ImagesCache();
            this.storedIdProvider = storedIdProvider;
        }

        private void AddBrickObjectsToCache(Brick brick)
        {
            if (this.storedIdProvider != null)
            {
                this.storedIdProvider.SetNextID(brick);
            }
            if (!(brick is CheckBoxTextBrick))
            {
                foreach (Brick brick3 in brick.Bricks)
                {
                    this.AddBrickObjectsToCache(brick3);
                }
            }
            VisualBrick brick2 = brick as VisualBrick;
            if (brick2 != null)
            {
                this.styleCache.AddSerializationObject(brick2.Style);
            }
        }

        void IXtraRootSerializationObject.AfterSerialize()
        {
        }

        SerializationInfo IXtraRootSerializationObject.GetIndexByObject(string propertyName, object obj) => 
            (propertyName == "Style") ? this.styleCache.GetIndexByObject(obj) : ((propertyName == "ImageEntry") ? this.imageCache.GetIndexByObject(obj) : null);

        object IXtraRootSerializationObject.GetObjectByIndex(string propertyName, int index) => 
            (propertyName == "Style") ? this.styleCache.GetObjectByIndex(index) : ((propertyName == "ImageEntry") ? this.imageCache.GetObjectByIndex(index) : ExceptionHelper.ThrowInvalidOperationException<object>());

        void ISerializationCacheProvider.AddBrick(object obj, XtraItemEventArgs e)
        {
        }

        void ISerializationCacheProvider.AddImage(object obj, XtraItemEventArgs e)
        {
            this.imageCache.AddDeserializationObject(obj, e);
        }

        void ISerializationCacheProvider.AddStyle(object obj, XtraItemEventArgs e)
        {
            this.styleCache.AddDeserializationObject(obj, e);
        }

        public void PrepareSerialization(ContinuousExportInfo info)
        {
            info.ExecuteExport(new BrickCollector(this), null);
        }

        protected class BrickCollector : IBrickExportVisitor
        {
            private StreamingSerializationRootObject rootObject;

            public BrickCollector(StreamingSerializationRootObject rootObject)
            {
                this.rootObject = rootObject;
            }

            void IBrickExportVisitor.ExportBrick(double horizontalOffset, double verticalOffset, Brick brick)
            {
                this.rootObject.AddBrickObjectsToCache(brick);
            }
        }
    }
}

