namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Entity.Model;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class StandaloneColumnGenerator : EditorsGeneratorBase
    {
        private readonly IModelItem definitionsProvider;
        private readonly FrameworkElement ownerElement;

        public StandaloneColumnGenerator(IModelItem definitionsProvider, FrameworkElement ownerElement)
        {
            this.definitionsProvider = definitionsProvider;
            this.ownerElement = ownerElement;
        }

        protected override void GenerateEditor(IEdmPropertyInfo property, Type editType, EditorsGeneratorBase.Initializer initializer)
        {
            IModelItem filterColumn = this.definitionsProvider.Context.CreateItem(typeof(PropertyDescription));
            this.GenerateEditor(property, filterColumn, (editType != null) ? this.definitionsProvider.Context.CreateItem(editType) : filterColumn.Context.CreateItem(typeof(TextEditSettings)), initializer);
        }

        private void GenerateEditor(IEdmPropertyInfo property, IModelItem filterColumn, IModelItem editSettings, EditorsGeneratorBase.Initializer initializer)
        {
            AttributesApplier.ApplyBaseAttributesForFilterColumn(property, filterColumn);
            AttributesApplier.ApplyDisplayFormatAttributesForEditSettings(property, () => editSettings, null);
            if (editSettings != null)
            {
                initializer.SetEditProperties(filterColumn, editSettings);
                filterColumn.Properties["EditSettings"].SetValue(editSettings);
            }
            this.definitionsProvider.Properties["CurrentColumn"].SetValue(filterColumn);
        }

        public override void GenerateEditorFromResources(IEdmPropertyInfo property, object resourceKey, EditorsGeneratorBase.Initializer initializer)
        {
            object resourceContent = GetResourceContent<PropertyDescription, BaseEditSettings, BaseEdit>(GetResourceTemplate(this.ownerElement, resourceKey));
            if (!(resourceContent is BaseEditSettings))
            {
                this.GenerateEditor(property, null, initializer);
            }
            else
            {
                IModelItem filterColumn = this.definitionsProvider.Context.CreateItem(typeof(PropertyDescription));
                this.GenerateEditor(property, filterColumn, new RuntimeEditingContext(resourceContent, null).GetRoot(), initializer);
            }
        }

        protected bool GetIsStandardValuesExclusive()
        {
            throw new InvalidOperationException();
        }

        protected bool GetIsStandardValuesSupported() => 
            false;

        protected override Type GetLookUpEditType() => 
            null;

        protected object GetStandardValues()
        {
            throw new InvalidOperationException();
        }

        public override void LookUp(IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo)
        {
            this.Object(property);
        }

        protected internal override EditorsGeneratorBase.Initializer LookUpInitializer(IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo) => 
            this.ObjectInitializer(property);

        public override void Text(IEdmPropertyInfo property, bool multiline)
        {
            if (multiline || !this.GetIsStandardValuesSupported())
            {
                base.Text(property, multiline);
            }
            else
            {
                int? maxLength = null;
                this.GenerateEditor(property, typeof(ComboBoxEditSettings), this.TextInitializer(property, multiline, maxLength));
            }
        }

        protected internal override EditorsGeneratorBase.Initializer TextInitializer(IEdmPropertyInfo property, bool multiline, int? maxLength = new int?()) => 
            (multiline || !this.GetIsStandardValuesSupported()) ? base.TextInitializer(property, multiline, maxLength) : new EditorsGeneratorBase.Initializer(delegate (IModelItem container, IModelItem edit) {
                edit.SetValue(LookUpEditSettingsBase.ItemsSourceProperty, this.GetStandardValues(), true);
                edit.SetValueIfNotSet(ButtonEditSettings.IsTextEditableProperty, !this.GetIsStandardValuesExclusive(), true);
                if (property.HasNullableType())
                {
                    edit.SetValueIfNotSet(ButtonEditSettings.NullValueButtonPlacementProperty, EditorPlacement.EditBox, true);
                }
            }, null);

        public override EditorsGeneratorBase.EditorsGeneratorTarget Target =>
            EditorsGeneratorBase.EditorsGeneratorTarget.Unknown;

        protected override EditorsGeneratorBase.EditorsGeneratorMode Mode =>
            EditorsGeneratorBase.EditorsGeneratorMode.EditSettings;
    }
}

