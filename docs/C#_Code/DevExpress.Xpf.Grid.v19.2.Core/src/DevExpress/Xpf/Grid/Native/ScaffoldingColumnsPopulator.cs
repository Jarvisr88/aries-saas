namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using System;

    public class ScaffoldingColumnsPopulator : DesignTimeColumnsPopulator
    {
        public ScaffoldingColumnsPopulator(IModelItem dataControlModel, Type columnType, Type bandType, GenerateBandWrapper bandRootWrapper) : base(dataControlModel, columnType, bandType, bandRootWrapper, false)
        {
        }

        protected override void ClearOldLayout()
        {
        }

        protected override void CreateLayoutCore()
        {
            base.CreateLayoutWithBands(base.DataControlModel, base.BandRootWrapper);
        }

        protected override void FillColumnProperties(IModelItem columnModel, GenerateColumnWrapper wrapper)
        {
            ScaffoldingColumnPropertiesPopulator populator = new ScaffoldingColumnPropertiesPopulator(columnModel, wrapper);
            populator.ApplyDefaultProperties(false);
            if (wrapper.SetScaffSmartProperty)
            {
                populator.SetSmartProperty();
            }
            else
            {
                populator.ApplySmartProperties(true);
                populator.ApplyPropertiesFromResource();
                IModelItem editSettings = populator.CreateEditSettings();
                populator.ApplyEditSettingsProperties(editSettings);
                populator.ApplyInitializer(editSettings);
            }
            populator.ApplyFieldName();
        }
    }
}

