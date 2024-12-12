namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class DragSubscriptionService
    {
        private readonly UIElement element;
        private readonly IDragEventListener listener;
        private bool isActiveCore;

        public DragSubscriptionService(IDragEventListener listener, UIElement element)
        {
            Guard.ArgumentNotNull(element, "element");
            Guard.ArgumentNotNull(listener, "listener");
            this.element = element;
            this.listener = listener;
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            this.listener.OnDragEnter(sender, new IndependentDragEventArgs(e));
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            this.listener.OnDragLeave(sender, new IndependentDragEventArgs(e));
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            this.listener.OnDragOver(sender, new IndependentDragEventArgs(e));
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            this.listener.OnDrop(sender, new IndependentDragEventArgs(e));
        }

        private void OnIsActiveChanged()
        {
            if (this.IsActive)
            {
                this.Subscribe();
            }
            else
            {
                this.UnSubscribe();
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.listener.OnMouseDown(sender, new IndependentMouseButtonEventArgs(e));
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            this.listener.OnMouseLeave(sender, new IndependentMouseEventArgs(e));
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            this.listener.OnMouseMove(sender, new IndependentMouseEventArgs(e));
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.listener.OnMouseUp(sender, new IndependentMouseButtonEventArgs(e));
        }

        private void Subscribe()
        {
            this.element.PreviewMouseMove += new MouseEventHandler(this.OnMouseMove);
            this.element.PreviewMouseDown += new MouseButtonEventHandler(this.OnMouseDown);
            this.element.PreviewMouseUp += new MouseButtonEventHandler(this.OnMouseUp);
            this.element.MouseLeave += new MouseEventHandler(this.OnMouseLeave);
            this.element.PreviewDrop += new DragEventHandler(this.OnDrop);
            this.element.PreviewDragEnter += new DragEventHandler(this.OnDragEnter);
            this.element.PreviewDragLeave += new DragEventHandler(this.OnDragLeave);
            this.element.PreviewDragOver += new DragEventHandler(this.OnDragOver);
        }

        private void UnSubscribe()
        {
            this.element.PreviewMouseMove -= new MouseEventHandler(this.OnMouseMove);
            this.element.PreviewMouseDown -= new MouseButtonEventHandler(this.OnMouseDown);
            this.element.PreviewMouseUp -= new MouseButtonEventHandler(this.OnMouseUp);
            this.element.PreviewDrop -= new DragEventHandler(this.OnDrop);
            this.element.PreviewDragEnter -= new DragEventHandler(this.OnDragEnter);
            this.element.PreviewDragLeave -= new DragEventHandler(this.OnDragLeave);
            this.element.PreviewDragOver -= new DragEventHandler(this.OnDragOver);
        }

        public IDragEventListener DragEventListener =>
            this.listener;

        public bool IsActive
        {
            get => 
                this.isActiveCore;
            set
            {
                if (this.isActiveCore != value)
                {
                    this.isActiveCore = value;
                    this.OnIsActiveChanged();
                }
            }
        }
    }
}

