namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Xpf.Editors.Flyout;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CalculatorResult
    {
        public CalculatorResult()
        {
            this.State = CalculationState.Uninitialized;
        }

        public Rect Bounds =>
            new Rect(this.Location, this.Size);

        public Point Location { get; set; }

        public System.Windows.Size Size { get; set; }

        public FlyoutPlacement Placement { get; set; }

        public CalculationState State { get; set; }

        public Point IndicatorOffset { get; set; }
    }
}

