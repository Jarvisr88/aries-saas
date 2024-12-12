namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public abstract class ColorTransformBase
    {
        protected ColorTransformBase()
        {
        }

        protected double ApplyDefaultGamma(double normalRgb) => 
            (normalRgb >= 0.0) ? ((normalRgb > 0.0031308) ? ((normalRgb >= 1.0) ? 1.0 : ((1.055 * Math.Pow(normalRgb, 0.41666666666666669)) - 0.055)) : (normalRgb * 12.92)) : 0.0;

        protected Color ApplyDefaultGamma(Color color) => 
            DXColor.FromArgb(this.ToIntValue(this.ApplyDefaultGamma(this.ToDoubleValue(color.R))), this.ToIntValue(this.ApplyDefaultGamma(this.ToDoubleValue(color.G))), this.ToIntValue(this.ApplyDefaultGamma(this.ToDoubleValue(color.B))));

        protected double ApplyInverseDefaultGamma(double normalRgb) => 
            (normalRgb >= 0.0) ? ((normalRgb > 0.04045) ? ((normalRgb >= 1.0) ? 1.0 : Math.Pow((normalRgb + 0.055) / 1.055, 2.4)) : (normalRgb / 12.92)) : 0.0;

        protected Color ApplyInverseDefaultGamma(Color color) => 
            DXColor.FromArgb(this.ToIntValue(this.ApplyInverseDefaultGamma(this.ToDoubleValue(color.R))), this.ToIntValue(this.ApplyInverseDefaultGamma(this.ToDoubleValue(color.G))), this.ToIntValue(this.ApplyInverseDefaultGamma(this.ToDoubleValue(color.B))));

        public abstract Color ApplyTransform(Color color);
        public abstract ColorTransformBase Clone();
        protected int GetFixRGBValue(int rgb) => 
            (rgb < 0) ? 0 : ((rgb > 0xff) ? 0xff : rgb);

        protected double ToDoubleValue(int value) => 
            ((double) value) / 255.0;

        protected int ToIntValue(double value) => 
            this.GetFixRGBValue((int) Math.Round((double) (255.0 * value), 0));

        public abstract void Visit(IColorTransformVisitor visitor);
    }
}

