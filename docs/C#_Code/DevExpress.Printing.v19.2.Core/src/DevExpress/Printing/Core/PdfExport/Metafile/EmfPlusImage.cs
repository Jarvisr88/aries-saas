namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class EmfPlusImage
    {
        public EmfPlusImage(MetaReader reader)
        {
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            switch (reader.ReadUInt32())
            {
                case 0:
                    throw new NotSupportedException();

                case 1:
                    this.Image = new EmfPlusBitmap(reader).Bitmap;
                    return;

                case 2:
                    this.Image = new EmfPlusMetafile(reader).Metafile;
                    return;
            }
        }

        public System.Drawing.Image Image { get; set; }
    }
}

