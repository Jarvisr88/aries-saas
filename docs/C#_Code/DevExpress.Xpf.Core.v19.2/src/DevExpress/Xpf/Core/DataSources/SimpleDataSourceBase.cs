namespace DevExpress.Xpf.Core.DataSources
{
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class SimpleDataSourceBase : DXDesignTimeControl, IDesignDataUpdater
    {
        protected SimpleDataSourceBase()
        {
            DesignDataManager.RegisterUpdater(base.GetType(), new StandartDesignDataUpdater());
        }

        protected virtual bool CanUpdateFromDesignData() => 
            (this.DesignData != null) && (this.DesignData.RowCount > 0);

        protected override ControlTemplate CreateControlTemplate() => 
            (ControlTemplate) GetResourceDictionary()["DataSourceTemplate"];

        private object CreateDesignTimeDataSource() => 
            this.CanUpdateFromDesignData() ? this.CreateDesignTimeDataSourceCore() : null;

        protected abstract object CreateDesignTimeDataSourceCore();
        void IDesignDataUpdater.UpdateDesignData(DependencyObject element)
        {
            this.UpdateData();
        }

        protected override string GetDesignTimeImageName() => 
            "DevExpress.Xpf.Core.Core.Images.DataSource.png";

        private static ResourceDictionary GetResourceDictionary()
        {
            string uriString = $"/{"DevExpress.Xpf.Core.v19.2"};component/DataSources/Resources/Resources.xaml";
            ResourceDictionary dictionary1 = new ResourceDictionary();
            dictionary1.Source = new Uri(uriString, UriKind.Relative);
            return dictionary1;
        }

        protected void UpdateData()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.DataCore = this.CreateDesignTimeDataSource();
            }
            else
            {
                this.DataCore = this.UpdateDataCore();
            }
        }

        protected abstract object UpdateDataCore();

        protected internal abstract object DataCore { get; set; }

        public IDesignDataSettings DesignData
        {
            get => 
                DesignDataManager.GetDesignData(this);
            set => 
                DesignDataManager.SetDesignData(this, value);
        }
    }
}

