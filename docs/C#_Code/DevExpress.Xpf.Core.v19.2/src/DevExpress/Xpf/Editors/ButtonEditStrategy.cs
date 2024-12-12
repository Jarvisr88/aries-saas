namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using System;

    public class ButtonEditStrategy : TextEditStrategy
    {
        public ButtonEditStrategy(ButtonEdit editor) : base(editor)
        {
        }

        public override void OnGotFocus()
        {
            base.OnGotFocus();
            this.UpdateNullValueButtonPlacement(this.Editor.ShowNullValueButtonOnFocusOnly);
        }

        public override void OnLostFocus()
        {
            base.OnLostFocus();
            this.UpdateNullValueButtonPlacement(this.Editor.ShowNullValueButtonOnFocusOnly);
        }

        protected override void SyncWithEditorInternal()
        {
            base.SyncWithEditorInternal();
            this.UpdateNullValueButtonPlacement(this.Editor.ShowNullValueButtonOnFocusOnly);
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.UpdateNullValueButtonPlacement(this.Editor.ShowNullValueButtonOnFocusOnly);
        }

        public virtual void UpdateNullValueButtonPlacement(bool showButtonOnFocus)
        {
            this.PropertyProvider.IsNullValueButtonVisible = showButtonOnFocus ? ((this.Editor.FocusManagement.IsFocusWithin && this.AllowTextInput) && !this.IsNullValue(base.ValueContainer.EditValue)) : !this.IsNullValue(base.ValueContainer.EditValue);
        }

        private ButtonEdit Editor =>
            base.Editor as ButtonEdit;

        private ButtonEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as ButtonEditPropertyProvider;
    }
}

