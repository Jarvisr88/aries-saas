namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Map")]
    public class ObjectToObjectConverter : IValueConverter
    {
        public ObjectToObjectConverter()
        {
            this.Map = new ObservableCollection<MapItem>();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static object Coerce(object value, Type targetType, bool ignoreImplicitXamlConversions = false, bool convertIntToEnum = false)
        {
            if ((value == null) || ((targetType == typeof(object)) || (value.GetType() == targetType)))
            {
                return value;
            }
            if (targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }
            Type underlyingType = Nullable.GetUnderlyingType(targetType);
            Type type1 = underlyingType;
            if (underlyingType == null)
            {
                Type local1 = underlyingType;
                type1 = targetType;
            }
            object obj2 = CoerceNonNullable(value, type1, ignoreImplicitXamlConversions, convertIntToEnum);
            if (underlyingType == null)
            {
                return obj2;
            }
            object[] args = new object[] { obj2 };
            return Activator.CreateInstance(targetType, args);
        }

        internal static object CoerceNonNullable(object value, Type targetType, bool ignoreImplicitXamlConversions, bool convertIntToEnum)
        {
            if (!ignoreImplicitXamlConversions && IsImplicitXamlConvertion(value.GetType(), targetType))
            {
                return value;
            }
            if (targetType == typeof(string))
            {
                return value.ToString();
            }
            if (targetType.IsEnum)
            {
                if (value is string)
                {
                    return Enum.Parse(targetType, (string) value, false);
                }
                if (convertIntToEnum)
                {
                    try
                    {
                        object obj2 = Enum.ToObject(targetType, value);
                        if (Enum.IsDefined(targetType, obj2))
                        {
                            return obj2;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            if (targetType == typeof(Color))
            {
                ColorConverter converter2 = new ColorConverter();
                return (!converter2.IsValid(value) ? value : converter2.ConvertFrom(value));
            }
            if ((targetType == typeof(Brush)) || (targetType == typeof(SolidColorBrush)))
            {
                BrushConverter converter3 = new BrushConverter();
                return (!converter3.IsValid(value) ? (!(value is Color) ? value : BrushesCache.GetBrush((Color) value)) : converter3.ConvertFrom(value));
            }
            TypeConverter converter = TypeDescriptor.GetConverter(targetType);
            try
            {
                return (((converter == null) || !converter.IsValid(value)) ? System.Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture) : converter.ConvertFrom(null, CultureInfo.InvariantCulture, value));
            }
            catch
            {
                return value;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<MapItem, object> selector = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<MapItem, object> local1 = <>c.<>9__18_0;
                selector = <>c.<>9__18_0 = item => item.Source;
            }
            MapItem item = this.Map.FirstOrDefault<MapItem>(this.MakeMapPredicate(selector, value));
            return Coerce((item == null) ? this.DefaultTarget : item.Target, targetType, false, false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Func<MapItem, object> selector = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<MapItem, object> local1 = <>c.<>9__19_0;
                selector = <>c.<>9__19_0 = item => item.Target;
            }
            MapItem item = this.Map.FirstOrDefault<MapItem>(this.MakeMapPredicate(selector, value));
            return Coerce((item == null) ? this.DefaultSource : item.Source, targetType, false, false);
        }

        internal static bool IsImplicitXamlConvertion(Type valueType, Type targetType) => 
            !(targetType == typeof(Thickness)) ? (!(targetType == typeof(GridLength)) ? ((targetType == typeof(ImageSource)) && ((valueType == typeof(string)) || (valueType == typeof(Uri)))) : true) : true;

        private Func<MapItem, bool> MakeMapPredicate(Func<MapItem, object> selector, object value) => 
            delegate (MapItem mapItem) {
                object obj1 = value;
                if (value == null)
                {
                    object local1 = value;
                    obj1 = string.Empty;
                }
                return SafeCompare(Coerce(selector(mapItem), obj1.GetType(), false, false), value);
            };

        public static bool SafeCompare(object left, object right) => 
            (left != null) ? left.Equals(right) : ((right != null) ? right.Equals(left) : true);

        public object DefaultSource { get; set; }

        public object DefaultTarget { get; set; }

        public ObservableCollection<MapItem> Map { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ObjectToObjectConverter.<>c <>9 = new ObjectToObjectConverter.<>c();
            public static Func<MapItem, object> <>9__18_0;
            public static Func<MapItem, object> <>9__19_0;

            internal object <Convert>b__18_0(MapItem item) => 
                item.Source;

            internal object <ConvertBack>b__19_0(MapItem item) => 
                item.Target;
        }
    }
}

