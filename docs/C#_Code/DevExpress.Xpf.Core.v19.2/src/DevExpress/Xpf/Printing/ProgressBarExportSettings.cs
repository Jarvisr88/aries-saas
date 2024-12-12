namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public static class ProgressBarExportSettings
    {
        public static readonly DependencyProperty PositionProperty;

        static ProgressBarExportSettings()
        {
            Type ownerType = typeof(ProgressBarExportSettings);
            PositionProperty = DependencyProperty.RegisterAttached("Position", typeof(int), ownerType, new PropertyMetadata(ExportSettingDefaultValue.ProgressBarPosition));
        }

        public static int GetPosition(DependencyObject d) => 
            (int) d.GetValue(PositionProperty);

        public static void SetPosition(DependencyObject d, int value)
        {
            d.SetValue(PositionProperty, value);
        }
    }
}

