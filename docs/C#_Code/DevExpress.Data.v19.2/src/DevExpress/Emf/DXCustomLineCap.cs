namespace DevExpress.Emf
{
    using System;

    public class DXCustomLineCap
    {
        private readonly double width;
        private readonly double height;
        private readonly double widthScale;
        private readonly bool filled;

        public DXCustomLineCap(double width, double height, double widthScale, bool filled)
        {
            this.width = width;
            this.height = height;
            this.widthScale = widthScale;
            this.filled = filled;
        }

        public double Width =>
            this.width;

        public double Height =>
            this.height;

        public double WidthScale =>
            this.widthScale;

        public bool Filled =>
            this.filled;
    }
}

