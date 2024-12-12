namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;

    public class TextInputAutoSuggestMaskSettings : TextInputMaskSettings
    {
        public TextInputAutoSuggestMaskSettings(AutoSuggestEdit editor) : base(editor)
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

        protected override void UpdateEditValueInternal(UpdateEditorSource updateSource)
        {
            base.UpdateEditValueInternal(updateSource);
        }

        private AutoSuggestEdit OwnerEdit =>
            base.OwnerEdit as AutoSuggestEdit;
    }
}

