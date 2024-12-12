namespace DevExpress.Xpf.Printing.BrickCollection
{
    using System;
    using System.Windows;

    public interface IVisualTreeWalker
    {
        DependencyObject GetChild(DependencyObject reference, int childIndex);
        int GetChildrenCount(DependencyObject reference);
    }
}

