namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;

    internal static class SizeToContentHelper
    {
        public static Size FitSizeToContent(SizeToContent sizeToContent, Size prevSize, Size newSize)
        {
            Size size;
            switch (sizeToContent)
            {
                case SizeToContent.Width:
                    size = new Size(prevSize.Width, newSize.Height);
                    break;

                case SizeToContent.Height:
                    size = new Size(newSize.Width, prevSize.Height);
                    break;

                case SizeToContent.WidthAndHeight:
                    size = new Size(prevSize.Width, prevSize.Height);
                    break;

                default:
                    size = newSize;
                    break;
            }
            return size;
        }
    }
}

