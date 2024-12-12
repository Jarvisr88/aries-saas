namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Themes;
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class PdfMRUSplitItem : BarSplitButtonItem
    {
        public PdfMRUSplitItem()
        {
            base.PopupControl = new DevExpress.Xpf.Bars.PopupMenu();
        }

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            BindingOperations.ClearBinding(this.PopupMenu, DevExpress.Xpf.Bars.PopupMenu.ItemLinksSourceProperty);
            this.PopupMenu.ClearValue(DevExpress.Xpf.Bars.PopupMenu.ItemTemplateProperty);
            if (newCommand != null)
            {
                base.DataContext = newCommand;
                new Binding("RecentFiles").Source = newCommand;
                PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
                resourceKey.ResourceKey = PdfViewerThemeKeys.BarSplitButtonItemStyle;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(resourceKey);
            }
        }

        private DevExpress.Xpf.Bars.PopupMenu PopupMenu =>
            base.PopupControl as DevExpress.Xpf.Bars.PopupMenu;
    }
}

