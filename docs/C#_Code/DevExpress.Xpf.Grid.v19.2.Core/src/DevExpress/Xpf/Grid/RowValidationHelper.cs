namespace DevExpress.Xpf.Grid
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Windows.Controls;

    internal static class RowValidationHelper
    {
        public static RowValidationError CreateEditorValidationError(DataViewBase view, int rowHandle, Exception exception, ColumnBase column)
        {
            object errorContent = (exception == null) ? view.GetLocalizedString(GridControlStringId.InvalidValueErrorMessage) : exception.Message;
            return view.CreateCellValidationError(errorContent, ErrorType.Default, rowHandle, column, null);
        }

        public static RowValidationError ValidateAttributes(DataViewBase view, object value, int rowHandle, ColumnBase column)
        {
            string validationAttributesErrorText = column.GetValidationAttributesErrorText(value, rowHandle);
            return (string.IsNullOrEmpty(validationAttributesErrorText) ? null : view.CreateCellValidationError(validationAttributesErrorText, ErrorType.Default, rowHandle, column, null));
        }

        public static RowValidationError ValidateEvents(DataViewBase view, object source, object value, int rowHandle, ColumnBase column)
        {
            GridRowValidationEventArgs e = view.CreateCellValidationEventArgs(source, value, rowHandle, column);
            ValidationResult result = null;
            Exception exception = null;
            try
            {
                view.OnValidation(column, e);
                result = new ValidationResult(e.IsValid, e.ErrorContent);
            }
            catch (Exception exception2)
            {
                exception = exception2;
                result = new ValidationResult(false, exception2.Message);
            }
            ErrorType errorType = (e.ErrorType == ErrorType.None) ? ErrorType.Default : e.ErrorType;
            return (result.IsValid ? null : view.CreateCellValidationError(result.ErrorContent, errorType, rowHandle, column, exception));
        }
    }
}

