namespace DevExpress.Xpf.Editors.RangeControl
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class NumericIntervalFactory : IntervalFactory
    {
        private const double DefaultMinLength = 5.0;

        protected bool Equals(NumericIntervalFactory other) => 
            this.Step.Equals(other.Step);

        public override bool Equals(object obj) => 
            (obj != null) ? (!ReferenceEquals(this, obj) ? (!(obj.GetType() != base.GetType()) ? this.Equals((NumericIntervalFactory) obj) : false) : true) : false;

        public override bool FormatText(object current, out string text, double fontSize, double length)
        {
            text = string.Empty;
            if (length < ((this.ActualMinLength * fontSize) * (1.0 + (Math.Log10((this.Step > 1.0) ? this.Step : (1.0 / this.Step)) / 5.0))))
            {
                return false;
            }
            string textFormat = this.TextFormat;
            string format = textFormat;
            if (textFormat == null)
            {
                string local1 = textFormat;
                format = this.DefaultTextFormat;
            }
            text = string.Format(format, current);
            return true;
        }

        public override int GetHashCode() => 
            this.Step.GetHashCode();

        public override object GetNextValue(object value) => 
            this.SnapInternal(Convert.ToDouble(value)) + this.Step;

        public override object Snap(object value) => 
            this.SnapInternal(Convert.ToDouble(value));

        private double SnapInternal(double value)
        {
            int num = (int) (value / this.Step);
            return (this.Step * num);
        }

        public double MinLength { get; set; }

        private string DefaultTextFormat =>
            "{0:n}";

        private double ActualMinLength =>
            this.MinLength.IsZero() ? 5.0 : this.MinLength;

        public double Step { get; set; }
    }
}

