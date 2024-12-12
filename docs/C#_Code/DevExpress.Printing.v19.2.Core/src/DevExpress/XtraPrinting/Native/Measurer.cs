namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Drawing;

    public abstract class Measurer : IMeasurer, IDisposable
    {
        private Graphics graph;

        protected Measurer();
        public virtual void Dispose();
        public abstract RectangleF GetRegionBounds(Region rgn, GraphicsUnit pageUnit);
        public abstract Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat, GraphicsUnit pageUnit);
        public SizeF MeasureString(string text, Font font, GraphicsUnit pageUnit);
        public abstract SizeF MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit pageUnit);
        public abstract SizeF MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit pageUnit);
        public SizeF MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit pageUnit);
        public SizeF MeasureStringI(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit pageUnit);

        protected Graphics Graph { get; }
    }
}

