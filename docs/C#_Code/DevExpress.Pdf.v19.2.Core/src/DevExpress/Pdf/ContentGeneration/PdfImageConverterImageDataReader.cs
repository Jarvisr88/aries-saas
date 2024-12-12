namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Data.Helpers;
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public abstract class PdfImageConverterImageDataReader : PdfDisposableObject
    {
        protected PdfImageConverterImageDataReader()
        {
        }

        public static PdfImageConverterImageDataReader Create(Bitmap bitmap, int componentsCount)
        {
            PdfImageConverterImageDataReader reader;
            try
            {
                if (SecurityHelper.IsUnmanagedCodeGrantedAndHasZeroHwnd && !AzureCompatibility.Enable)
                {
                    return new PdfImageConverterBitmapDataReader(bitmap, componentsCount);
                }
            }
            catch
            {
            }
            try
            {
                reader = new PdfImageConverterStreamReader(bitmap, componentsCount);
            }
            catch
            {
                using (Bitmap bitmap2 = new Bitmap(bitmap))
                {
                    reader = new PdfImageConverterStreamReader(bitmap2, componentsCount);
                }
            }
            return reader;
        }

        public abstract int ReadNextRow(byte[] buffer, int count);
    }
}

