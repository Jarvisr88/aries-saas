namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Grid;
    using System;

    public class DesignTimeColumnsPopulator
    {
        private readonly Type ColumnType;
        private readonly Type BandType;
        private readonly bool ExpandSmartProperties;
        protected readonly IModelItem DataControlModel;
        protected readonly GenerateBandWrapper BandRootWrapper;

        public DesignTimeColumnsPopulator(IModelItem dataControlModel, Type columnType, Type bandType, GenerateBandWrapper bandRootWrapper, bool expandSmartProperties)
        {
            this.ExpandSmartProperties = expandSmartProperties;
            this.DataControlModel = dataControlModel;
            this.ColumnType = columnType;
            this.BandType = bandType;
            this.BandRootWrapper = bandRootWrapper;
        }

        protected virtual void ClearOldLayout()
        {
            this.DataControlModel.Properties["Bands"].Collection.Clear();
            this.DataControlModel.Properties["Columns"].Collection.Clear();
        }

        private IModelItem CreateBand(GenerateBandWrapper wrapper)
        {
            IModelItem item = this.DataControlModel.Context.CreateItem(this.BandType);
            item.Properties[BaseColumn.HeaderProperty.Name].SetValue(wrapper.Header);
            return item;
        }

        private IModelItem CreateColumn(GenerateColumnWrapper wrapper)
        {
            IModelItem columnModel = this.DataControlModel.Context.CreateItem(this.ColumnType);
            this.FillColumnProperties(columnModel, wrapper);
            return columnModel;
        }

        public void CreateLayout()
        {
            this.ClearOldLayout();
            this.CreateLayoutCore();
        }

        protected virtual void CreateLayoutCore()
        {
            if (this.ExpandSmartProperties)
            {
                this.CreateLayoutWithBands(this.DataControlModel, this.BandRootWrapper);
            }
            else
            {
                this.CreateLayoutWithotBands(this.DataControlModel, this.BandRootWrapper);
            }
        }

        protected void CreateLayoutWithBands(IModelItem bandRootModel, GenerateBandWrapper bandRootWrapper)
        {
            foreach (GenerateBandWrapper wrapper in bandRootWrapper.BandWrappers)
            {
                IModelItem item = this.CreateBand(wrapper);
                this.CreateLayoutWithBands(item, wrapper);
                bandRootModel.Properties["Bands"].Collection.Add(item);
            }
            foreach (GenerateColumnWrapper wrapper2 in bandRootWrapper.ColumnWrappers)
            {
                bandRootModel.Properties["Columns"].Collection.Add(this.CreateColumn(wrapper2));
            }
        }

        private void CreateLayoutWithotBands(IModelItem bandRootModel, GenerateBandWrapper bandRootWrapper)
        {
            foreach (GenerateColumnWrapper wrapper in bandRootWrapper.GetAllColumnWrappers())
            {
                bandRootModel.Properties["Columns"].Collection.Add(this.CreateColumn(wrapper));
            }
        }

        public void ExpandColumnProperties(IModelItem columnModel)
        {
            string currentValue = (string) columnModel.Properties[ColumnBase.FieldNameProperty.Name].Value.GetCurrentValue();
            GenerateColumnWrapper wrapper = GenerateColumnWrapperHelper.FindColumnWrapper(this.BandRootWrapper, currentValue);
            if (wrapper != null)
            {
                this.FillColumnProperties(columnModel, wrapper);
            }
        }

        protected virtual void FillColumnProperties(IModelItem columnModel, GenerateColumnWrapper wrapper)
        {
            DesignTimeColumnPropertiesPopulator populator = new DesignTimeColumnPropertiesPopulator(columnModel, wrapper);
            populator.ApplyRuntimeProperties();
            populator.ApplyFieldName();
            if (!this.ExpandSmartProperties)
            {
                populator.SetSmartProperty();
            }
            else
            {
                populator.ClearSmartProperty();
                populator.ApplyDefaultProperties(false);
                populator.ApplySmartProperties(true);
                populator.ApplyPropertiesFromResource();
                IModelItem editSettings = populator.CreateEditSettings();
                populator.ApplyEditSettingsProperties(editSettings);
                populator.ApplyInitializer(editSettings);
            }
        }
    }
}

