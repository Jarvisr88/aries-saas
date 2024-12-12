namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusPathPointType
    {
        public EmfPlusPathPointType(MetaReader reader)
        {
            this.Type = (PathPointType) reader.ReadByte();
        }

        public PathPointType Type { get; set; }
    }
}

