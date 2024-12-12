namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlsPictureData
    {
        public XlsPictureData(XlsImageFormat imageFormat, int widthInPixels, int heightInPixels, byte[] imageBytes, byte[] imageDigest)
        {
            Guard.ArgumentNotNull(imageBytes, "imageBytes");
            Guard.ArgumentNotNull(imageDigest, "imageDigest");
            this.ImageFormat = imageFormat;
            this.WidthInPixels = widthInPixels;
            this.HeightInPixels = heightInPixels;
            this.NumberOfReferences = 1;
            this.ImageBytes = imageBytes;
            this.ImageDigest = imageDigest;
        }

        public bool EqualsDigest(byte[] digest)
        {
            if (digest == null)
            {
                return false;
            }
            if (this.ImageDigest.Length != digest.Length)
            {
                return false;
            }
            for (int i = 0; i < this.ImageDigest.Length; i++)
            {
                if (this.ImageDigest[i] != digest[i])
                {
                    return false;
                }
            }
            return true;
        }

        public XlsImageFormat ImageFormat { get; private set; }

        public int WidthInPixels { get; private set; }

        public int HeightInPixels { get; private set; }

        public int NumberOfReferences { get; set; }

        public byte[] ImageBytes { get; private set; }

        public byte[] ImageDigest { get; private set; }
    }
}

