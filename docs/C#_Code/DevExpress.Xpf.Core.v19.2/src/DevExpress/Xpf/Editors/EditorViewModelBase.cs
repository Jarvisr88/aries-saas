namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class EditorViewModelBase : FrameworkElement
    {
        private WeakReference editor;

        public EditorViewModelBase(BaseEdit editor)
        {
            this.editor = new WeakReference(editor);
        }

        public BaseEdit OwnerEdit =>
            (BaseEdit) this.editor.Target;

        public ActualPropertyProvider PropertyProvider =>
            ActualPropertyProvider.GetProperties(this.OwnerEdit);
    }
}

