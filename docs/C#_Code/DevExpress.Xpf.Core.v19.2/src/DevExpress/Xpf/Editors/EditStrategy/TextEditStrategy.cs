namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class TextEditStrategy : EditStrategyBase
    {
        public TextEditStrategy(TextEditBase editor) : base(editor)
        {
        }

        public override void AfterOnGotFocus()
        {
            base.AfterOnGotFocus();
            this.PerformSelectAllOnGotFocus();
        }

        protected override bool CalcShowNullTextProperties() => 
            !this.PropertyProvider.ShowNullTextIfFocused ? base.CalcShowNullTextProperties() : (base.AllowKeyHandling || base.AllowEditing);

        public virtual bool CanCopy() => 
            this.TextInputService.CanCopy();

        public virtual bool CanCut() => 
            this.TextInputService.CanCut();

        public virtual bool CanDelete() => 
            this.TextInputService.CanDelete();

        public virtual bool CanPaste() => 
            this.TextInputService.CanPaste();

        public virtual bool CanSelectAll() => 
            this.TextInputService.CanSelectAll();

        protected internal virtual bool CanSpinDown() => 
            this.AllowSpin;

        protected internal virtual bool CanSpinUp() => 
            this.AllowSpin;

        public virtual bool CanUndo() => 
            this.TextInputService.CanUndo();

        public virtual void CaretIndexChanged(int value)
        {
            this.TextInputService.Select(value, 0);
        }

        public virtual void Clear()
        {
            base.SetEditValueForce(null);
        }

        protected override void ClearUndoStack()
        {
            base.ClearUndoStack();
            this.EditBox.ClearUndoStack();
        }

        public override string CoerceDisplayText(string displayText)
        {
            string str = base.IsInSupportInitialize ? string.Empty : this.GetDisplayText();
            return base.CoerceDisplayText(str);
        }

        public override object ConvertToBaseValue(object value) => 
            (value == DBNull.Value) ? null : value;

        public virtual void Copy()
        {
            this.TextInputService.Copy();
        }

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new TextEditorValidator(this.Editor);

        protected virtual ItemsProviderService CreateItemsProviderService() => 
            new ItemsProviderService(this.Editor);

        protected virtual PopupService CreatePopupService() => 
            new PopupService(this.Editor);

        protected virtual RangeEditorService CreateRangeEditService() => 
            new RangeEditorService(this.Editor);

        protected virtual TextInputServiceBase CreateTextInputService() => 
            new DevExpress.Xpf.Editors.Services.TextInputService(this.Editor);

        protected override BaseEditingSettingsService CreateTextInputSettingsService() => 
            new TextEditSettingsService(this.Editor);

        protected override ValueContainerService CreateValueContainerService() => 
            new TextInputValueContainerService(this.Editor);

        public virtual void Cut()
        {
            this.TextInputService.Cut();
        }

        public virtual void Delete()
        {
            this.TextInputService.Delete();
        }

        internal virtual void FlushPendingEditActions(UpdateEditorSource updateSource)
        {
            this.TextInputService.FlushPendingEditActions(updateSource);
        }

        public override void FocusEditCore()
        {
            base.FocusEditCore();
            this.PerformSelectAllOnGotFocus();
        }

        protected internal override string FormatDisplayText(object editValue, bool applyFormatting) => 
            !this.TextInputService.ShouldUseFormatting ? base.FormatDisplayText(editValue, applyFormatting) : this.TextInputService.FormatDisplayText(editValue, applyFormatting);

        protected override string FormatDisplayTextInternal(object editValue, bool applyFormatting) => 
            this.ProcessTextWithCharacterCasing(base.FormatDisplayTextInternal(editValue, applyFormatting));

        protected virtual bool GetAllowDropInternal() => 
            this.PropertyProvider.MaskType == MaskType.None;

        protected override object GetEditableObject() => 
            this.EditBox.Text;

        public virtual void HighlightedTextChanged(string text)
        {
            this.Settings.HighlightedText = text;
            this.EditBox.HighlightedText = text;
        }

        public virtual void HighlightedTextCriteriaChanged(HighlightedTextCriteria criteria)
        {
            this.Settings.HighlightedTextCriteria = criteria;
            this.EditBox.HighlightedTextCriteria = criteria;
        }

        public override void Initialize()
        {
            this.TextInputService.Initialize();
        }

        protected override void InitializeServices()
        {
            base.InitializeServices();
            this.PropertyProvider.RegisterService<TextInputServiceBase>(this.CreateTextInputService());
            this.PropertyProvider.RegisterService<RangeEditorService>(this.CreateRangeEditService());
            this.PropertyProvider.RegisterService<ItemsProviderService>(this.CreateItemsProviderService());
            this.PropertyProvider.RegisterService<PopupService>(this.CreatePopupService());
        }

        public override void MouseUp(MouseButtonEventArgs e)
        {
            base.MouseUp(e);
            this.TextInputService.MouseUp(e);
        }

        public virtual bool NeedsKey(Key key, ModifierKeys modifiers) => 
            this.TextInputService.NeedsKey(key, modifiers);

        public override void OnGotFocus()
        {
            base.OnGotFocus();
            this.TextInputService.GotFocus();
        }

        public override void OnLoaded()
        {
            base.OnLoaded();
            this.UpdateDisplayText();
        }

        public override void OnLostFocus()
        {
            base.OnLostFocus();
            this.TextInputService.LostFocus();
        }

        public override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            this.TextInputService.PreviewMouseWheel(e);
        }

        public virtual void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            this.TextInputService.PreviewTextInput(e);
        }

        protected internal virtual void OnTextChanged(string oldText, string text)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(TextEditBase.TextProperty, oldText, text);
            }
        }

        public virtual void Paste()
        {
            this.TextInputService.Paste();
        }

        protected internal virtual void Paste(string text)
        {
        }

        protected override void PerformNullInput()
        {
            base.PerformNullInput();
            this.TextInputService.PerformNullInput();
        }

        protected virtual void PerformSelectAllOnGotFocus()
        {
            if (this.Editor.CanSelectAllOnGotFocus)
            {
                this.Editor.PerformKeyboardSelectAll();
            }
        }

        public bool PerformSpinDown() => 
            this.CanSpinDown() ? (!this.RaiseSpin(false) ? this.SpinDown() : true) : false;

        public bool PerformSpinUp() => 
            this.CanSpinUp() ? (!this.RaiseSpin(true) ? this.SpinUp() : true) : false;

        protected internal override void PrepareForCheckAllowLostKeyboardFocus()
        {
            base.PrepareForCheckAllowLostKeyboardFocus();
            if (this.Editor.EditMode == EditMode.Standalone)
            {
                this.TextInputService.FlushPendingEditActions(UpdateEditorSource.DontValidate);
            }
        }

        public override void PreviewMouseDown(MouseButtonEventArgs e)
        {
            base.PreviewMouseDown(e);
            this.TextInputService.PreviewMouseDown(e);
        }

        public override void PreviewMouseUp(MouseButtonEventArgs e)
        {
            base.PreviewMouseUp(e);
            this.TextInputService.PreviewMouseUp(e);
        }

        public virtual bool ProcessNewValue(string editText) => 
            false;

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDownInternal(e);
            this.TextInputService.PreviewKeyDown(e);
        }

        private string ProcessTextWithCharacterCasing(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            string str = text;
            System.Windows.Controls.CharacterCasing characterCasing = this.CharacterCasing;
            if (characterCasing == System.Windows.Controls.CharacterCasing.Lower)
            {
                str = text.ToLower();
            }
            else if (characterCasing == System.Windows.Controls.CharacterCasing.Upper)
            {
                str = text.ToUpper();
            }
            return str;
        }

        private bool RaiseSpin(bool isUp)
        {
            SpinEventArgs e = new SpinEventArgs(isUp);
            this.Editor.RaiseEvent(e);
            return e.Handled;
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__47_0;
            if (<>c.<>9__47_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__47_0;
                getBaseValueHandler = <>c.<>9__47_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(TextEditBase.TextProperty, getBaseValueHandler, <>c.<>9__47_1 ??= ((PropertyCoercionHandler) (baseValue => ((baseValue != null) ? Convert.ToString(baseValue, CultureInfo.InvariantCulture) : string.Empty))));
        }

        public override void ResetIsValueChanged()
        {
            base.ResetIsValueChanged();
            this.TextInputService.Reset();
        }

        protected override void ResetValidationErrorInternal()
        {
            base.ResetValidationErrorInternal();
            this.TextInputService.SetInitialEditValue(base.ValueContainer.EditValue);
        }

        protected internal virtual bool? RestoreDisplayText()
        {
            if (this.EditBox == null)
            {
                return null;
            }
            CursorPositionSnapshot snapshot = new CursorPositionSnapshot(this.EditBox.SelectionStart, this.EditBox.SelectionLength, this.EditBox.Text, false);
            bool flag = this.ResetValidationError();
            base.DoValidate(UpdateEditorSource.ValueChanging);
            snapshot.ApplyToEdit(this.Editor);
            return new bool?(flag);
        }

        public virtual void Select(int start, int length)
        {
            this.TextInputService.Select(start, length);
        }

        public virtual void SelectAll()
        {
            this.TextInputService.SelectAll();
        }

        public virtual void SetSelectedText(string value)
        {
            this.TextInputService.InsertText(value);
        }

        public virtual void SetSelectionLength(int value)
        {
            this.TextInputService.Select(this.EditBox.SelectionStart, value);
        }

        public virtual void SetSelectionStart(int value)
        {
            this.TextInputService.Select(value, this.EditBox.SelectionLength);
        }

        protected override bool ShouldRestoreCursorPosition() => 
            !((this.TextInputService.ShouldUseFormatting || (this.ApplyDisplayTextConversion || this.IsMultilineText)) || this.PropertyProvider.SuppressFeatures);

        protected virtual bool SkipEditCoreDisplayTextUpdate(string displayText)
        {
            if ((base.IsInSupportInitialize || (!this.Editor.IsPrintingMode && (this.Editor.EditMode == EditMode.InplaceInactive))) || this.Editor.AllowUpdateTextBlockWhenPrinting)
            {
                return false;
            }
            if (this.EditBox.Text == displayText)
            {
                return true;
            }
            int num = !string.IsNullOrEmpty(displayText) ? displayText.Split(Environment.NewLine.ToCharArray()).Length : 0;
            return ((this.EditBox.LineCount == num) && (this.Editor.GetActualTextWrapping() == TextWrapping.NoWrap));
        }

        protected virtual bool SpinDown() => 
            this.TextInputService.SpinDown();

        protected virtual bool SpinUp() => 
            this.TextInputService.SpinUp();

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            this.TextInputService.UpdateIme();
        }

        protected override void SyncWithEditorInternal()
        {
            this.TextInputService.SyncWithEditor();
            this.Editor.IsNullTextVisible = base.ShouldShowNullText;
        }

        protected override void SyncWithValueInternal()
        {
            this.TextInputService.SetInitialEditValue(base.ValueContainer.EditValue);
        }

        public virtual void Undo()
        {
            this.TextInputService.Undo();
        }

        public virtual void UnselectAll()
        {
            if (this.EditBox != null)
            {
                this.EditBox.UnselectAll();
            }
        }

        public override void UpdateAllowDrop(bool isVisible)
        {
            base.UpdateAllowDrop(isVisible);
            this.EditBox.AllowDrop = !isVisible && this.GetAllowDropInternal();
        }

        protected override void UpdateDisplayTextAndRestoreCursorPosition()
        {
            CursorPositionSnapshot snapshot = new CursorPositionSnapshot(this.EditBox.SelectionStart, this.EditBox.SelectionLength, this.EditBox.Text, false);
            this.UpdateDisplayTextInternal();
            snapshot.ApplyToEdit(this.Editor);
        }

        protected override void UpdateEditCoreTextInternal(string displayText)
        {
            if (!this.SkipEditCoreDisplayTextUpdate(displayText))
            {
                this.EditBox.EditValue = displayText;
                if (this.TextInputService.ShouldUseFormatting)
                {
                    this.EditBox.Select(this.TextInputService.SelectionStart, this.TextInputService.SelectionLength);
                }
            }
        }

        protected virtual void UpdateEditorForce(object value)
        {
            if (this.EditBox != null)
            {
                this.EditBox.EditValue = this.GetDisplayText();
            }
        }

        protected internal virtual void ValidateOnEnterKeyPressed(KeyEventArgs e)
        {
            base.DoValidate(UpdateEditorSource.EnterKeyPressed);
            base.ValueContainer.FlushEditValue();
            this.UpdateDisplayText();
        }

        public virtual bool IsInProcessNewValueDialog =>
            false;

        protected bool IsMultilineText =>
            this.Editor.AcceptsReturn;

        protected System.Windows.Controls.CharacterCasing CharacterCasing =>
            this.Editor.GetActualCharactedCasing();

        private TextEditSettings Settings =>
            base.Settings as TextEditSettings;

        protected internal bool IsSelectAll =>
            (this.EditBox != null) && (this.EditBox.SelectionLength == this.EditBox.Text.Length);

        protected override bool IsNullTextSupported =>
            true;

        protected internal virtual bool NeedsEnter =>
            this.TextInputService.AcceptsReturn;

        protected internal virtual bool AllowSpin =>
            this.Editor.IsEnabled && (!this.Editor.IsReadOnly && base.AllowKeyHandling);

        protected TextEditBase Editor =>
            base.Editor as TextEditBase;

        protected EditBoxWrapper EditBox =>
            this.Editor.EditBox;

        protected TextInputServiceBase TextInputService =>
            this.PropertyProvider.GetService<TextInputServiceBase>();

        private TextEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as TextEditPropertyProvider;

        protected IItemsProvider2 ItemsProvider =>
            this.PropertyProvider.GetService<ItemsProviderService>().ItemsProvider;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextEditStrategy.<>c <>9 = new TextEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__47_0;
            public static PropertyCoercionHandler <>9__47_1;

            internal object <RegisterUpdateCallbacks>b__47_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__47_1(object baseValue) => 
                (baseValue != null) ? Convert.ToString(baseValue, CultureInfo.InvariantCulture) : string.Empty;
        }
    }
}

