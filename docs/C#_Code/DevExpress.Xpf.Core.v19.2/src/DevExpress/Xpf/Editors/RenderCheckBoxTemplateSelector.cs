namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;

    public class RenderCheckBoxTemplateSelector : RenderTemplateSelector
    {
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content) => 
            ThemeHelper.GetResourceProvider(chrome).GetRenderCheckBoxTemplate(chrome);
    }
}

