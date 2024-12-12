namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Windows;

    public abstract class DragDropInfoBase
    {
        private readonly DragDropInfoVisualSource source;

        public DragDropInfoBase(DragDropInfoVisualSource source)
        {
            this.source = source;
        }

        public FrameworkElement Element =>
            this.source.Element;

        public object OriginalSource =>
            this.source.OriginalSource;

        public object Owner =>
            this.source.Owner;
    }
}

