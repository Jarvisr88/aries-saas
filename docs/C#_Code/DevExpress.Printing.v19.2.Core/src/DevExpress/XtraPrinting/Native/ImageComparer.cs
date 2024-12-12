namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Drawing;
    using System;
    using System.Drawing;

    public class ImageComparer
    {
        public static bool Equals(ImageSource x, ImageSource y);
        public static bool Equals(Image x, Image y);
        private static bool Equals(byte[] x, byte[] y);
    }
}

