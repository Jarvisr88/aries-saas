namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutControlSelectionChangedEventArgs : EventArgs
    {
        public LayoutControlSelectionChangedEventArgs(FrameworkElements selectedElements)
        {
            this.SelectedElements = selectedElements;
        }

        public FrameworkElements SelectedElements { get; private set; }
    }
}

