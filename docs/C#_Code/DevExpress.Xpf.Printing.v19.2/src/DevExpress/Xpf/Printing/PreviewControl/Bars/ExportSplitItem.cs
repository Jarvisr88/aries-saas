namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Printing.Themes;
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class ExportSplitItem : BarSplitButtonItem
    {
        public ExportSplitItem()
        {
            base.PopupControl = new DevExpress.Xpf.Bars.PopupMenu();
        }

        protected virtual NewDocumentViewerThemeKeys GetItemTemplateKey() => 
            NewDocumentViewerThemeKeys.ExportPopupMenuItemTemplate;

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            BindingOperations.ClearBinding(this.PopupMenu, DevExpress.Xpf.Bars.PopupMenu.ItemLinksSourceProperty);
            this.PopupMenu.ClearValue(DevExpress.Xpf.Bars.PopupMenu.ItemTemplateProperty);
            if (newCommand == null)
            {
                base.ClearValue(FrameworkContentElement.StyleProperty);
            }
            else
            {
                base.DataContext = newCommand;
                base.Style = base.TryFindResource(typeof(ExportSplitItem)) as Style;
                Binding binding = new Binding("Commands");
                this.PopupMenu.SetBinding(DevExpress.Xpf.Bars.PopupMenu.ItemLinksSourceProperty, binding);
                NewDocumentViewerThemeKeyExtension resourceKey = new NewDocumentViewerThemeKeyExtension();
                resourceKey.ResourceKey = this.GetItemTemplateKey();
                this.PopupMenu.ItemTemplate = base.FindResource(resourceKey) as DataTemplate;
            }
        }

        private DevExpress.Xpf.Bars.PopupMenu PopupMenu =>
            base.PopupControl as DevExpress.Xpf.Bars.PopupMenu;
    }
}

