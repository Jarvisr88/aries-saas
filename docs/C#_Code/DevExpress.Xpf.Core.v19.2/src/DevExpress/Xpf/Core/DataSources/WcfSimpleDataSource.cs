namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class WcfSimpleDataSource : SimpleDataSource, IWcfStandartDataSource, IWcfDataSource, IDataSource
    {
        public static readonly DependencyProperty ServiceRootProperty;

        static WcfSimpleDataSource()
        {
            Type ownerType = typeof(WcfSimpleDataSource);
            ServiceRootProperty = DependencyPropertyManager.Register("ServiceRoot", typeof(Uri), ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(WcfSimpleDataSource.OnServiceRootChanged)));
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new WcfDataSourceStrategyStandart(this);

        private static void OnServiceRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfSimpleDataSource source = (WcfSimpleDataSource) d;
            if (source.Strategy is WcfDataSourceStrategyBase)
            {
                ((WcfDataSourceStrategyBase) source.Strategy).Update(source.ServiceRoot);
            }
            source.UpdateData();
        }

        public Uri ServiceRoot
        {
            get => 
                (Uri) base.GetValue(ServiceRootProperty);
            set => 
                base.SetValue(ServiceRootProperty, value);
        }
    }
}

