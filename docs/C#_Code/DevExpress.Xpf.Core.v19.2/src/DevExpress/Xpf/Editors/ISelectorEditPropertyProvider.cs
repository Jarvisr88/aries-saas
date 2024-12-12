namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Popups;
    using System.Windows;

    public interface ISelectorEditPropertyProvider
    {
        Style GetItemContainerStyle();

        DevExpress.Xpf.Editors.Popups.SelectionViewModel SelectionViewModel { get; }
    }
}

