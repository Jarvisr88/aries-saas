namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    public class CellEditor : CellEditorBase
    {
        private HashSet<ValidationError> bindingErrors;

        public CellEditor()
        {
            this.SetDefaultStyleKey(typeof(CellEditor));
            base.FocusVisualStyle = null;
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
            KeyboardNavigation.SetDirectionalNavigation(this, KeyboardNavigationMode.Cycle);
        }

        protected override void ApplyEditSettingsToEditor(IBaseEdit editCore)
        {
            base.ApplyEditSettingsToEditor(editCore);
            base.View.RaiseEditorInitialized(base.Column, editCore);
        }

        protected override void ApplySettingsToEditorFromTemplate(IBaseEdit editCore)
        {
            base.ApplySettingsToEditorFromTemplate(editCore);
            base.View.RaiseEditorInitialized(base.Column, editCore);
        }

        public override void CancelEditInVisibleEditor()
        {
            BindingExpressionBase editValueBinding = base.GetEditValueBinding(base.editCore);
            if (editValueBinding != null)
            {
                editValueBinding.UpdateTarget();
            }
            base.CancelEditInVisibleEditor();
            base.CellData.OnRowChanged();
            base.View.ViewBehavior.OnCurrentCellEditCancelled();
        }

        protected override void CancelRowEdit(KeyEventArgs e)
        {
            e.Handled = base.View.IsFocusedRowModified;
            if ((e.Key == Key.Escape) && (base.View.AreUpdateRowButtonsShown && !base.View.IsEditing))
            {
                base.View.CancelRowChangesCore(false);
            }
            else
            {
                base.View.CancelRowEdit();
            }
        }

        protected override bool CanRefreshContent() => 
            this.IsInTree && base.GridCellEditorOwner.CanRefreshContent;

        private bool CheckBindingErrors()
        {
            if (!this.HasBindingErrors)
            {
                return true;
            }
            ValidationError error = this.bindingErrors.First<ValidationError>();
            object[] errors = new object[] { error };
            RowValidationError validationError = base.View.CreateCellValidationError(error.ErrorContent, errors, ErrorType.Default, base.RowData.RowHandle.Value, base.Column, error.Exception);
            if (validationError == null)
            {
                return true;
            }
            this.ShowError(validationError, true);
            return false;
        }

        protected override IBaseEdit CreateEditor(BaseEditSettings settings)
        {
            IBaseEdit editor = base.CreateEditor(settings, base.View.AssignEditorSettings);
            base.View.RaiseEditorInitialized(base.Column, editor);
            if (this.RowHandle == -2147483647)
            {
                editor.DisableExcessiveUpdatesInInplaceInactiveMode = false;
            }
            return editor;
        }

        private RowValidationError GetValidationError()
        {
            int rowHandle = base.RowData.RowHandle.Value;
            RowValidationError error = null;
            if (!base.Edit.DoValidate())
            {
                error = RowValidationHelper.CreateEditorValidationError(base.View, rowHandle, null, base.Column);
            }
            if (error != null)
            {
                return error;
            }
            if (!base.View.AllowCommitOnValidationAttributeError)
            {
                error = RowValidationHelper.ValidateAttributes(base.View, base.EditableValue, base.RowData.RowHandle.Value, base.Column);
                if (error != null)
                {
                    return error;
                }
            }
            return (!base.View.AllowLeaveInvalidEditor ? RowValidationHelper.ValidateEvents(base.View, this, base.EditableValue, base.RowData.RowHandle.Value, base.Column) : null);
        }

        protected override bool IsProperEditorSettings() => 
            EditSettingsComparer.IsCompatibleEditSettings(base.editCore, base.ColumnCore.ActualEditSettingsCore);

        private void OnBindingError(object sender, ValidationErrorEventArgs e)
        {
            base.Edit.IsValueChanged = true;
            if ((e.Action == ValidationErrorEventAction.Added) && (e.Error.RuleInError.ValidationStep < ValidationStep.UpdatedValue))
            {
                this.bindingErrors.Add(e.Error);
            }
            else
            {
                this.bindingErrors.Remove(e.Error);
            }
        }

        protected override void OnColumnContentChanged(object sender, ColumnContentChangedEventArgs e)
        {
            if (ReferenceEquals(e.Property, ColumnBase.CellDisplayTemplateProperty))
            {
                this.UpdateContent(true);
            }
            else
            {
                base.OnColumnContentChanged(sender, e);
            }
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
            base.View.ViewBehavior.OnFocusedRowCellModified();
        }

        protected override void OnHiddenEditor(bool closeEditor)
        {
            if (base.Column.ActualBinding != null)
            {
                this.UpdateDisplayMemberBindingValue();
            }
            base.OnHiddenEditor(closeEditor);
            base.CellData.UpdateValue(false);
            base.CellData.ResetInitialValue();
            Binding.RemoveSourceUpdatedHandler(this, new EventHandler<DataTransferEventArgs>(this.OnSourceUpdated));
            Validation.RemoveErrorHandler(this, new EventHandler<ValidationErrorEventArgs>(this.OnBindingError));
            this.bindingErrors = null;
        }

        protected override void OnInnerContentChanged(object sender, EventArgs e)
        {
            if (!base.CellData.UpdateValueLocker.IsLocked || (base.ColumnCore.HasTemplateSelector || base.DataControl.RepopuateColumnLocker.IsLocked))
            {
                base.OnInnerContentChanged(sender, e);
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if ((((e.Key != Key.V) || ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control)) && ((e.Key != Key.Insert) || ((Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.Shift))) || !this.AllowClipboardPaste)
            {
                base.OnPreviewKeyDown(e);
            }
            else
            {
                e.Handled = true;
                ((ITableView) base.View).OnPaste();
            }
            if (this.UpdateContentOnShowEditor)
            {
                base.OwnerEnqueueImmediateActionCore(new DelegateAction(() => this.OnPreviewKeyDownNewItemRow(e)));
            }
            else
            {
                this.OnPreviewKeyDownNewItemRow(e);
            }
        }

        private void OnPreviewKeyDownNewItemRow(KeyEventArgs e)
        {
            if ((((this.RowHandle == -2147483647) || base.View.EnterMoveNextColumn) && (e.Key == Key.Return)) && !this.IsEditorVisible)
            {
                base.View.SelectionStrategy.OnBeforeProcessKeyDown(e);
                base.View.MoveNextCell(false, true);
                base.View.SelectionStrategy.OnAfterProcessKeyDown(e);
                this.Owner.EditorWasClosed = false;
            }
        }

        protected override void OnRowDataChanged(RowData oldValue)
        {
            base.OnRowDataChanged(oldValue);
            if (!this.IsInTree && (oldValue != null))
            {
                base.ClearViewCurrentCellEditor(oldValue.View.InplaceEditorOwner);
            }
        }

        protected override void OnShowEditor()
        {
            base.OnShowEditor();
            Binding.AddSourceUpdatedHandler(this, new EventHandler<DataTransferEventArgs>(this.OnSourceUpdated));
            Validation.AddErrorHandler(this, new EventHandler<ValidationErrorEventArgs>(this.OnBindingError));
            this.bindingErrors = new HashSet<ValidationError>();
            base.CellData.SetInitialValue();
        }

        private void OnSourceUpdated(object sender, DataTransferEventArgs e)
        {
            base.Edit.IsValueChanged = true;
            this.OnEditValueChanged();
        }

        public override bool PostEditor(bool flushPendingEditActions = true)
        {
            IInputElement focusedElement = Keyboard.FocusedElement;
            base.UpdateFocusIfNeeded(base.GetEditValueBinding(base.editCore) != null, true);
            bool flag = false;
            if ((base.View == null) || !base.View.AreUpdateRowButtonsShown)
            {
                if (base.View.ShowUpdateRowButtonsCore != ShowUpdateRowButtons.Never)
                {
                    if (!base.Edit.IsValueChanged)
                    {
                        if (this.HasCellEditTemplateAssigned)
                        {
                            base.Edit.IsValueChanged = true;
                        }
                        else if (base.Edit.IsEditorActive && (base.Edit.EditValue != base.EditableValue))
                        {
                            base.Edit.IsValueChanged = true;
                        }
                    }
                    base.Edit.IsValueChanged = base.Edit.IsValueChanged ? true : !base.RowDataCore.NeedUpdateRowButtonsInit(base.Column).Item1;
                }
                flag = base.PostEditor(flushPendingEditActions);
            }
            else
            {
                if (base.Edit.IsValueChanged)
                {
                    base.RowDataCore.UpdateRowButtonsWasChanged = true;
                    if (base.View.AllowLeaveInvalidEditor)
                    {
                        base.CellData.UpdateCellError(base.CellData.RowData.RowHandleCore, base.Column, true);
                    }
                    else
                    {
                        this.SetValidationError(this.Validate() as RowValidationError);
                        flag = !base.View.HasCellEditorError;
                        if (flag)
                        {
                            this.ValidateEditor(flushPendingEditActions);
                        }
                    }
                }
                if (!flag)
                {
                    base.HideEditor(true);
                }
            }
            if (base.View.HasCellEditorError && ((focusedElement != null) && (LayoutHelper.IsChildElement(base.View.CurrentCellEditor, (DependencyObject) focusedElement, true) && !ReferenceEquals(focusedElement, Keyboard.FocusedElement))))
            {
                focusedElement.Focus();
            }
            return flag;
        }

        protected override bool PostEditorCore()
        {
            if (base.View.HasCellEditorError)
            {
                return false;
            }
            if (base.HasAccessToCellValue)
            {
                BindingExpressionBase editValueBinding = base.GetEditValueBinding(base.editCore);
                if ((editValueBinding != null) && !this.HasBindingErrors)
                {
                    editValueBinding.UpdateTarget();
                }
                if (!this.SkipSetValue())
                {
                    try
                    {
                        object editableValue = base.EditableValue;
                        if (editValueBinding != null)
                        {
                            editableValue = base.CellData.Value;
                        }
                        if ((editValueBinding == null) || ((editableValue != base.CellData.InitialValue) && (editableValue != base.DataControl.GetCellValue(this.RowHandle, base.Column.FieldName))))
                        {
                            base.DataControl.SetCellValueCore(base.View.DataProviderBase.CurrentControllerRow, base.FieldNameCore, editableValue);
                        }
                        if (!this.CheckBindingErrors())
                        {
                            return false;
                        }
                    }
                    catch (Exception exception)
                    {
                        this.ShowError(base.View.CreateCellValidationError(exception.Message, ErrorType.Default, base.RowData.RowHandle.Value, base.Column, exception), true);
                        return false;
                    }
                }
                if ((base.View != null) && ((base.View.DataControl != null) && !base.View.AllowLeaveInvalidEditor))
                {
                    base.RowData.UpdateData();
                }
            }
            return true;
        }

        protected override void RestoreValidationError()
        {
            this.RestoreValidationError(true);
        }

        protected void RestoreValidationError(bool customValidate)
        {
            base.View.ValidationError = null;
            base.RowData.UpdateDataErrors(customValidate);
        }

        protected override DataTemplate SelectTemplate()
        {
            if (base.IsFocusedCell && base.View.IsEditorOpen)
            {
                DataTemplate template = base.Column.ActualCellEditTemplateSelector.SelectTemplate(base.CellData, this);
                if (template != null)
                {
                    return template;
                }
            }
            else
            {
                DataTemplate template2 = base.Column.ActualCellDisplayTemplateSelector.SelectTemplate(base.CellData, this);
                if (template2 != null)
                {
                    return template2;
                }
            }
            return base.Column.ActualCellTemplateSelector.SelectTemplate(base.CellData, this);
        }

        internal void SetValidationError(RowValidationError validationError)
        {
            base.View.ValidationError = validationError;
            BaseEditHelper.SetValidationError(base.RowData, validationError);
            base.CellData.SetValidationError(validationError);
            base.Edit.SetValidationError(validationError);
            base.RowData.UpdateIndicatorState();
        }

        private void ShowError(RowValidationError validationError, bool customValidate = true)
        {
            this.SetValidationError(null);
            if (validationError != null)
            {
                this.SetValidationError(validationError);
            }
            else if ((base.View == null) || !base.View.AllowLeaveInvalidEditor)
            {
                this.RestoreValidationError(customValidate);
            }
        }

        private bool SkipSetValue()
        {
            RowData rowData = base.View.GetRowData(base.View.DataProviderBase.CurrentControllerRow);
            return ((rowData != null) && (base.RowData.Row != rowData.Row));
        }

        private void UpdateDisplayMemberBindingValue()
        {
            if ((base.RowData.Row == null) && (base.RowData.RowHandle.Value == -2147483647))
            {
                base.RowData.Row = base.RowData.treeBuilder.GetRowValue(base.RowData);
            }
            base.CellData.UpdateGridCellDataValue();
        }

        protected override void UpdateEditContext()
        {
            Tuple<bool, object> tuple = base.RowData.NeedUpdateRowButtonsInit(base.ColumnCore);
            if ((base.GetEditValueBinding(base.editCore) == null) && tuple.Item1)
            {
                base.UpdateEditContext();
            }
            else if (!tuple.Item1)
            {
                base.EditableValue = tuple.Item2;
            }
        }

        protected override void UpdateEditorDataContextValue(object newValue)
        {
            if (base.GetEditValueBinding(base.editCore) == null)
            {
                base.EditorDataContext.Value = newValue;
            }
        }

        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            if (base.GetEditValueBinding(editor) == null)
            {
                base.UpdateEditValueCore(editor);
            }
        }

        protected override void UpdateValidationError()
        {
            base.Edit.SetValidationError(BaseEdit.GetValidationError(base.CellData));
        }

        public BaseValidationError Validate()
        {
            ColumnBase column = base.Column;
            if (column == null)
            {
                return null;
            }
            BaseValidationError error = null;
            error = RowValidationHelper.ValidateAttributes(base.View, base.EditableValue, this.RowHandle, column);
            return ((error == null) ? RowValidationHelper.ValidateEvents(base.View, base.CellData, base.EditableValue, this.RowHandle, column) : error);
        }

        public override void ValidateEditorCore()
        {
            bool enableImmediatePosting;
            if ((base.View == null) || (base.View.DataProviderBase == null))
            {
                return;
            }
            else
            {
                if (base.EditValueChangedLocker.IsLocked && (base.View.EnableImmediatePosting && (base.ValidationErrorCache == null)))
                {
                    base.ValidationErrorCache = this.GetValidationError();
                    base.ValidationErrorCacheIsValid = true;
                }
                if (base.View.AllowLeaveInvalidEditor)
                {
                    bool? allowLiveDataShaping = base.View.DataProviderBase.AllowLiveDataShaping;
                    bool flag = false;
                    if (!((allowLiveDataShaping.GetValueOrDefault() == flag) ? (allowLiveDataShaping != null) : false))
                    {
                        enableImmediatePosting = base.View.EnableImmediatePosting;
                        goto TR_0001;
                    }
                }
                enableImmediatePosting = true;
            }
        TR_0001:
            this.ShowError(base.ValidationErrorCacheIsValid ? base.ValidationErrorCache : this.GetValidationError(), enableImmediatePosting);
        }

        protected override bool OptimizeEditorPerformance =>
            true;

        protected override bool IsInTree =>
            base.IsInTree && (base.RowDataCore != null);

        protected internal override int RowHandle =>
            (base.RowDataCore.RowHandleCore != null) ? base.RowDataCore.RowHandleCore.Value : -2147483648;

        protected override bool IsReadOnly =>
            base.Column.IsActualReadOnly;

        protected override bool OverrideCellTemplate =>
            (base.Column == null) || ReferenceEquals(base.Column.OwnerControl, null);

        protected override bool AssignEditorSettings =>
            (base.View != null) ? base.View.AssignEditorSettings : true;

        protected override bool UpdateContentOnShowEditor =>
            (base.Column != null) ? (!this.HasCellEditTemplateAssigned ? ((base.Column.ActualCellDisplayTemplateSelector != null) && ((base.Column.ActualCellDisplayTemplateSelector.Template != null) || (base.Column.ActualCellDisplayTemplateSelector.Selector != null))) : true) : false;

        protected override bool HasCellEditTemplateAssigned =>
            (base.Column != null) ? ((base.Column.ActualCellEditTemplateSelector != null) && ((base.Column.ActualCellEditTemplateSelector.Template != null) || (base.Column.ActualCellEditTemplateSelector.Selector != null))) : false;

        protected internal override bool HasBindingErrors =>
            (this.bindingErrors != null) && (this.bindingErrors.Count > 0);

        private bool AllowClipboardPaste
        {
            get
            {
                ITableView view = base.View as ITableView;
                return ((view != null) ? (!base.View.IsEditing && view.AllowClipboardPaste) : false);
            }
        }
    }
}

