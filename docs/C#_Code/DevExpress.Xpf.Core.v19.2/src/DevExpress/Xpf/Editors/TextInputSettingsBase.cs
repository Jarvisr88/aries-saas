namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Input;

    public abstract class TextInputSettingsBase
    {
        private readonly Locker updateTextLocker = new Locker();
        private readonly Locker restoreSelectionLocker = new Locker();

        protected TextInputSettingsBase(TextEditBase editor)
        {
            this.OwnerEdit = editor;
            this.AssignEditorInternal();
        }

        protected virtual void AssignEditorInternal()
        {
            this.AssignProperties();
        }

        protected internal virtual void AssignProperties()
        {
            this.UpdateIme();
        }

        protected internal virtual bool CanCopy() => 
            this.HasSelection;

        protected internal virtual bool CanCut() => 
            this.EditingSettings.AllowEditing && this.HasSelection;

        protected internal virtual bool CanDelete() => 
            this.EditingSettings.AllowEditing && this.HasSelection;

        protected internal virtual bool CanPaste() => 
            this.EditingSettings.AllowEditing && DXClipboard.ContainsText();

        protected internal virtual bool CanProcessKeyUpDown(System.Windows.Input.KeyEventArgs e)
        {
            bool flag = ModifierKeysHelper.ContainsModifiers(ModifierKeysHelper.GetKeyboardModifiers(e));
            return ((this.OwnerEdit.EditMode == EditMode.Standalone) ? !flag : flag);
        }

        protected internal virtual bool CanSelectAll() => 
            this.EditingSettings.AllowKeyHandling;

        protected internal virtual bool CanUndo() => 
            true;

        protected internal virtual object ConvertEditValueForFormatDisplayText(object convertedValue) => 
            convertedValue;

        protected internal virtual void Copy()
        {
            this.EditBox.Copy();
        }

        protected internal virtual EditStrategyBase CreateEditStrategy() => 
            new TextEditStrategy(this.OwnerEdit);

        protected internal virtual void Cut()
        {
        }

        protected internal virtual void Delete()
        {
        }

        protected internal virtual bool DoValidate(object editValue, UpdateEditorSource source) => 
            true;

        protected internal virtual void FlushPendingEditActions(UpdateEditorSource updateSource)
        {
        }

        protected internal virtual string FormatDisplayText(object editValue, bool applyFormatting)
        {
            Func<object, string> evaluator = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Func<object, string> local1 = <>c.<>9__36_0;
                evaluator = <>c.<>9__36_0 = x => x.ToString();
            }
            return this.ConvertEditValueForFormatDisplayText(editValue).Return<object, string>(evaluator, (<>c.<>9__36_1 ??= () => string.Empty));
        }

        protected internal virtual bool GetAcceptsReturn() => 
            false;

        protected virtual object GetDisplayValue(object editValue)
        {
            LookUpEditableItem item = editValue as LookUpEditableItem;
            return (((item != null) || this.EditingSettings.IsInLookUpMode) ? ((item == null) ? editValue : (this.EditingSettings.IsInLookUpMode ? item.DisplayValue : editValue)) : editValue);
        }

        protected virtual object GetEditValueForSyncWithEditor()
        {
            string text = this.EditBox.Text;
            object editValue = (!string.IsNullOrEmpty(text) || !this.EditStrategy.ReplaceTextWithNull(text)) ? text : null;
            return this.ValueContainer.ConvertEditTextToEditValueCandidate(editValue);
        }

        protected internal virtual void InsertText(string value)
        {
        }

        protected virtual bool IsProperSelectionLength() => 
            this.EditBox.SelectionLength <= 0;

        protected internal virtual bool IsValueValid(object value) => 
            true;

        protected internal virtual bool NeedsKey(Key key, ModifierKeys modifier) => 
            false;

        protected bool NeedsNavigateKeyLeftRight(Key key, ModifierKeys modifiers, Func<bool> checkMethod) => 
            (this.OwnerEdit.EditMode == EditMode.InplaceActive) ? (!this.OwnerEdit.InplaceEditing.HandleTextNavigation(key, modifiers) ? (!this.IsSelectAll ? checkMethod() : ModifierKeysHelper.ContainsModifiers(modifiers)) : checkMethod()) : true;

        protected bool NeedsNavigateUpDown(Key key, ModifierKeys modifiers, Func<bool> checkMethod) => 
            (this.OwnerEdit.EditMode == EditMode.InplaceActive) ? (!this.OwnerEdit.InplaceEditing.HandleTextNavigation(key, modifiers) ? checkMethod() : true) : true;

        protected internal virtual void Paste()
        {
        }

        protected internal virtual void PerformGotFocus()
        {
        }

        protected internal virtual void PerformLostFocus()
        {
            this.restoreSelectionLocker.Unlock();
            if (this.IsSelectAll && this.OwnerEdit.SelectAllOnMouseUp)
            {
                this.Select(0, 0);
            }
        }

        protected internal virtual void PerformNullInput()
        {
        }

        protected internal virtual object ProcessConversion(object value, UpdateEditorSource updateSource) => 
            this.PropertyProvider.ValueTypeConverter.ConvertBack(value);

        protected internal virtual void ProcessMouseUp(MouseButtonEventArgs e)
        {
            if (!this.restoreSelectionLocker.IsLocked)
            {
                if (this.OwnerEdit.SelectAllOnMouseUp && this.IsProperSelectionLength())
                {
                    this.OwnerEdit.PerformKeyboardSelectAll();
                }
                this.restoreSelectionLocker.LockOnce();
            }
        }

        protected internal virtual void ProcessPreviewKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (!e.Handled && this.EditingSettings.AllowKeyHandling)
            {
                bool? nullable1;
                if ((e.Key == Key.Return) && this.OwnerEdit.ValidateOnEnterKeyPressed)
                {
                    this.EditStrategy.ValidateOnEnterKeyPressed(e);
                }
                if (e.Key == Key.Escape)
                {
                    nullable1 = this.EditStrategy.RestoreDisplayText();
                }
                else
                {
                    nullable1 = null;
                }
                bool? nullable = nullable1;
                if ((e.Key == Key.Up) && this.CanProcessKeyUpDown(e))
                {
                    nullable = new bool?(this.EditStrategy.PerformSpinUp());
                }
                if ((e.Key == Key.Down) && this.CanProcessKeyUpDown(e))
                {
                    nullable = new bool?(this.EditStrategy.PerformSpinDown());
                }
                if (CapsLockHelper.IsCapsLockToggled)
                {
                    this.EditStrategy.CoerceToolTip();
                }
                if (nullable != null)
                {
                    e.Handled = nullable.Value;
                }
            }
        }

        protected internal virtual void ProcessPreviewMouseDown(System.Windows.Input.MouseEventArgs e)
        {
            if (this.EditStrategy.IsSelectAll)
            {
                this.restoreSelectionLocker.LockOnce();
            }
        }

        protected internal virtual void ProcessPreviewMouseUp(System.Windows.Input.MouseEventArgs e)
        {
        }

        protected internal virtual void ProcessPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if ((!e.Handled && this.AllowSpinOnMouseWheel) && (e.Delta != 0))
            {
                bool flag = false;
                int num2 = Math.Max(1, Math.Abs((int) (e.Delta / SystemInformation.MouseWheelScrollDelta)));
                for (int i = 0; i < num2; i++)
                {
                    flag = (e.Delta <= 0) ? this.EditStrategy.PerformSpinDown() : this.EditStrategy.PerformSpinUp();
                }
                e.Handled = flag;
            }
        }

        protected internal virtual void ProcessPreviewTextInput(TextCompositionEventArgs e)
        {
        }

        protected internal virtual void ProcessSyncWithEditor()
        {
            object editValueForSyncWithEditor = this.GetEditValueForSyncWithEditor();
            this.ValueContainer.SetEditValue(editValueForSyncWithEditor, UpdateEditorSource.TextInput);
        }

        protected internal virtual object ProvideEditValue(object editValue, UpdateEditorSource updateSource)
        {
            LookUpEditableItem item = editValue as LookUpEditableItem;
            return (((item != null) || this.EditingSettings.IsInLookUpMode) ? ((item != null) ? item.EditValue : editValue) : editValue);
        }

        protected internal virtual void Reset()
        {
        }

        protected internal virtual void Select(int selectionStart, int selectionLength)
        {
            this.EditBox.Select(selectionStart, selectionLength);
        }

        protected internal virtual void SetInitialEditValue(object editValue)
        {
            this.EditStrategy.UpdateDisplayText();
        }

        protected internal virtual bool SpinDown() => 
            false;

        protected internal virtual bool SpinUp() => 
            false;

        protected internal virtual void Undo()
        {
        }

        protected virtual void UpdateEditValue(UpdateEditorSource updateSource)
        {
            this.updateTextLocker.DoLockedActionIfNotLocked(() => this.UpdateEditValueInternal(updateSource));
        }

        protected virtual void UpdateEditValueInternal(UpdateEditorSource updateSource)
        {
        }

        protected internal virtual void UpdateIme()
        {
        }

        public bool AllowRejectUnknownValues { get; private set; }

        protected BaseEditingSettingsService EditingSettings =>
            this.PropertyProvider.GetService<BaseEditingSettingsService>();

        protected TextEditBase OwnerEdit { get; private set; }

        protected TextEditStrategy EditStrategy =>
            this.OwnerEdit.EditStrategy as TextEditStrategy;

        protected ValueContainerService ValueContainer =>
            this.PropertyProvider.GetService<ValueContainerService>();

        protected ActualPropertyProvider PropertyProvider =>
            this.OwnerEdit.PropertyProvider;

        protected EditBoxWrapper EditBox =>
            this.OwnerEdit.EditBox;

        protected internal virtual int SelectionStart =>
            this.EditBox.SelectionStart;

        protected internal virtual int SelectionLength =>
            this.EditBox.SelectionLength;

        private bool HasSelection =>
            this.SelectionLength > 0;

        protected internal virtual bool ShouldUseFormatting =>
            false;

        protected bool IsSelectAll =>
            (this.EditBox != null) && (this.EditBox.SelectionLength == this.EditBox.Text.Length);

        protected bool AllowSpinOnMouseWheel
        {
            get
            {
                if (!this.OwnerEdit.FocusManagement.IsFocusWithin)
                {
                    return false;
                }
                Func<TextEdit, bool> evaluator = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Func<TextEdit, bool> local1 = <>c.<>9__30_0;
                    evaluator = <>c.<>9__30_0 = x => x.AllowSpinOnMouseWheel;
                }
                return (this.OwnerEdit as TextEdit).Return<TextEdit, bool>(evaluator, (<>c.<>9__30_1 ??= () => false));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextInputSettingsBase.<>c <>9 = new TextInputSettingsBase.<>c();
            public static Func<TextEdit, bool> <>9__30_0;
            public static Func<bool> <>9__30_1;
            public static Func<object, string> <>9__36_0;
            public static Func<string> <>9__36_1;

            internal string <FormatDisplayText>b__36_0(object x) => 
                x.ToString();

            internal string <FormatDisplayText>b__36_1() => 
                string.Empty;

            internal bool <get_AllowSpinOnMouseWheel>b__30_0(TextEdit x) => 
                x.AllowSpinOnMouseWheel;

            internal bool <get_AllowSpinOnMouseWheel>b__30_1() => 
                false;
        }
    }
}

