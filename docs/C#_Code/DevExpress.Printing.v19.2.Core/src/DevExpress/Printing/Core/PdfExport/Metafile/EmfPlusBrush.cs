namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class EmfPlusBrush
    {
        public EmfPlusBrush(MetaReader reader)
        {
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            switch (reader.ReadUInt32())
            {
                case 0:
                    this.Brush = new SolidBrush(reader.ReadEmfPlusARGB());
                    return;

                case 1:
                    this.Brush = new HatchBrush((HatchStyle) reader.ReadUInt32(), reader.ReadEmfPlusARGB(), reader.ReadEmfPlusARGB());
                    return;

                case 3:
                    this.Brush = new EmfPlusPathGradientBrushData(reader).Brush;
                    return;

                case 4:
                    this.Brush = new EmfPlusLinearGradientBrushData(reader).Brush;
                    return;
            }
            throw new NotSupportedException();
        }

        public System.Drawing.Brush Brush { get; set; }
    }
}

