namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;

    public class LayoutTabControlSelectionChangedEventArgs : EventArgs
    {
        public LayoutTabControlSelectionChangedEventArgs(object oldContent, object newContent)
        {
            this.OldContent = oldContent;
            this.NewContent = newContent;
        }

        public object NewContent { get; private set; }

        public object OldContent { get; private set; }
    }
}

