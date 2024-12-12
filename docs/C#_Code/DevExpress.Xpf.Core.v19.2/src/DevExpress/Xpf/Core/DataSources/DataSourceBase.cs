namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class DataSourceBase : SimpleDataSourceBase, IDataSource
    {
        public static readonly DependencyProperty ContextTypeProperty;
        public static readonly DependencyProperty PathProperty;
        private DataSourceStrategyBase strategy;
        private readonly BaseDataSourceStrategySelector strategySelector;
        private object contextInstance;

        static DataSourceBase()
        {
            Type ownerType = typeof(DataSourceBase);
            ContextTypeProperty = DependencyPropertyManager.Register("ContextType", typeof(Type), ownerType, new FrameworkPropertyMetadata((d, e) => ((DataSourceBase) d).OnContextTypeChanged()));
            PathProperty = DependencyPropertyManager.Register("Path", typeof(string), ownerType, new FrameworkPropertyMetadata((d, e) => ((DataSourceBase) d).OnPathChanged()));
        }

        public DataSourceBase()
        {
            this.Initialize();
            this.strategy = this.CreateDataSourceStrategy();
            this.strategySelector = this.CreateDataSourceStrategySelector();
        }

        protected override bool CanUpdateFromDesignData() => 
            base.CanUpdateFromDesignData() && this.Strategy.CanGetDesignData();

        protected virtual object CreateData(object value) => 
            this.Strategy.CreateData(value);

        protected virtual DataSourceStrategyBase CreateDataSourceStrategy() => 
            new DataSourceStrategyBase(this);

        protected virtual BaseDataSourceStrategySelector CreateDataSourceStrategySelector() => 
            new BaseDataSourceStrategySelector();

        protected override object CreateDesignTimeDataSourceCore() => 
            new BaseGridDesignTimeDataSource(this.Strategy.GetDataObjectType(), base.DesignData.RowCount, base.DesignData.UseDistinctValues, null, null, this.Strategy.GetDesignTimeProperties());

        protected virtual void Initialize()
        {
        }

        protected void OnContextTypeChanged()
        {
            this.UpdateStrategy();
            base.UpdateData();
        }

        protected virtual void OnPathChanged()
        {
            this.UpdateStrategy();
            base.UpdateData();
        }

        protected override object UpdateDataCore()
        {
            this.strategy = this.strategySelector.SelectStrategy(this, this.Strategy);
            this.strategy.Update(this.ContextType, this.Path);
            if (!this.Strategy.CanUpdateData())
            {
                return null;
            }
            this.contextInstance = this.Strategy.CreateContextIstance();
            object dataMemberValue = this.Strategy.GetDataMemberValue(this.contextInstance);
            return this.CreateData(dataMemberValue);
        }

        private void UpdateStrategy()
        {
            if (this.Strategy != null)
            {
                this.Strategy.Update(this.ContextType, this.Path);
            }
        }

        protected DataSourceStrategyBase Strategy =>
            this.strategy;

        public Type ContextType
        {
            get => 
                (Type) base.GetValue(ContextTypeProperty);
            set => 
                base.SetValue(ContextTypeProperty, value);
        }

        object IDataSource.ContextInstance =>
            this.contextInstance;

        object IDataSource.Data =>
            this.DataCore;

        public string Path
        {
            get => 
                (string) base.GetValue(PathProperty);
            set => 
                base.SetValue(PathProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataSourceBase.<>c <>9 = new DataSourceBase.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataSourceBase) d).OnContextTypeChanged();
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DataSourceBase) d).OnPathChanged();
            }
        }
    }
}

