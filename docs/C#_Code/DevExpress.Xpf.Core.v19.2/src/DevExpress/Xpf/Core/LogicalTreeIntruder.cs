namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public static class LogicalTreeIntruder
    {
        private static UIElement fakeVisualParent = new UIElement();

        public static void AddLogicalChild(FrameworkElement logicalParent, UIElement element)
        {
            new UIElementCollectionIntruder(fakeVisualParent, logicalParent).AddLogicalChild(element);
        }

        public static void RemoveLogicalChild(UIElement element)
        {
            FrameworkElement parent = LogicalTreeHelper.GetParent(element) as FrameworkElement;
            new UIElementCollectionIntruder(fakeVisualParent, parent).RemoveLogicalChild(element);
        }

        private class UIElementCollectionIntruder : UIElementCollection
        {
            public UIElementCollectionIntruder(UIElement visualParent, FrameworkElement logicalParent) : base(visualParent, logicalParent)
            {
            }

            public void AddLogicalChild(UIElement element)
            {
                base.SetLogicalParent(element);
            }

            public void RemoveLogicalChild(UIElement element)
            {
                base.ClearLogicalParent(element);
            }
        }
    }
}

