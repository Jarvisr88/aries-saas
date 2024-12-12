namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Runtime.CompilerServices;

    public class EmfPlusGraphicsVersion
    {
        public EmfPlusGraphicsVersion(MetaReader reader)
        {
            uint num = reader.ReadUInt32();
            this.MetafileSignature = (int) (num >> 12);
            this.GraphicsVersion = ((DevExpress.Printing.Core.PdfExport.Metafile.GraphicsVersion) num) & ((DevExpress.Printing.Core.PdfExport.Metafile.GraphicsVersion) 0xfff);
        }

        public int MetafileSignature { get; set; }

        public DevExpress.Printing.Core.PdfExport.Metafile.GraphicsVersion GraphicsVersion { get; set; }
    }
}

