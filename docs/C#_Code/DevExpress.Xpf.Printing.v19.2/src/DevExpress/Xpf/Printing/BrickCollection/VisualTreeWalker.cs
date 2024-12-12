namespace DevExpress.Xpf.Printing.BrickCollection
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class VisualTreeWalker : IVisualTreeWalker
    {
        public DependencyObject GetChild(DependencyObject reference, int childIndex) => 
            VisualTreeHelper.GetChild(reference, childIndex);

        public int GetChildrenCount(DependencyObject reference) => 
            VisualTreeHelper.GetChildrenCount(reference);
    }
}

