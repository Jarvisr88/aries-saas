namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Windows;

    public class DragDropInfoVisualSource
    {
        private readonly FrameworkElement element;
        private readonly object originalSource;
        private readonly object owner;

        public DragDropInfoVisualSource(FrameworkElement element, object originalSource, object owner)
        {
            this.element = element;
            this.originalSource = originalSource;
            this.owner = owner;
        }

        public FrameworkElement Element =>
            this.element;

        public object OriginalSource =>
            this.originalSource;

        public object Owner =>
            this.owner;
    }
}

