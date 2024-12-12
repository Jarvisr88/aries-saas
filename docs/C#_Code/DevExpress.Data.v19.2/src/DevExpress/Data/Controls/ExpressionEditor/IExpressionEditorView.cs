namespace DevExpress.Data.Controls.ExpressionEditor
{
    using DevExpress.Utils;
    using System;

    public interface IExpressionEditorView : IView<IExpressionEditorPresenter>
    {
        string ExpressionString { get; set; }
    }
}

