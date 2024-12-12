namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class FlyoutContainer
    {
        public event EventHandler Closed
        {
            add
            {
                this.ClosedSubscribe(value);
            }
            remove
            {
                this.ClosedUnSubscribe(value);
            }
        }

        public event EventHandler Opened
        {
            add
            {
                this.OpenedSubscribe(value);
            }
            remove
            {
                this.OpenedUnSubscribe(value);
            }
        }

        protected FlyoutContainer()
        {
        }

        protected virtual void ClosedSubscribe(EventHandler value)
        {
        }

        protected virtual void ClosedUnSubscribe(EventHandler value)
        {
        }

        protected virtual void OpenedSubscribe(EventHandler value)
        {
        }

        protected virtual void OpenedUnSubscribe(EventHandler value)
        {
        }

        public virtual UIElement Child { get; set; }

        public FrameworkElement Element { virtual get; private set; }

        public virtual double HorizontalOffset { get; set; }

        public virtual double VerticalOffset { get; set; }

        public virtual bool IsOpen { get; set; }
    }
}

