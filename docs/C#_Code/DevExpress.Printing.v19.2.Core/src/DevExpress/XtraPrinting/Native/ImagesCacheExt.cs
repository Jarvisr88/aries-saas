namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Runtime.CompilerServices;

    internal static class ImagesCacheExt
    {
        public static void AddDeserializationObject(this ImagesCache imagesCache, object obj, XtraItemEventArgs e)
        {
            XtraPropertyInfo info = e.Item.ChildProperties["Index"];
            if (info != null)
            {
                ImageEntry entry = (ImageEntry) obj;
                string s = info.Value.ToString();
                imagesCache.Add(int.Parse(s), entry.Image);
            }
        }

        public static SerializationInfo GetIndexByObject(this ImagesCache imagesCache, object obj) => 
            null;

        public static object GetObjectByIndex(this ImagesCache imagesCache, int index) => 
            new ImageEntry(imagesCache.GetImageByKey(index));
    }
}

