namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows.Input;
    using System.Windows.Media;

    public interface IConditionalFormattingDialogBuilder
    {
        BarButtonItem CreateBarButtonItem(BarItemLinkCollection links, string name, string content, bool beginGroup, ImageSource image, ICommand command, object commandParameter);
        BarSplitButtonItem CreateBarSplitButtonItem(BarItemLinkCollection links, string name, string content, bool beginGroup, ImageSource image);
        BarSubItem CreateBarSubItem(string name, string content, bool beginGroup, ImageSource image, ICommand command);
        BarSubItem CreateBarSubItem(BarItemLinkCollection links, string name, string content, bool beginGroup, ImageSource image, ICommand command);
    }
}

