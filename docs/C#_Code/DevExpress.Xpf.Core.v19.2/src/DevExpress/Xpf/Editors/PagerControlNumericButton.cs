namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class PagerControlNumericButton : PagerControlNavigationButton
    {
        public static readonly DependencyProperty NumberProperty;
        public static readonly DependencyProperty ShowEllipsisProperty;

        static PagerControlNumericButton()
        {
            Type ownerType = typeof(PagerControlNumericButton);
            NumberProperty = DependencyPropertyManager.Register("Number", typeof(int), ownerType, new FrameworkPropertyMetadata(0));
            ShowEllipsisProperty = DependencyPropertyManager.Register("ShowEllipsis", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
        }

        public PagerControlNumericButton()
        {
            this.SetDefaultStyleKey(typeof(PagerControlNumericButton));
        }

        public int Number
        {
            get => 
                (int) base.GetValue(NumberProperty);
            set => 
                base.SetValue(NumberProperty, value);
        }

        public bool ShowEllipsis
        {
            get => 
                (bool) base.GetValue(ShowEllipsisProperty);
            set => 
                base.SetValue(ShowEllipsisProperty, value);
        }
    }
}

