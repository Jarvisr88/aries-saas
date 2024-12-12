namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class EditGridCellData : GridCellData, IDataErrorInfo
    {
        public EditGridCellData(RowData rowData) : base(rowData)
        {
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (ReferenceEquals(e.Property, BaseEdit.ValidationErrorProperty) && !this.HasBindingErrors)
            {
                base.RaisePropertyChanged("Value");
            }
            base.OnPropertyChanged(e);
        }

        protected override void OnValueChanged(object oldValue)
        {
            base.OnValueChanged(oldValue);
            if (this.IsEditing)
            {
                base.View.ViewBehavior.OnFocusedRowCellModified();
                base.Editor.OnDataContextValueChanged(base.Value);
            }
        }

        internal override void UpdateEditorButtonVisibility()
        {
            if (base.editor != null)
            {
                base.editor.UpdateEditorButtonVisibility();
            }
        }

        internal override bool IsEditing =>
            base.View.IsEditingCore && ReferenceEquals(base.View.CurrentCellEditor, base.editor);

        protected bool HasBindingErrors =>
            (base.Editor != null) ? base.Editor.HasBindingErrors : false;

        string IDataErrorInfo.Error =>
            string.Empty;

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                BaseValidationError validationError = BaseEdit.GetValidationError(this);
                if (validationError == null)
                {
                    BaseValidationError local1 = validationError;
                    return null;
                }
                object errorContent = validationError.ErrorContent;
                if (errorContent != null)
                {
                    return errorContent.ToString();
                }
                object local2 = errorContent;
                return null;
            }
        }
    }
}

