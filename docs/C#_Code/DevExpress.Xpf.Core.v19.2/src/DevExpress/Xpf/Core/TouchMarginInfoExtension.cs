namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class TouchMarginInfoExtension : TouchPaddingInfoExtension
    {
        public TouchMarginInfoExtension()
        {
            base.TargetProperty = FrameworkElement.MarginProperty;
        }
    }
}

