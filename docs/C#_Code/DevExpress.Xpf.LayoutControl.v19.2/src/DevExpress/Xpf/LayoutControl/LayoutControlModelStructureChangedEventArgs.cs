namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class LayoutControlModelStructureChangedEventArgs : LayoutControlModelChangedEventArgs
    {
        public LayoutControlModelStructureChangedEventArgs(string changeDescription, FrameworkElement element)
        {
            base.ChangeDescription = changeDescription;
            this.Element = element;
        }

        public FrameworkElement Element { get; private set; }
    }
}

