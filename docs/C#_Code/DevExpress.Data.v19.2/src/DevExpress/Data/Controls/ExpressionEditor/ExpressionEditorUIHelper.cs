namespace DevExpress.Data.Controls.ExpressionEditor
{
    using System;

    public static class ExpressionEditorUIHelper
    {
        public static bool RunExpressionEditor(ref string expressionString, IExpressionEditorView view, ExpressionEditorContext context);
        public static bool RunExpressionEditor(ref string expressionString, IExpressionEditorView view, ExpressionEditorContext context, Func<string, string> validate);
    }
}

