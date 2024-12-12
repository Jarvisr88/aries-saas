namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using DevExpress.Data.Helpers;
    using DevExpress.Entity.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class PropertyInfoExtensions
    {
        public static string GetActualMask(this IEdmPropertyInfo propertyInfo, string mask) => 
            (!propertyInfo.Attributes.ApplyFormatInEditMode || string.IsNullOrEmpty(propertyInfo.Attributes.DataFormatString)) ? mask : propertyInfo.Attributes.DataFormatString;

        public static string GetDisplayName(this IEdmPropertyInfo propertyInfo) => 
            !propertyInfo.HasDisplayAttribute() ? MasterDetailHelper.SplitPascalCaseString(propertyInfo.Name) : propertyInfo.DisplayName;

        public static IEnumerable<IEdmPropertyInfo> GetNavigationProperties(this IEntityProperties properties)
        {
            Func<IEdmPropertyInfo, bool> predicate = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<IEdmPropertyInfo, bool> local1 = <>c.<>9__5_0;
                predicate = <>c.<>9__5_0 = x => x.IsNavigationProperty;
            }
            return properties.AllProperties.Where<IEdmPropertyInfo>(predicate);
        }

        public static IEnumerable<IEdmPropertyInfo> GetProperties(this IEntityProperties properties)
        {
            Func<IEdmPropertyInfo, bool> predicate = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<IEdmPropertyInfo, bool> local1 = <>c.<>9__4_0;
                predicate = <>c.<>9__4_0 = x => !x.IsNavigationProperty;
            }
            return properties.AllProperties.Where<IEdmPropertyInfo>(predicate);
        }

        public static Type GetUnderlyingClrType(this IEdmPropertyInfo propertyInfo) => 
            Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

        public static bool HasDisplayAttribute(this IEdmPropertyInfo propertyInfo)
        {
            MemberDescriptor contextObject = propertyInfo.ContextObject as MemberDescriptor;
            if ((contextObject != null) && (contextObject.Attributes != null))
            {
                using (IEnumerator enumerator = contextObject.Attributes.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        object current = enumerator.Current;
                        if (current is DisplayNameAttribute)
                        {
                            return true;
                        }
                    }
                }
            }
            return (propertyInfo.DisplayName != propertyInfo.Name);
        }

        public static bool HasNullableType(this IEdmPropertyInfo propertyInfo) => 
            Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyInfoExtensions.<>c <>9 = new PropertyInfoExtensions.<>c();
            public static Func<IEdmPropertyInfo, bool> <>9__4_0;
            public static Func<IEdmPropertyInfo, bool> <>9__5_0;

            internal bool <GetNavigationProperties>b__5_0(IEdmPropertyInfo x) => 
                x.IsNavigationProperty;

            internal bool <GetProperties>b__4_0(IEdmPropertyInfo x) => 
                !x.IsNavigationProperty;
        }
    }
}

