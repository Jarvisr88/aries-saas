namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class BaseDragDropStrategy
    {
        private readonly DragDropElementHelper helper;
        private readonly ISupportDragDrop supportDragDrop;

        public BaseDragDropStrategy(ISupportDragDrop supportDragDrop, DragDropElementHelper helper);
        public virtual BaseLocationStrategy CreateLocationStrategy();
        public virtual FrameworkElement GetDragElement();
        public virtual FrameworkElement GetSourceElement();
        public virtual FrameworkElement GetTopVisual(FrameworkElement node);
        public virtual void UpdateLocation(IndependentMouseEventArgs e);

        protected DragDropElementHelper Helper { get; }

        protected ISupportDragDrop SupportDragDrop { get; }

        protected FrameworkElement TopVisual { get; }

        public virtual FrameworkElement SubscribedElement { get; }
    }
}

