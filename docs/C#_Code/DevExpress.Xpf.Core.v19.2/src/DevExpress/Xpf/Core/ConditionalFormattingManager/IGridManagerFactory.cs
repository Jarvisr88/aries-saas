namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System.Windows;
    using System.Windows.Controls;

    public interface IGridManagerFactory
    {
        UIElement CreateGrid();
        ContentControl CreatePreviewControl();
    }
}

