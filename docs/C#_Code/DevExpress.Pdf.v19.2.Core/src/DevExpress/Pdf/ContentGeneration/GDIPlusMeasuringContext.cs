namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;

    public class GDIPlusMeasuringContext : IDisposable
    {
        private readonly Graphics graphics = GraphicsHelper.CreateGraphics();
        private readonly StringFormat format;

        public GDIPlusMeasuringContext()
        {
            this.graphics.PageUnit = GraphicsUnit.Point;
            this.format = (StringFormat) StringFormat.GenericTypographic.Clone();
            this.format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
        }

        public void Dispose()
        {
            this.graphics.Dispose();
            this.format.Dispose();
        }

        public float GetCharWidth(char ch, Font font) => 
            this.graphics.MeasureString(ch.ToString(), font, new SizeF(2.147484E+09f, 999999f), this.format).Width;
    }
}

