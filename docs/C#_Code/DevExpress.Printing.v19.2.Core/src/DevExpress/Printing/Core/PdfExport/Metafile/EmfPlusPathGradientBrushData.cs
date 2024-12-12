namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusPathGradientBrushData
    {
        public EmfPlusPathGradientBrushData(MetaReader reader)
        {
            BrushDataFlags flags = (BrushDataFlags) reader.ReadUInt32();
            WrapMode mode = (WrapMode) reader.ReadInt32();
            Color color = reader.ReadEmfPlusARGB();
            this.Brush = new SolidBrush(color);
        }

        public System.Drawing.Brush Brush { get; set; }
    }
}

