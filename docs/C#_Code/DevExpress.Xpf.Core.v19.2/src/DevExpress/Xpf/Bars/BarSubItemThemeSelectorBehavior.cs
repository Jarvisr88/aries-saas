namespace DevExpress.Xpf.Bars
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Windows;

    [TargetType(typeof(BarSubItem))]
    public class BarSubItemThemeSelectorBehavior : BarItemThemeSelectorBehavior<BarSubItem>
    {
        public static readonly DependencyProperty ShowModeProperty;

        static BarSubItemThemeSelectorBehavior();
        protected override object CreateStyleKey();
        protected virtual void OnShowModeChanged(BarSubItemThemeSelectorShowMode oldValue);
        private static void OnShowModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);

        public BarSubItemThemeSelectorShowMode ShowMode { get; set; }
    }
}

