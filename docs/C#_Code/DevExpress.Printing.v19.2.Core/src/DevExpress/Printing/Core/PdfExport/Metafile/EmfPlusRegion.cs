namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class EmfPlusRegion
    {
        public EmfPlusRegion(MetaReader reader)
        {
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            this.Region = new System.Drawing.Region();
            uint num = reader.ReadUInt32();
            this.Region = new EmfPlusRegionNode(reader).Region;
        }

        public System.Drawing.Region Region { get; set; }
    }
}

