namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Windows.Input;

    public abstract class TextInputServiceBase : BaseEditBaseService
    {
        protected TextInputServiceBase(TextEditBase editor) : base(editor)
        {
        }

        public virtual bool CanCopy() => 
            this.TextInputSettings.CanCopy();

        public virtual bool CanCut() => 
            this.TextInputSettings.CanCut();

        public virtual bool CanDelete() => 
            this.TextInputSettings.CanDelete();

        public virtual bool CanPaste() => 
            this.TextInputSettings.CanPaste();

        public virtual bool CanSelectAll() => 
            this.TextInputSettings.CanSelectAll();

        public virtual bool CanUndo() => 
            this.TextInputSettings.CanUndo();

        public virtual void Copy()
        {
            this.TextInputSettings.Copy();
        }

        public virtual void Cut()
        {
            this.TextInputSettings.Cut();
        }

        public virtual void Delete()
        {
            this.TextInputSettings.Delete();
        }

        public virtual void FlushPendingEditActions(UpdateEditorSource updateSource)
        {
            this.TextInputSettings.FlushPendingEditActions(updateSource);
        }

        public virtual string FormatDisplayText(object editValue, bool applyFormatting) => 
            this.TextInputSettings.FormatDisplayText(editValue, applyFormatting);

        protected virtual bool GetShouldUseFormatting() => 
            false;

        public virtual void GotFocus()
        {
            this.TextInputSettings.PerformGotFocus();
        }

        public virtual void Initialize()
        {
        }

        public virtual void InsertText(string value)
        {
            this.TextInputSettings.InsertText(value);
        }

        public virtual bool IsValueValid(object value) => 
            this.TextInputSettings.IsValueValid(value);

        public virtual void LostFocus()
        {
            this.TextInputSettings.PerformLostFocus();
        }

        public virtual void MouseUp(MouseButtonEventArgs e)
        {
            this.TextInputSettings.ProcessMouseUp(e);
        }

        public virtual bool NeedsKey(Key key, ModifierKeys modifier) => 
            this.TextInputSettings.NeedsKey(key, modifier);

        public void Paste()
        {
            this.TextInputSettings.Paste();
        }

        public virtual void PerformNullInput()
        {
            this.TextInputSettings.PerformNullInput();
        }

        public virtual void PreviewKeyDown(KeyEventArgs e)
        {
            this.TextInputSettings.ProcessPreviewKeyDown(e);
        }

        public virtual void PreviewMouseDown(MouseEventArgs e)
        {
            this.TextInputSettings.ProcessPreviewMouseDown(e);
        }

        public virtual void PreviewMouseUp(MouseEventArgs e)
        {
            this.TextInputSettings.ProcessPreviewMouseUp(e);
        }

        public virtual void PreviewMouseWheel(MouseWheelEventArgs e)
        {
            this.TextInputSettings.ProcessPreviewMouseWheel(e);
        }

        public virtual void PreviewTextInput(TextCompositionEventArgs e)
        {
            this.TextInputSettings.ProcessPreviewTextInput(e);
        }

        public virtual object ProcessConversion(object value, UpdateEditorSource updateEditorSource) => 
            this.TextInputSettings.ProcessConversion(value, updateEditorSource);

        public virtual void Reset()
        {
            this.TextInputSettings.Reset();
        }

        public virtual void Select(int selectionStart, int selectionLength)
        {
            this.TextInputSettings.Select(selectionStart, selectionLength);
        }

        public virtual void SelectAll()
        {
            this.Select(0, this.EditBox.Text.Length);
        }

        public virtual void SetInitialEditValue(object editValue)
        {
            this.TextInputSettings.SetInitialEditValue(editValue);
        }

        public virtual bool SpinDown() => 
            this.TextInputSettings.SpinDown();

        public virtual bool SpinUp() => 
            this.TextInputSettings.SpinUp();

        public virtual void SyncWithEditor()
        {
            this.TextInputSettings.ProcessSyncWithEditor();
        }

        public virtual void Undo()
        {
            this.TextInputSettings.Undo();
        }

        public virtual void UpdateIme()
        {
            this.TextInputSettings.UpdateIme();
        }

        private TextEditPropertyProvider PropertyProvider =>
            (TextEditPropertyProvider) base.PropertyProvider;

        private TextEditBase OwnerEdit =>
            (TextEditBase) base.OwnerEdit;

        private TextInputSettingsBase TextInputSettings =>
            this.PropertyProvider.TextInputSettings;

        protected EditBoxWrapper EditBox =>
            this.OwnerEdit.EditBox;

        public virtual int SelectionStart =>
            this.TextInputSettings.SelectionStart;

        public virtual int SelectionLength =>
            this.TextInputSettings.SelectionLength;

        public virtual bool ShouldUseFormatting =>
            this.TextInputSettings.ShouldUseFormatting;

        public virtual bool AcceptsReturn =>
            this.TextInputSettings.GetAcceptsReturn();
    }
}

