namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing.Drawing2D;

    public class EmfPlusLinearGradientBrushOptionalData
    {
        public EmfPlusLinearGradientBrushOptionalData(MetaReader reader, LinearGradientBrush brush, BrushDataFlags flags)
        {
            if (flags.HasFlag(BrushDataFlags.BrushDataTransform))
            {
                brush.Transform = reader.ReadMatrix();
            }
        }
    }
}

