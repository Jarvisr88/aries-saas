namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Windows.Input;

    public interface IInplaceEditorManager
    {
        void PreviewKeyDown(KeyEventArgs args);
        void PreviewMouseDown(KeyEventArgs args);
        void PreviewTextInput(TextCompositionEventArgs args);
    }
}

