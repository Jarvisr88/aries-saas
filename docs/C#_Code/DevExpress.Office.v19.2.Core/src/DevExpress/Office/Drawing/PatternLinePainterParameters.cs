namespace DevExpress.Office.Drawing
{
    using System;

    public abstract class PatternLinePainterParameters
    {
        private float[] dotPattern;
        private float[] dashPattern;
        private float[] dashSmallGapPattern;
        private float[] dashDotPattern;
        private float[] dashDotDotPattern;
        private float[] longDashPattern;
        private float pixelPenWidth;
        private float pixelStep;

        protected PatternLinePainterParameters()
        {
        }

        protected float[] CreatePattern(float[] pattern, float dpiX)
        {
            int length = pattern.Length;
            float[] numArray = new float[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = this.PixelsToUnits(pattern[i], dpiX) / 5f;
            }
            return numArray;
        }

        public virtual void Initialize(float dpiX)
        {
            float[] pattern = new float[] { 10f, 10f };
            this.dotPattern = this.CreatePattern(pattern, dpiX);
            float[] singleArray2 = new float[] { 40f, 20f };
            this.dashPattern = this.CreatePattern(singleArray2, dpiX);
            float[] singleArray3 = new float[] { 40f, 10f };
            this.dashSmallGapPattern = this.CreatePattern(singleArray3, dpiX);
            float[] singleArray4 = new float[] { 40f, 13f, 13f, 13f };
            this.dashDotPattern = this.CreatePattern(singleArray4, dpiX);
            float[] singleArray5 = new float[] { 30f, 10f, 10f, 10f, 10f, 10f };
            this.dashDotDotPattern = this.CreatePattern(singleArray5, dpiX);
            float[] singleArray6 = new float[] { 80f, 40f };
            this.longDashPattern = this.CreatePattern(singleArray6, dpiX);
            this.pixelPenWidth = this.PixelsToUnits(1f, dpiX);
            this.pixelStep = this.PixelsToUnits(2f, dpiX);
        }

        protected internal abstract float PixelsToUnits(float value, float dpi);

        public virtual float[] DotPattern =>
            this.dotPattern;

        public virtual float[] DashPattern =>
            this.dashPattern;

        public virtual float[] DashSmallGapPattern =>
            this.dashSmallGapPattern;

        public virtual float[] DashDotPattern =>
            this.dashDotPattern;

        public virtual float[] DashDotDotPattern =>
            this.dashDotDotPattern;

        public virtual float[] LongDashPattern =>
            this.longDashPattern;

        public float PixelPenWidth =>
            this.pixelPenWidth;

        public float PixelStep =>
            this.pixelStep;
    }
}

