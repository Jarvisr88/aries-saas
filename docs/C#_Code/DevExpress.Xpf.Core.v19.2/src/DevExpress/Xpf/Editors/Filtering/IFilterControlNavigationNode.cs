namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;

    public interface IFilterControlNavigationNode
    {
        void ProcessKeyDown(KeyEventArgs e, UIElement focusedChild);
        bool ShowPopupMenu(UIElement child);

        IList<UIElement> Children { get; }

        IFilterControlNavigationNode ParentNode { get; }

        IList<IFilterControlNavigationNode> SubNodes { get; }
    }
}

