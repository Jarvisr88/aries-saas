namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class WmfLogBrush
    {
        public void Read(MetaReader reader)
        {
            Color color = reader.ReadColorRGB();
            HatchStyle hatchstyle = (HatchStyle) reader.ReadInt16();
            switch (reader.ReadUInt16())
            {
                case 0:
                    this.Brush = new SolidBrush(color);
                    return;

                case 1:
                    this.Brush = new NullBrush();
                    return;

                case 2:
                    this.Brush = new HatchBrush(hatchstyle, color);
                    break;

                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    break;

                default:
                    return;
            }
        }

        public System.Drawing.Brush Brush { get; set; }
    }
}

