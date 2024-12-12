namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class AppearanceHelper
    {
        public static Appearance GetActualAppearance(Appearance parentAppearance, Appearance appearance)
        {
            parentAppearance ??= new Appearance();
            appearance ??= new Appearance();
            return new Appearance { 
                Normal = Merge(parentAppearance.Normal, appearance.Normal),
                Active = Merge(parentAppearance.Active, appearance.Active)
            };
        }

        public static AppearanceObject Merge(AppearanceObject parentAppearance, AppearanceObject appearance)
        {
            AppearanceObject obj2 = new AppearanceObject();
            foreach (DependencyProperty property in AppearanceObject.MergedProperties)
            {
                object parentValue = parentAppearance.GetValue(property);
                object obj4 = appearance.GetValue(property);
                obj2.SetValue(property, Merge(parentValue, obj4));
            }
            return obj2;
        }

        internal static object Merge(double parentValue, double value) => 
            double.IsNaN(value) ? parentValue : value;

        internal static object Merge(object parentValue, object value) => 
            !(parentValue is double) ? (!(parentValue is Color) ? (value ?? parentValue) : Merge((Color) parentValue, (Color) value)) : Merge((double) parentValue, (double) value);

        internal static object Merge(Color parentValue, Color value) => 
            (Colors.Transparent == value) ? parentValue : value;

        internal static AppearanceObject Update(AppearanceObject result, AppearanceObject parentAppearance, AppearanceObject appearance)
        {
            foreach (DependencyProperty property in AppearanceObject.MergedProperties)
            {
                object parentValue = parentAppearance.GetValue(property);
                object obj3 = appearance.GetValue(property);
                result.SetValue(property, Merge(parentValue, obj3));
            }
            return result;
        }

        internal static Appearance UpdateAppearance(Appearance result, Appearance parentAppearance, Appearance appearance)
        {
            parentAppearance ??= new Appearance();
            appearance ??= new Appearance();
            Update(result.Normal, parentAppearance.Normal, appearance.Normal);
            Update(result.Active, parentAppearance.Active, appearance.Active);
            return result;
        }
    }
}

