namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class DateTimeChangedEventArgs : EventArgs
    {
        public DateTimeChangedEventArgs(DateTime newValue)
        {
            this.Value = newValue;
        }

        public DateTime Value { get; private set; }
    }
}

