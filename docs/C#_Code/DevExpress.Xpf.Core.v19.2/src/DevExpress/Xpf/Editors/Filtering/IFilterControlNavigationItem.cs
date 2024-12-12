namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Windows;

    public interface IFilterControlNavigationItem
    {
        bool ShowPopupMenu();

        UIElement Child { get; }
    }
}

