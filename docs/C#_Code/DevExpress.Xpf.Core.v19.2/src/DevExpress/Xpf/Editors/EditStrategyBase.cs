namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public abstract class EditStrategyBase : IEditStrategy
    {
        private readonly Locker editValueLocker = new Locker();
        private readonly Locker editBoxTextLocker = new Locker();
        private readonly Locker coerceValueLocker = new Locker();
        private readonly Locker isInSupportInitialize = new Locker();
        private readonly Locker raiseEventsLocker = new Locker();
        private readonly Locker syncWithEditorLocker = new Locker();
        private FrameworkElement tooltipContent;

        protected EditStrategyBase(BaseEdit editor)
        {
            this.<Editor>k__BackingField = editor;
            this.ValidationState = new DevExpress.Xpf.Editors.ValidationState(this, editor);
            this.<PropertyUpdater>k__BackingField = new PropertyCoercionHelper(this.Editor);
            this.<RemoveNullTextFromUndoStack>k__BackingField = new PostponedAction(() => this.PropertyProvider.IsNullTextVisible);
            this.InitializeServices();
            this.RegisterUpdateCallbacks();
            this.<ApplyStyleSettingsAction>k__BackingField = new PostponedAction(() => this.IsInSupportInitialize);
        }

        protected internal virtual void AddILogicalOwnerChild(object child)
        {
        }

        protected virtual void AfterApplyStyleSettings()
        {
            if ((this.Editor.EditMode == EditMode.Standalone) && this.Editor.IsKeyboardFocusWithin)
            {
                this.Editor.Focus();
            }
        }

        public virtual void AfterOnGotFocus()
        {
            this.RemoveNullTextFromUndoStack.Perform();
        }

        public void ApplyStyleSettings(BaseEditStyleSettings settings)
        {
            this.ApplyStyleSettingsAction.PerformPostpone(delegate {
                this.ApplyStyleSettingsInternal(settings);
                this.AfterApplyStyleSettings();
            });
        }

        protected virtual void ApplyStyleSettingsInternal(BaseEditStyleSettings settings)
        {
            if (settings != null)
            {
                settings.ApplyToEdit(this.Editor);
            }
        }

        protected virtual bool CalcShowNullTextProperties() => 
            this.ApplyDisplayTextConversion;

        protected internal virtual void CancelTextSearch()
        {
        }

        protected virtual void ClearUndoStack()
        {
        }

        private void ClosePreviousToolTip()
        {
            ToolTip toolTip = LayoutHelper.FindLayoutOrVisualParentObject<ToolTip>(this.tooltipContent, false, null);
            if (toolTip != null)
            {
                this.ForceClose(toolTip);
            }
        }

        public virtual object CoerceBaseValidationError(BaseValidationError error) => 
            (this.ValidationState.EditorValidationError == null) ? ((this.Editor.ErrorFromBinding == null) ? error : this.Editor.ErrorFromBinding) : this.ValidationState.EditorValidationError;

        public virtual string CoerceDisplayText(string displayText) => 
            displayText;

        public virtual object CoerceEditValue(object value) => 
            this.CoerceValue(BaseEdit.EditValueProperty, value);

        public virtual object CoerceMaskType(MaskType maskType) => 
            maskType;

        public virtual object CoerceText(string text) => 
            this.CoerceValue(TextEditBase.TextProperty, text);

        public void CoerceToolTip()
        {
            this.Editor.CoerceValue(FrameworkElement.ToolTipProperty);
        }

        public virtual object CoerceValidationToolTip(object tooltip)
        {
            FrameworkElement element = this.CreateHackedContainerForInternalToolTip(tooltip);
            bool flag = true;
            if (this.ShowInternalToolTip())
            {
                element = this.CreateToolTip(this.GetInternalToolTipContent(), this.CreateInternalToolTipAsToolTip(tooltip), new Func<DataTemplate>(this.GetInternalToolTipTemplate));
            }
            if (this.Editor.ShowErrorToolTip)
            {
                if (this.ValidationState.EditorValidationError != null)
                {
                    element = this.CreateToolTip(this.ValidationState.EditorValidationError, false, () => this.Editor.ErrorToolTipContentTemplate);
                }
                else if (this.Editor.ErrorFromBinding != null)
                {
                    element = this.CreateToolTip(this.Editor.ErrorFromBinding, false, () => this.Editor.ErrorToolTipContentTemplate);
                }
                else if (this.Editor.ValidationError != null)
                {
                    element = this.CreateToolTip(this.Editor.ValidationError, false, () => this.Editor.ErrorToolTipContentTemplate);
                }
                else
                {
                    flag = false;
                }
            }
            if (!flag && this.IsTextTrimmed())
            {
                element = this.CreateToolTip(this.GetTrimmedToolTipContent(), tooltip is ToolTip, () => this.Editor.TrimmedTextToolTipContentTemplate);
            }
            this.ClosePreviousToolTip();
            this.tooltipContent = element;
            return element;
        }

        public virtual object CoerceValue(DependencyProperty dp, object value)
        {
            if (this.IsInSupportInitialize)
            {
                this.PropertyUpdater.SetSyncValue(dp, value, SyncValueUpdateMode.Default);
            }
            if (this.ShouldLockUpdate)
            {
                this.PropertyUpdater.SetSyncValue(dp, value, SyncValueUpdateMode.Update);
            }
            return value;
        }

        protected internal virtual object ConvertEditValueForFormatDisplayText(object convertedValue) => 
            this.ValueContainer.ConvertEditValueForFormatDisplayText(convertedValue);

        public virtual object ConvertToBaseValue(object value) => 
            value;

        protected virtual EditorSpecificValidator CreateEditorValidatorService() => 
            new EditorSpecificValidator(this.Editor);

        private FrameworkElement CreateHackedContainerForInternalToolTip(object tooltip)
        {
            if ((tooltip is ToolTip) || (tooltip == null))
            {
                return (FrameworkElement) tooltip;
            }
            ToolTipContentControl control = new ToolTipContentControl();
            FrameworkElement element = tooltip as FrameworkElement;
            ContentControl control2 = ((element != null) ? ((ContentControl) element.Tag) : null) as ContentControl;
            if (control2 != null)
            {
                control2.Content = null;
            }
            control.Content = tooltip;
            if (element != null)
            {
                element.Tag = control;
            }
            return control;
        }

        protected virtual ValueTypeConverter CreateInputTextConverter()
        {
            ValueTypeConverter converter1 = new ValueTypeConverter();
            converter1.TargetType = this.Editor.EditValueType;
            return converter1;
        }

        protected virtual bool CreateInternalToolTipAsToolTip(object tooltip) => 
            tooltip is ToolTip;

        protected virtual string CreateStrategyErrorContent() => 
            string.Empty;

        protected virtual BaseEditingSettingsService CreateTextInputSettingsService() => 
            new BaseEditingSettingsService(this.Editor);

        public virtual FrameworkElement CreateToolTip(object toolTip, bool isToolTip, Func<DataTemplate> getToolTipTemplateHandler) => 
            isToolTip ? this.CreateToolTipCore(toolTip, getToolTipTemplateHandler()) : this.CreateToolTipWithContentControl(toolTip, getToolTipTemplateHandler());

        private FrameworkElement CreateToolTipCore(object toolTip, DataTemplate contentTemplate)
        {
            if (toolTip == null)
            {
                return null;
            }
            ToolTip tip1 = new ToolTip();
            tip1.Content = toolTip;
            tip1.ContentTemplate = contentTemplate;
            return tip1;
        }

        private FrameworkElement CreateToolTipWithContentControl(object toolTip, DataTemplate contentTemplate)
        {
            if (toolTip == null)
            {
                return null;
            }
            ToolTipContentControl control1 = new ToolTipContentControl();
            control1.Content = toolTip;
            control1.ContentTemplate = contentTemplate;
            return control1;
        }

        private BaseValidationError CreateValidationError(ValidationEventArgs args)
        {
            BaseValidationError error = new BaseValidationError(args.ErrorContent, args.Exception, args.ErrorType);
            Func<string, bool> evaluator = <>c.<>9__129_0;
            if (<>c.<>9__129_0 == null)
            {
                Func<string, bool> local1 = <>c.<>9__129_0;
                evaluator = <>c.<>9__129_0 = x => x == "hidden error";
            }
            error.IsHidden = (args.ErrorContent as string).Return<string, bool>(evaluator, <>c.<>9__129_1 ??= () => false);
            return error;
        }

        protected virtual DevExpress.Xpf.Editors.Services.ValidationService CreateValidationService() => 
            new DevExpress.Xpf.Editors.Services.ValidationService(this.Editor);

        protected virtual DevExpress.Xpf.Editors.Services.ValueChangingService CreateValueChangingService() => 
            new DevExpress.Xpf.Editors.Services.ValueChangingService(this.Editor);

        protected virtual ValueContainerService CreateValueContainerService() => 
            new ValueContainerService(this.Editor);

        protected virtual ValueTypeConverter CreateValueTypeConverter()
        {
            ValueTypeConverter converter1 = new ValueTypeConverter();
            converter1.TargetType = this.Editor.EditValueType;
            return converter1;
        }

        public virtual void DisplayTextChanged(string displayText)
        {
            this.UpdateToolTip();
        }

        protected internal virtual bool DoTextSearch(string text, int startIndex, ref object result) => 
            false;

        public bool DoValidate(UpdateEditorSource updateSource)
        {
            this.PrepareForCheckAllowLostKeyboardFocus();
            bool flag = this.DoValidateInternal(this.EditValue, updateSource);
            this.UpdateDisplayText();
            return flag;
        }

        protected internal bool DoValidateInternal(object value, UpdateEditorSource updateSource)
        {
            if (this.PropertyProvider.SuppressFeatures)
            {
                this.UpdateEditValue(value, updateSource, true);
                return true;
            }
            this.ValidationState.TryReset(updateSource);
            if (!this.UpdateSourceValidation(updateSource))
            {
                return false;
            }
            if (!this.ValidationState.IsEmpty)
            {
                return this.ValidationState.IsValid;
            }
            object convertedValue = null;
            BaseValidationError error = this.ProcessValueConversion(value, out convertedValue, updateSource);
            if (error != null)
            {
                this.UpdateErrorProvider(false, error);
                return false;
            }
            ValidationEventArgs args = new ValidationEventArgs(BaseEdit.ValidateEvent, this, convertedValue, CultureInfo.CurrentCulture, updateSource);
            if (!this.Validator.DoValidate(value, convertedValue, updateSource))
            {
                args.IsValid = false;
                args.ErrorContent = this.Validator.GetValidationError();
                this.UpdateErrorProvider(args.IsValid, this.CreateValidationError(args));
                return false;
            }
            this.RaiseValidateEvent(args, updateSource);
            this.UpdateEditValue(convertedValue, updateSource, args.IsValid);
            this.UpdateErrorProvider(args.IsValid, this.CreateValidationError(args));
            return args.IsValid;
        }

        public virtual void EditValueChanged(object oldValue, object newValue)
        {
            this.UpdateEditValue(oldValue, newValue, (value1, value2) => this.SyncWithValue(BaseEdit.EditValueProperty, value1, value2), false);
        }

        public virtual void EditValueConverterChanged(IValueConverter converter)
        {
            this.UpdateEditValue(this.Editor.EditValue, (oldValue, newValue) => this.SyncWithValue(), true);
        }

        public virtual void EditValuePostDelayChanged(int value)
        {
            this.ValueContainer.UpdatePostMode();
        }

        public virtual void EditValuePostModeChanged(PostMode value)
        {
            this.ValueContainer.UpdatePostMode();
        }

        public virtual void EditValueTypeChanged(Type type)
        {
            this.UpdateEditValue(this.Editor.EditValue, (oldValue, newValue) => this.SyncWithValue(), true);
        }

        public virtual void FocusEditCore()
        {
            KeyboardHelper.Focus((UIElement) this.Editor.EditCore);
        }

        private void ForceClose(ToolTip toolTip)
        {
            toolTip.SetCurrentValue(UIElement.VisibilityProperty, Visibility.Collapsed);
            toolTip.IsOpen = false;
            RoutedEventHandler toolTipClosedDelegate = null;
            toolTipClosedDelegate = delegate (object sender, RoutedEventArgs args) {
                toolTip.Closed -= toolTipClosedDelegate;
                toolTip.SetCurrentValue(UIElement.VisibilityProperty, Visibility.Visible);
            };
            toolTip.Closed += toolTipClosedDelegate;
        }

        protected void ForceShowToolTip()
        {
            ToolTip tip = LayoutHelper.FindLayoutOrVisualParentObject<ToolTip>(this.tooltipContent, false, null);
            if (tip != null)
            {
                tip.IsOpen ??= true;
            }
        }

        protected internal void ForceSyncWithValueInternal()
        {
            if (!this.ShouldLockUpdate)
            {
                try
                {
                    this.editValueLocker.Lock();
                    this.SyncWithValueInternal();
                }
                finally
                {
                    this.editValueLocker.Unlock();
                }
            }
        }

        protected virtual string FormatAfterException(string formatString, object editValue) => 
            this.FormatDisplayTextFast(editValue);

        protected internal virtual string FormatDisplayText(object editValue, bool applyFormatting)
        {
            if (this.ShouldApplyNullTextToDisplayText)
            {
                if (this.ShouldGetNullTextAsDisplayText(editValue))
                {
                    return this.Editor.NullText;
                }
                if (this.ShouldShowEmptyTextInternal(editValue))
                {
                    return string.Empty;
                }
            }
            return this.FormatDisplayTextInternal(editValue, applyFormatting);
        }

        protected virtual string FormatDisplayTextFast(object editValue) => 
            (editValue != null) ? editValue.ToString() : string.Empty;

        protected virtual string FormatDisplayTextInternal(object editValue, bool applyFormatting)
        {
            object obj2 = this.ConvertEditValueForFormatDisplayText(editValue);
            return (!this.ShouldFormatDisplayTextFast(applyFormatting) ? this.TryFormatDisplayText(FormatStringConverter.GetDisplayFormat(this.PropertyProvider.DisplayFormatString), obj2) : this.FormatDisplayTextFast(obj2));
        }

        protected virtual object GetDefaultValue() => 
            null;

        protected virtual string GetDisplayText() => 
            this.Editor.GetDisplayText(this.GetValueForDisplayText(), this.ApplyDisplayTextConversion);

        protected virtual object GetEditableObject() => 
            null;

        protected virtual object GetEditValueForEditTextInternal() => 
            null;

        protected virtual object GetEditValueInternal() => 
            this.ValueContainer.EditValue;

        protected virtual object GetInternalToolTipContent() => 
            null;

        protected virtual DataTemplate GetInternalToolTipTemplate() => 
            this.Editor.TrimmedTextToolTipContentTemplate;

        protected virtual object GetNullableValue() => 
            this.Editor.NullValue;

        protected virtual object GetOnInitializedSyncValue() => 
            this.ValueContainer.EditValue;

        protected virtual object GetTrimmedToolTipContent() => 
            new ToolTipContent(this.PropertyProvider.DisplayText);

        protected virtual object GetValueForDisplayText() => 
            this.ValueContainer.EditValue;

        internal bool HasToolTipContent() => 
            this.tooltipContent != null;

        public virtual void Initialize()
        {
        }

        protected virtual void InitializeServices()
        {
            this.PropertyProvider.RegisterService<ValueContainerService>(this.CreateValueContainerService());
            this.PropertyProvider.RegisterService<DevExpress.Xpf.Editors.Services.ValidationService>(this.CreateValidationService());
            this.PropertyProvider.RegisterService<EditorSpecificValidator>(this.CreateEditorValidatorService());
            this.PropertyProvider.RegisterService<BaseEditingSettingsService>(this.CreateTextInputSettingsService());
            this.PropertyProvider.RegisterService<DevExpress.Xpf.Editors.Services.ValueChangingService>(this.CreateValueChangingService());
        }

        protected virtual bool IsComputedNullValue(object value) => 
            !Equals(value, this.PropertyProvider.NullValue) ? (this.PropertyProvider.ShowNullTextForEmptyValue ? BaseEditSettings.IsStringEmpty(value) : false) : true;

        public virtual void IsEditorActiveChanged(bool value)
        {
        }

        protected virtual bool IsNativeNullValue(object value) => 
            BaseEditSettings.IsNativeNullValue(value);

        protected virtual bool IsNullInputActivatingKey(Key key) => 
            (FrenchKeyboardDetector.IsFrenchKeyboard || (GermanKeyboardDetector.IsGermanKeyboard || SwedishKeyboardDetector.IsSwedishKeyboard)) ? (key == Key.Delete) : ((key == Key.Delete) || (key == Key.D0));

        public virtual bool IsNullValue(object value) => 
            this.IsNativeNullValue(value) || this.IsComputedNullValue(value);

        protected internal virtual void IsReadOnlyChanged()
        {
            if (!this.IsInSupportInitialize)
            {
                this.UpdateDisplayText();
            }
        }

        private bool IsTextTrimmed() => 
            (this.Editor.EditCore is TextBlock) ? TextBlockService.GetIsTextTrimmed((TextBlock) this.Editor.EditCore) : false;

        protected void LockRaiseValueChangingEvents()
        {
            this.raiseEventsLocker.Lock();
        }

        public virtual void MouseUp(MouseButtonEventArgs e)
        {
        }

        protected internal virtual bool NeedsEnterKey(ModifierKeys modifiers) => 
            false;

        public virtual void OnGotFocus()
        {
            this.IsValueChanged = false;
            MouseEventLockHelper.SubscribeMouseEvents(this.Editor);
            this.RemoveNullTextFromUndoStack.PerformPostpone(new Action(this.ClearUndoStack));
        }

        public virtual void OnInitialized()
        {
            this.ApplyStyleSettingsAction.Perform();
            this.PerformUpdateValueOnInitialized();
        }

        public void OnIsNullTextVisibleChanged(bool isVisible)
        {
            this.UpdateNullTextForeground(isVisible);
            this.UpdateAllowDrop(isVisible);
        }

        public virtual void OnLoaded()
        {
            this.DoValidate(UpdateEditorSource.ValueChanging);
            this.ApplyStyleSettings(this.StyleSettings);
        }

        public virtual void OnLostFocus()
        {
            if (this.IsValueChanged)
            {
                this.Editor.IsValueChanged = this.IsValueChanged;
                this.IsValueChanged = false;
            }
        }

        public virtual void OnMouseWheel(MouseWheelEventArgs e)
        {
        }

        protected internal virtual void OnNullTextChanged(string nullText)
        {
            this.SyncWithValue();
        }

        protected internal virtual void OnNullValueChanged(object nullValue)
        {
            this.SyncWithValue();
        }

        public virtual void OnPreviewLostFocus(DependencyObject oldFocus, DependencyObject newFocus)
        {
        }

        protected internal virtual void OnUnloaded()
        {
        }

        protected virtual void PerformNullInput()
        {
            this.ValueContainer.SetEditValue(this.Editor.NullValue, UpdateEditorSource.TextInput);
            this.UpdateDisplayText();
        }

        public virtual void PerformUpdateValueOnInitialized()
        {
            this.SyncWithValue();
        }

        protected internal virtual void PrepareForCheckAllowLostKeyboardFocus()
        {
        }

        public virtual void PreviewMouseDown(MouseButtonEventArgs e)
        {
        }

        public virtual void PreviewMouseUp(MouseButtonEventArgs e)
        {
        }

        protected internal virtual void ProcessEditModeChanged(EditMode oldValue, EditMode newValue)
        {
            this.UpdateEditorOnEditingChange(newValue != EditMode.InplaceInactive);
        }

        public void ProcessKeyDown(KeyEventArgs e)
        {
            if (this.AllowKeyHandling)
            {
                this.ProcessKeyDownInternal(e);
            }
        }

        protected virtual void ProcessKeyDownInternal(KeyEventArgs e)
        {
        }

        public void ProcessPreviewKeyDown(KeyEventArgs e)
        {
            if (this.AllowKeyHandling)
            {
                if (!this.ShouldProcessNullInput(e))
                {
                    this.ProcessPreviewKeyDownInternal(e);
                }
                else
                {
                    this.PerformNullInput();
                    e.Handled = true;
                }
            }
        }

        protected virtual void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
        }

        private BaseValidationError ProcessValueConversion(object baseValue, out object convertedValue, UpdateEditorSource updateSource)
        {
            convertedValue = this.ValueContainer.ProcessConversion(baseValue, updateSource);
            return this.PropertyProvider.ValueTypeConverter.ValidationError;
        }

        public virtual bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource) => 
            this.ValueContainer.ProvideEditValue(value, out provideValue, updateSource);

        private void RaiseValidateEvent(ValidationEventArgs args, UpdateEditorSource updateSource)
        {
            if ((updateSource != UpdateEditorSource.DontValidate) && this.Editor.CausesValidation)
            {
                try
                {
                    this.Editor.RaiseEvent(args);
                }
                catch (Exception exception)
                {
                    args.IsValid = false;
                    args.Exception = exception;
                    args.ErrorType = ErrorType.Critical;
                }
            }
        }

        public virtual void RaiseValueChangedEvents(object oldValue, object newValue)
        {
            if (!this.ShouldLockRaiseEvents && !Equals(oldValue, newValue))
            {
                this.Editor.RaiseEvent(new EditValueChangedEventArgs(oldValue, newValue));
            }
        }

        public virtual bool RaiseValueChangingEvents(object oldValue, object newValue)
        {
            if (this.ShouldLockRaiseEvents)
            {
                return true;
            }
            EditValueChangingEventArgs e = new EditValueChangingEventArgs(oldValue, newValue);
            this.Editor.RaiseEvent(e);
            return (e.Handled ? !e.IsCancel : true);
        }

        protected virtual void RegisterUpdateCallbacks()
        {
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__85_0;
            if (<>c.<>9__85_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__85_0;
                getBaseValueHandler = <>c.<>9__85_0 = baseValue => baseValue;
            }
            this.PropertyUpdater.Register(BaseEdit.EditValueProperty, getBaseValueHandler, <>c.<>9__85_1 ??= baseValue => baseValue);
        }

        public virtual void Release()
        {
        }

        protected internal virtual void RemoveILogicalOwnerChild(object child)
        {
        }

        protected internal virtual bool ReplaceTextWithNull(object editValue) => 
            this.IsNullTextSupported && (this.Editor.ShowNullText && (this.IsNullValue(editValue) && (!this.ApplyDisplayTextConversion && !this.ValueContainer.HasValueCandidate)));

        public void ResetErrorProvider()
        {
            this.UpdateErrorRepresentation();
        }

        public virtual void ResetIsValueChanged()
        {
            this.ValueChangingService.SetIsValueChanged(false);
        }

        protected internal virtual void ResetOnValueChanging()
        {
        }

        internal void ResetToolTipContent()
        {
            this.tooltipContent = null;
        }

        public virtual bool ResetValidationError()
        {
            if (!this.ValueContainer.HasValueCandidate || this.IsInPostponedUpdate)
            {
                return false;
            }
            this.ValueContainer.Reset();
            this.ValidationState.Reset();
            this.ResetValidationErrorInternal();
            return true;
        }

        protected virtual void ResetValidationErrorInternal()
        {
        }

        public void SetEditValueForce(object editValue)
        {
            this.ResetValidationError();
            BaseEditHelper.SetCurrentValue(this.Editor, BaseEdit.EditValueProperty, editValue);
        }

        public virtual void SetNullValue(object parameter)
        {
            this.ValueContainer.UndoTempValue();
            this.ValueContainer.SetEditValue(this.Editor.NullValue, UpdateEditorSource.ValueChanging);
            this.UpdateDisplayText();
        }

        protected virtual bool ShouldCoerceToolTip() => 
            TextBlockService.GetIsTextTrimmed((TextBlock) this.Editor.EditCore) || this.ShowInternalToolTip();

        protected virtual bool ShouldFlushEditValue(UpdateEditorSource updateSource, bool isValid) => 
            isValid && ((updateSource == UpdateEditorSource.DoValidate) || ((updateSource == UpdateEditorSource.LostFocus) || (updateSource == UpdateEditorSource.EnterKeyPressed)));

        protected virtual bool ShouldFormatDisplayTextFast(bool applyFormatting) => 
            !applyFormatting || string.IsNullOrEmpty(this.PropertyProvider.DisplayFormatString);

        protected internal virtual bool ShouldGetNullTextAsDisplayText(object editValue) => 
            this.IsNullTextSupported && (this.PropertyProvider.ShowNullText && (this.IsNullValue(editValue) && (this.ApplyDisplayTextConversion && !this.PropertyProvider.HasDisplayTextProviderText)));

        protected internal virtual bool ShouldProcessNullInput(KeyEventArgs e) => 
            this.Editor.AllowNullInput && (!this.Editor.IsReadOnly && (this.AllowKeyHandling && (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && this.IsNullInputActivatingKey(e.Key))));

        public virtual bool ShouldResetValidateState(UpdateEditorSource updateSource) => 
            updateSource != UpdateEditorSource.DontValidate;

        protected virtual bool ShouldRestoreCursorPosition() => 
            false;

        protected internal virtual bool ShouldShowEmptyTextInternal(object editValue) => 
            this.IsNullTextSupported && (this.PropertyProvider.ShowNullText && (this.IsNullValue(editValue) && (!this.ApplyDisplayTextConversion && (!this.ValueContainer.HasValueCandidate && this.ShouldSyncWithEditor))));

        protected internal virtual bool ShouldShowNullTextInternal(object editValue) => 
            this.IsNullTextSupported && (this.PropertyProvider.ShowNullText && (this.IsNullValue(editValue) && (this.CalcShowNullTextProperties() && !this.PropertyProvider.HasDisplayTextProviderText)));

        protected virtual bool ShowInternalToolTip() => 
            false;

        public virtual void StyleSettingsChanged(BaseEditStyleSettings settings)
        {
            this.Editor.Settings.SetCurrentValue(BaseEditSettings.StyleSettingsProperty, this.StyleSettings);
            this.ApplyStyleSettings(this.StyleSettings);
        }

        public void SupportInitializeBeginInit()
        {
            this.isInSupportInitialize.Lock();
            this.LockRaiseValueChangingEvents();
        }

        public void SupportInitializeEndInit()
        {
            this.isInSupportInitialize.Unlock();
            this.OnInitialized();
            this.UnlockRaiseValueChangingEvents();
        }

        public void SyncEditCoreProperties()
        {
            this.SyncEditCorePropertiesInternal();
        }

        protected virtual void SyncEditCorePropertiesInternal()
        {
            this.UpdateDisplayText();
        }

        public void SyncWithEditor()
        {
            if (!this.ShouldLockUpdate && this.ShouldSyncWithEditor)
            {
                try
                {
                    this.editBoxTextLocker.Lock();
                    this.SyncWithEditorInternal();
                }
                finally
                {
                    this.editBoxTextLocker.Unlock();
                }
            }
        }

        protected virtual void SyncWithEditorInternal()
        {
            string editableObject = (string) this.GetEditableObject();
            object editValue = (!string.IsNullOrEmpty(editableObject) || !this.ReplaceTextWithNull(editableObject)) ? editableObject : this.Editor.NullValue;
            editValue = this.ValueContainer.ConvertEditTextToEditValueCandidate(editValue);
            this.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
        }

        public void SyncWithValue()
        {
            if (!this.ShouldLockUpdate)
            {
                try
                {
                    this.editValueLocker.Lock();
                    this.PropertyUpdater.Update();
                    this.SyncWithValueInternal();
                }
                finally
                {
                    this.editValueLocker.Unlock();
                }
            }
        }

        public void SyncWithValue(DependencyProperty dp, object oldValue, object newValue)
        {
            if (!this.ShouldLockUpdate)
            {
                try
                {
                    this.editValueLocker.Lock();
                    this.ResetValidationError();
                    this.PropertyUpdater.Update(dp, oldValue, newValue);
                    this.SyncWithValueInternal();
                }
                finally
                {
                    this.editValueLocker.Unlock();
                }
            }
        }

        protected virtual void SyncWithValueInternal()
        {
            this.UpdateDisplayText();
        }

        public virtual void ThemeChanged()
        {
            this.ApplyStyleSettings(this.StyleSettings);
        }

        private string TryFormatDisplayText(string formatString, object editValue)
        {
            try
            {
                return this.TryFormatDisplayTextInternal(formatString, editValue);
            }
            catch
            {
                return this.FormatAfterException(formatString, editValue);
            }
        }

        protected virtual string TryFormatDisplayTextInternal(string formatString, object editValue)
        {
            if ((editValue is decimal) && ((formatString == "{0:d}") || (formatString == "{0:D}")))
            {
                return editValue.ToString();
            }
            object[] args = new object[] { editValue };
            return string.Format(CultureInfo.CurrentCulture, formatString, args);
        }

        protected void UnlockRaiseValueChangingEvents()
        {
            this.raiseEventsLocker.Unlock();
        }

        public virtual void UpdateAllowDrop(bool isVisible)
        {
        }

        public virtual void UpdateDataContext(DependencyObject target)
        {
            BaseEdit.SetOwnerEdit(target, this.Editor);
        }

        public virtual void UpdateDisplayText()
        {
            if (this.ShouldRestoreCursorPosition())
            {
                this.UpdateDisplayTextAndRestoreCursorPosition();
            }
            else
            {
                this.UpdateDisplayTextInternal();
            }
        }

        protected virtual void UpdateDisplayTextAndRestoreCursorPosition()
        {
            this.UpdateDisplayTextInternal();
        }

        protected virtual void UpdateDisplayTextInternal()
        {
            this.PropertyProvider.SetDisplayText(this.CoerceDisplayText(null));
            this.UpdateEditCoreText(this.PropertyProvider.DisplayText);
            this.Editor.IsNullTextVisible = this.ShouldShowNullText;
            if (!this.PropertyProvider.SuppressFeatures)
            {
                this.Editor.DisplayText = this.PropertyProvider.DisplayText;
            }
        }

        protected virtual void UpdateEditCoreText(string displayText)
        {
            if (this.Editor.EditCore != null)
            {
                this.syncWithEditorLocker.DoLockedAction(() => this.UpdateEditCoreTextInternal(displayText));
            }
        }

        protected virtual void UpdateEditCoreTextInternal(string displayText)
        {
            if (this.Editor.EditCore is TextBlock)
            {
                this.Editor.EditCore.SetValue(TextBlock.TextProperty, string.IsNullOrEmpty(displayText) ? " " : displayText);
            }
            else if (this.Editor.EditCore is TextBox)
            {
                (this.Editor.EditCore as TextBox).Text = displayText;
            }
        }

        protected internal virtual void UpdateEditorOnEditingChange(bool syncWithValue)
        {
            this.DoValidate(UpdateEditorSource.LostFocus);
            if (syncWithValue)
            {
                this.SyncWithValue();
            }
        }

        private void UpdateEditValue(object editValue, UpdateEditorSource updateSource, bool isValid)
        {
            if (this.ShouldFlushEditValue(updateSource, isValid))
            {
                this.ValueContainer.FlushEditValueCandidate(editValue, updateSource);
            }
        }

        private void UpdateEditValue(object newValue, Action<object, object> syncWithValueCallback, bool updateConverters = false)
        {
            this.UpdateEditValue(null, newValue, syncWithValueCallback, updateConverters);
        }

        protected virtual void UpdateEditValue(object oldValue, object newValue, Action<object, object> syncWithValueCallback, bool updateConverters)
        {
            if (this.ValueContainer.HasTempValue)
            {
                this.ValueContainer.UndoTempValue();
            }
            if (updateConverters)
            {
                this.UpdateValueConverters();
            }
            if (this.ShouldUpdateErrorProvider)
            {
                this.ResetValidationError();
            }
            if (this.ShouldLockUpdate)
            {
                this.DoValidateInternal(newValue, UpdateEditorSource.ValueChanging);
                this.UpdateDisplayText();
            }
            else
            {
                syncWithValueCallback(oldValue, newValue);
                if (this.ShouldUpdateErrorProvider)
                {
                    this.DoValidateInternal(newValue, UpdateEditorSource.ValueChanging);
                }
            }
        }

        private void UpdateErrorProvider(bool isValid, BaseValidationError error)
        {
            this.ValidationState.Reset();
            this.PropertyProvider.SetHiddenValidationError(null);
            if (!isValid)
            {
                if (error.IsHidden)
                {
                    this.PropertyProvider.SetHiddenValidationError(error);
                }
                else
                {
                    this.ValidationState.Initialize(false, error);
                }
            }
            this.UpdateErrorRepresentation();
        }

        private void UpdateErrorRepresentation()
        {
            this.Editor.CoerceValue(BaseEdit.ValidationErrorPropertyKey.DependencyProperty);
            this.CoerceToolTip();
        }

        public virtual void UpdateNullTextForeground(bool isVisible)
        {
        }

        private bool UpdateSourceValidation(UpdateEditorSource updateSource) => 
            (updateSource != UpdateEditorSource.DontValidate) ? ((updateSource == UpdateEditorSource.ValueChanging) || ((updateSource == UpdateEditorSource.DoValidate) || (((updateSource != UpdateEditorSource.TextInput) || this.Editor.FocusManagement.IsFocusWithin) ? (!this.Editor.ValidateOnTextInput ? (!this.Editor.ValidateOnEnterKeyPressed ? (updateSource == UpdateEditorSource.LostFocus) : ((updateSource == UpdateEditorSource.EnterKeyPressed) || (updateSource == UpdateEditorSource.LostFocus))) : true) : true))) : false;

        private void UpdateToolTip()
        {
            if ((this.Editor.EditCore is TextBlock) && this.ShouldCoerceToolTip())
            {
                this.CoerceToolTip();
            }
        }

        protected virtual void UpdateValueConverters()
        {
            this.PropertyProvider.SetValueTypeConverter(this.CreateValueTypeConverter());
        }

        private PostponedAction ApplyStyleSettingsAction { get; }

        protected DevExpress.Xpf.Editors.ValidationState ValidationState { get; set; }

        public object EditValue =>
            this.GetEditValueInternal();

        protected BaseEditingSettingsService TextEditingSettings =>
            this.PropertyProvider.GetService<BaseEditingSettingsService>();

        protected virtual bool IsNullTextSupported =>
            false;

        protected bool ShouldShowEmptyText =>
            this.ShouldShowEmptyTextInternal(this.ValueContainer.EditValue);

        protected bool ShouldShowNullText =>
            this.ShouldShowNullTextInternal(this.ValueContainer.EditValue);

        protected virtual bool ApplyDisplayTextConversion =>
            !this.Editor.FocusManagement.IsFocusWithin || (!this.AllowKeyHandling || !this.AllowEditing);

        protected virtual bool ShouldSyncWithEditor =>
            !this.editBoxTextLocker.IsLocked && (!this.syncWithEditorLocker.IsLocked && this.AllowEditing);

        protected bool AllowEditing =>
            this.TextEditingSettings.AllowEditing;

        protected bool AllowKeyHandling =>
            this.TextEditingSettings.AllowKeyHandling;

        private DevExpress.Xpf.Editors.Services.ValueChangingService ValueChangingService =>
            this.PropertyProvider.GetService<DevExpress.Xpf.Editors.Services.ValueChangingService>();

        public virtual bool IsValueChanged
        {
            get => 
                this.ValueChangingService.IsValueChanged || this.ValueContainer.HasValueCandidate;
            set
            {
                if (value && this.ApplyDisplayTextConversion)
                {
                    this.ValueChangingService.SetIsValueChanged(true);
                }
                if (!value)
                {
                    this.ResetIsValueChanged();
                }
            }
        }

        protected internal virtual bool AllowTextInput =>
            this.AllowEditing;

        protected internal virtual bool ShouldApplyNullTextToDisplayText =>
            true;

        protected bool ShouldLockUpdate =>
            this.IsLockedByValueChanging || this.IsInSupportInitialize;

        protected bool ShouldLockRaiseEvents =>
            this.raiseEventsLocker.IsLocked;

        private bool ShouldUpdateErrorProvider =>
            !this.ValueContainer.IsLockedByValueChanging && !this.ValueContainer.IsPostponedValueChanging;

        public bool IsInSupportInitialize =>
            this.isInSupportInitialize.IsLocked;

        public bool IsInPostponedUpdate =>
            this.ValueContainer.IsPostponedValueChanging;

        protected internal virtual bool IsLockedByValueChanging =>
            this.editValueLocker.IsLocked || this.coerceValueLocker.IsLocked;

        protected ValueContainerService ValueContainer =>
            this.PropertyProvider.GetService<ValueContainerService>();

        protected BaseEdit Editor { get; }

        protected BaseEditSettings Settings =>
            this.Editor.Settings;

        protected ActualPropertyProvider PropertyProvider =>
            this.Editor.PropertyProvider;

        protected BaseEditStyleSettings StyleSettings =>
            this.PropertyProvider.StyleSettings;

        public EditorSpecificValidator Validator =>
            this.PropertyProvider.GetService<EditorSpecificValidator>();

        protected internal PropertyCoercionHelper PropertyUpdater { get; }

        protected PostponedAction RemoveNullTextFromUndoStack { get; }

        protected DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager =>
            this.Editor.ImmediateActionsManager;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditStrategyBase.<>c <>9 = new EditStrategyBase.<>c();
            public static PropertyCoercionHandler <>9__85_0;
            public static PropertyCoercionHandler <>9__85_1;
            public static Func<string, bool> <>9__129_0;
            public static Func<bool> <>9__129_1;

            internal bool <CreateValidationError>b__129_0(string x) => 
                x == "hidden error";

            internal bool <CreateValidationError>b__129_1() => 
                false;

            internal object <RegisterUpdateCallbacks>b__85_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__85_1(object baseValue) => 
                baseValue;
        }
    }
}

