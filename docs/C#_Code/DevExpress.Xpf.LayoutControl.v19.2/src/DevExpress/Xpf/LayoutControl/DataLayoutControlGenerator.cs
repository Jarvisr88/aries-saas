namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Utils.Filtering;
    using DevExpress.Utils.Filtering.Internal;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;

    public class DataLayoutControlGenerator : DataLayoutControlEditorsGeneratorBase
    {
        private static readonly double LookupLayoutItemMaxHeight = (SystemParameters.PrimaryScreenHeight / 3.0);
        private readonly DataLayoutControl control;
        private readonly Action<FrameworkElement> addItemAction;

        public DataLayoutControlGenerator(DataLayoutControl control, Action<FrameworkElement> addItemAction)
        {
            this.control = control;
            this.addItemAction = addItemAction;
        }

        private DataLayoutItem CreateDataLayoutItem(IEdmPropertyInfo property, Func<DataLayoutItem> createItem)
        {
            DataLayoutItem input = this.control.GenerateItem(property.PropertyType, property, createItem);
            input.Do<FrameworkElement>(this.addItemAction);
            return input;
        }

        public override void FilterBooleanChoice(IEdmPropertyInfo property, FilterBooleanEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            this.CreateDataLayoutItem(property, null);
        }

        public override void FilterBooleanChoiceProperty(IEdmPropertyInfo property, FilterBooleanEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            throw new NotImplementedException();
        }

        public override void FilterEnumChoice(IEdmPropertyInfo property, FilterEnumEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            this.CreateDataLayoutItem(property, null);
        }

        public override void FilterEnumChoiceProperty(IEdmPropertyInfo property, FilterEnumEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            throw new NotImplementedException();
        }

        public override void FilterLookup(IEdmPropertyInfo property, FilterLookupEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            DataLayoutItem dObj = this.CreateDataLayoutItem(property, null);
            if (((((LookupUIEditorType) settings.EditorType) == LookupUIEditorType.Default) || (((LookupUIEditorType) settings.EditorType) == LookupUIEditorType.List)) && DataLayoutItem.IsPropertyHasDefaultValue(dObj, FrameworkElement.MaxHeightProperty))
            {
                dObj.MaxHeight = LookupLayoutItemMaxHeight;
            }
        }

        public override void FilterLookupProperty(IEdmPropertyInfo property, FilterLookupEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            throw new NotImplementedException();
        }

        public override void FilterRange(IEdmPropertyInfo property, FilterRangeEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            this.CreateDataLayoutItem(property, null);
        }

        public override void FilterRangeProperty(IEdmPropertyInfo property, FilterRangeEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            throw new NotImplementedException();
        }

        protected override void GenerateEditor(IEdmPropertyInfo property, Type editType, EditorsGeneratorBase.Initializer initializer)
        {
            this.CreateDataLayoutItem(property, null);
        }

        public override void GenerateEditorFromResources(IEdmPropertyInfo property, object resourceKey, EditorsGeneratorBase.Initializer initializer)
        {
            DataTemplate template = GetResourceTemplate(this.control, resourceKey);
            object resourceContent = GetResourceContent<DataLayoutItem, BaseEditSettings, BaseEdit>(template);
            if (!(resourceContent is DataLayoutItem))
            {
                this.CreateDataLayoutItem(property, delegate {
                    DataLayoutItem item = this.control.CreateItem();
                    item.editorTemplate = template;
                    return item;
                });
            }
            else
            {
                DataLayoutItem dataLayoutItem = (DataLayoutItem) resourceContent;
                if (dataLayoutItem.Content != null)
                {
                    base.ApplyProperties(property, new RuntimeEditingContext(dataLayoutItem, null).GetRoot(), new RuntimeEditingContext(dataLayoutItem.Content, null).GetRoot(), initializer, dataLayoutItem.Content is BaseEdit);
                }
                this.CreateDataLayoutItem(property, () => dataLayoutItem);
            }
        }
    }
}

