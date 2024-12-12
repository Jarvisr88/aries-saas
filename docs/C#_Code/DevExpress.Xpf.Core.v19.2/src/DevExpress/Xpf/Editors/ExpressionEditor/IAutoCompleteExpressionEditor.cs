namespace DevExpress.Xpf.Editors.ExpressionEditor
{
    using DevExpress.Xpf.Editors.ExpressionEditor.Native;

    public interface IAutoCompleteExpressionEditor : ISupportExpressionString
    {
        AutoCompleteExpressionEditorContextWrapper Context { get; }
    }
}

