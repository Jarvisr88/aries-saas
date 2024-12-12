namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;
    using System.Windows;

    public class ScaffoldingColumnPropertiesPopulator : DesignTimeColumnPropertiesPopulator
    {
        public ScaffoldingColumnPropertiesPopulator(IModelItem columnModel, GenerateColumnWrapper wrapper) : base(columnModel, wrapper)
        {
        }

        protected override bool CanApplyEditSettings() => 
            (base.wrapper.EditSettingsWrapper.EditSettingsType != GenerateEditSettingsWrapper.DefaultEditSettingsType) || (base.wrapper.EditSettingsWrapper.Properties.Count > 0);

        protected override bool IsPropertySet(IModelItem modelItem, DependencyProperty property) => 
            false;
    }
}

