namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TypedCollectionViewSource : CollectionViewDataSourceBase, ITypedDataSource, IDataSource
    {
        public static readonly DependencyProperty AdapterTypeProperty;

        static TypedCollectionViewSource()
        {
            Type ownerType = typeof(TypedCollectionViewSource);
            AdapterTypeProperty = DependencyPropertyManager.Register("AdapterType", typeof(Type), ownerType, new FrameworkPropertyMetadata((d, e) => ((TypedCollectionViewSource) d).UpdateData()));
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new TypedDataSourceStrategy(this);

        public Type AdapterType
        {
            get => 
                (Type) base.GetValue(AdapterTypeProperty);
            set => 
                base.SetValue(AdapterTypeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TypedCollectionViewSource.<>c <>9 = new TypedCollectionViewSource.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TypedCollectionViewSource) d).UpdateData();
            }
        }
    }
}

