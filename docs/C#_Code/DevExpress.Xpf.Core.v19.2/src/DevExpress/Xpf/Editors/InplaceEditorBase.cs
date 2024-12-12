namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interop;

    public abstract class InplaceEditorBase : DataContentPresenter, IDisplayTextProvider, IBaseEditOwner
    {
        private const int EscCode = 0x1b;
        public static bool CloseEditorOnLostKeyboardFocus;
        protected IBaseEdit editCore;
        protected IBaseEditWrapper editWrapper;
        private EditableDataObject editorDataContext;
        private readonly Locker IsPostInProgressLocker;
        private const string tab = "\t";
        private bool waitTemplateApplied;

        static InplaceEditorBase()
        {
            UIElement.FocusableProperty.OverrideMetadata(typeof(InplaceEditorBase), new FrameworkPropertyMetadata(true));
        }

        protected InplaceEditorBase()
        {
            // Unresolved stack state at '0000007C'
        }

        internal void ActivateOnLeftMouseButton(MouseButtonEventArgs e, bool isMouseDown)
        {
            if (((this.IsCellFocused && !this.Edit.IsEditorActive) && !this.ShouldProcessMouseEventsInInplaceInactiveMode(e)) && this.Owner.IsActivationAction(isMouseDown ? ActivationAction.MouseLeftButtonDown : ActivationAction.MouseLeftButtonUp, e, true))
            {
                this.ShowEditorIfNotVisible(!isMouseDown);
                if ((this.ReraiseMouseEventEditor != null) && this.ReraiseMouseEventEditor.ShouldReraiseEvents)
                {
                    if (isMouseDown)
                    {
                        this.RaisePostponedMouseEvent(e);
                        e.Handled = (this.editCore != null) || this.UpdateContentOnShowEditor;
                    }
                    else
                    {
                        this.Owner.EnqueueImmediateAction(new DelegateAction(() => this.ShouldReraiseActivationAction(ActivationAction.MouseLeftButtonUp, e, false)));
                    }
                }
            }
        }

        internal void ActivateOnStylusUp()
        {
            if (this.IsCellFocused && !this.Edit.IsEditorActive)
            {
                this.ShowEditorIfNotVisible(true);
            }
        }

        protected virtual void ApplyEditSettingsToEditor(IBaseEdit editCore)
        {
            this.EditorColumn.EditSettings.ApplyToEdit(editCore, this.AssignEditorSettings, this.EditorColumn, true);
        }

        protected virtual void ApplySettingsToEditorFromTemplate(IBaseEdit editCore)
        {
            BaseEditHelper.ApplySettings(editCore, this.EditorColumn.EditSettings, this.EditorColumn);
        }

        public virtual void CancelEditInVisibleEditor()
        {
            if (this.IsEditorVisible)
            {
                this.Edit.ClearEditorError();
                this.RestoreValidationError();
                this.HideEditor(true);
            }
        }

        protected virtual void CancelRowEdit(KeyEventArgs e)
        {
        }

        protected virtual bool CanCommitPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if ((e.OldFocus == null) || !(e.OldFocus is DependencyObject))
            {
                return true;
            }
            if (ReferenceEquals(FocusManager.GetFocusScope((DependencyObject) e.OldFocus), FocusManager.GetFocusScope(this)))
            {
                return true;
            }
            if (ReferenceEquals(e.NewFocus, this.editCore))
            {
                return false;
            }
            DependencyObject newFocus = e.NewFocus as DependencyObject;
            DependencyObject editCore = this.editCore as DependencyObject;
            return ((editCore == null) || ((newFocus == null) || (ReferenceEquals(FocusManager.GetFocusScope(newFocus), FocusManager.GetFocusScope(editCore)) ? !LayoutHelper.IsChildElementEx(editCore, newFocus, false) : false)));
        }

        public virtual bool CanShowEditor() => 
            this.IsCellFocused;

        protected virtual void CheckFocus()
        {
        }

        public void ClearError()
        {
            this.Edit.ClearEditorError();
        }

        protected void ClearViewCurrentCellEditor(InplaceEditorOwnerBase owner)
        {
            if (ReferenceEquals(owner.CurrentCellEditor, this))
            {
                owner.CurrentCellEditor = null;
            }
        }

        public void CommitEditor(bool closeEditor = false)
        {
            this.CommitEditorLocker.DoLockedActionIfNotLocked(() => this.CommitEditorCore(closeEditor));
        }

        private void CommitEditorCore(bool closeEditor)
        {
            if (this.IsEditorVisible && this.PostEditor(true))
            {
                this.HideEditor(closeEditor);
            }
        }

        protected virtual IBaseEdit CreateEditor(BaseEditSettings settings) => 
            this.CreateEditor(settings, true);

        protected virtual IBaseEdit CreateEditor(BaseEditSettings settings, bool assignEditorSettings) => 
            settings.CreateEditor(assignEditorSettings, this.EditorColumn, this.GetEditorOptimizationMode());

        bool? IDisplayTextProvider.GetDisplayText(string originalDisplayText, object value, out string displayText)
        {
            if (this.IsInTree)
            {
                return this.Owner.GetDisplayText(this, originalDisplayText, value, out displayText);
            }
            displayText = originalDisplayText;
            return true;
        }

        protected virtual bool? GetAllowDefaultButton() => 
            null;

        protected abstract object GetEditableValue();
        protected abstract EditableDataObject GetEditorDataContext();
        protected virtual EditorOptimizationMode GetEditorOptimizationMode() => 
            EditorOptimizationMode.Disabled;

        protected BindingExpressionBase GetEditValueBinding(IBaseEdit editor)
        {
            if (!CompatibilitySettings.AllowEditValueBindingInInplaceEditors)
            {
                return null;
            }
            BaseEdit target = editor as BaseEdit;
            if (target != null)
            {
                return BindingOperations.GetBindingExpressionBase(target, BaseEdit.EditValueProperty);
            }
            InplaceBaseEdit edit2 = editor as InplaceBaseEdit;
            return ((edit2 == null) ? null : BindingOperations.GetBindingExpressionBase(edit2, InplaceBaseEdit.EditValueProperty));
        }

        protected virtual bool HasEditorError() => 
            false;

        public void HideEditor(bool closeEditor)
        {
            if (closeEditor)
            {
                this.Owner.EditorWasClosed = true;
            }
            if (this.IsEditorVisible)
            {
                this.UpdateFocusIfNeeded(false, false);
                this.HideEditorInternal();
                this.OnHiddenEditor(closeEditor);
                if (this.UpdateContentOnShowEditor)
                {
                    this.UpdateContent(true);
                }
            }
        }

        protected virtual void HideEditorInternal()
        {
            if (this.IsEditorVisible)
            {
                this.Edit.EditMode = EditMode.InplaceInactive;
                this.Edit.EditValueChanged -= new EditValueChangedEventHandler(this.OnEditValueChanged);
                this.UpdateEditValue(this.editCore);
                if (ReferenceEquals(this.Owner.ActiveEditor, this.editCore))
                {
                    this.Owner.ActiveEditor = null;
                }
            }
        }

        protected virtual void InitializeBaseEdit(IBaseEdit newEdit, BaseEditSourceType newBaseEditSourceType)
        {
            this.EditorSourceType = newBaseEditSourceType;
            newEdit.EditMode = EditMode.InplaceInactive;
            this.SetEdit(newEdit);
            this.UpdateEditValue(newEdit);
            newEdit.InvalidValueBehavior = InvalidValueBehavior.AllowLeaveEditor;
            this.UpdateEditorButtonVisibility();
            this.UpdateDisplayTemplate(false);
            this.UpdateValidationError();
            this.UpdateValidationErrorTemplate(newEdit);
            this.SetDisplayTextProvider(newEdit);
        }

        internal void InitializeEditorFromTemplate(IBaseEdit editFromTemplate)
        {
            if (editFromTemplate != null)
            {
                editFromTemplate.ShouldDisableExcessiveUpdatesInInplaceInactiveMode = false;
                this.InitializeBaseEdit(editFromTemplate, BaseEditSourceType.CellTemplate);
                this.ApplySettingsToEditorFromTemplate(editFromTemplate);
                this.UpdateHighlightingText(this.LastHighlightingProperties, true);
            }
            else
            {
                if (this.AllowCustomEditors && (base.ContentTemplate != null))
                {
                    this.editWrapper = new CustomEditWrapper(this);
                }
                if (this.EditorSourceType == BaseEditSourceType.CellTemplate)
                {
                    this.SetEdit(null);
                }
            }
        }

        private void InplaceEditorBase_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.UnsubscribingFocusScope();
            if ((this.Owner != null) && !base.IsVisible)
            {
                this.Owner.CommitEditorForce();
            }
        }

        protected internal bool IsChildElementOrMessageBox(IInputElement element) => 
            this.Edit.IsChildElement(element, this.EditorRoot) || this.IsMessageBox(element);

        protected abstract bool IsInactiveEditorButtonVisible();
        private bool IsMessageBox(IInputElement focus) => 
            BrowserInteropHelper.IsBrowserHosted ? this.IsMessageBoxCoreXBAP(focus) : this.IsMessageBoxCore(focus);

        private bool IsMessageBoxCore(IInputElement focus)
        {
            WindowContentHolder holder = focus as WindowContentHolder;
            if (holder == null)
            {
                return false;
            }
            FloatingContainer container = holder.Container as FloatingContainer;
            return ((container != null) ? container.ShowModal : false);
        }

        private bool IsMessageBoxCoreXBAP(IInputElement focus) => 
            focus is DXMessageBox;

        protected virtual bool IsProperEditorSettings() => 
            ReferenceEquals(BaseEditHelper.GetEditSettings(this.editCore), this.EditorColumn.EditSettings);

        protected bool IsReadOnlyHasBindingExpression()
        {
            BaseEdit editCore = this.editCore as BaseEdit;
            return ((editCore != null) && (BindingOperations.GetBindingExpressionBase(editCore, BaseEdit.IsReadOnlyProperty) != null));
        }

        internal void LockEditorFocus()
        {
            this.Edit.LockEditorFocus();
        }

        protected internal bool NeedsKey(KeyEventArgs e, bool defaultValue) => 
            this.Owner.NeedsKey(BaseEditHelper.GetKey(e), ModifierKeysHelper.GetKeyboardModifiers(e), defaultValue);

        protected virtual void NullEditorInEditorDataContext()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.waitTemplateApplied = false;
            IBaseEdit templateChild = base.GetTemplateChild("PART_Editor") as IBaseEdit;
            this.InitializeEditorFromTemplate(templateChild);
            if (this.IsCellFocused)
            {
                this.RefreshFocus();
            }
        }

        protected virtual void OnColumnContentChanged(object sender, ColumnContentChangedEventArgs e)
        {
            if (e.Property == null)
            {
                this.UpdateContent(true);
            }
        }

        protected override void OnContentChanged(object oldValue)
        {
        }

        public void OnDataContextValueChanged(object value)
        {
            if ((this.editCore == null) && this.Edit.IsEditorActive)
            {
                this.EditableValue = value;
            }
        }

        protected virtual void OnEditorActivated()
        {
            this.Edit.IsValueChanged = false;
        }

        protected virtual void OnEditorActivated(object sender, RoutedEventArgs e)
        {
            if (this.Owner.ActiveEditor != null)
            {
                this.OnEditorActivated();
            }
        }

        protected virtual void OnEditorPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (this.IsInTree && !this.IsChildElementOrMessageBox(e.NewFocus))
            {
                PopupBaseEdit editCore = this.editCore as PopupBaseEdit;
                if ((editCore == null) || !editCore.IsPopupCloseInProgress)
                {
                    if (!CloseEditorOnLostKeyboardFocus)
                    {
                        DependencyObject focusScope = FocusManager.GetFocusScope(this);
                        DependencyObject newFocus = e.NewFocus as DependencyObject;
                        if ((newFocus != null) && ((focusScope is UIElement) && (((UIElement) focusScope).IsVisible && !ReferenceEquals(FocusManager.GetFocusScope(newFocus), focusScope))))
                        {
                            ((UIElement) focusScope).PreviewGotKeyboardFocus += new KeyboardFocusChangedEventHandler(this.scopeFocus_PreviewGotKeyboardFocus);
                            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.InplaceEditorBase_IsVisibleChanged);
                            return;
                        }
                    }
                    this.LockEditorFocus();
                    if (this.IsEditorVisible && this.Edit.IsEditorActive)
                    {
                        this.CommitEditor(false);
                    }
                    this.UnlockEditorFocus();
                    if (this.HasEditorError())
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        protected virtual void OnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
        }

        protected virtual void OnHiddenEditor(bool closeEditor)
        {
            this.UpdateEditorButtonVisibility();
        }

        protected override void OnInnerContentChangedCore()
        {
            if (!this.IsEditorVisible)
            {
                base.OnInnerContentChangedCore();
                if (!this.IsInTree)
                {
                    return;
                }
                this.UpdateContent(false);
                this.UpdateEditorButtonVisibility();
            }
            this.UpdateValidationError();
        }

        protected void OnIsFocusedCellChanged()
        {
            if (this.IsCellFocused)
            {
                this.RefreshFocus();
            }
            else
            {
                if (this.IsEditorVisible)
                {
                    this.CommitEditor(false);
                }
                if (this.IsInTree)
                {
                    if (this.GetIsKeyboardFocusWithin())
                    {
                        KeyboardHelper.Focus((UIElement) this.Owner.OwnerElement);
                    }
                    this.ClearViewCurrentCellEditor(this.Owner);
                }
            }
            BaseEditHelper.SetShowBorderInInplaceInactiveMode(this.editCore, this.IsCellFocused);
            this.UpdateEditorButtonVisibility();
        }

        protected void OnOwnerChanged(IInplaceEditorColumn oldValue)
        {
            this.UpdateData();
            if (oldValue != null)
            {
                oldValue.ContentChanged -= this.ColumnContentChangedEventHandler.Handler;
            }
            if (this.EditorColumn != null)
            {
                this.EditorColumn.ContentChanged += this.ColumnContentChangedEventHandler.Handler;
                this.UpdateContent(true);
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            bool defaultValue = this.Edit.IsActivatingKey(e) && !this.UpdateContentOnShowEditor;
            if (this.AllowCustomEditors && ((e.Key == Key.Return) || (e.Key == Key.F2)))
            {
                defaultValue = true;
            }
            if (!this.IsEditorVisible && this.Owner.IsActivationAction(ActivationAction.KeyDown, e, defaultValue))
            {
                this.ShowEditorAndSelectAll();
                if (!this.IsEditorVisible && !this.waitTemplateApplied)
                {
                    this.RaiseKeyDownEvent(e);
                }
                else
                {
                    this.Owner.EnqueueImmediateAction(new ProcessActivatingKeyAction(this, e));
                    e.Handled = true;
                }
            }
            else if (!this.NeedsKey(e, this.Edit.NeedsKey(e)))
            {
                if (e.Key == Key.Return)
                {
                    if (this.IsEditorVisible)
                    {
                        this.CommitEditor(true);
                        this.CheckFocus();
                        e.Handled = true;
                    }
                    else if (!this.AllowCustomEditors)
                    {
                        this.ShowEditorAndSelectAll();
                        e.Handled = true;
                    }
                }
                if ((e.Key == Key.F2) && (!this.AllowCustomEditors && !this.IsEditorVisible))
                {
                    this.ShowEditorAndSelectAll();
                    e.Handled = true;
                }
                if (e.Key == Key.Escape)
                {
                    if (!this.IsEditorVisible)
                    {
                        this.CancelRowEdit(e);
                    }
                    else
                    {
                        this.CancelEditInVisibleEditor();
                        this.CheckFocus();
                        e.Handled = true;
                    }
                }
                if (!e.Handled)
                {
                    this.RaiseKeyDownEvent(e);
                }
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);
            if ((e.Text != "\t") && (!string.IsNullOrEmpty(e.Text) && (string.IsNullOrEmpty(e.ControlText) && (string.IsNullOrEmpty(e.SystemText) && (e.Text[0] != '\x001b')))))
            {
                if (!this.IsEditorVisible)
                {
                    if (!this.Owner.IsActivationAction(ActivationAction.TextInput, e, (this.editCore != null) && !this.UpdateContentOnShowEditor))
                    {
                        return;
                    }
                    this.ShowEditorAndSelectAll();
                }
                if (this.ShouldReraiseEvents && !this.Edit.IsEditorActive)
                {
                    this.Owner.EnqueueImmediateAction(new RaisePostponedTextInputEventAction(this, e, () => this.ShouldReraiseActivationAction(ActivationAction.TextInput, e, (this.editCore != null) && !this.UpdateContentOnShowEditor)));
                    e.Handled = true;
                }
            }
        }

        protected virtual void OnShowEditor()
        {
        }

        private void OnShowEditor(bool selectAll)
        {
            if (!this.IsEditorVisible)
            {
                this.UpdateEditTemplate();
                this.UpdateIsEditorReadOnly();
                this.Owner.ActiveEditor = this.editCore;
                this.SetActiveEditMode();
                if ((this.editCore == null) && this.AllowCustomEditors)
                {
                    this.OnEditorActivated();
                }
                this.Edit.SetKeyboardFocus();
                this.UpdateEditContext();
                this.Edit.EditValueChanged += new EditValueChangedEventHandler(this.OnEditValueChanged);
            }
            this.OnShowEditor();
            this.Owner.EditorWasClosed = false;
            this.UpdateEditorButtonVisibility();
            this.Edit.IsValueChanged = false;
            if (selectAll)
            {
                this.Owner.EnqueueImmediateAction(new SelectAllAction(this));
            }
        }

        protected void OwnerEnqueueImmediateActionCore(IAction action)
        {
            this.Owner.EnqueueImmediateAction(action);
        }

        public virtual bool PostEditor(bool flushPendingEditActions = true)
        {
            bool result = true;
            if (this.Edit.IsValueChanged)
            {
                this.IsPostInProgressLocker.DoLockedActionIfNotLocked(delegate {
                    this.ValidateEditor(flushPendingEditActions);
                    result = this.PostEditorCore();
                });
            }
            return result;
        }

        protected abstract bool PostEditorCore();
        internal void ProcessActivatingKey(KeyEventArgs e)
        {
            if (this.ShouldReraiseActivationAction(ActivationAction.KeyDown, e, false))
            {
                new RaisePostponedKeyDownEventAction(this, e).Execute();
            }
            else
            {
                this.Edit.ProcessActivatingKey(e);
            }
        }

        protected internal virtual bool ProcessKeyForLookUp(KeyEventArgs e) => 
            this.IsEditorVisible && (this.Edit.IsActivatingKey(e) || !this.NeedsKey(e, this.Edit.NeedsKey(e)));

        protected virtual void ProcessMouseEventInInplaceInactiveMode(MouseButtonEventArgs e)
        {
            this.RaisePostponedMouseEvent(e);
        }

        private void RaiseKeyDownEvent(KeyEventArgs e)
        {
            if ((e.Key != Key.Escape) && this.Edit.CanHandleBubblingEvent)
            {
                e.Handled = true;
            }
            this.Owner.ProcessKeyDown(e);
        }

        private void RaisePostponedMouseEvent(MouseButtonEventArgs e)
        {
            this.Owner.EnqueueImmediateAction(new RaisePostponedMouseEventAction(this.ReraiseMouseEventEditor, e, () => this.ShouldReraiseActivationAction(ActivationAction.MouseLeftButtonDown, e, (this.editCore != null) && !this.UpdateContentOnShowEditor)));
        }

        protected virtual bool RaiseShowingEditor() => 
            true;

        private void RefreshFocus()
        {
            if (this.IsInTree)
            {
                if (this.Owner.CanFocusEditor)
                {
                    this.SetKeyboardFocus();
                }
                this.UpdateEditorToShow();
            }
        }

        protected virtual void RestoreValidationError()
        {
        }

        private void scopeFocus_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            DependencyObject newFocus = e.NewFocus as DependencyObject;
            if ((newFocus != null) && ReferenceEquals(FocusManager.GetFocusScope(newFocus), FocusManager.GetFocusScope(this)))
            {
                this.UnsubscribingFocusScope();
                DependencyObject source = e.Source as DependencyObject;
                if (!base.IsKeyboardFocusWithin)
                {
                    if (this.CanCommitPreviewGotKeyboardFocus(e))
                    {
                        this.CommitEditor(false);
                    }
                    if ((this.Owner.ActiveEditor != null) && (this.Owner.ActiveEditor.ValidationError != null))
                    {
                        e.Handled = true;
                        Keyboard.Focus(FocusManager.GetFocusedElement(FocusManager.GetFocusScope(this)));
                    }
                }
            }
        }

        internal void SelectAll()
        {
            this.Edit.SelectAll();
        }

        protected virtual DataTemplate SelectTemplate() => 
            null;

        protected virtual void SetActiveEditMode()
        {
            this.Edit.EditMode = EditMode.InplaceActive;
            this.UpdateEditValue(this.editCore);
        }

        protected virtual void SetDisplayTextProvider(IBaseEdit newEdit)
        {
            BaseEditHelper.SetDisplayTextProvider(newEdit, this);
        }

        protected virtual void SetEdit(IBaseEdit value)
        {
            if (!ReferenceEquals(this.editCore, value))
            {
                if (this.editCore != null)
                {
                    this.editCore.EditorActivated -= new RoutedEventHandler(this.OnEditorActivated);
                    this.editCore.PreviewLostKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.OnEditorPreviewLostKeyboardFocus);
                }
                this.editCore = value;
                if (base.IsKeyboardFocusWithin && !base.IsKeyboardFocused)
                {
                    KeyboardHelper.Focus((UIElement) this);
                }
                this.editWrapper = (this.editCore != null) ? new BaseEditWrapper(this.editCore) : null;
                if (this.editCore != null)
                {
                    this.editCore.EditorActivated += new RoutedEventHandler(this.OnEditorActivated);
                    this.editCore.PreviewLostKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnEditorPreviewLostKeyboardFocus);
                }
            }
        }

        protected virtual void SetEditorInEditorDataContext()
        {
        }

        internal void SetKeyboardFocus()
        {
            this.Edit.SetKeyboardFocus();
            if (!this.GetIsKeyboardFocusWithin())
            {
                KeyboardHelper.Focus((UIElement) this);
            }
        }

        protected virtual bool ShouldProcessMouseEventsInInplaceInactiveMode(MouseButtonEventArgs e) => 
            false;

        internal bool ShouldReraiseActivationAction(ActivationAction action, RoutedEventArgs e, bool defaulValue) => 
            this.Owner.ShouldReraiseActivationAction(action, e, defaulValue);

        public void ShowEditor()
        {
            this.ShowEditor(false);
        }

        internal bool ShowEditor(bool selectAll) => 
            this.CanShowEditor() ? this.ShowEditorCore(selectAll) : false;

        internal void ShowEditorAndSelectAll()
        {
            this.ShowEditor(true);
        }

        internal void ShowEditorAndSelectAllIfNeeded(KeyEventArgs e)
        {
            if (this.Owner.IsActivationAction(ActivationAction.KeyUp, e, true))
            {
                this.ShowEditorAndSelectAll();
                if (this.IsEditorVisible)
                {
                    this.Owner.ShouldReraiseActivationAction(ActivationAction.KeyUp, e, false);
                }
            }
        }

        protected virtual bool ShowEditorCore(bool selectAll) => 
            this.Owner.ShowEditor(selectAll);

        public void ShowEditorIfNotVisible(bool selectAll)
        {
            if (!this.IsEditorVisible)
            {
                this.ShowEditor(selectAll);
            }
        }

        protected internal virtual bool ShowEditorInternal(bool selectAll)
        {
            if (!this.UpdateContentOnShowEditor)
            {
                this.OnShowEditor(selectAll);
            }
            else
            {
                this.waitTemplateApplied = true;
                this.UpdateContent(true);
                this.Owner.EnqueueImmediateAction(new DelegateAction(delegate {
                    this.OnShowEditor(selectAll);
                }));
            }
            return true;
        }

        internal void UnlockEditorFocus()
        {
            this.Edit.UnlockEditorFocus();
        }

        private void UnsubscribingFocusScope()
        {
            UIElement focusScope = FocusManager.GetFocusScope(this) as UIElement;
            if (focusScope != null)
            {
                focusScope.PreviewGotKeyboardFocus -= new KeyboardFocusChangedEventHandler(this.scopeFocus_PreviewGotKeyboardFocus);
                base.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(this.InplaceEditorBase_IsVisibleChanged);
            }
        }

        protected virtual void UpdateContent(bool updateDisplayTemplate = true)
        {
            if (this.HasAccessToCellValue)
            {
                DataTemplate objB = this.SelectTemplate();
                if ((objB != null) && !this.OverrideCellTemplate)
                {
                    this.UpdateFocusIfNeeded(false, false);
                    if ((this.EditorSourceType == BaseEditSourceType.EditSettings) || !ReferenceEquals(base.ContentTemplate, objB))
                    {
                        this.SetEdit(null);
                    }
                    base.ContentTemplate = objB;
                    base.Content = this.EditorDataContext;
                    if (this.editCore != null)
                    {
                        this.ApplySettingsToEditorFromTemplate(this.editCore);
                    }
                }
                else if ((this.editCore == null) || ((this.EditorSourceType == BaseEditSourceType.CellTemplate) || !this.IsProperEditorSettings()))
                {
                    if (this.editCore != null)
                    {
                        this.HideEditorInternal();
                        this.editCore.DataContext = null;
                        this.SetEdit(null);
                    }
                    base.ContentTemplate = null;
                    IBaseEdit newEdit = this.CreateEditor(this.EditorColumn.EditSettings);
                    this.UpdateEditorDataContext();
                    newEdit.DataContext = this.EditorDataContext;
                    this.InitializeBaseEdit(newEdit, BaseEditSourceType.EditSettings);
                    base.Content = newEdit;
                }
                else if (ReferenceEquals(BaseEditHelper.GetEditSettings(this.editCore), this.EditorColumn.EditSettings))
                {
                    this.UpdateViewInfoProperties();
                }
                else
                {
                    this.editCore.BeginInit();
                    this.ApplyEditSettingsToEditor(this.editCore);
                    this.UpdateEditorDataContext();
                    if (this.editCore != null)
                    {
                        this.editCore.DataContext = this.EditorDataContext;
                        this.editCore.EndInit();
                    }
                }
                if (updateDisplayTemplate)
                {
                    this.UpdateDisplayTemplate(true);
                }
            }
        }

        protected void UpdateData()
        {
            if (this.HasAccessToCellValue)
            {
                this.EditorDataContext = this.GetEditorDataContext();
            }
            else
            {
                this.EditorDataContext = null;
                base.Content = null;
            }
        }

        protected virtual void UpdateDisplayTemplate(bool updateForce = false)
        {
            if ((this.editCore != null) && (this.EditorSourceType == BaseEditSourceType.EditSettings))
            {
                this.editCore.ShouldDisableExcessiveUpdatesInInplaceInactiveMode = this.OptimizeEditorPerformance && ReferenceEquals(this.EditorColumn.DisplayTemplate, null);
            }
            if (updateForce || (this.EditorColumn.DisplayTemplate != null))
            {
                this.Edit.SetDisplayTemplate(this.EditorColumn.DisplayTemplate);
            }
        }

        public void UpdateDisplayText()
        {
            if (this.editCore != null)
            {
                this.editCore.UpdateDisplayText();
            }
        }

        protected virtual void UpdateEditContext()
        {
            if (this.HasAccessToCellValue)
            {
                this.EditableValue = this.GetEditableValue();
            }
        }

        public virtual void UpdateEditorButtonVisibility()
        {
            if (this.IsInTree)
            {
                this.Edit.ShowEditorButtons = this.IsEditorVisible || this.IsInactiveEditorButtonVisible();
            }
        }

        protected virtual void UpdateEditorDataContext()
        {
        }

        private void UpdateEditorToShow()
        {
            if (this.IsCellFocused)
            {
                this.Owner.CurrentCellEditor = this;
            }
        }

        protected virtual void UpdateEditTemplate()
        {
            this.Edit.SetEditTemplate(this.EditorColumn.EditTemplate);
        }

        protected void UpdateEditValue(IBaseEdit editor)
        {
            if (editor != null)
            {
                this.UpdateEditValueCore(editor);
            }
        }

        protected abstract void UpdateEditValueCore(IBaseEdit editor);
        protected void UpdateFocusIfNeeded(bool force = false, bool postEditor = false)
        {
            if (((force || ((postEditor ? this.HasCellEditTemplateAssigned : this.UpdateContentOnShowEditor) || (this.editCore == null))) && base.IsKeyboardFocusWithin) && !base.IsFocused)
            {
                KeyboardHelper.Focus((UIElement) this);
            }
        }

        public void UpdateHighlightingText(TextHighlightingProperties highlightingProperties, bool skipEqualsCheck)
        {
            if ((this.LastHighlightingProperties != null) || (highlightingProperties != null))
            {
                if ((this.LastHighlightingProperties == null) || (highlightingProperties == null))
                {
                    this.UpdateHighlightingTextInternal(highlightingProperties);
                }
                else if (skipEqualsCheck || ((this.LastHighlightingProperties.FilterCondition != highlightingProperties.FilterCondition) || (this.LastHighlightingProperties.Text != highlightingProperties.Text)))
                {
                    this.UpdateHighlightingTextInternal(highlightingProperties);
                }
            }
        }

        private void UpdateHighlightingTextInternal(TextHighlightingProperties highlightingProperties)
        {
            this.LastHighlightingProperties = highlightingProperties;
            if (this.editCore != null)
            {
                if (this.EditorSourceType == BaseEditSourceType.CellTemplate)
                {
                    BaseEditHelper.UpdateHighlightingText(this.editCore, highlightingProperties);
                }
                else
                {
                    SearchControlHelper.UpdateTextHighlighting(this.editCore.Settings, highlightingProperties);
                }
            }
        }

        protected void UpdateIsEditorReadOnly()
        {
            bool isReadOnly = this.IsReadOnly;
            if ((this.Edit.IsReadOnly != isReadOnly) && !this.IsReadOnlyHasBindingExpression())
            {
                this.Edit.IsReadOnly = isReadOnly;
            }
        }

        protected virtual void UpdateValidationError()
        {
        }

        private void UpdateValidationErrorTemplate(IBaseEdit edit)
        {
            DataTemplate validationErrorTemplate;
            if (this.EditorSourceType == BaseEditSourceType.CellTemplate)
            {
                validationErrorTemplate = edit.ValidationErrorTemplate;
            }
            else
            {
                Func<BaseEditSettings, DataTemplate> evaluator = <>c.<>9__153_0;
                if (<>c.<>9__153_0 == null)
                {
                    Func<BaseEditSettings, DataTemplate> local1 = <>c.<>9__153_0;
                    evaluator = <>c.<>9__153_0 = x => x.ValidationErrorTemplate;
                }
                validationErrorTemplate = edit.Settings.Return<BaseEditSettings, DataTemplate>(evaluator, <>c.<>9__153_1 ??= ((Func<DataTemplate>) (() => null)));
            }
            DataTemplate template = validationErrorTemplate;
            this.Edit.SetValidationErrorTemplate(template);
        }

        protected void UpdateViewInfoProperties()
        {
            if (this.editCore != null)
            {
                this.EditorColumn.EditSettings.AssignViewInfoProperties(this.editCore, this.EditorColumn);
            }
        }

        public virtual void ValidateEditor(bool flushPendingEditActions = true)
        {
            if (this.IsEditorVisible)
            {
                if (flushPendingEditActions)
                {
                    this.Edit.FlushPendingEditActions();
                }
                this.ValidateEditorCore();
            }
        }

        public virtual void ValidateEditorCore()
        {
        }

        public virtual bool IsEditorVisible =>
            this.Edit.EditMode == EditMode.InplaceActive;

        protected virtual bool ShouldReraiseEvents =>
            this.IsEditorVisible || this.waitTemplateApplied;

        protected BaseEditSourceType EditorSourceType { get; set; }

        public IBaseEditWrapper Edit =>
            this.editWrapper ?? FakeBaseEdit.Instance;

        protected internal object EditableValue
        {
            get => 
                this.Edit.EditValue;
            set => 
                this.Edit.EditValue = value;
        }

        protected abstract InplaceEditorOwnerBase Owner { get; }

        protected abstract IInplaceEditorColumn EditorColumn { get; }

        protected internal virtual bool IsInTree =>
            this.Owner != null;

        protected bool HasAccessToCellValue =>
            this.IsInTree && (this.EditorColumn != null);

        internal Locker CommitEditorLocker =>
            this.Owner.CommitEditorLocker;

        protected abstract bool IsCellFocused { get; }

        protected abstract bool IsReadOnly { get; }

        protected virtual bool AssignEditorSettings =>
            true;

        private ColumnContentChangedEventHandler<InplaceEditorBase> ColumnContentChangedEventHandler { get; set; }

        private InnerContentChangedEventHandler<InplaceEditorBase> InnerContentChangedEventHandler { get; set; }

        protected virtual bool OptimizeEditorPerformance =>
            false;

        protected EditableDataObject EditorDataContext
        {
            get => 
                this.editorDataContext;
            set
            {
                if (!ReferenceEquals(this.EditorDataContext, value))
                {
                    if (this.EditorDataContext != null)
                    {
                        this.EditorDataContext.ContentChanged -= this.InnerContentChangedEventHandler.Handler;
                        this.NullEditorInEditorDataContext();
                    }
                    this.editorDataContext = value;
                    if (this.EditorDataContext != null)
                    {
                        this.EditorDataContext.ContentChanged += this.InnerContentChangedEventHandler.Handler;
                        this.SetEditorInEditorDataContext();
                    }
                }
            }
        }

        protected abstract bool OverrideCellTemplate { get; }

        protected virtual bool UpdateContentOnShowEditor =>
            false;

        protected virtual bool HasCellEditTemplateAssigned =>
            false;

        protected internal TextHighlightingProperties LastHighlightingProperties { get; set; }

        protected virtual InplaceEditorBase ReraiseMouseEventEditor =>
            this;

        protected virtual bool AllowCustomEditors =>
            false;

        public bool IsCustomEditor =>
            (this.editCore == null) || (this.GetEditValueBinding(this.editCore) != null);

        protected virtual DependencyObject EditorRoot =>
            null;

        HorizontalAlignment IBaseEditOwner.DefaultHorizontalAlignment =>
            (this.EditorColumn != null) ? this.EditorColumn.DefaultHorizontalAlignment : HorizontalAlignment.Left;

        bool IBaseEditOwner.HasTextDecorations =>
            (this.EditorColumn != null) && this.EditorColumn.HasTextDecorations;

        bool? IBaseEditOwner.AllowDefaultButton =>
            this.GetAllowDefaultButton();

        IDisplayTextProvider IBaseEditOwner.DisplayTextProvider =>
            this;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InplaceEditorBase.<>c <>9 = new InplaceEditorBase.<>c();
            public static Action<InplaceEditorBase, object, ColumnContentChangedEventArgs> <>9__56_0;
            public static Action<WeakEventHandler<InplaceEditorBase, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler>, object> <>9__56_1;
            public static Action<InplaceEditorBase, object, EventArgs> <>9__56_2;
            public static Func<BaseEditSettings, DataTemplate> <>9__153_0;
            public static Func<DataTemplate> <>9__153_1;

            internal void <.ctor>b__56_0(InplaceEditorBase owner, object o, ColumnContentChangedEventArgs e)
            {
                owner.OnColumnContentChanged(o, e);
            }

            internal void <.ctor>b__56_1(WeakEventHandler<InplaceEditorBase, ColumnContentChangedEventArgs, ColumnContentChangedEventHandler> h, object e)
            {
                ((IInplaceEditorColumn) e).ContentChanged -= h.Handler;
            }

            internal void <.ctor>b__56_2(InplaceEditorBase owner, object o, EventArgs e)
            {
                owner.OnInnerContentChanged(o, e);
            }

            internal DataTemplate <UpdateValidationErrorTemplate>b__153_0(BaseEditSettings x) => 
                x.ValidationErrorTemplate;

            internal DataTemplate <UpdateValidationErrorTemplate>b__153_1() => 
                null;
        }

        protected enum BaseEditSourceType
        {
            EditSettings,
            CellTemplate
        }
    }
}

