namespace DevExpress.Office.Model
{
    using DevExpress.Utils;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ScRGBColor
    {
        private static readonly ScRGBColor defaultValue;
        private int scR;
        private int scG;
        private int scB;
        public static ScRGBColor DefaultValue =>
            defaultValue;
        public static Color ToRgb(int scR, int scG, int scB)
        {
            ScRGBColor color = new ScRGBColor(scR, scG, scB);
            return color.ToRgb();
        }

        public ScRGBColor(int scR, int scG, int scB)
        {
            this.scR = scR;
            this.scG = scG;
            this.scB = scB;
        }

        public int ScR
        {
            get => 
                this.scR;
            set => 
                this.scR = this.GetValidValue(value);
        }
        public int ScG
        {
            get => 
                this.scG;
            set => 
                this.scG = this.GetValidValue(value);
        }
        public int ScB
        {
            get => 
                this.scB;
            set => 
                this.scB = this.GetValidValue(value);
        }
        private int GetValidValue(int value) => 
            (value < 0) ? 0 : value;

        public Color ToRgb()
        {
            double x = (this.scR * 1f) / 100000f;
            double num2 = (this.scG * 1f) / 100000f;
            double num3 = (this.scB * 1f) / 100000f;
            x = (x <= 0.0031308) ? (12.92 * x) : ((1.055 * Math.Pow(x, 0.41666666666666669)) - 0.055);
            num2 = (num2 <= 0.0031308) ? (12.92 * num2) : ((1.055 * Math.Pow(num2, 0.41666666666666669)) - 0.055);
            num2 *= 255.0;
            num3 = ((num3 <= 0.0031308) ? (12.92 * num3) : ((1.055 * Math.Pow(num3, 0.41666666666666669)) - 0.055)) * 255.0;
            return DXColor.FromArgb(Convert.ToInt32((double) (x * 255.0)), Convert.ToInt32(num2), Convert.ToInt32(num3));
        }

        static ScRGBColor()
        {
            defaultValue = new ScRGBColor(0, 0, 0);
        }
    }
}

