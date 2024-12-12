namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public abstract class PLinqDataSourceBase : ItemsSourceDataSourceBase
    {
        private static readonly DependencyPropertyKey DataPropertyKey;
        public static readonly DependencyProperty DataProperty;

        static PLinqDataSourceBase()
        {
            Type ownerType = typeof(PLinqDataSourceBase);
            DataPropertyKey = DependencyPropertyManager.RegisterReadOnly("Data", typeof(IListSource), ownerType, new FrameworkPropertyMetadata());
            DataProperty = DataPropertyKey.DependencyProperty;
        }

        protected PLinqDataSourceBase()
        {
        }

        protected override object CreateDesignTimeDataSourceCore()
        {
            Type dataObjectType = this.GetDataObjectType();
            return ((dataObjectType != null) ? new BaseGridDesignTimeDataSource(dataObjectType, base.DesignData.RowCount, base.DesignData.UseDistinctValues, base.DesignData.FlattenHierarchy) : null);
        }

        protected abstract Type GetDataObjectType();

        public IListSource Data
        {
            get => 
                (IListSource) base.GetValue(DataProperty);
            protected set => 
                base.SetValue(DataPropertyKey, value);
        }

        protected internal override object DataCore
        {
            get => 
                this.Data;
            set => 
                this.Data = value as IListSource;
        }
    }
}

