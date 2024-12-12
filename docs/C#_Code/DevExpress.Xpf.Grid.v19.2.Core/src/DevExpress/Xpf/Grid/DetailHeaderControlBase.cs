namespace DevExpress.Xpf.Grid
{
    using System;
    using System.Windows;

    public abstract class DetailHeaderControlBase : DetailRowControlBase
    {
        public static readonly DependencyProperty ShowLastDetailMarginProperty = DependencyPropertyManager.Register("ShowLastDetailMargin", typeof(bool), typeof(DetailHeaderControlBase), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty DetailDescriptorProperty = DependencyPropertyManager.Register("DetailDescriptor", typeof(DetailDescriptorBase), typeof(DetailHeaderControlBase), new PropertyMetadata(null));
        public static readonly DependencyProperty ShowBottomLineProperty = DependencyPropertyManager.Register("ShowBottomLine", typeof(bool), typeof(DetailHeaderControlBase), new PropertyMetadata(false));

        protected DetailHeaderControlBase()
        {
        }

        public bool ShowLastDetailMargin
        {
            get => 
                (bool) base.GetValue(ShowLastDetailMarginProperty);
            set => 
                base.SetValue(ShowLastDetailMarginProperty, value);
        }

        public DetailDescriptorBase DetailDescriptor
        {
            get => 
                (DetailDescriptorBase) base.GetValue(DetailDescriptorProperty);
            set => 
                base.SetValue(DetailDescriptorProperty, value);
        }

        public bool ShowBottomLine
        {
            get => 
                (bool) base.GetValue(ShowBottomLineProperty);
            set => 
                base.SetValue(ShowBottomLineProperty, value);
        }
    }
}

