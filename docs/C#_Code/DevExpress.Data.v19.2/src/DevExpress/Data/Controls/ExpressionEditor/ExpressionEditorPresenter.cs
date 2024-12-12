namespace DevExpress.Data.Controls.ExpressionEditor
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class ExpressionEditorPresenter : PresenterBase<ExpressionEditorModel, IExpressionEditorView, ExpressionEditorPresenter>, IExpressionEditorPresenter
    {
        private readonly Func<string, string> validate;

        public ExpressionEditorPresenter(ExpressionEditorModel model, IExpressionEditorView view, ExpressionEditorContext context, Func<string, string> validate);
        public ExpressionEditorPresenter(ExpressionEditorModel model, IExpressionEditorView view, Func<string, string> validate, ExpressionEditorContext context);
        protected override void Commit();
        protected override void InitViewCore();
        protected override string Validate();

        public ExpressionEditorContext Context { get; private set; }
    }
}

