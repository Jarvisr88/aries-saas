namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusLinearGradientBrushData
    {
        public EmfPlusLinearGradientBrushData(MetaReader reader)
        {
            BrushDataFlags flags = (BrushDataFlags) reader.ReadUInt32();
            WrapMode mode = (WrapMode) reader.ReadInt32();
            RectangleF rect = reader.ReadRectF();
            reader.ReadInt32();
            reader.ReadInt32();
            this.Brush = new LinearGradientBrush(rect, reader.ReadEmfPlusARGB(), reader.ReadEmfPlusARGB(), 0f);
            this.Brush.WrapMode = mode;
            EmfPlusLinearGradientBrushOptionalData data1 = new EmfPlusLinearGradientBrushOptionalData(reader, this.Brush, flags);
        }

        public LinearGradientBrush Brush { get; set; }
    }
}

