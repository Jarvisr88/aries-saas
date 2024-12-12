namespace DevExpress.Pdf.Native
{
    using System;

    public class JPXImageDecodeResult
    {
        private readonly byte[] imageData;
        private readonly int componentsCount;

        public JPXImageDecodeResult(byte[] imageData, int componentsCount)
        {
            this.imageData = imageData;
            this.componentsCount = componentsCount;
        }

        public byte[] ImageData =>
            this.imageData;

        public int ComponentsCount =>
            this.componentsCount;
    }
}

