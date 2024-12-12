namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Controls;

    public class FilterPopupVirtualizingStackPanel : DXVirtualizingStackPanel
    {
        public const int MaxNonVirtualItemsCount = 200;
        private static ItemsPanelTemplate filterPopupVirtualizingStackPanelTemplate;
        private static ItemsPanelTemplate stackPanelTemplate;

        private static ItemsPanelTemplate CreateVirtualizingStackPanelTemplate() => 
            XamlHelper.GetItemsPanelTemplate(string.Format("<{0}:{2} xmlns:{0}=\"{1}\"/>", "dx", "http://schemas.devexpress.com/winfx/2008/xaml/core", typeof(FilterPopupVirtualizingStackPanel).Name));

        public static ItemsPanelTemplate GetItemsPanelTemplate(int recordCount) => 
            (recordCount > 200) ? FilterPopupVirtualizingStackPanelTemplate : StackPanelTemplate;

        private static ItemsPanelTemplate FilterPopupVirtualizingStackPanelTemplate =>
            filterPopupVirtualizingStackPanelTemplate ??= CreateVirtualizingStackPanelTemplate();

        private static ItemsPanelTemplate StackPanelTemplate =>
            stackPanelTemplate ??= XamlHelper.GetItemsPanelTemplate("<StackPanel/>");
    }
}

