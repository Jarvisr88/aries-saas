namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Mask;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class TextInputMaskSettings : TextInputSettingsBase, IMaskManagerProvider
    {
        private readonly Locker selectionLocker;
        private readonly Locker maskSelectionLocker;

        public TextInputMaskSettings(TextEditBase editor) : base(editor)
        {
            this.selectionLocker = new Locker();
            this.maskSelectionLocker = new Locker();
        }

        [DebuggerHidden, CompilerGenerated]
        private void <>n__0(int selectionStart, int selectionLength)
        {
            base.Select(selectionStart, selectionLength);
        }

        protected internal override void AssignProperties()
        {
            base.AssignProperties();
            this.UpdateMaskManager();
        }

        protected internal override bool CanUndo() => 
            this.MaskManager.CanUndo;

        protected internal virtual DevExpress.Data.Mask.MaskManager CreateDefaultMaskManager()
        {
            TimeSpan span;
            CultureInfo maskCulture = this.OwnerEdit.MaskCulture;
            maskCulture ??= CultureInfo.CurrentCulture;
            string mask = this.OwnerEdit.Mask;
            mask ??= string.Empty;
            switch (this.OwnerEdit.MaskType)
            {
                case MaskType.DateTime:
                    return new DateTimeMaskManager(mask, false, maskCulture, this.OwnerEdit.AllowNullInput);

                case MaskType.DateTimeAdvancingCaret:
                    return new DateTimeMaskManager(mask, true, maskCulture, this.OwnerEdit.AllowNullInput);

                case MaskType.Numeric:
                    return new NumericMaskManager(mask, maskCulture, this.OwnerEdit.AllowNullInput, new bool?(!this.PropertyProvider.AlwaysShowDecimalSeparator));

                case MaskType.RegEx:
                    if (this.OwnerEdit.MaskIgnoreBlank && (mask.Length > 0))
                    {
                        mask = "(" + mask + ")?";
                    }
                    return new RegExpMaskManager(mask, false, this.OwnerEdit.MaskAutoComplete != AutoCompleteType.None, this.OwnerEdit.MaskAutoComplete == AutoCompleteType.Optimistic, this.OwnerEdit.MaskShowPlaceHolders, this.OwnerEdit.MaskPlaceHolder, maskCulture);

                case MaskType.Regular:
                    return new LegacyMaskManager(LegacyMaskInfo.GetRegularMaskInfo(mask, maskCulture), this.OwnerEdit.MaskPlaceHolder, this.OwnerEdit.MaskSaveLiteral, this.OwnerEdit.MaskIgnoreBlank);

                case MaskType.Simple:
                    return new LegacyMaskManager(LegacyMaskInfo.GetSimpleMaskInfo(mask, maskCulture), this.OwnerEdit.MaskPlaceHolder, this.OwnerEdit.MaskSaveLiteral, this.OwnerEdit.MaskIgnoreBlank);

                case MaskType.TimeSpan:
                {
                    TimeSpan local3;
                    TimeSpanCultureInfoBase base1 = maskCulture as TimeSpanCultureInfoBase;
                    TimeSpanCultureInfoBase cultureInfo = base1;
                    if (base1 == null)
                    {
                        TimeSpanCultureInfoBase local1 = base1;
                        cultureInfo = new TimeSpanCultureInfo(maskCulture.Name);
                    }
                    if (TimeSpanMaskGanttOptions.GetDayDuration(this.OwnerEdit) != null)
                    {
                        local3 = TimeSpanMaskGanttOptions.GetDayDuration(this.OwnerEdit).Value;
                    }
                    else
                    {
                        span = new TimeSpan();
                        local3 = span;
                    }
                    return new TimeSpanMaskManager(mask, false, cultureInfo, this.OwnerEdit.AllowNullInput, TimeSpanMaskOptions.GetAssignValueToEnteredLiteral(this.OwnerEdit), TimeSpanMaskOptions.GetChangeNextPartOnCycleValueChange(this.OwnerEdit), (TimeSpanMaskInputMode) TimeSpanMaskOptions.GetInputMode(this.OwnerEdit), (TimeSpanMaskPart) TimeSpanMaskOptions.GetDefaultPart(this.OwnerEdit), TimeSpanMaskOptions.GetAllowNegativeValue(this.OwnerEdit), local3);
                }
                case MaskType.TimeSpanAdvancingCaret:
                {
                    TimeSpan local4;
                    TimeSpanCultureInfoBase base2 = maskCulture as TimeSpanCultureInfoBase;
                    TimeSpanCultureInfoBase cultureInfo = base2;
                    if (base2 == null)
                    {
                        TimeSpanCultureInfoBase local2 = base2;
                        cultureInfo = new TimeSpanCultureInfo(maskCulture.Name);
                    }
                    if (TimeSpanMaskGanttOptions.GetDayDuration(this.OwnerEdit) != null)
                    {
                        local4 = TimeSpanMaskGanttOptions.GetDayDuration(this.OwnerEdit).Value;
                    }
                    else
                    {
                        span = new TimeSpan();
                        local4 = span;
                    }
                    return new TimeSpanMaskManager(mask, true, cultureInfo, this.OwnerEdit.AllowNullInput, TimeSpanMaskOptions.GetAssignValueToEnteredLiteral(this.OwnerEdit), TimeSpanMaskOptions.GetChangeNextPartOnCycleValueChange(this.OwnerEdit), (TimeSpanMaskInputMode) TimeSpanMaskOptions.GetInputMode(this.OwnerEdit), (TimeSpanMaskPart) TimeSpanMaskOptions.GetDefaultPart(this.OwnerEdit), TimeSpanMaskOptions.GetAllowNegativeValue(this.OwnerEdit), local4);
                }
            }
            return null;
        }

        protected internal override void Cut()
        {
            if (this.CanCut())
            {
                this.Copy();
                bool flag = this.MaskManager.Delete();
                this.UpdateEditor(flag && !this.IsInLocalEditAction, UpdateEditorSource.TextInput);
                if (!flag && this.OwnerEdit.MaskBeepOnError)
                {
                    BeepOnErrorHelper.Process();
                }
            }
        }

        protected internal override void Delete()
        {
            if (this.CanDelete())
            {
                this.PerformDelete();
                this.EditStrategy.UpdateDisplayText();
            }
        }

        DevExpress.Data.Mask.MaskManager IMaskManagerProvider.CreateNew() => 
            this.CreateDefaultMaskManager();

        void IMaskManagerProvider.LocalEditActionPerformed()
        {
            this.LocalEditActionPerformed();
        }

        void IMaskManagerProvider.SetMaskManagerValue(object editValue)
        {
            this.SetMaskManagerValue(editValue);
        }

        void IMaskManagerProvider.UpdateRequired()
        {
            this.UpdateRequired();
        }

        protected internal override void FlushPendingEditActions(UpdateEditorSource updateEditor)
        {
            base.FlushPendingEditActions(updateEditor);
            this.MaskManager.FlushPendingEditActions();
            this.UpdateEditor(this.IsInLocalEditAction || this.IsUpdateRequired, updateEditor);
            this.IsInLocalEditAction = false;
            this.IsUpdateRequired = false;
        }

        protected internal override string FormatDisplayText(object editValue, bool applyFormatting) => 
            this.ShouldUseFormatting ? this.MaskManager.DisplayText : base.FormatDisplayText(editValue, applyFormatting);

        protected override object GetEditValueForSyncWithEditor()
        {
            object editValueForSyncWithEditor = base.GetEditValueForSyncWithEditor();
            if (Equals(editValueForSyncWithEditor, this.MaskManager.GetCurrentEditValue()))
            {
                return editValueForSyncWithEditor;
            }
            this.MaskManager.SetInitialEditValue(editValueForSyncWithEditor);
            return this.MaskManager.GetCurrentEditValue();
        }

        private bool GetUseDisplayTextFromMask() => 
            (this.OwnerEdit.FocusManagement.IsFocusWithin && base.EditingSettings.AllowKeyHandling) ? this.GetUseDisplayTextFromMaskFocused() : this.GetUseDisplayTextFromMaskUnfocused();

        protected virtual bool GetUseDisplayTextFromMaskFocused() => 
            !this.RangeEditorService.ShouldRoundToBounds ? (this.IsInLocalEditAction || (this.IsUpdateRequired || ((base.EditingSettings.AllowEditing || !this.EditStrategy.ShouldShowNullTextInternal(this.MaskManager.GetCurrentEditValue())) ? (!this.OwnerEdit.AllowNullInput || !this.EditStrategy.ShouldShowEmptyTextInternal(this.MaskManager.GetCurrentEditValue())) : false))) : true;

        protected virtual bool GetUseDisplayTextFromMaskUnfocused() => 
            this.OwnerEdit.MaskUseAsDisplayFormat && !this.EditStrategy.IsNullValue(base.ValueContainer.EditValue);

        protected internal override void InsertText(string text)
        {
            bool flag = this.MaskManager.Insert(text);
            this.UpdateEditor(flag && !this.IsInLocalEditAction, UpdateEditorSource.TextInput);
            if (!flag && this.OwnerEdit.MaskBeepOnError)
            {
                BeepOnErrorHelper.Process();
            }
        }

        protected override bool IsProperSelectionLength() => 
            !this.maskSelectionLocker.IsLocked;

        protected internal override bool IsValueValid(object value) => 
            !this.IsPlaceHoldersSupport || this.MaskManager.IsMatch;

        private void LocalEditActionPerformed()
        {
            this.IsInLocalEditAction = true;
            this.IsUpdateRequired = false;
            this.ValueChangingService.SetIsValueChanged(true);
        }

        protected virtual void MaskPropertyChanged()
        {
            this.UpdateMaskManager();
        }

        protected virtual void MaskTypePropertyChanged()
        {
        }

        protected internal override bool NeedsKey(Key key, ModifierKeys modifier) => 
            (key != Key.Next) && ((key != Key.Prior) && (((key == Key.Left) || (key == Key.Home)) ? base.NeedsNavigateKeyLeftRight(key, modifier, () => this.MaskManager.CheckCursorLeft()) : (((key == Key.Right) || (key == Key.End)) ? base.NeedsNavigateKeyLeftRight(key, modifier, () => this.MaskManager.CheckCursorRight()) : (((key == Key.Up) || (key == Key.Down)) ? base.NeedsNavigateUpDown(key, modifier, () => ModifierKeysHelper.ContainsModifiers(modifier)) : true))));

        protected internal override void Paste()
        {
            if (this.CanPaste())
            {
                this.InsertText(DXClipboard.GetText());
            }
        }

        private void PerformBackspace()
        {
            if (base.EditingSettings.AllowEditing)
            {
                bool update = this.MaskManager.Backspace();
                this.UpdateEditor(update, UpdateEditorSource.TextInput);
            }
        }

        internal void PerformDelete()
        {
            if (base.EditingSettings.AllowEditing)
            {
                bool update = this.MaskManager.Delete();
                this.UpdateEditor(update, UpdateEditorSource.TextInput);
                if (!update && this.OwnerEdit.MaskBeepOnError)
                {
                    BeepOnErrorHelper.Process();
                }
            }
        }

        protected internal override void PerformGotFocus()
        {
            base.PerformGotFocus();
            this.MaskManager.GotFocus();
            this.SetMaskManagerValue(base.ValueContainer.EditValue);
            this.EditStrategy.UpdateDisplayText();
        }

        protected internal override void PerformLostFocus()
        {
            base.PerformLostFocus();
            this.MaskManager.LostFocus();
            this.FlushPendingEditActions(UpdateEditorSource.LostFocus);
            this.maskSelectionLocker.Unlock();
        }

        protected internal override void PerformNullInput()
        {
            if (base.EditingSettings.AllowEditing)
            {
                this.SetMaskManagerValue(this.OwnerEdit.NullValue);
                this.UpdateEditValue(UpdateEditorSource.ValueChanging);
                this.MaskManager.SelectAll();
                this.IsInLocalEditAction = false;
                this.IsUpdateRequired = false;
                this.EditStrategy.UpdateDisplayText();
            }
        }

        protected internal override object ProcessConversion(object value, UpdateEditorSource updateSource)
        {
            if (this.ShouldUpdateDateTimeKindInDateTimeMask(value))
            {
                value = new DateTime(((DateTime) value).Ticks, this.PropertyProvider.DateTimeKind);
            }
            return base.ProcessConversion(value, updateSource);
        }

        protected internal override void ProcessPreviewKeyDown(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDown(e);
            if (base.EditingSettings.AllowKeyHandling && !e.Handled)
            {
                bool? nullable = null;
                if ((e.Key == Key.Prior) || (e.Key == Key.Home))
                {
                    this.MaskManager.CursorHome(ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                    this.UpdateEditor(this.IsUpdateRequired, UpdateEditorSource.TextInput);
                    nullable = true;
                }
                if ((e.Key == Key.Next) || (e.Key == Key.End))
                {
                    this.MaskManager.CursorEnd(ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                    this.UpdateEditor(this.IsUpdateRequired, UpdateEditorSource.TextInput);
                    nullable = true;
                }
                if (e.Key == Key.Right)
                {
                    this.MaskManager.CursorRight(ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                    this.UpdateEditor(this.IsUpdateRequired, UpdateEditorSource.TextInput);
                    nullable = true;
                }
                if (e.Key == Key.Left)
                {
                    this.MaskManager.CursorLeft(ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)));
                    this.UpdateEditor(this.IsUpdateRequired, UpdateEditorSource.TextInput);
                    nullable = true;
                }
                if (e.Key == Key.Back)
                {
                    this.PerformBackspace();
                    nullable = true;
                }
                if ((e.Key == Key.Delete) && !this.EditStrategy.ShouldProcessNullInput(e))
                {
                    this.PerformDelete();
                    nullable = true;
                }
                if ((e.Key == Key.A) && (ModifierKeysHelper.IsOnlyCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && !ModifierKeysHelper.IsAltPressed(ModifierKeysHelper.GetKeyboardModifiers(e))))
                {
                    this.EditStrategy.SelectAll();
                    nullable = true;
                }
                if ((e.Key == Key.Space) && base.EditingSettings.AllowEditing)
                {
                    this.InsertText(" ");
                    this.EditStrategy.UpdateDisplayText();
                    nullable = true;
                }
                if ((e.Key == Key.Return) && this.OwnerEdit.AcceptsReturn)
                {
                    nullable = true;
                }
                if (nullable != null)
                {
                    e.Handled = true;
                }
            }
        }

        protected internal override void ProcessPreviewMouseDown(MouseEventArgs e)
        {
            base.ProcessPreviewMouseDown(e);
            int characterIndexFromPoint = base.EditBox.GetCharacterIndexFromPoint(e.GetPosition(this.OwnerEdit.EditCore), true);
            if (characterIndexFromPoint >= 0)
            {
                if (!ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers()))
                {
                    base.EditBox.CaretIndex = characterIndexFromPoint;
                }
                else
                {
                    int num2 = base.EditBox.CaretIndex - characterIndexFromPoint;
                    int start = Math.Min(characterIndexFromPoint, (num2 > 0) ? (base.EditBox.SelectionStart + base.EditBox.SelectionLength) : base.EditBox.SelectionStart);
                    base.EditBox.Select(start, (num2 > 0) ? (num2 + base.EditBox.SelectionLength) : Math.Abs(num2));
                    e.Handled = true;
                }
                if (this.IsInLocalEditAction)
                {
                    this.FlushPendingEditActions(UpdateEditorSource.TextInput);
                }
                this.MaskManager.CursorToDisplayPosition(characterIndexFromPoint, false);
            }
        }

        protected internal override void ProcessPreviewMouseUp(MouseEventArgs e)
        {
            base.ProcessPreviewMouseUp(e);
            if (base.EditBox.SelectionLength > 0)
            {
                this.maskSelectionLocker.LockOnce();
            }
            this.Select(base.EditBox.SelectionStart, base.EditBox.SelectionLength);
            this.EditStrategy.UpdateDisplayText();
        }

        protected internal override void ProcessPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.ProcessPreviewMouseWheel(e);
            this.EditStrategy.UpdateDisplayText();
        }

        protected internal override void ProcessPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!TextEditStrategyTextInputHelper.ShouldIgnoreTextInput(e.Text) && base.EditingSettings.AllowEditing)
            {
                base.ProcessPreviewTextInput(e);
                this.InsertText(e.Text);
                e.Handled = true;
            }
        }

        protected internal override void ProcessSyncWithEditor()
        {
            base.ProcessSyncWithEditor();
            this.EditStrategy.UpdateDisplayText();
            this.Reset();
        }

        protected internal override object ProvideEditValue(object editValue, UpdateEditorSource updateSource) => 
            !this.ShouldUpdateDateTimeKindInDateTimeMask(editValue) ? base.ProvideEditValue(editValue, updateSource) : new DateTime(((DateTime) editValue).Ticks, this.PropertyProvider.DateTimeKind);

        protected internal override void Reset()
        {
            base.Reset();
            this.IsUpdateRequired = false;
            this.IsInLocalEditAction = false;
        }

        protected internal override void Select(int selectionStart, int selectionLength)
        {
            this.selectionLocker.DoLockedActionIfNotLocked(delegate {
                this.<>n__0(selectionStart, selectionLength);
                if (this.IsSelectAll)
                {
                    this.MaskManager.SelectAll();
                }
                else
                {
                    this.MaskManager.CursorToDisplayPosition(selectionStart, false);
                    this.MaskManager.CursorToDisplayPosition(selectionStart + selectionLength, true);
                    this.UpdateEditor(false, UpdateEditorSource.TextInput);
                    if (this.IsSelectAll)
                    {
                        this.MaskManager.SelectAll();
                    }
                }
            });
        }

        protected internal override void SetInitialEditValue(object editValue)
        {
            if (Equals(this.MaskManager.GetCurrentEditValue(), editValue))
            {
                base.SetInitialEditValue(editValue);
            }
            else
            {
                this.SetMaskManagerValue(editValue);
                this.UpdateEditor(false, UpdateEditorSource.TextInput);
            }
        }

        protected virtual void SetMaskManagerValue(object value)
        {
            object maskValue = this.EditStrategy.ConvertToBaseValue(value);
            try
            {
                if (this.RangeEditorService.ShouldRoundToBounds && !this.OwnerEdit.AllowNullInput)
                {
                    maskValue = this.RangeEditorService.CorrectToBounds(maskValue);
                }
                this.MaskManager.SetInitialEditValue(maskValue);
            }
            catch (Exception)
            {
                if (this.OwnerEdit.EditMode == EditMode.Standalone)
                {
                    throw;
                }
            }
        }

        private bool ShouldImmediatellyRound(UpdateEditorSource updateSource) => 
            this.OwnerEdit.CausesValidation ? (((updateSource != UpdateEditorSource.TextInput) || !this.OwnerEdit.ValidateOnTextInput) ? (((updateSource != UpdateEditorSource.EnterKeyPressed) || !this.OwnerEdit.ValidateOnEnterKeyPressed) ? ((updateSource == UpdateEditorSource.LostFocus) || ((updateSource == UpdateEditorSource.ValueChanging) || (updateSource == UpdateEditorSource.DoValidate))) : true) : true) : false;

        private bool ShouldUpdateDateTimeKindInDateTimeMask(object value) => 
            (value is DateTime) ? (this.PropertyProvider.IsDateTimeKindAssigned ? ((((DateTime) value).Kind != this.PropertyProvider.DateTimeKind) ? ((this.OwnerEdit.MaskType == MaskType.DateTime) || (this.OwnerEdit.MaskType == MaskType.DateTimeAdvancingCaret)) : false) : false) : false;

        protected internal override bool SpinDown()
        {
            if (!this.SpinDownInternal())
            {
                return false;
            }
            this.UpdateEditor(!this.IsInLocalEditAction, UpdateEditorSource.TextInput);
            return true;
        }

        protected virtual bool SpinDownInternal() => 
            this.RangeEditorService.SpinDown(base.ValueContainer.EditValue, this);

        protected internal override bool SpinUp()
        {
            if (!this.SpinUpInternal())
            {
                return false;
            }
            this.UpdateEditor(!this.IsInLocalEditAction, UpdateEditorSource.TextInput);
            return true;
        }

        protected virtual bool SpinUpInternal() => 
            this.RangeEditorService.SpinUp(base.ValueContainer.EditValue, this);

        protected internal override void Undo()
        {
            if (this.MaskManager.Undo())
            {
                this.UpdateEditor(true, UpdateEditorSource.TextInput);
            }
            else if (this.OwnerEdit.MaskBeepOnError)
            {
                BeepOnErrorHelper.Process();
            }
        }

        private void UpdateEditor(bool update, UpdateEditorSource updateSource)
        {
            if (update)
            {
                this.UpdateEditValue(updateSource);
                this.IsUpdateRequired = false;
                this.IsInLocalEditAction = false;
            }
            this.EditStrategy.UpdateDisplayText();
        }

        protected override void UpdateEditValueInternal(UpdateEditorSource updateSource)
        {
            object currentEditValue = this.MaskManager.GetCurrentEditValue();
            if (!this.RangeEditorService.ShouldRoundToBounds || (this.RangeEditorService.InRange(currentEditValue) || !this.ShouldImmediatellyRound(updateSource)))
            {
                base.ValueContainer.SetEditValue(currentEditValue, updateSource);
            }
            else
            {
                object initialEditValue = this.RangeEditorService.CorrectToBounds(currentEditValue);
                this.MaskManager.SetInitialEditValue(initialEditValue);
                base.ValueContainer.SetEditValue(initialEditValue, updateSource);
            }
        }

        protected internal override void UpdateIme()
        {
            base.UpdateIme();
            this.OwnerEdit.Do<TextEdit>(x => base.EditBox.IsImeEnabled(false));
        }

        protected internal void UpdateMaskManager()
        {
            this.MaskManager = new WpfMaskManager(this);
            this.MaskManager.Initialize();
            this.SetMaskManagerValue(base.ValueContainer.EditValue);
        }

        internal void UpdateRequired()
        {
            this.IsUpdateRequired = true;
            this.IsInLocalEditAction = false;
            this.ValueChangingService.SetIsValueChanged(true);
        }

        protected internal WpfMaskManager MaskManager { get; set; }

        private bool IsPlaceHoldersSupport =>
            (this.OwnerEdit.MaskType == MaskType.RegEx) || ((this.OwnerEdit.MaskType == MaskType.Regular) || (this.OwnerEdit.MaskType == MaskType.Simple));

        private DevExpress.Xpf.Editors.Services.RangeEditorService RangeEditorService =>
            this.OwnerEdit.PropertyProvider.GetService<DevExpress.Xpf.Editors.Services.RangeEditorService>();

        private DevExpress.Xpf.Editors.Services.ValueChangingService ValueChangingService =>
            this.OwnerEdit.PropertyProvider.GetService<DevExpress.Xpf.Editors.Services.ValueChangingService>();

        private TextEditPropertyProvider PropertyProvider =>
            (TextEditPropertyProvider) this.OwnerEdit.PropertyProvider;

        private TextEdit OwnerEdit =>
            (TextEdit) base.OwnerEdit;

        private TextEditStrategy EditStrategy =>
            (TextEditStrategy) this.OwnerEdit.EditStrategy;

        protected internal override int SelectionStart =>
            this.MaskManager.DisplaySelectionStart;

        protected internal override int SelectionLength =>
            this.MaskManager.DisplaySelectionLength;

        protected internal override bool ShouldUseFormatting =>
            this.GetUseDisplayTextFromMask();

        protected internal bool IsUpdateRequired { get; private set; }

        protected internal bool IsInLocalEditAction { get; private set; }

        WpfMaskManager IMaskManagerProvider.Instance =>
            this.MaskManager ?? new WpfMaskManager(this);
    }
}

