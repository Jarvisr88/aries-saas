namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer.Themes;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public abstract class DocumentViewerBarSubItem : BarSubItem
    {
        protected DocumentViewerBarSubItem()
        {
        }

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            CommandBase base2 = newCommand as CommandBase;
            if (base2 == null)
            {
                base.ClearValue(FrameworkContentElement.StyleProperty);
            }
            else
            {
                base.DataContext = base2;
                DocumentViewerThemeKeyExtension resourceKey = new DocumentViewerThemeKeyExtension();
                resourceKey.ResourceKey = DocumentViewerThemeKeys.BarSubItemStyle;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(resourceKey);
            }
        }
    }
}

