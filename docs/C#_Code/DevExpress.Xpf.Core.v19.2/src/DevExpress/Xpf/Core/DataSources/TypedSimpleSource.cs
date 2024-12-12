namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TypedSimpleSource : SimpleDataSource, ITypedDataSource, IDataSource
    {
        public static readonly DependencyProperty AdapterTypeProperty;

        static TypedSimpleSource()
        {
            Type ownerType = typeof(TypedSimpleSource);
            AdapterTypeProperty = DependencyPropertyManager.Register("AdapterType", typeof(Type), ownerType, new FrameworkPropertyMetadata((d, e) => ((TypedSimpleSource) d).UpdateData()));
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
            public static readonly TypedSimpleSource.<>c <>9 = new TypedSimpleSource.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TypedSimpleSource) d).UpdateData();
            }
        }
    }
}

