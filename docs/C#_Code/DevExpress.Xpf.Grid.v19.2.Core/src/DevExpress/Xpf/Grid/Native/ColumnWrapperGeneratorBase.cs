namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data.Helpers;
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;

    public abstract class ColumnWrapperGeneratorBase : EditorsGeneratorBase
    {
        protected readonly GenerateBandWrapper BandWrapper;
        protected readonly List<GenerateColumnWrapper> ColumnWrapperCollection;

        public ColumnWrapperGeneratorBase(GenerateBandWrapper bandWrapper)
        {
            this.BandWrapper = bandWrapper;
            this.ColumnWrapperCollection = bandWrapper.ColumnWrappers;
        }

        private void FillColumnWrapperProperties(GenerateColumnWrapper wrapper, IEdmPropertyInfo property, string displayMember)
        {
            AttributesApplier.ApplyBaseAttributes(property, delegate (string x) {
                string str = x + (string.IsNullOrEmpty(displayMember) ? null : ("." + displayMember));
                wrapper.FieldName = str;
                if (str != x)
                {
                    wrapper.Header ??= SplitStringHelper.SplitPascalCaseString(x);
                }
            }, null, x => wrapper.Header = x, x => wrapper.HeaderToolTip = x, () => wrapper.ReadOnly = true, x => wrapper.AllowEditing = x, () => wrapper.Visible = false, () => wrapper.Visible = false, null);
        }

        private void FillEditSettingsWrapperProperties(IEdmPropertyInfo property, GenerateColumnWrapper columnWrapper, Type editType)
        {
            GenerateEditSettingsWrapper wrapper = new GenerateEditSettingsWrapper {
                EditSettingsType = editType
            };
            columnWrapper.EditSettingsWrapper = wrapper;
            AttributesApplier.ApplyDisplayFormatAttributes(property, x => wrapper.Properties[BaseEditSettings.NullTextProperty] = x, x => wrapper.Properties[BaseEditSettings.DisplayFormatProperty] = x, null);
            if ((wrapper.EditSettingsType == null) && (wrapper.Properties.Count > 0))
            {
                wrapper.EditSettingsType = typeof(TextEditSettings);
            }
        }

        protected override void GenerateEditor(IEdmPropertyInfo property, Type editType, EditorsGeneratorBase.Initializer initializer)
        {
            this.GenerateEditor(property, editType, initializer, null, null);
        }

        protected virtual void GenerateEditor(IEdmPropertyInfo property, Type editType, EditorsGeneratorBase.Initializer initializer, object resourceKey, string displayMember)
        {
            GenerateColumnWrapper wrapper1 = new GenerateColumnWrapper(this.BandWrapper.GetNextPropertyIndex(), property.PropertyType, this, (PropertyDescriptor) property.ContextObject);
            wrapper1.Initializer = initializer;
            wrapper1.EditorResourceKey = resourceKey;
            GenerateColumnWrapper wrapper = wrapper1;
            this.FillColumnWrapperProperties(wrapper, property, displayMember);
            this.FillEditSettingsWrapperProperties(property, wrapper, editType);
            this.ColumnWrapperCollection.Add(wrapper);
        }

        public object GetResourceContentFromTemplate(DataTemplate template) => 
            GetResourceContent<ColumnBase, BaseEditSettings, BaseEdit>(template);

        public DataTemplate GetResourceTemplate(IModelItem columnModel, object resourceKey) => 
            GetResourceTemplate((FrameworkElement) columnModel.Context.GetRoot().GetCurrentValue(), resourceKey);

        protected override EditorsGeneratorBase.EditorsGeneratorMode Mode =>
            EditorsGeneratorBase.EditorsGeneratorMode.EditSettings;

        public override EditorsGeneratorBase.EditorsGeneratorTarget Target =>
            EditorsGeneratorBase.EditorsGeneratorTarget.GridControl;
    }
}

