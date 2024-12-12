namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Windows;

    public class EditorRenderCheckBoxTemplateSelector : RenderCheckBoxTemplateSelector
    {
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content)
        {
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(chrome);
            EditorRenderCheckBoxContext context2 = (EditorRenderCheckBoxContext) context;
            return ((context2.DisplayMode != CheckEditDisplayMode.Image) ? base.SelectTemplate(chrome, context, content) : ((context2.GlyphTemplate != null) ? resourceProvider.GetRenderImageWithTemplateCheckBoxTemplate(chrome) : resourceProvider.GetRenderImageCheckBoxTemplate(chrome)));
        }
    }
}

