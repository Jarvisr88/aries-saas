﻿namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;

    public sealed class RenderSpinButtonDownTemplateSelector : RenderTemplateSelector
    {
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content) => 
            ThemeHelper.GetResourceProvider(chrome).GetRenderSpinDownButtonTemplate(chrome);
    }
}
