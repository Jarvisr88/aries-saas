namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Input;

    public class CellEditorEditingProvider : IInplaceEditingProvider
    {
        public bool HandleScrollNavigation(Key key, ModifierKeys keys) => 
            true;

        public bool HandleTextNavigation(Key key, ModifierKeys keys) => 
            true;
    }
}

