namespace DevExpress.XtraEditors
{
    using System;
    using System.Runtime.CompilerServices;

    public class RulerInfoArgs
    {
        public RulerInfoArgs(object minValue, object maxValue, double rulerWidthInPixels);

        public object MinValue { get; private set; }

        public object MaxValue { get; private set; }

        public double RulerWidthInPixels { get; private set; }
    }
}

