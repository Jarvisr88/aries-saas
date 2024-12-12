namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    internal class EmfPlusBitmap
    {
        public EmfPlusBitmap(MetaReader reader)
        {
            int num3 = reader.ReadInt32();
            BitmapDataType type = (BitmapDataType) reader.ReadUInt32();
            byte[] bitmapData = reader.ReadToEnd();
            this.Bitmap = DIBHelper.CreateBitmap(bitmapData, reader.ReadInt32(), reader.ReadInt32(), (PixelFormat) reader.ReadUInt32());
        }

        public System.Drawing.Bitmap Bitmap { get; set; }
    }
}

