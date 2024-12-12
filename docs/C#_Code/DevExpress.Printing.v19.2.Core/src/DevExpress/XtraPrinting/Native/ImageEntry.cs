namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class ImageEntry : IXtraSupportShouldSerialize, IDisposable
    {
        private DevExpress.XtraPrinting.Drawing.ImageSource imageSource;

        public ImageEntry();
        public ImageEntry(System.Drawing.Image image);
        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName);
        public void Dispose();
        private static DevExpress.XtraPrinting.Drawing.ImageSource HandleImage(DevExpress.XtraPrinting.Drawing.ImageSource value);

        [XtraSerializableProperty]
        public DevExpress.XtraPrinting.Drawing.ImageSource ImageSource { get; set; }

        [XtraSerializableProperty]
        public System.Drawing.Image Image { get; set; }

        public class ImageEntryEqualityComparer : IEqualityComparer<object>
        {
            public static readonly ImageEntry.ImageEntryEqualityComparer Instance;

            static ImageEntryEqualityComparer();
            private ImageEntryEqualityComparer();
            bool IEqualityComparer<object>.Equals(object x, object y);
            int IEqualityComparer<object>.GetHashCode(object obj);
        }
    }
}

