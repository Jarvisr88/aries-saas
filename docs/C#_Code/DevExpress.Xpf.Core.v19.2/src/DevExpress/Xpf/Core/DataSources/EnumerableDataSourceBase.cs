namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Windows;

    public abstract class EnumerableDataSourceBase : DataSourceBase
    {
        private static readonly DependencyPropertyKey DataPropertyKey;
        public static readonly DependencyProperty DataProperty;

        static EnumerableDataSourceBase()
        {
            Type ownerType = typeof(EnumerableDataSourceBase);
            DataPropertyKey = DependencyPropertyManager.RegisterReadOnly("Data", typeof(IEnumerable), ownerType, new FrameworkPropertyMetadata());
            DataProperty = DataPropertyKey.DependencyProperty;
        }

        protected EnumerableDataSourceBase()
        {
        }

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

