namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data;
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Exceptions;
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;

    public static class ConditionalFormattingDialogHelper
    {
        public static bool ShowInvalidExpressionError(IMessageBoxService service)
        {
            service.Show(ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_CustomConditionDialog_InvalidExpressionMessageEx), ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_CustomConditionDialog_InvalidExpressionMessageTitle));
            return false;
        }

        public static bool ValidateExpression(string expression, IMessageBoxService service, IDataColumnInfo columnInfo)
        {
            bool flag;
            string str = expression;
            string criteria = ManagerHelper.ConvertExpression(expression, columnInfo, new Func<IDataColumnInfo, string, string>(UnboundExpressionConvertHelper.ConvertToFields));
            try
            {
                CriteriaOperator.Parse(criteria, (object[]) null);
            }
            catch (CriteriaParserException exception)
            {
                service.Show(string.Format(ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_CustomConditionDialog_InvalidExpressionMessage), exception.Column), ConditionalFormattingLocalizer.GetString(ConditionalFormattingStringId.ConditionalFormatting_CustomConditionDialog_InvalidExpressionMessageTitle));
                return false;
            }
            catch
            {
                return ShowInvalidExpressionError(service);
            }
            try
            {
                UnboundExpressionConvertHelper.ValidateExpressionFields(columnInfo, str);
                return true;
            }
            catch (Exception)
            {
                flag = ShowInvalidExpressionError(service);
            }
            return flag;
        }
    }
}

