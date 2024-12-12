namespace DevExpress.Data.Controls.ExpressionEditor
{
    using DevExpress.Data.Filtering.Helpers;

    public interface ICriteriaOperatorValidatorProvider
    {
        ErrorsEvaluatorCriteriaValidator GetCriteriaOperatorValidator(ExpressionEditorContext context);
    }
}

