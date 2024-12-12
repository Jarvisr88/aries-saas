namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusPath
    {
        public EmfPlusPath(MetaReader reader)
        {
            EmfPlusGraphicsVersion version = new EmfPlusGraphicsVersion(reader);
            uint num = reader.ReadUInt32();
            uint num2 = reader.ReadUInt16();
            bool compressed = (num2 & 0x4000) == 0x4000;
            bool flag2 = (num2 & 0x1000) == 0x1000;
            bool flag3 = (num2 & 0x800) == 0x800;
            reader.ReadUInt16();
            PointF[] pts = reader.ReadPoints((long) num, compressed);
            this.Path = new GraphicsPath(pts, reader.ReadBytes((int) num));
        }

        public GraphicsPath Path { get; set; }
    }
}

