namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class ItemsSourceDataSourceBase : SimpleDataSourceBase
    {
        public static readonly DependencyProperty ItemsSourceProperty;

        static ItemsSourceDataSourceBase()
        {
            Type ownerType = typeof(ItemsSourceDataSourceBase);
            ItemsSourceProperty = DependencyPropertyManager.Register("ItemsSource", typeof(IEnumerable), ownerType, new FrameworkPropertyMetadata((d, e) => ((ItemsSourceDataSourceBase) d).UpdateData()));
        }

        protected ItemsSourceDataSourceBase()
        {
        }

        public IEnumerable ItemsSource
        {
            get => 
                (IEnumerable) base.GetValue(ItemsSourceProperty);
            set => 
                base.SetValue(ItemsSourceProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsSourceDataSourceBase.<>c <>9 = new ItemsSourceDataSourceBase.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ItemsSourceDataSourceBase) d).UpdateData();
            }
        }
    }
}

