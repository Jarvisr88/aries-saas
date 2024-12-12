namespace DevExpress.Xpf.Editors.ExpressionEditor.Native
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.ExpressionEditor;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    [StructLayout(LayoutKind.Sequential)]
    public struct ExpressionEditorParams
    {
        public ExpressionEditorDialogClosedDelegate ClosedHandler { get; private set; }
        public IDataColumnInfo Column { get; private set; }
        public Window DialogWindow { get; set; }
        public ExpressionEditorMode Mode { get; set; }
        public ISupportExpressionString AutoCompleteEditor { get; set; }
        public ISupportExpressionString StandardEditor { get; set; }
        public DevExpress.Xpf.Core.Theme Theme { get; set; }
        public string CustomExpression { get; set; }
        public FrameworkElement RootElement { get; set; }
        public ExpressionEditorParams(IDataColumnInfo column, ExpressionEditorDialogClosedDelegate closedHandler)
        {
            this = new ExpressionEditorParams();
            this.ClosedHandler = closedHandler;
            this.Column = column;
            this.Mode = ExpressionEditorMode.Default;
            this.AutoCompleteEditor = null;
            this.StandardEditor = null;
            this.CustomExpression = null;
            this.Theme = null;
            this.DialogWindow = null;
            this.RootElement = null;
        }

        public ExpressionEditorParams(ExpressionEditorCreatedEventArgsBase args, ExpressionEditorDialogClosedDelegate closedHandler) : this(args.Column, closedHandler)
        {
            this.Mode = args._actualMode;
            this.AutoCompleteEditor = args._autoCompleteExpressionEditor;
            this.StandardEditor = args._expressionEditorControl;
            this.CustomExpression = args.Expression;
        }
    }
}

