namespace DevExpress.Office.Utils
{
    using System;
    using System.Drawing;

    public class NativeImageChangedEventArgs : EventArgs
    {
        private readonly Size desiredImageSizeInTwips;

        public NativeImageChangedEventArgs(Size desiredImageSizeInTwips)
        {
            this.desiredImageSizeInTwips = desiredImageSizeInTwips;
        }

        public Size DesiredImageSizeInTwips =>
            this.desiredImageSizeInTwips;
    }
}

