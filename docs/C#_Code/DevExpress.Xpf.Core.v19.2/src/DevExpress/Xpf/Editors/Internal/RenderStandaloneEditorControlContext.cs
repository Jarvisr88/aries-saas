namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;

    public class RenderStandaloneEditorControlContext : RenderRealControlContext
    {
        public RenderStandaloneEditorControlContext(RenderStandaloneEditorControl factory) : base(factory)
        {
        }

        protected override void AttachDataContext(FrameworkElement root)
        {
            this.Control.DataContext = root.TemplatedParent;
        }

        protected internal override void AttachToVisualTree(FrameworkElement root)
        {
            base.AttachToVisualTree(root);
            this.Control.Focusable = false;
            this.Control.Owner = root.TemplatedParent as BaseEdit;
        }

        protected internal override void DetachFromVisualTree(FrameworkElement root)
        {
            base.DetachFromVisualTree(root);
            this.Control.Owner = null;
        }

        private EditorControl Control =>
            (EditorControl) base.Control;
    }
}

