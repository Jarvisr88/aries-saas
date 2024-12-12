namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;

    public class RenderButtonTemplateSelector : RenderTemplateSelector
    {
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content)
        {
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(chrome);
            ButtonInfo o = content as ButtonInfo;
            return ((o == null) ? (!(content is SpinButtonInfo) ? null : resourceProvider.GetRenderSpinButtonContainerTemplate(chrome)) : (!o.IsPropertySet(ButtonInfoBase.TemplateProperty) ? resourceProvider.GetRenderButtonContainerTemplate(chrome) : resourceProvider.GetRenderRealButtonContainerTemplate(chrome)));
        }
    }
}

