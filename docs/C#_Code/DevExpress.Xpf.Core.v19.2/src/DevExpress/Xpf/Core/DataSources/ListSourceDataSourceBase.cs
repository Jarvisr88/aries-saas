namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public abstract class ListSourceDataSourceBase : DataSourceBase
    {
        private static readonly DependencyPropertyKey DataPropertyKey;
        public static readonly DependencyProperty DataProperty;

        static ListSourceDataSourceBase()
        {
            Type ownerType = typeof(ListSourceDataSourceBase);
            DataPropertyKey = DependencyPropertyManager.RegisterReadOnly("Data", typeof(IListSource), ownerType, new FrameworkPropertyMetadata());
            DataProperty = DataPropertyKey.DependencyProperty;
        }

        protected ListSourceDataSourceBase()
        {
        }

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

