namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Windows;

    public class IEnumerableDataSource : ItemsSourceDataSourceBase
    {
        private static readonly DependencyPropertyKey DataPropertyKey;
        public static readonly DependencyProperty DataProperty;

        static IEnumerableDataSource()
        {
            Type ownerType = typeof(IEnumerableDataSource);
            DataPropertyKey = DependencyPropertyManager.RegisterReadOnly("Data", typeof(IEnumerable), ownerType, new FrameworkPropertyMetadata());
            DataProperty = DataPropertyKey.DependencyProperty;
        }

        protected override bool CanUpdateFromDesignData() => 
            base.CanUpdateFromDesignData() ? ((base.DesignData.DataObjectType != null) || (base.ItemsSource != null)) : false;

        protected override object CreateDesignTimeDataSourceCore()
        {
            Type dataObjectType = base.DesignData.DataObjectType ?? DataSourceHelper.ExtractEnumerableType(base.ItemsSource);
            return ((dataObjectType != null) ? new BaseGridDesignTimeDataSource(dataObjectType, base.DesignData.RowCount, base.DesignData.UseDistinctValues, base.DesignData.FlattenHierarchy) : null);
        }

        protected override object UpdateDataCore() => 
            base.ItemsSource;

        public IEnumerable Data
        {
            get => 
                (IEnumerable) base.GetValue(DataProperty);
            protected set => 
                base.SetValue(DataPropertyKey, value);
        }

        protected internal override object DataCore
        {
            get => 
                this.Data;
            set => 
                this.Data = value as IEnumerable;
        }
    }
}

