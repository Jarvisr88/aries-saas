namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ValueContainerService : BaseEditBaseService
    {
        public ValueContainerService(BaseEdit editor) : base(editor)
        {
            this.<ValueContainer>k__BackingField = new EditValueContainer(editor);
        }

        public virtual object ConvertEditTextToEditValueCandidate(object editValue) => 
            editValue;

        public virtual object ConvertEditValueForFormatDisplayText(object convertedValue) => 
            convertedValue;

        public void FlushEditValue()
        {
            this.ValueContainer.FlushEditValue();
        }

        public void FlushEditValueCandidate(object editValue, UpdateEditorSource updateSource)
        {
            this.ValueContainer.FlushEditValueCandidate(editValue, updateSource);
        }

        public virtual object ProcessConversion(object value, UpdateEditorSource updateSource) => 
            this.Validator.ProcessConversion(value, updateSource);

        public virtual bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource) => 
            this.ValueContainer.ProvideEditValue(value, out provideValue, updateSource);

        public void Reset()
        {
            this.ValueContainer.Reset();
        }

        public void ResetUpdateSource()
        {
            this.ValueContainer.ResetUpdateSource();
        }

        public virtual void SetEditValue(object editValue, UpdateEditorSource updateSource)
        {
            this.ValueContainer.SetEditValue(editValue, updateSource);
        }

        public void UndoTempValue()
        {
            this.ValueContainer.UndoTempValue();
        }

        public void UpdatePostMode()
        {
            this.ValueContainer.UpdatePostMode();
        }

        public object EditValue =>
            this.ValueContainer.EditValue;

        public bool HasValueCandidate =>
            this.ValueContainer.HasValueCandidate;

        public bool IsLockedByValueChanging =>
            this.ValueContainer.IsLockedByValueChanging;

        public bool IsPostponedValueChanging =>
            this.ValueContainer.IsPostponedValueChanging;

        public UpdateEditorSource UpdateSource =>
            this.ValueContainer.UpdateSource;

        private EditValueContainer ValueContainer { get; }

        private EditorSpecificValidator Validator =>
            base.PropertyProvider.GetService<EditorSpecificValidator>();

        public bool HasTempValue =>
            this.ValueContainer.HasTempValue;
    }
}

