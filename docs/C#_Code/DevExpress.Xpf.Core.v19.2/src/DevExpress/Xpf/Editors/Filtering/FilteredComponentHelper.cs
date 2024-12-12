namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class FilteredComponentHelper
    {
        public static IEnumerable<FilterColumn> GetColumnsByItemsSource(FrameworkElement ownerElement, object itemsSource, bool upperCasePropertyNames = false) => 
            itemsSource.With<object, IEnumerable<FilterColumn>>(delegate (object x) {
                Func<Type, IEnumerable<FilterColumn>> <>9__1;
                Func<Type, IEnumerable<FilterColumn>> evaluator = <>9__1;
                if (<>9__1 == null)
                {
                    Func<Type, IEnumerable<FilterColumn>> local1 = <>9__1;
                    evaluator = <>9__1 = y => GetColumnsByType(ownerElement, y, upperCasePropertyNames);
                }
                return x.GetType().GetGenericArguments().FirstOrDefault<Type>().With<Type, IEnumerable<FilterColumn>>(evaluator);
            });

        public static IEnumerable<FilterColumn> GetColumnsByType(FrameworkElement ownerElement, Type type, bool upperCasePropertyNames)
        {
            StandardColumnsProvider provider = new StandardColumnsProvider(ownerElement);
            Func<PropertyDescriptor, bool> predicate = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<PropertyDescriptor, bool> local1 = <>c.<>9__1_0;
                predicate = <>c.<>9__1_0 = x => !(x.PropertyType == typeof(string)) ? !typeof(IEnumerable).IsAssignableFrom(x.PropertyType) : true;
            }
            return TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>().Where<PropertyDescriptor>(predicate).Select<PropertyDescriptor, FilterColumn>(delegate (PropertyDescriptor x) {
                PropertyDescription standardColumn = provider.GetStandardColumn(x);
                string caption = standardColumn.ColumnCaption ?? standardColumn.FieldName;
                FilterColumn column1 = new FilterColumn();
                column1.ColumnCaption = UpdateColumnCaption(caption, upperCasePropertyNames);
                column1.EditSettings = standardColumn.EditSettings;
                column1.ColumnType = x.PropertyType;
                column1.FieldName = standardColumn.FieldName;
                return column1;
            });
        }

        private static string UpdateColumnCaption(string caption, bool upperCasePropertyNames) => 
            !upperCasePropertyNames ? caption : caption.ToUpper();

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FilteredComponentHelper.<>c <>9 = new FilteredComponentHelper.<>c();
            public static Func<PropertyDescriptor, bool> <>9__1_0;

            internal bool <GetColumnsByType>b__1_0(PropertyDescriptor x) => 
                !(x.PropertyType == typeof(string)) ? !typeof(IEnumerable).IsAssignableFrom(x.PropertyType) : true;
        }
    }
}

