namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public class PreviewClickEventArgs : EventArgs
    {
        private readonly string elementTag;
        private readonly FrameworkElement element;

        public PreviewClickEventArgs(string elementTag, FrameworkElement element)
        {
            this.elementTag = elementTag;
            this.element = element;
        }

        public string ElementTag =>
            this.elementTag;

        public FrameworkElement Element =>
            this.element;
    }
}

