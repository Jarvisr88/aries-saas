namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Data;
    using DevExpress.Data.Controls.ExpressionEditor;
    using DevExpress.Xpf.Editors.ExpressionEditor.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class ExpressionEditorCreatedEventArgsBase : RoutedEventArgs
    {
        private bool _useStandardEditorOnly;
        internal IAutoCompleteExpressionEditor _autoCompleteExpressionEditor;
        internal DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorControl _expressionEditorControl;

        public ExpressionEditorCreatedEventArgsBase(DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorControl control, IDataColumnInfo column)
        {
            this.Column = column;
            this.ExpressionEditorMode = DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode.Standard;
            this.Expression = null;
            this._expressionEditorControl = control;
            this._useStandardEditorOnly = true;
        }

        public ExpressionEditorCreatedEventArgsBase(RoutedEvent routedEvent, IDataColumnInfo column, ExpressionEditorContext context = null) : base(routedEvent)
        {
            this.Column = column;
            this.ExpressionEditorMode = DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode.Default;
            this.Expression = null;
        }

        protected DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode GetActualMode() => 
            !this._useStandardEditorOnly ? ((this.ExpressionEditorMode != DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode.Default) ? this.ExpressionEditorMode : ((this._expressionEditorControl != null) ? DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode.Standard : DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode.AutoComplete)) : DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode.Standard;

        public IDataColumnInfo Column { get; private set; }

        public DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode ExpressionEditorMode { get; set; }

        public string Expression { get; set; }

        public virtual IAutoCompleteExpressionEditor AutoCompleteExpressionEditorControl
        {
            get
            {
                this._autoCompleteExpressionEditor ??= ExpressionEditorHelper.GetAutoCompleteExpressionEditorControl(this.Column, false);
                return this._autoCompleteExpressionEditor;
            }
        }

        public virtual DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorControl ExpressionEditorControl
        {
            get
            {
                this._expressionEditorControl ??= new DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorControl(this.Column);
                return this._expressionEditorControl;
            }
        }

        protected IAutoCompleteExpressionEditor ActualAutocompleteEditor
        {
            get => 
                this._autoCompleteExpressionEditor;
            set => 
                this._autoCompleteExpressionEditor = value;
        }

        protected DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorControl ActualStandardEditor
        {
            get => 
                this._expressionEditorControl;
            set => 
                this._expressionEditorControl = value;
        }

        internal DevExpress.Xpf.Editors.ExpressionEditor.ExpressionEditorMode _actualMode =>
            this.GetActualMode();
    }
}

