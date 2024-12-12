namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Editors.ExpressionEditor;
    using System;
    using System.Windows;

    public class UnboundExpressionEditorEventArgs : ExpressionEditorCreatedEventArgsBase
    {
        public UnboundExpressionEditorEventArgs(ExpressionEditorControl control, ColumnBase column) : base(control, column)
        {
        }

        internal UnboundExpressionEditorEventArgs(RoutedEvent routedEvent, ColumnBase column) : base(routedEvent, column, null)
        {
        }

        internal IAutoCompleteExpressionEditor _autoCompleteExpressionEditor =>
            base.ActualAutocompleteEditor;

        internal ExpressionEditorControl _expressionEditorControl =>
            base.ActualStandardEditor;

        internal ExpressionEditorMode ActualMode =>
            base.GetActualMode();

        public DataViewBase Source =>
            ((ColumnBase) base.Column).View;
    }
}

