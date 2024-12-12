namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.ServerMode;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public abstract class PLinqServerModeDataSourceBase : ListSourceDataSourceBase
    {
        public static readonly DependencyProperty DefaultSortingProperty;
        private PLinqServerModeDataSource pLinqSource;

        static PLinqServerModeDataSourceBase()
        {
            Type ownerType = typeof(PLinqServerModeDataSourceBase);
            DefaultSortingProperty = DependencyPropertyManager.Register("DefaultSorting", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((PLinqServerModeDataSourceBase) d).UpdateData()));
        }

        protected PLinqServerModeDataSourceBase()
        {
        }

        protected override object CreateData(object value)
        {
            this.pLinqSource.ElementType = base.Strategy.GetDataObjectType();
            this.pLinqSource.ItemsSource = base.Strategy.CreateData(value) as IEnumerable;
            return this.pLinqSource.Data;
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.pLinqSource = new PLinqServerModeDataSource();
            Binding binding = new Binding("DefaultSorting");
            binding.Source = this;
            this.pLinqSource.SetBinding(PLinqServerModeDataSource.DefaultSortingProperty, binding);
        }

        public string DefaultSorting
        {
            get => 
                (string) base.GetValue(DefaultSortingProperty);
            set => 
                base.SetValue(DefaultSortingProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PLinqServerModeDataSourceBase.<>c <>9 = new PLinqServerModeDataSourceBase.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PLinqServerModeDataSourceBase) d).UpdateData();
            }
        }
    }
}

