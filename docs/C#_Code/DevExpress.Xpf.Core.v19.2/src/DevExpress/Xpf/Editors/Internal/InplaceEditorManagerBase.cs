namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public abstract class InplaceEditorManagerBase : IInplaceEditorManager
    {
        private IBaseEdit editor;
        private InplaceEditorOwnerBase editorOwner;
        private IInplaceEditorOwnerInfo ownerInfo;

        protected InplaceEditorManagerBase()
        {
        }

        public virtual void Assign(IBaseEdit editor, InplaceEditorOwnerBase editorOwner, IInplaceEditorOwnerInfo editorInfo)
        {
            this.editor = editor;
            this.editorOwner = editorOwner;
            this.ownerInfo = editorInfo;
        }

        public virtual bool PostEditor() => 
            false;

        public virtual void PreviewKeyDown(KeyEventArgs args)
        {
        }

        public virtual void PreviewMouseDown(KeyEventArgs args)
        {
        }

        public virtual void PreviewTextInput(TextCompositionEventArgs args)
        {
        }

        public virtual bool ShowEditor(bool selectAll = false) => 
            false;

        protected internal IBaseEdit Editor =>
            this.editor;

        protected internal InplaceEditorOwnerBase EditorOwner =>
            this.editorOwner;

        protected internal IInplaceEditorOwnerInfo OwnerInfo =>
            this.ownerInfo;

        protected virtual bool IsEditorVisible =>
            this.Editor.EditMode == EditMode.InplaceActive;

        protected virtual bool IsReadOnly =>
            this.OwnerInfo.IsReadOnly;

        protected virtual bool HasAccessToCell =>
            this.OwnerInfo.IsInTree && (this.OwnerInfo.EditorColumn != null);
    }
}

