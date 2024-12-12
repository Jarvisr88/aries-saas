namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public interface IEditorSource
    {
        void AcceptEditableValue(UITypeEditorValue value);
        UITypeEditorValue GetEditableValue(DependencyObject owner, object defaultValue);

        object Content { get; }
    }
}

