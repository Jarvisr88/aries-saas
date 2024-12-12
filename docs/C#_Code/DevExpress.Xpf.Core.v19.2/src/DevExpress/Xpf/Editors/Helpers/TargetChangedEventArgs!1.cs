namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class TargetChangedEventArgs<T> : EventArgs
    {
        public TargetChangedEventArgs(T value)
        {
            this.Value = value;
        }

        public T Value { get; private set; }
    }
}

