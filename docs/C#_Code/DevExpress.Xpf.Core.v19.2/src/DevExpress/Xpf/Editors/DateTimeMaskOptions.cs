namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public class DateTimeMaskOptions : DependencyObject
    {
        public static readonly DependencyProperty DateTimeKindProperty;

        static DateTimeMaskOptions()
        {
            Type ownerType = typeof(DateTimeMaskOptions);
            DateTimeKindProperty = DependencyProperty.RegisterAttached("DateTimeKind", typeof(DateTimeKind?), ownerType, new UIPropertyMetadata(null, new PropertyChangedCallback(DateTimeMaskOptions.DateTimeKindPropertyChanged)));
        }

        private static void DateTimeKindPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextEdit edit = d as TextEdit;
            if (edit != null)
            {
                TextEditPropertyProvider propertyProvider = (TextEditPropertyProvider) edit.PropertyProvider;
                DateTimeKind? newValue = (DateTimeKind?) e.NewValue;
                propertyProvider.SetDateTimeKindAssigned(newValue != null);
                propertyProvider.SetDateTimeKind(newValue.GetValueOrDefault());
            }
        }

        public static DateTimeKind? GetDateTimeKind(DependencyObject d) => 
            (DateTimeKind?) d.GetValue(DateTimeKindProperty);

        public static void SetDateTimeKind(DependencyObject d, DateTimeKind? value)
        {
            d.SetValue(DateTimeKindProperty, value);
        }
    }
}

