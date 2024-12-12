namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public static class EmptyImage
    {
        private static readonly Image image;

        static EmptyImage();

        public static Image Instance { get; }
    }
}

