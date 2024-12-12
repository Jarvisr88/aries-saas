namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;

    public class PdfResourcesCacheCompositeKey : PdfResourcesCacheKey
    {
        private readonly Color color;
        private readonly int resolution;

        public PdfResourcesCacheCompositeKey(object obj, Color color, int resolution) : base(obj)
        {
            this.color = color;
            this.resolution = resolution;
        }

        public override bool Equals(object obj)
        {
            PdfResourcesCacheCompositeKey key = obj as PdfResourcesCacheCompositeKey;
            return ((key != null) && (base.Equals(obj) && ((key.color == this.color) && (key.resolution == this.resolution))));
        }

        public override int GetHashCode() => 
            (this.resolution.GetHashCode() ^ this.color.GetHashCode()) ^ base.GetHashCode();
    }
}

