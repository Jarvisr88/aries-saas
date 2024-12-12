namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System.Windows;

    public class RenderStandaloneEditorControl : RenderRealControl
    {
        protected override FrameworkRenderElementContext CreateContextInstance() => 
            new RenderStandaloneEditorControlContext(this);

        protected override FrameworkElement CreateFrameworkElement(FrameworkRenderElementContext context) => 
            new EditorControl();
    }
}

