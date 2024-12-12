namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System.Windows;

    public class RenderEditorControl : RenderRealControl
    {
        protected override FrameworkRenderElementContext CreateContextInstance() => 
            new RenderEditorControlContext(this);

        protected override FrameworkElement CreateFrameworkElement(FrameworkRenderElementContext context)
        {
            EditorControlStub stub1 = new EditorControlStub();
            stub1.Focusable = false;
            stub1.FocusVisualStyle = null;
            return stub1;
        }
    }
}

