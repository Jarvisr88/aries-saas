namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DataContentPresenter : ContentPresenter
    {
        private DataTemplate lastTemplate;
        private static Action<DataContentPresenter, object, EventArgs> changed;

        static DataContentPresenter()
        {
            changed = (owner, o, e) => owner.OnInnerContentChanged(o, e);
        }

        public DataContentPresenter()
        {
            this.InnerContentChangedEventHandler = new InnerContentChangedEventHandler<DataContentPresenter>(this, changed);
        }

        protected virtual bool CanRefreshContent() => 
            true;

        protected override DataTemplate ChooseTemplate()
        {
            this.lastTemplate = base.ChooseTemplate();
            return this.lastTemplate;
        }

        protected virtual void OnContentChanged(object oldValue)
        {
            if ((oldValue != null) && (oldValue is INotifyContentChanged))
            {
                ((INotifyContentChanged) oldValue).ContentChanged -= this.InnerContentChangedEventHandler.Handler;
            }
            if ((base.Content != null) && (base.Content is INotifyContentChanged))
            {
                ((INotifyContentChanged) base.Content).ContentChanged += this.InnerContentChangedEventHandler.Handler;
            }
        }

        protected virtual void OnContentInvalidated()
        {
        }

        protected virtual void OnInnerContentChanged(object sender, EventArgs e)
        {
            if (this.CanRefreshContent())
            {
                this.OnInnerContentChangedCore();
            }
        }

        protected virtual void OnInnerContentChangedCore()
        {
            if (!ReferenceEquals(this.lastTemplate, base.ChooseTemplate()))
            {
                object content = base.Content;
                base.Content = null;
                base.Content = content;
                this.OnContentInvalidated();
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, ContentPresenter.ContentProperty))
            {
                this.OnContentChanged(e.OldValue);
            }
        }

        private InnerContentChangedEventHandler<DataContentPresenter> InnerContentChangedEventHandler { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataContentPresenter.<>c <>9 = new DataContentPresenter.<>c();

            internal void <.cctor>b__14_0(DataContentPresenter owner, object o, EventArgs e)
            {
                owner.OnInnerContentChanged(o, e);
            }
        }
    }
}

