namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class WindowButtonHelper
    {
        public static readonly DependencyProperty IsMouseOverProperty;
        public static readonly DependencyProperty IsActiveAppearanceProperty;
        public static readonly DependencyProperty IsYesNoDialogProperty;

        static WindowButtonHelper();
        public static bool GetIsActiveAppearance(DependencyObject element);
        public static bool GetIsMouseOver(DependencyObject element);
        public static bool GetIsYesNoDialog(DependencyObject element);
        public static void SetIsActiveAppearance(DependencyObject element, bool value);
        public static void SetIsMouseOver(DependencyObject element, bool value);
        public static void SetIsYesNoDialog(DependencyObject element, bool value);
    }
}

