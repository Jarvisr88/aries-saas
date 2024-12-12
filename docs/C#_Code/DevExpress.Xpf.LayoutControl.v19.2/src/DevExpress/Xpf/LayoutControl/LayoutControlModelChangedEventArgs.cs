namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class LayoutControlModelChangedEventArgs : EventArgs
    {
        protected LayoutControlModelChangedEventArgs()
        {
        }

        public string ChangeDescription { get; protected set; }
    }
}

