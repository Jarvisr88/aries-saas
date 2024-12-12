namespace DevExpress.Xpf.Editors.RangeControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class LayoutChangedEventArgs : EventArgs
    {
        public LayoutChangedEventArgs(LayoutChangedType changeType, object start = null, object end = null)
        {
            this.Start = start;
            this.End = end;
            this.ChangeType = changeType;
        }

        public object Start { get; private set; }

        public object End { get; private set; }

        public LayoutChangedType ChangeType { get; private set; }
    }
}

