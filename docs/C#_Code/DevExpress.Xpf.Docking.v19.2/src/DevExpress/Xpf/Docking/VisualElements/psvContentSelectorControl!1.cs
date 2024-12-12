namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [DXToolboxBrowsable(false)]
    public class psvContentSelectorControl<T> : psvSelector<T> where T: class
    {
        private static readonly DependencyPropertyKey SelectedContentPropertyKey;
        public static readonly DependencyProperty SelectedContentProperty;
        public static readonly DependencyProperty SelectedContentTemplateProperty;

        static psvContentSelectorControl()
        {
            DependencyPropertyRegistrator<psvContentSelectorControl<T>> registrator = new DependencyPropertyRegistrator<psvContentSelectorControl<T>>();
            T defValue = default(T);
            registrator.RegisterReadonly<T>("SelectedContent", ref psvContentSelectorControl<T>.SelectedContentPropertyKey, ref psvContentSelectorControl<T>.SelectedContentProperty, defValue, (dObj, e) => ((psvContentSelectorControl<T>) dObj).OnSelectedContentChanged((T) e.NewValue, (T) e.OldValue), (CoerceValueCallback) ((dObj, value) => ((psvContentSelectorControl<T>) dObj).CoerceSelectedContent((T) value)));
            registrator.Register<DataTemplate>("SelectedContentTemplate", ref psvContentSelectorControl<T>.SelectedContentTemplateProperty, null, (dObj, e) => ((psvContentSelectorControl<T>) dObj).OnSelectedContentTemplateChanged(), null);
        }

        protected virtual T CoerceSelectedContent(T value) => 
            base.SelectedItem;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartSelectedContentPresenter = LayoutItemsHelper.GetTemplateChild<psvContentPresenter>(this);
            if (this.PartSelectedContentPresenter != null)
            {
                this.Forward(this.PartSelectedContentPresenter, ContentPresenter.ContentTemplateProperty, "SelectedContentTemplate", BindingMode.OneWay);
            }
        }

        protected override void OnDispose()
        {
            if (this.PartSelectedContentPresenter != null)
            {
                this.PartSelectedContentPresenter.Dispose();
                this.PartSelectedContentPresenter = null;
            }
            base.OnDispose();
        }

        protected virtual void OnSelectedContentChanged(T newValue, T oldValue)
        {
        }

        protected virtual void OnSelectedContentTemplateChanged()
        {
        }

        protected override void OnSelectedItemChanged(T item, T oldItem)
        {
            base.OnSelectedItemChanged(item, oldItem);
            base.CoerceValue(psvContentSelectorControl<T>.SelectedContentProperty);
        }

        public T SelectedContent =>
            (T) base.GetValue(psvContentSelectorControl<T>.SelectedContentProperty);

        public DataTemplate SelectedContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(psvContentSelectorControl<T>.SelectedContentTemplateProperty);
            set => 
                base.SetValue(psvContentSelectorControl<T>.SelectedContentTemplateProperty, value);
        }

        protected psvContentPresenter PartSelectedContentPresenter { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvContentSelectorControl<T>.<>c <>9;

            static <>c()
            {
                psvContentSelectorControl<T>.<>c.<>9 = new psvContentSelectorControl<T>.<>c();
            }

            internal void <.cctor>b__3_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvContentSelectorControl<T>) dObj).OnSelectedContentChanged((T) e.NewValue, (T) e.OldValue);
            }

            internal object <.cctor>b__3_1(DependencyObject dObj, object value) => 
                ((psvContentSelectorControl<T>) dObj).CoerceSelectedContent((T) value);

            internal void <.cctor>b__3_2(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvContentSelectorControl<T>) dObj).OnSelectedContentTemplateChanged();
            }
        }
    }
}

