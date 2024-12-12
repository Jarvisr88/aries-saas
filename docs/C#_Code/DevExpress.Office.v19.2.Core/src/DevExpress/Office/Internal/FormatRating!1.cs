namespace DevExpress.Office.Internal
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct FormatRating<TFormat>
    {
        private readonly TFormat documentFormat;
        private readonly int rate;
        public FormatRating(TFormat documentFormat, int rate)
        {
            this.documentFormat = documentFormat;
            this.rate = rate;
        }

        public TFormat DocumentFormat =>
            this.documentFormat;
        public int Rate =>
            this.rate;
    }
}

