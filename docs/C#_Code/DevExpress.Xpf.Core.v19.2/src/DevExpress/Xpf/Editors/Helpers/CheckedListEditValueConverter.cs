namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class CheckedListEditValueConverter : IValueConverter
    {
        private bool isCheckedEditor = true;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            this.IsCheckedEditor ? (!(value is IEnumerable) ? value : ((IEnumerable) value).Cast<object>().ToList<object>()) : (!(value is IEnumerable) ? value : ((IEnumerable) value).Cast<object>().FirstOrDefault<object>());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<Type> interfaces = targetType.GetInterfaces();
            if (targetType.IsInterface)
            {
                interfaces = interfaces.Union<Type>(targetType.Yield<Type>());
            }
            if (interfaces == null)
            {
                return value;
            }
            Func<Type, bool> predicate = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<Type, bool> local1 = <>c.<>9__5_0;
                predicate = <>c.<>9__5_0 = x => x.Name.Contains("IEnumerable") && x.IsGenericType;
            }
            Func<Type, Type> evaluator = <>c.<>9__5_1;
            if (<>c.<>9__5_1 == null)
            {
                Func<Type, Type> local2 = <>c.<>9__5_1;
                evaluator = <>c.<>9__5_1 = x => x.GetGenericArguments().FirstOrDefault<Type>();
            }
            Type t = interfaces.FirstOrDefault<Type>(predicate).Return<Type, Type>(evaluator, <>c.<>9__5_2 ??= ((Func<Type>) (() => null)));
            return ((t != null) ? Converter.ConvertToList(value, t) : value);
        }

        public bool IsCheckedEditor
        {
            get => 
                this.isCheckedEditor;
            set => 
                this.isCheckedEditor = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckedListEditValueConverter.<>c <>9 = new CheckedListEditValueConverter.<>c();
            public static Func<Type, bool> <>9__5_0;
            public static Func<Type, Type> <>9__5_1;
            public static Func<Type> <>9__5_2;

            internal bool <ConvertBack>b__5_0(Type x) => 
                x.Name.Contains("IEnumerable") && x.IsGenericType;

            internal Type <ConvertBack>b__5_1(Type x) => 
                x.GetGenericArguments().FirstOrDefault<Type>();

            internal Type <ConvertBack>b__5_2() => 
                null;
        }

        private static class Converter
        {
            private static MethodInfo convertToListMethod;

            static Converter()
            {
                Type[] types = new Type[] { typeof(object) };
                convertToListMethod = typeof(CheckedListEditValueConverter.Converter).GetMethod("ConvertToList", types);
            }

            private static T Convert<T>(object value)
            {
                if (value != null)
                {
                    return (!typeof(T).IsAssignableFrom(value.GetType()) ? ((T) System.Convert.ChangeType(value, typeof(T))) : ((T) value));
                }
                return default(T);
            }

            public static IList<T> ConvertToList<T>(object value)
            {
                switch (value)
                {
                    case (IEnumerable<T> _):
                        return ((IEnumerable<T>) value).ToList<T>();
                        break;
                }
                if (value is T[])
                {
                    return ((T[]) value).ToList<T>();
                }
                if (value is object[])
                {
                    Converter<object, T> converter = <>c__4<T>.<>9__4_0;
                    if (<>c__4<T>.<>9__4_0 == null)
                    {
                        Converter<object, T> local1 = <>c__4<T>.<>9__4_0;
                        converter = <>c__4<T>.<>9__4_0 = e => Convert<T>(e);
                    }
                    return Array.ConvertAll<object, T>((object[]) value, converter).ToList<T>();
                }
                if (!(value is T))
                {
                    return (!(value is IEnumerable) ? new List<T>() : ((IEnumerable) value).OfType<T>().ToList<T>());
                }
                List<T> list1 = new List<T>();
                list1.Add((T) value);
                return list1;
            }

            public static IEnumerable ConvertToList(object value, Type t)
            {
                Type[] typeArguments = new Type[] { t };
                object[] parameters = new object[] { value };
                return (IEnumerable) convertToListMethod.MakeGenericMethod(typeArguments).Invoke(null, parameters);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c__4<T>
            {
                public static readonly CheckedListEditValueConverter.Converter.<>c__4<T> <>9;
                public static Converter<object, T> <>9__4_0;

                static <>c__4()
                {
                    CheckedListEditValueConverter.Converter.<>c__4<T>.<>9 = new CheckedListEditValueConverter.Converter.<>c__4<T>();
                }

                internal T <ConvertToList>b__4_0(object e) => 
                    CheckedListEditValueConverter.Converter.Convert<T>(e);
            }
        }
    }
}

