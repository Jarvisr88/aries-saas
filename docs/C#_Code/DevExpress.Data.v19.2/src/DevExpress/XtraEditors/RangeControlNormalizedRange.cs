namespace DevExpress.XtraEditors
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class RangeControlNormalizedRange
    {
        private double minimum;
        private double maximum;

        protected virtual double ConstrainMaximum(double value);
        protected virtual double ConstrainMinimum(double value);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalSetMaximum(double value);
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalSetMinimum(double value);
        protected virtual void OnMaximumChanged();
        protected virtual void OnMinimumChanged();

        public IRangeControl Owner { get; set; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }
    }
}

