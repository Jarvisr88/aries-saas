namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Runtime.CompilerServices;

    public class RotateAngleChangedEventArgs : EventArgs
    {
        public RotateAngleChangedEventArgs(double oldValue, double newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public double OldValue { get; private set; }

        public double NewValue { get; private set; }
    }
}

