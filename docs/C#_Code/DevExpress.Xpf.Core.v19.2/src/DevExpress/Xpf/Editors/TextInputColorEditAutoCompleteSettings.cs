namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Windows.Media;

    public class TextInputColorEditAutoCompleteSettings : TextInputAutoCompleteSettingsBase
    {
        public TextInputColorEditAutoCompleteSettings(TextEditBase editor) : base(editor)
        {
        }

        protected override object GetEditValueForSyncWithEditor()
        {
            object editValueForSyncWithEditor = base.GetEditValueForSyncWithEditor();
            LookUpEditableItem item1 = new LookUpEditableItem();
            item1.DisplayValue = editValueForSyncWithEditor;
            item1.EditValue = base.OwnerEdit.EditValue;
            return item1;
        }

        protected internal override object ProvideEditValue(object editValue, UpdateEditorSource updateSource)
        {
            Color color;
            return (!Text2ColorHelper.TryConvert(this.GetDisplayValue(editValue), out color) ? base.ProvideEditValue(editValue, updateSource) : new SolidColorBrush(color));
        }
    }
}

