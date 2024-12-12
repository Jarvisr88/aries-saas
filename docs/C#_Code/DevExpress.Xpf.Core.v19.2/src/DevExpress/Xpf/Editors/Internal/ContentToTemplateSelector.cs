namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;

    public class ContentToTemplateSelector : RenderTemplateSelector
    {
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content) => 
            content as RenderTemplate;
    }
}

