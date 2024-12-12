namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public class ImageInfo
    {
        private System.Drawing.Image image;
        private long hashCode;

        public ImageInfo(System.Drawing.Image image);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public System.Drawing.Image Image { get; }
    }
}

