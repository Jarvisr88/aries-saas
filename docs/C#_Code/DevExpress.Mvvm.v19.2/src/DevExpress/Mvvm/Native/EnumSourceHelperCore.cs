namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Data;
    using System.Windows.Media;

    public static class EnumSourceHelperCore
    {
        public static readonly string ValueMemberName;
        public static readonly string DisplayMemberName;
        private const int DefaultDisplayOrder = 0x2710;

        static EnumSourceHelperCore()
        {
            Expression[] expressionArray1 = new Expression[] { Expression.Constant(null, typeof(string)), Expression.Constant(null, typeof(string)), Expression.Constant(null, typeof(object)), Expression.Constant(null, typeof(ImageSource)) };
            ValueMemberName = BindableBase.GetPropertyName<object>(Expression.Lambda<Func<object>>(Expression.Property(Expression.New((ConstructorInfo) methodof(EnumMemberInfo..ctor), (IEnumerable<Expression>) expressionArray1), (MethodInfo) methodof(EnumMemberInfo.get_Id)), new ParameterExpression[0]));
            Expression[] expressionArray2 = new Expression[] { Expression.Constant(null, typeof(string)), Expression.Constant(null, typeof(string)), Expression.Constant(null, typeof(object)), Expression.Constant(null, typeof(ImageSource)) };
            DisplayMemberName = BindableBase.GetPropertyName<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.New((ConstructorInfo) methodof(EnumMemberInfo..ctor), (IEnumerable<Expression>) expressionArray2), (MethodInfo) methodof(EnumMemberInfo.get_Name)), new ParameterExpression[0]));
        }

        private static bool CanCreateSvgImageSource(Func<string, ImageSource> getSvgImageSource, string imageUri) => 
            (getSvgImageSource != null) && (!string.IsNullOrEmpty(imageUri) && imageUri.EndsWith(".svg", StringComparison.InvariantCultureIgnoreCase));

        public static int GetEnumCount(Type enumType) => 
            Enum.GetValues(enumType).Length;

        private static string GetEnumName(FieldInfo field, Enum value, IValueConverter nameConverter, bool splitNames)
        {
            if (nameConverter != null)
            {
                return (nameConverter.Convert(value, typeof(string), null, CultureInfo.CurrentCulture) as string);
            }
            string fieldDisplayName = DataAnnotationsAttributeHelper.GetFieldDisplayName(field);
            string str2 = fieldDisplayName ?? (TypeDescriptor.GetConverter(value.GetType()).ConvertTo(value, typeof(string)) as string);
            return ((!splitNames || (fieldDisplayName != null)) ? str2 : SplitStringHelper.SplitPascalCaseString(str2));
        }

        public static IEnumerable<EnumMemberInfo> GetEnumSource(Type enumType, bool useUnderlyingEnumValue = true, IValueConverter nameConverter = null, bool splitNames = false, EnumMembersSortMode sortMode = 0, Func<string, bool, string> getKnownImageUriCallback = null, bool showImage = true, bool showName = true, Func<string, ImageSource> getSvgImageSource = null)
        {
            if ((enumType == null) || !enumType.IsEnum)
            {
                return Enumerable.Empty<EnumMemberInfo>();
            }
            Func<string, ImageSource> func1 = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<string, ImageSource> local1 = <>c.<>9__4_0;
                func1 = <>c.<>9__4_0 = uri => (ImageSource) new ImageSourceConverter().ConvertFrom(uri);
            }
            Func<string, ImageSource> getImageSource = func1;
            Func<FieldInfo, bool> predicate = <>c.<>9__4_1;
            if (<>c.<>9__4_1 == null)
            {
                Func<FieldInfo, bool> local2 = <>c.<>9__4_1;
                predicate = <>c.<>9__4_1 = field => DataAnnotationsAttributeHelper.GetAutoGenerateField(field);
            }
            IEnumerable<EnumMemberInfo> source = enumType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where<FieldInfo>(predicate).Select<FieldInfo, EnumMemberInfo>(delegate (FieldInfo field) {
                Enum enum2 = (Enum) field.GetValue(null);
                string str = GetEnumName(field, enum2, nameConverter, splitNames);
                Tuple<string, string> tuple = GetImageInfo(MetadataHelper.GetAttribute<ImageAttribute>(field, false), MetadataHelper.GetAttribute<DXImageAttribute>(field, false), null, getKnownImageUriCallback);
                string imageUri = ViewModelBase.IsInDesignMode ? null : (tuple.Item1 ?? tuple.Item2);
                return new EnumMemberInfo(str, DataAnnotationsAttributeHelper.GetFieldDescription(field), useUnderlyingEnumValue ? GetUnderlyingEnumValue(enum2) : enum2, showImage, showName, () => imageUri.With<string, ImageSource>(TryGetImageSource(CanCreateSvgImageSource(getSvgImageSource, imageUri) ? getSvgImageSource : getImageSource)), DataAnnotationsAttributeHelper.GetFieldOrder(field));
            });
            switch (sortMode)
            {
                case EnumMembersSortMode.DisplayName:
                {
                    Func<EnumMemberInfo, string> func4 = <>c.<>9__4_4;
                    if (<>c.<>9__4_4 == null)
                    {
                        Func<EnumMemberInfo, string> local3 = <>c.<>9__4_4;
                        func4 = <>c.<>9__4_4 = x => x.Name;
                    }
                    source = source.OrderBy<EnumMemberInfo, string>(func4);
                    break;
                }
                case EnumMembersSortMode.DisplayNameDescending:
                {
                    Func<EnumMemberInfo, string> func5 = <>c.<>9__4_5;
                    if (<>c.<>9__4_5 == null)
                    {
                        Func<EnumMemberInfo, string> local4 = <>c.<>9__4_5;
                        func5 = <>c.<>9__4_5 = x => x.Name;
                    }
                    source = source.OrderByDescending<EnumMemberInfo, string>(func5);
                    break;
                }
                case EnumMembersSortMode.DisplayNameLength:
                {
                    Func<EnumMemberInfo, int> func6 = <>c.<>9__4_6;
                    if (<>c.<>9__4_6 == null)
                    {
                        Func<EnumMemberInfo, int> local5 = <>c.<>9__4_6;
                        func6 = <>c.<>9__4_6 = x => x.Name.Length;
                    }
                    source = source.OrderBy<EnumMemberInfo, int>(func6);
                    break;
                }
                case EnumMembersSortMode.DisplayNameLengthDescending:
                {
                    Func<EnumMemberInfo, int> func7 = <>c.<>9__4_7;
                    if (<>c.<>9__4_7 == null)
                    {
                        Func<EnumMemberInfo, int> local6 = <>c.<>9__4_7;
                        func7 = <>c.<>9__4_7 = x => x.Name.Length;
                    }
                    source = source.OrderByDescending<EnumMemberInfo, int>(func7);
                    break;
                }
                default:
                    break;
            }
            Func<EnumMemberInfo, int?> keySelector = <>c.<>9__4_8;
            if (<>c.<>9__4_8 == null)
            {
                Func<EnumMemberInfo, int?> local7 = <>c.<>9__4_8;
                keySelector = <>c.<>9__4_8 = x => (x.Order != null) ? x.Order : 0x2710;
            }
            return source.OrderBy<EnumMemberInfo, int?>(keySelector).ToArray<EnumMemberInfo>();
        }

        public static Tuple<string, string> GetImageInfo(ImageAttribute image, DXImageAttribute dxImage, string defaultImageName, Func<string, bool, string> getKnownImageUriCallback)
        {
            if (image != null)
            {
                return Tuple.Create<string, string>(image.ImageUri, null);
            }
            Func<DXImageAttribute, string> evaluator = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<DXImageAttribute, string> local1 = <>c.<>9__7_0;
                evaluator = <>c.<>9__7_0 = x => x.ImageName;
            }
            string local2 = dxImage.With<DXImageAttribute, string>(evaluator);
            string local10 = local2;
            if (local2 == null)
            {
                string local3 = local2;
                local10 = defaultImageName;
            }
            string imageName = local10;
            string local5 = dxImage.With<DXImageAttribute, string>(<>c.<>9__7_1 ??= x => x.SmallImageUri);
            string local11 = local5;
            if (local5 == null)
            {
                string local6 = local5;
                local11 = GetKnownImageUri(getKnownImageUriCallback, imageName, false);
            }
            Func<DXImageAttribute, string> func2 = <>c.<>9__7_2;
            if (<>c.<>9__7_2 == null)
            {
                Func<DXImageAttribute, string> local7 = <>c.<>9__7_2;
                func2 = <>c.<>9__7_2 = x => x.LargeImageUri;
            }
            string local8 = dxImage.With<DXImageAttribute, string>(func2);
            string local12 = local8;
            if (local8 == null)
            {
                string local9 = local8;
                local12 = GetKnownImageUri(getKnownImageUriCallback, imageName, true);
            }
            return Tuple.Create<string, string>(local11, local12);
        }

        private static string GetKnownImageUri(Func<string, bool, string> getKnownImageUriCallback, string imageName, bool large) => 
            ((getKnownImageUriCallback == null) || string.IsNullOrEmpty(imageName)) ? null : getKnownImageUriCallback(imageName, large);

        private static object GetUnderlyingEnumValue(Enum value)
        {
            Type underlyingType = Enum.GetUnderlyingType(value.GetType());
            return Convert.ChangeType(value, underlyingType, CultureInfo.CurrentCulture);
        }

        private static Func<string, ImageSource> TryGetImageSource(Func<string, ImageSource> getImageSource) => 
            delegate (string uri) {
                ImageSource source;
                try
                {
                    source = getImageSource(uri);
                }
                catch
                {
                    throw new ArgumentException($"The Uri {uri} cannot be converted to an image.");
                }
                return source;
            };

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumSourceHelperCore.<>c <>9 = new EnumSourceHelperCore.<>c();
            public static Func<string, ImageSource> <>9__4_0;
            public static Func<FieldInfo, bool> <>9__4_1;
            public static Func<EnumMemberInfo, string> <>9__4_4;
            public static Func<EnumMemberInfo, string> <>9__4_5;
            public static Func<EnumMemberInfo, int> <>9__4_6;
            public static Func<EnumMemberInfo, int> <>9__4_7;
            public static Func<EnumMemberInfo, int?> <>9__4_8;
            public static Func<DXImageAttribute, string> <>9__7_0;
            public static Func<DXImageAttribute, string> <>9__7_1;
            public static Func<DXImageAttribute, string> <>9__7_2;

            internal ImageSource <GetEnumSource>b__4_0(string uri) => 
                (ImageSource) new ImageSourceConverter().ConvertFrom(uri);

            internal bool <GetEnumSource>b__4_1(FieldInfo field) => 
                DataAnnotationsAttributeHelper.GetAutoGenerateField(field);

            internal string <GetEnumSource>b__4_4(EnumMemberInfo x) => 
                x.Name;

            internal string <GetEnumSource>b__4_5(EnumMemberInfo x) => 
                x.Name;

            internal int <GetEnumSource>b__4_6(EnumMemberInfo x) => 
                x.Name.Length;

            internal int <GetEnumSource>b__4_7(EnumMemberInfo x) => 
                x.Name.Length;

            internal int? <GetEnumSource>b__4_8(EnumMemberInfo x) => 
                (x.Order != null) ? x.Order : 0x2710;

            internal string <GetImageInfo>b__7_0(DXImageAttribute x) => 
                x.ImageName;

            internal string <GetImageInfo>b__7_1(DXImageAttribute x) => 
                x.SmallImageUri;

            internal string <GetImageInfo>b__7_2(DXImageAttribute x) => 
                x.LargeImageUri;
        }
    }
}

