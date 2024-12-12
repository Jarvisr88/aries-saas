namespace DevExpress.Data.Camera
{
    using System;
    using System.Runtime.InteropServices;

    [ComVisible(false)]
    internal class MediaTypes
    {
        public static readonly Guid Video;
        public static readonly Guid Interleaved;
        public static readonly Guid Audio;
        public static readonly Guid Text;
        public static readonly Guid Stream;

        static MediaTypes();
    }
}

