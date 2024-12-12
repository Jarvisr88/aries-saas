namespace DevExpress.Office.Drawing
{
    using System;

    public abstract class ColorTransformValueBase : ColorTransformBase
    {
        private readonly int value;

        protected ColorTransformValueBase(int value)
        {
            this.value = value;
        }

        protected int ApplyRGBModulation(int rgb)
        {
            double modulation = ((double) this.Value) / 100000.0;
            return this.ApplyRGBModulationCore(rgb, modulation);
        }

        protected double ApplyRGBModulation(double normalRgb, double modulation) => 
            this.GetFixRGBNormalValue(normalRgb * modulation);

        protected int ApplyRGBModulationCore(int rgb, double modulation) => 
            base.ToIntValue(base.ApplyDefaultGamma(this.ApplyRGBModulation(base.ApplyInverseDefaultGamma(base.ToDoubleValue(rgb)), modulation)));

        protected int ApplyRGBOffset(int rgb)
        {
            double offset = ((double) this.Value) / 100000.0;
            return base.ToIntValue(base.ApplyDefaultGamma(this.ApplyRGBOffset(base.ApplyInverseDefaultGamma(base.ToDoubleValue(rgb)), offset)));
        }

        protected double ApplyRGBOffset(double normalRgb, double offset) => 
            this.GetFixRGBNormalValue(normalRgb + offset);

        protected double GetFixRGBNormalValue(double rgb) => 
            (rgb < 0.0) ? 0.0 : ((rgb > 1.0) ? 1.0 : rgb);

        protected int GetRGBFromValue()
        {
            double normalRgb = ((double) this.Value) / 100000.0;
            return base.ToIntValue(base.ApplyDefaultGamma(normalRgb));
        }

        public int Value =>
            this.value;
    }
}

