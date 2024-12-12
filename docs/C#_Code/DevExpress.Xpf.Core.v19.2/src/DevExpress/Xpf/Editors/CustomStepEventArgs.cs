namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class CustomStepEventArgs : EventArgs
    {
        public CustomStepEventArgs(bool isIncrement, bool isLarge, double originalValue)
        {
            this.IsLargeStep = isLarge;
            this.IsIncrement = isIncrement;
            this.OriginalValue = originalValue;
            this.Value = originalValue;
        }

        public bool Handled { get; set; }

        public bool IsLargeStep { get; private set; }

        public bool IsIncrement { get; private set; }

        public double OriginalValue { get; private set; }

        public double Value { get; set; }
    }
}

