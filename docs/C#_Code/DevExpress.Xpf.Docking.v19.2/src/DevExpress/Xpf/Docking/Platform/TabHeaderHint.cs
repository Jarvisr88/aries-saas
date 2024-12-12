namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class TabHeaderHint : BaseTabHint
    {
        static TabHeaderHint()
        {
            new DependencyPropertyRegistrator<TabHeaderHint>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public TabHeaderHint() : base(TabHintType.TabHeader)
        {
        }
    }
}

