namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlPageMargins
    {
        private const double k = 2.54;
        private double[] margins = new double[] { 0.7, 0.7, 0.75, 0.75, 0.3, 0.3 };

        private double GetValue(int index) => 
            (this.PageUnits != XlPageUnits.Centimeters) ? this.margins[index] : (this.margins[index] * 2.54);

        private void SetValue(int index, double value, string valueName)
        {
            if (this.PageUnits == XlPageUnits.Centimeters)
            {
                if ((value < 0.0) || (value > 124.46000000000001))
                {
                    throw new ArgumentOutOfRangeException($"{valueName} margin out of range 0..124.46 centimeters");
                }
                this.margins[index] = value / 2.54;
            }
            else
            {
                if ((value < 0.0) || (value > 49.0))
                {
                    throw new ArgumentOutOfRangeException($"{valueName} margin out of range 0..49 inches");
                }
                this.margins[index] = value;
            }
        }

        public XlPageUnits PageUnits { get; set; }

        public double Left
        {
            get => 
                this.GetValue(0);
            set => 
                this.SetValue(0, value, "Left");
        }

        public double Right
        {
            get => 
                this.GetValue(1);
            set => 
                this.SetValue(1, value, "Right");
        }

        public double Top
        {
            get => 
                this.GetValue(2);
            set => 
                this.SetValue(2, value, "Top");
        }

        public double Bottom
        {
            get => 
                this.GetValue(3);
            set => 
                this.SetValue(3, value, "Bottom");
        }

        public double Header
        {
            get => 
                this.GetValue(4);
            set => 
                this.SetValue(4, value, "Header");
        }

        public double Footer
        {
            get => 
                this.GetValue(5);
            set => 
                this.SetValue(5, value, "Footer");
        }

        internal double LeftInches =>
            this.margins[0];

        internal double RightInches =>
            this.margins[1];

        internal double TopInches =>
            this.margins[2];

        internal double BottomInches =>
            this.margins[3];

        internal double HeaderInches =>
            this.margins[4];

        internal double FooterInches =>
            this.margins[5];
    }
}

