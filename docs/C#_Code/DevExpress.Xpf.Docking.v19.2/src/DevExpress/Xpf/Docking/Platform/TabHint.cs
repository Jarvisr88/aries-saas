namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class TabHint : BaseTabHint
    {
        static TabHint()
        {
            new DependencyPropertyRegistrator<TabHint>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public TabHint() : base(TabHintType.Tab)
        {
        }
    }
}

