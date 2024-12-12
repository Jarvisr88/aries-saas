namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class WcfCollectionViewSource : CollectionViewDataSourceBase, IWcfStandartDataSource, IWcfDataSource, IDataSource
    {
        public static readonly DependencyProperty ServiceRootProperty;

        static WcfCollectionViewSource()
        {
            Type ownerType = typeof(WcfCollectionViewSource);
            ServiceRootProperty = DependencyPropertyManager.Register("ServiceRoot", typeof(Uri), ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(WcfCollectionViewSource.OnServiceRootChanged)));
        }

        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new WcfDataSourceStrategyStandart(this);

        private static void OnServiceRootChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WcfCollectionViewSource source = (WcfCollectionViewSource) d;
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

