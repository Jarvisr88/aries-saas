namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Themes;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class PageLayoutBarItem : DocumentViewerBarSubItem
    {
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
                PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
                resourceKey.ResourceKey = PdfViewerThemeKeys.BarSubItemStyle;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(resourceKey);
            }
        }
    }
}

