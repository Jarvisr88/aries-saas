namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class TextInputAutoSuggestSettings : TextInputAutoCompleteSettingsBase
    {
        public TextInputAutoSuggestSettings(AutoSuggestEdit editor) : base(editor)
        {
        }

        protected override object GetEditValueForSyncWithEditor()
        {
            object editValueForSyncWithEditor = base.GetEditValueForSyncWithEditor();
            this.OwnerEdit.RaiseQuerySubmitted(editValueForSyncWithEditor);
            LookUpEditableItem item1 = new LookUpEditableItem();
            item1.DisplayValue = editValueForSyncWithEditor;
            item1.EditValue = editValueForSyncWithEditor;
            return item1;
        }

        protected internal override object ProvideEditValue(object editValue, UpdateEditorSource updateSource)
        {
            LookUpEditableItem item = editValue as LookUpEditableItem;
            if (item == null)
            {
                if ((updateSource == UpdateEditorSource.TextInput) || ((updateSource == UpdateEditorSource.ValueChanging) || ((updateSource == UpdateEditorSource.EnterKeyPressed) || (updateSource == UpdateEditorSource.LostFocus))))
                {
                    this.OwnerEdit.RaiseTextChanged(editValue?.ToString(), AutoSuggestEditChangeTextReason.ValueChanged);
                }
                return editValue;
            }
            if (!item.Completed)
            {
                AutoSuggestEditChangeTextReason reason = item.AcceptedFromPopup ? AutoSuggestEditChangeTextReason.ChosenFromPopup : AutoSuggestEditChangeTextReason.TextInput;
                this.OwnerEdit.RaiseTextChanged(item.DisplayValue?.ToString(), reason);
                item.Completed = true;
            }
            return item.EditValue;
        }

        private AutoSuggestEdit OwnerEdit =>
            base.OwnerEdit as AutoSuggestEdit;
    }
}

