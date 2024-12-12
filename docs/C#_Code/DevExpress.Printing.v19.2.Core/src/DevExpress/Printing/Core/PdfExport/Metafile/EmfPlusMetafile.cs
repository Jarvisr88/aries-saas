namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class EmfPlusMetafile
    {
        public EmfPlusMetafile(MetaReader reader)
        {
            this.Type = (MetafileDataType) reader.ReadUInt32();
            uint num = reader.ReadUInt32();
            if (this.Type == MetafileDataType.MetafileDataTypeWmfPlaceable)
            {
                reader.ReadBytes(0x18);
            }
            MemoryStream stream = new MemoryStream(reader.ReadBytes((int) num));
            this.Metafile = (System.Drawing.Imaging.Metafile) Image.FromStream(stream, true);
        }

        public MetafileDataType Type { get; set; }

        public System.Drawing.Imaging.Metafile Metafile { get; set; }
    }
}

