namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.ServerMode;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public abstract class PLinqInstantFeedbackDataSourceBase : ListSourceDataSourceBase
    {
        public static readonly DependencyProperty DefaultSortingProperty;
        private PLinqInstantFeedbackDataSource pLinqSource;

        static PLinqInstantFeedbackDataSourceBase()
        {
            Type ownerType = typeof(PLinqInstantFeedbackDataSourceBase);
            DefaultSortingProperty = DependencyPropertyManager.Register("DefaultSorting", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, (d, e) => ((PLinqInstantFeedbackDataSourceBase) d).UpdateData()));
        }

        protected PLinqInstantFeedbackDataSourceBase()
        {
        }

        protected override object CreateData(object value)
        {
            this.pLinqSource.ItemsSource = base.Strategy.CreateData(value) as IEnumerable;
            return this.pLinqSource.Data;
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new GenericPropertyDataSourceStrategy(this);

        protected override void Initialize()
        {
            base.Initialize();
            this.pLinqSource = new PLinqInstantFeedbackDataSource();
            Binding binding = new Binding("DefaultSorting");
            binding.Source = this;
            this.pLinqSource.SetBinding(PLinqInstantFeedbackDataSource.DefaultSortingProperty, binding);
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
            public static readonly PLinqInstantFeedbackDataSourceBase.<>c <>9 = new PLinqInstantFeedbackDataSourceBase.<>c();

            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PLinqInstantFeedbackDataSourceBase) d).UpdateData();
            }
        }
    }
}

