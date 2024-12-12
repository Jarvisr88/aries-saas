namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class PagerControlNavigationButton : Button
    {
        static PagerControlNavigationButton()
        {
            UIElement.FocusableProperty.OverrideMetadata(typeof(PagerControlNavigationButton), new FrameworkPropertyMetadata(false));
        }

        public PagerControlNavigationButton()
        {
            this.SetDefaultStyleKey(typeof(PagerControlNavigationButton));
        }
    }
}

