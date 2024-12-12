namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.DXBinding;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class BindingValueHelper : FrameworkElement
    {
        public static readonly DependencyProperty ValueProperty;
        private readonly Action<object> changedCallback;

        static BindingValueHelper()
        {
            ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(BindingValueHelper), new PropertyMetadata(null, (d, e) => ((BindingValueHelper) d).OnValueChanged(e.OldValue)));
        }

        public BindingValueHelper(Action<object> changedCallback)
        {
            this.changedCallback = changedCallback;
        }

        public void ApplyBindings(DetailDescriptorSelector detailSelector, object dataContext)
        {
            base.DataContext = dataContext;
            Func<DetailDescriptorTrigger, object> getValue = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<DetailDescriptorTrigger, object> local1 = <>c.<>9__7_0;
                getValue = <>c.<>9__7_0 = e => e.DetailDescriptor;
            }
            this.Style = DXTriggerHelper.CreateStyle<DetailDescriptorTrigger>(detailSelector.Items, ValueProperty, getValue, detailSelector.DefaultValue);
        }

        public void Clear()
        {
            base.Style = null;
            base.DataContext = null;
        }

        private void OnValueChanged(object oldValue)
        {
            if (this.changedCallback != null)
            {
                this.changedCallback(oldValue);
            }
        }

        public object Value
        {
            get => 
                base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BindingValueHelper.<>c <>9 = new BindingValueHelper.<>c();
            public static Func<DetailDescriptorTrigger, object> <>9__7_0;

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BindingValueHelper) d).OnValueChanged(e.OldValue);
            }

            internal object <ApplyBindings>b__7_0(DetailDescriptorTrigger e) => 
                e.DetailDescriptor;
        }
    }
}

