namespace DevExpress.Xpf.Ribbon
{
    using System;
    using System.Windows;

    public static class SimplifiedModeSettings
    {
        public static readonly DependencyProperty LocationProperty = DependencyProperty.RegisterAttached("Location", typeof(SimplifiedModeLocation?), typeof(SimplifiedModeSettings), new PropertyMetadata(SimplifiedModeLocation.All));

        public static SimplifiedModeLocation? GetLocation(DependencyObject obj) => 
            (SimplifiedModeLocation?) obj.GetValue(LocationProperty);

        public static void SetLocation(DependencyObject obj, SimplifiedModeLocation? value)
        {
            obj.SetValue(LocationProperty, value);
        }
    }
}

