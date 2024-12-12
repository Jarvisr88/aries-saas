namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;

    public class InplaceButtonInfoTemplateSelector : RenderTemplateSelector
    {
        public override RenderTemplate SelectTemplate(FrameworkElement chrome, FrameworkRenderElementContext context, object content)
        {
            ButtonInfo info = (ButtonInfo) content;
            InplaceResourceProvider resourceProvider = ThemeHelper.GetResourceProvider(chrome);
            GlyphKind glyphKind = info.GlyphKind;
            if (info.ContentRenderTemplate != null)
            {
                return info.ContentRenderTemplate;
            }
            switch (glyphKind)
            {
                case GlyphKind.User:
                    return resourceProvider.GetButtonInfoUserGlyphKindTemplate(chrome);

                case GlyphKind.Custom:
                    return resourceProvider.GetButtonInfoCustomGlyphKindTemplate(chrome);

                case GlyphKind.DropDown:
                    return resourceProvider.GetButtonInfoDropDownGlyphKindTemplate(chrome);

                case GlyphKind.Regular:
                    return resourceProvider.GetButtonInfoRegularGlyphKindTemplate(chrome);

                case GlyphKind.Right:
                    return resourceProvider.GetButtonInfoRightGlyphKindTemplate(chrome);

                case GlyphKind.Left:
                    return resourceProvider.GetButtonInfoLeftGlyphKindTemplate(chrome);

                case GlyphKind.Up:
                    return resourceProvider.GetButtonInfoUpGlyphKindTemplate(chrome);

                case GlyphKind.Down:
                    return resourceProvider.GetButtonInfoDownGlyphKindTemplate(chrome);

                case GlyphKind.Cancel:
                    return resourceProvider.GetButtonInfoCancelGlyphKindTemplate(chrome);

                case GlyphKind.Apply:
                    return resourceProvider.GetButtonInfoApplyGlyphKindTemplate(chrome);

                case GlyphKind.Plus:
                    return resourceProvider.GetButtonInfoPlusGlyphKindTemplate(chrome);

                case GlyphKind.Minus:
                    return resourceProvider.GetButtonInfoMinusGlyphKindTemplate(chrome);

                case GlyphKind.Redo:
                    return resourceProvider.GetButtonInfoRedoGlyphKindTemplate(chrome);

                case GlyphKind.Undo:
                    return resourceProvider.GetButtonInfoUndoGlyphKindTemplate(chrome);

                case GlyphKind.Refresh:
                    return resourceProvider.GetButtonInfoRefreshGlyphKindTemplate(chrome);

                case GlyphKind.Search:
                    return resourceProvider.GetButtonInfoSearchGlyphKindTemplate(chrome);

                case GlyphKind.NextPage:
                    return resourceProvider.GetButtonInfoNextPageGlyphKindTemplate(chrome);

                case GlyphKind.PrevPage:
                    return resourceProvider.GetButtonInfoPrevPageGlyphKindTemplate(chrome);

                case GlyphKind.Last:
                    return resourceProvider.GetButtonInfoLastGlyphKindTemplate(chrome);

                case GlyphKind.First:
                    return resourceProvider.GetButtonInfoFirstGlyphKindTemplate(chrome);

                case GlyphKind.Edit:
                    return resourceProvider.GetButtonInfoEditGlyphKindTemplate(chrome);
            }
            return resourceProvider.GetButtonInfoNoneGlyphKindTemplate(chrome);
        }
    }
}

