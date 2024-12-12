namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class ThemedWindowOptions
    {
        public static readonly DependencyProperty UseWindow10HeaderColorProperty;
        public static readonly DependencyProperty UseCustomDialogFooterProperty;
        public static readonly DependencyProperty AllowSystemMenuProperty;
        public static readonly DependencyProperty ShowOverPopupsProperty;
        public static readonly DependencyProperty DeclareHeaderAsContentProperty;

        static ThemedWindowOptions();
        public static bool GetAllowSystemMenu(DependencyObject element);
        public static bool GetDeclareHeaderAsContent(DependencyObject element);
        public static bool GetShowOverPopups(Window element);
        public static bool GetUseCustomDialogFooter(DependencyObject element);
        public static bool GetUseWindow10HeaderColor(DependencyObject element);
        public static void SetAllowSystemMenu(DependencyObject element, bool value);
        public static void SetDeclareHeaderAsContent(DependencyObject element, bool value);
        public static void SetShowOverPopups(Window element, bool value);
        public static void SetUseCustomDialogFooter(DependencyObject element, bool value);
        public static void SetUseWindow10HeaderColor(DependencyObject element, bool value);
        private static void ShowOverPopupsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
    }
}

