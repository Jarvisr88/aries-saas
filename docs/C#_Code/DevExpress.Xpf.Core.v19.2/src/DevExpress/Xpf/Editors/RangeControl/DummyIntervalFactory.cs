namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Runtime.InteropServices;

    public class DummyIntervalFactory : IntervalFactory
    {
        public override bool FormatText(object current, out string text, double fontSize, double length)
        {
            text = string.Empty;
            return false;
        }

        public override object GetNextValue(object value) => 
            value;

        public override object Snap(object value) => 
            value;
    }
}

