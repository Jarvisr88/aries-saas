namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Windows;

    public static class TrackBarExportSettings
    {
        public static readonly DependencyProperty PositionProperty;
        public static readonly DependencyProperty MinimumProperty;
        public static readonly DependencyProperty MaximumProperty;

        static TrackBarExportSettings()
        {
            Type ownerType = typeof(TrackBarExportSettings);
            PositionProperty = DependencyProperty.RegisterAttached("Position", typeof(int), ownerType, new PropertyMetadata(ExportSettingDefaultValue.TrackBarPosition));
            MinimumProperty = DependencyProperty.RegisterAttached("Minimum", typeof(int), ownerType, new PropertyMetadata(ExportSettingDefaultValue.TrackBarMinimum));
            MaximumProperty = DependencyProperty.RegisterAttached("Maximum", typeof(int), ownerType, new PropertyMetadata(ExportSettingDefaultValue.TrackBarMaximum));
        }

        public static int GetMaximum(DependencyObject d) => 
            (int) d.GetValue(MaximumProperty);

        public static int GetMinimum(DependencyObject d) => 
            (int) d.GetValue(MinimumProperty);

        public static int GetPosition(DependencyObject d) => 
            (int) d.GetValue(PositionProperty);

        public static void SetMaximum(DependencyObject d, int value)
        {
            d.SetValue(MaximumProperty, value);
        }

        public static void SetMinimum(DependencyObject d, int value)
        {
            d.SetValue(MinimumProperty, value);
        }

        public static void SetPosition(DependencyObject d, int value)
        {
            d.SetValue(PositionProperty, value);
        }
    }
}

