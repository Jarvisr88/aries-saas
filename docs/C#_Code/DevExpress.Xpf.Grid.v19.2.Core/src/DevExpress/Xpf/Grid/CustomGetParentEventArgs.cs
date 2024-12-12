namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Runtime.CompilerServices;

    public class CustomGetParentEventArgs : EventArgs
    {
        private readonly object child;

        public CustomGetParentEventArgs(object child)
        {
            this.child = child;
        }

        public object Parent { get; set; }

        public object Child =>
            this.child;

        public bool Handled { get; set; }
    }
}

