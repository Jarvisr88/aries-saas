namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Runtime.CompilerServices;

    internal abstract class ViewEventSubscriber<T> : IDisposable
    {
        protected ViewEventSubscriber(T source, BaseView view)
        {
            this.Source = source;
            this.Root = source;
            this.View = view;
            if (this.Source != null)
            {
                this.Subscribe(this.Source);
            }
        }

        public void Dispose()
        {
            T local;
            if (this.Source != null)
            {
                this.UnSubscribe(this.Source);
                local = default(T);
                this.Source = local;
            }
            this.View = null;
            local = default(T);
            this.Root = local;
            GC.SuppressFinalize(this);
        }

        protected abstract void Subscribe(T element);
        protected abstract void UnSubscribe(T element);

        protected BaseView View { get; private set; }

        public T Root { get; set; }

        public T Source { get; private set; }
    }
}

