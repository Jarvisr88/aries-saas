namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class BarControlCollection : UIElementCollection
    {
        public BarControlCollection(UIElement visualParent, FrameworkElement logicalParent);
        public override int Add(UIElement element);
        public override void Insert(int index, UIElement element);
    }
}

