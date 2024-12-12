namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class ImagesContainer : ObjectContainer<ImageSource>, IImagesContainer
    {
        private readonly List<IDisposable> storedObjects;
        private readonly Dictionary<object, ImageSource> persistentObjects;

        public ImagesContainer();
        public void Add(object key, ImageSource imageSource);
        public void Add(object key, Image image);
        private object AddInternal(object key, ImageSource imageSource);
        public void AddPersistent(object key, ImageSource imageSource);
        public void AddPersistent(object key, Image image);
        public override void Clear();
        public bool ContainsImage(object key);
        protected override bool EntryReferencesEquals(IDisposable entry1, IDisposable entry2);
        public Image GetImage(Image image);
        public Image GetImage(object key, Image image);
        public Image GetImageByKey(object key);
        public ImageSource GetImageSource(object key, ImageSource imageSource);
        public ImageSource GetImageSourceByKey(object key);
        public void ResetHash();
        public bool TryGetImageSourceByKey(object key, out ImageSource imageSource);
    }
}

