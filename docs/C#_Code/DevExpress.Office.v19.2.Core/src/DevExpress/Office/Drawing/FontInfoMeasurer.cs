namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Layout;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public abstract class FontInfoMeasurer : IDisposable
    {
        private bool isDisposed;
        private readonly DocumentLayoutUnitConverter unitConverter;

        protected FontInfoMeasurer(DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
            this.Initialize();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            bool flag1 = disposing;
            this.isDisposed = true;
        }

        protected abstract void Initialize();
        public virtual int MeasureCharacterWidth(char character, FontInfo fontInfo) => 
            (int) Math.Ceiling((double) this.MeasureCharacterWidthF(character, fontInfo));

        public abstract float MeasureCharacterWidthF(char character, FontInfo fontInfo);
        public abstract float MeasureMaxDigitWidthF(FontInfo fontInfo);
        public virtual Size MeasureMultilineText(string text, FontInfo fontInfo, int availableWidth) => 
            Size.Empty;

        public abstract Size MeasureString(string text, FontInfo fontInfo);

        internal bool IsDisposed =>
            this.isDisposed;

        protected internal DocumentLayoutUnitConverter UnitConverter =>
            this.unitConverter;
    }
}

