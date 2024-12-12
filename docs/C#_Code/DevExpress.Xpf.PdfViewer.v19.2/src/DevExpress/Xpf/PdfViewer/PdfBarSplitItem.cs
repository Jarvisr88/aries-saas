namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Themes;
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class PdfBarSplitItem : BarSplitButtonItem
    {
        public PdfBarSplitItem()
        {
            base.PopupControl = new DevExpress.Xpf.Bars.PopupMenu();
        }

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            BindingOperations.ClearBinding(this.PopupMenu, DevExpress.Xpf.Bars.PopupMenu.ItemLinksSourceProperty);
            this.PopupMenu.ClearValue(DevExpress.Xpf.Bars.PopupMenu.ItemTemplateProperty);
            this.PopupMenu.ClearValue(DevExpress.Xpf.Bars.PopupMenu.ItemTemplateProperty);
            if (newCommand != null)
            {
                base.DataContext = newCommand;
                Binding binding = new Binding("Commands") {
                    Source = newCommand
                };
                this.PopupMenu.SetBinding(DevExpress.Xpf.Bars.PopupMenu.ItemLinksSourceProperty, binding);
                PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
                resourceKey.ResourceKey = PdfViewerThemeKeys.PopupMenuItemTemplate;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                this.PopupMenu.ItemTemplate = (DataTemplate) base.FindResource(resourceKey);
                PdfViewerThemeKeyExtension extension2 = new PdfViewerThemeKeyExtension();
                extension2.ResourceKey = PdfViewerThemeKeys.BarSplitButtonItemStyle;
                extension2.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(extension2);
            }
        }

        private DevExpress.Xpf.Bars.PopupMenu PopupMenu =>
            base.PopupControl as DevExpress.Xpf.Bars.PopupMenu;
    }
}

