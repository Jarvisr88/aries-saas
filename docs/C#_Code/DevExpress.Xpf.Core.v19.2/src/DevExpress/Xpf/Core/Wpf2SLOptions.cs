namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class Wpf2SLOptions : DependencyObject
    {
        public static readonly DependencyProperty AllowProcessNodeProperty = DependencyProperty.RegisterAttached("AllowProcessNode", typeof(bool), typeof(Wpf2SLOptions), new UIPropertyMetadata(false));
        public static readonly DependencyProperty TagProperty = DependencyProperty.RegisterAttached("Tag", typeof(string), typeof(Wpf2SLOptions), new PropertyMetadata(string.Empty, new PropertyChangedCallback(Wpf2SLOptions.TagChanged)));

        public static bool GetAllowProcessNode(DependencyObject obj) => 
            (bool) obj.GetValue(AllowProcessNodeProperty);

        public static string GetTag(DependencyObject obj) => 
            (string) obj.GetValue(TagProperty);

        public static void SetAllowProcessNode(object obj, bool value)
        {
            if (obj is DependencyObject)
            {
                ((DependencyObject) obj).SetValue(AllowProcessNodeProperty, value);
            }
        }

        public static void SetTag(DependencyObject obj, string value)
        {
            obj.SetValue(TagProperty, value);
        }

        public static void TagChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}

