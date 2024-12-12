namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class FloatingLayoutItemsControl : LayoutItemsControl
    {
        static FloatingLayoutItemsControl()
        {
            new DependencyPropertyRegistrator<FloatingLayoutItemsControl>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        protected override DependencyObject GetContainerForItemOverride() => 
            new FloatingMultiTemplateControl();

        protected override bool IsItemItsOwnContainerOverride(object item) => 
            item is FloatingMultiTemplateControl;

        internal override void PrepareForWindowClosing()
        {
            for (int i = 0; i < base.Items.Count; i++)
            {
                FloatingMultiTemplateControl control = base.ItemContainerGenerator.ContainerFromIndex(i) as FloatingMultiTemplateControl;
                if (control != null)
                {
                    control.QueryClearTemplate();
                }
            }
        }
    }
}

