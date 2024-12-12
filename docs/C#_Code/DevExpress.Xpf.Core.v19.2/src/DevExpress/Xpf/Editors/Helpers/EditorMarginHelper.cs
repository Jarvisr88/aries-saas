namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows;

    public class EditorMarginHelper : DependencyObject
    {
        public static readonly DependencyProperty MarginProperty = DependencyPropertyManager.RegisterAttached("Margin", typeof(string), typeof(EditorMarginHelper), new PropertyMetadata(new PropertyChangedCallback(EditorMarginHelper.PropertyChangedMargin)));

        public static string GetMargin(DependencyObject obj) => 
            (string) obj.GetValue(MarginProperty);

        private static void PropertyChangedMargin(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is string)
            {
                char[] separator = new char[] { ',' };
                string[] strArray = ((string) e.NewValue).Split(separator);
                EditorMarginCorrector corrector1 = new EditorMarginCorrector();
                corrector1.ErrorMargin = new Thickness(double.Parse(strArray[4]), double.Parse(strArray[5]), double.Parse(strArray[6]), double.Parse(strArray[7]));
                corrector1.Margin = new Thickness(double.Parse(strArray[0]), double.Parse(strArray[1]), double.Parse(strArray[2]), double.Parse(strArray[3]));
                EditorMarginCorrector corrector = corrector1;
                EditorMarginCorrector.SetCorrector(d, corrector);
            }
        }

        public static void SetMargin(DependencyObject obj, string value)
        {
            obj.SetValue(MarginProperty, value);
        }
    }
}

