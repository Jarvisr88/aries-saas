namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutInfo
    {
        public double GetNormalValue(double comparable)
        {
            double num = this.ComparableEnd - this.ComparableStart;
            return ((comparable - this.ComparableStart) / num);
        }

        public double ComparableStart { get; set; }

        public double ComparableEnd { get; set; }

        public double ComparableSelectionStart { get; set; }

        public double ComparableSelectionEnd { get; set; }

        public double ComparableVisibleStart { get; set; }

        public double ComparableVisibleEnd { get; set; }

        public double ComparableRenderVisibleStart { get; set; }

        public double ComparableRenderVisibleEnd { get; set; }
    }
}

