namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Utils.Filtering;
    using DevExpress.Utils.Filtering.Internal;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DataLayoutItemGenerator : DataLayoutControlEditorsGeneratorBase
    {
        private bool allowGenerateContent;

        public DataLayoutItemGenerator(DataLayoutItem owner, bool allowGenerateContent)
        {
            this.Owner = owner;
            this.allowGenerateContent = allowGenerateContent;
            if (!this.allowGenerateContent)
            {
                this.Content = owner.Content as FrameworkElement;
            }
        }

        private EditorsGeneratorBase.Initializer CustomizeInitializer(EditorsGeneratorBase.Initializer initializer, bool clearLabel, bool clearToolTip) => 
            initializer + new EditorsGeneratorBase.Initializer(null, delegate (IModelItem container) {
                if (clearLabel)
                {
                    container.SetValue(LayoutItem.LabelProperty, null, true);
                }
                if (clearToolTip)
                {
                    container.SetValue(FrameworkElement.ToolTipProperty, null, true);
                }
            });

        public override void FilterBooleanChoice(IEdmPropertyInfo property, FilterBooleanEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            DataTemplate filteringTemplate;
            switch (settings.EditorType)
            {
                case BooleanUIEditorType.Default:
                case BooleanUIEditorType.Check:
                    filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.BooleanCheck);
                    initializer = this.CustomizeInitializer(initializer, true, true);
                    break;

                case BooleanUIEditorType.Toggle:
                    filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.BooleanToggle);
                    initializer = this.CustomizeInitializer(initializer, true, true);
                    break;

                case BooleanUIEditorType.List:
                    filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.BooleanList);
                    initializer = this.CustomizeInitializer(initializer, true, true);
                    break;

                case BooleanUIEditorType.DropDown:
                    filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.BooleanDropDown);
                    initializer = this.CustomizeInitializer(initializer, true, true);
                    break;

                default:
                    throw new NotImplementedException();
            }
            this.GenerateEditorFromResourcesCore(property, filteringTemplate, initializer);
        }

        public override void FilterBooleanChoiceProperty(IEdmPropertyInfo property, FilterBooleanEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            throw new NotImplementedException();
        }

        public override void FilterEnumChoice(IEdmPropertyInfo property, FilterEnumEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            DataTemplate template;
            switch (settings.EditorType)
            {
                case LookupUIEditorType.Default:
                case LookupUIEditorType.List:
                    template = settings.UseFlags ? this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.EnumCheckedList) : this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.EnumList);
                    break;

                case LookupUIEditorType.DropDown:
                    template = settings.UseFlags ? this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.EnumCheckedDropDown) : this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.EnumDropDown);
                    break;

                case LookupUIEditorType.TokenBox:
                    template = settings.UseFlags ? this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.EnumToken) : this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.EnumList);
                    break;

                default:
                    throw new NotImplementedException();
            }
            this.GenerateEditorFromResourcesCore(property, template, this.CustomizeInitializer(initializer, true, true));
        }

        public override void FilterEnumChoiceProperty(IEdmPropertyInfo property, FilterEnumEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            throw new NotImplementedException();
        }

        public override void FilterLookup(IEdmPropertyInfo property, FilterLookupEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            DataTemplate filteringTemplate;
            switch (settings.EditorType)
            {
                case LookupUIEditorType.Default:
                case LookupUIEditorType.List:
                    filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.LookupCheckedList);
                    break;

                case LookupUIEditorType.DropDown:
                    filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.LookupCheckedDropDown);
                    break;

                case LookupUIEditorType.TokenBox:
                    filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.LookupToken);
                    break;

                default:
                    throw new NotImplementedException();
            }
            this.GenerateEditorFromResourcesCore(property, filteringTemplate, this.CustomizeInitializer(initializer, true, true));
        }

        public override void FilterLookupProperty(IEdmPropertyInfo property, FilterLookupEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            throw new NotImplementedException();
        }

        public override void FilterRange(IEdmPropertyInfo property, FilterRangeEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            DataTemplate filteringTemplate;
            if (settings.NumericEditorType == null)
            {
                if (settings.DateTimeEditorType == null)
                {
                    throw new InvalidOperationException();
                }
                DateTimeRangeUIEditorType? dateTimeEditorType = settings.DateTimeEditorType;
                if (dateTimeEditorType == null)
                {
                    goto TR_0009;
                }
                else
                {
                    switch (dateTimeEditorType.GetValueOrDefault())
                    {
                        case DateTimeRangeUIEditorType.Default:
                        case DateTimeRangeUIEditorType.Range:
                        case DateTimeRangeUIEditorType.Calendar:
                            filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.DateTimeRangeDefault);
                            break;

                        case DateTimeRangeUIEditorType.Picker:
                            filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.DateTimeRangePicker);
                            break;

                        default:
                            goto TR_0009;
                    }
                }
                goto TR_0001;
            }
            else
            {
                RangeUIEditorType? numericEditorType = settings.NumericEditorType;
                if (numericEditorType == null)
                {
                    goto TR_0000;
                }
                else
                {
                    switch (numericEditorType.GetValueOrDefault())
                    {
                        case RangeUIEditorType.Default:
                            filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.RangeDefault);
                            break;

                        case RangeUIEditorType.Range:
                            filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.RangeTrack);
                            break;

                        case RangeUIEditorType.Text:
                            filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.RangeText);
                            break;

                        case RangeUIEditorType.Spin:
                            filteringTemplate = this.GetFilteringTemplate(FilteringUIGeneratorThemeKeys.RangeSpin);
                            break;

                        default:
                            goto TR_0000;
                    }
                }
                goto TR_0001;
            }
            goto TR_0009;
        TR_0000:
            throw new NotImplementedException();
        TR_0001:
            this.GenerateEditorFromResourcesCore(property, filteringTemplate, this.CustomizeInitializer(initializer, true, true));
            return;
        TR_0009:
            throw new NotImplementedException();
        }

        public override void FilterRangeProperty(IEdmPropertyInfo property, FilterRangeEditorSettings settings, EditorsGeneratorBase.Initializer initializer)
        {
            this.GenerateEditor(property, null, initializer, true);
        }

        protected override void GenerateEditor(IEdmPropertyInfo property, Type editType, EditorsGeneratorBase.Initializer initializer)
        {
            IEditingContext context = new RuntimeEditingContext(this.Owner, null);
            this.GenerateEditor(property, context.CreateItem(editType), initializer, true);
        }

        private void GenerateEditor(IEdmPropertyInfo property, IModelItem edit, EditorsGeneratorBase.Initializer initializer, bool customizeEditor)
        {
            if (!this.allowGenerateContent)
            {
                base.ApplyProperties(property, new RuntimeEditingContext(this.Owner, null).GetRoot(), new RuntimeEditingContext(this.Content, null).GetRoot(), initializer, this.Content is BaseEdit);
            }
            else
            {
                base.ApplyProperties(property, new RuntimeEditingContext(this.Owner, null).GetRoot(), edit, initializer, customizeEditor);
                this.Content = (FrameworkElement) edit.GetCurrentValue();
            }
        }

        public override void GenerateEditorFromResources(IEdmPropertyInfo property, object resourceKey, EditorsGeneratorBase.Initializer initializer)
        {
            DataTemplate resource = this.GetResourceTemplate(resourceKey) ?? this.Owner.editorTemplate;
            this.GenerateEditorFromResourcesCore(property, resource, initializer);
        }

        private void GenerateEditorFromResourcesCore(IEdmPropertyInfo property, DataTemplate resource, EditorsGeneratorBase.Initializer initializer)
        {
            if (resource != null)
            {
                object resourceContent = GetResourceContent<DataLayoutItem, BaseEditSettings, BaseEdit>(resource);
                if (resourceContent is BaseEditSettings)
                {
                    IBaseEdit root = ((BaseEditSettings) resourceContent).CreateEditor(EditorOptimizationMode.Disabled);
                    this.GenerateEditor(property, new RuntimeEditingContext(root, null).GetRoot(), initializer, true);
                }
                else if (resourceContent is BaseEdit)
                {
                    this.GenerateEditor(property, new RuntimeEditingContext(resourceContent, null).GetRoot(), initializer, true);
                }
                else
                {
                    this.GenerateEditor(property, new RuntimeEditingContext(resourceContent, null).GetRoot(), initializer, false);
                }
            }
        }

        private DataTemplate GetFilteringTemplate(FilteringUIGeneratorThemeKeys key)
        {
            FilteringUIGeneratorThemeKeyExtension extension1 = new FilteringUIGeneratorThemeKeyExtension();
            extension1.IsThemeIndependent = true;
            extension1.ResourceKey = key;
            return this.GetResourceTemplate(extension1);
        }

        private DataTemplate GetResourceTemplate(object key) => 
            (DataTemplate) (this.Owner.GetDataLayoutControl() ?? this.Owner).TryFindResource(key);

        public FrameworkElement Content { get; private set; }

        public DataLayoutItem Owner { get; private set; }
    }
}

