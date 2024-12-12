namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;

    public class BindingToEventHelper<T> : ITargetChangedHelper<T>
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private readonly DependencyProperty property;
        [CompilerGenerated]
        private TargetChangedEventHandler<T> TargetChanged;

        public event TargetChangedEventHandler<T> TargetChanged
        {
            [CompilerGenerated] add
            {
                TargetChangedEventHandler<T> targetChanged = this.TargetChanged;
                while (true)
                {
                    TargetChangedEventHandler<T> comparand = targetChanged;
                    TargetChangedEventHandler<T> handler3 = comparand + value;
                    targetChanged = Interlocked.CompareExchange<TargetChangedEventHandler<T>>(ref this.TargetChanged, handler3, comparand);
                    if (ReferenceEquals(targetChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                TargetChangedEventHandler<T> targetChanged = this.TargetChanged;
                while (true)
                {
                    TargetChangedEventHandler<T> comparand = targetChanged;
                    TargetChangedEventHandler<T> handler3 = comparand - value;
                    targetChanged = Interlocked.CompareExchange<TargetChangedEventHandler<T>>(ref this.TargetChanged, handler3, comparand);
                    if (ReferenceEquals(targetChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public BindingToEventHelper(FrameworkElement element, DependencyProperty property, Func<object, T> getValueHandler)
        {
            this.Element = element;
            this.property = property;
            this.GetValueHandler = getValueHandler;
            this.Provider = new PropertyProvider<T>((BindingToEventHelper<T>) this);
        }

        public void RaiseTargetChanged(T value)
        {
            if (this.TargetChanged != null)
            {
                this.TargetChanged(this, new TargetChangedEventArgs<T>(value));
            }
        }

        public FrameworkElement Element { get; private set; }

        public DependencyProperty Property =>
            this.property;

        public Func<object, T> GetValueHandler { get; set; }

        private PropertyProvider<T> Provider { get; set; }

        private class PropertyProvider : DependencyObject
        {
            public static readonly DependencyProperty TargetProperty;

            static PropertyProvider()
            {
                Type ownerType = typeof(BindingToEventHelper<T>.PropertyProvider);
                BindingToEventHelper<T>.PropertyProvider.TargetProperty = DependencyPropertyManager.Register("Target", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (d, e) => ((BindingToEventHelper<T>.PropertyProvider) d).TargetChanged(e.NewValue)));
            }

            public PropertyProvider(BindingToEventHelper<T> helper)
            {
                this.TargetChangedHelper = helper;
                Binding binding = new Binding();
                binding.Path = new PropertyPath(helper.Property);
                binding.Mode = BindingMode.OneWay;
                binding.Source = helper.Element;
                BindingOperations.SetBinding(this, BindingToEventHelper<T>.PropertyProvider.TargetProperty, binding);
            }

            private void TargetChanged(object newValue)
            {
                T local = this.TargetChangedHelper.GetValueHandler(newValue);
                this.TargetChangedHelper.RaiseTargetChanged(local);
            }

            private BindingToEventHelper<T> TargetChangedHelper { get; set; }

            public object Target
            {
                get => 
                    base.GetValue(BindingToEventHelper<T>.PropertyProvider.TargetProperty);
                set => 
                    base.SetValue(BindingToEventHelper<T>.PropertyProvider.TargetProperty, value);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly BindingToEventHelper<T>.PropertyProvider.<>c <>9;

                static <>c()
                {
                    BindingToEventHelper<T>.PropertyProvider.<>c.<>9 = new BindingToEventHelper<T>.PropertyProvider.<>c();
                }

                internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
                {
                    ((BindingToEventHelper<T>.PropertyProvider) d).TargetChanged(e.NewValue);
                }
            }
        }
    }
}

