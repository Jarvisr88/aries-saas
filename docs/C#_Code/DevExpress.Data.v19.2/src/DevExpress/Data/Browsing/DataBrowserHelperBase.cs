namespace DevExpress.Data.Browsing
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class DataBrowserHelperBase
    {
        protected DataBrowserHelperBase();
        private static IList ForceList(object enumerable);
        private static object GetFirstItemByEnumerable(IEnumerable enumerable);
        private static object GetFirstItemByEnumerator(IEnumerator enumerator);
        protected virtual object GetList(object list);
        public PropertyDescriptorCollection GetListItemProperties(object list);
        public virtual PropertyDescriptorCollection GetListItemProperties(object list, PropertyDescriptor[] listAccessors);
        private PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable);
        private PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable, PropertyDescriptor[] listAccessors);
        private PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable iEnumerable, PropertyDescriptor[] listAccessors, int startIndex);
        private PropertyDescriptorCollection GetListItemPropertiesByInstance(object target, PropertyDescriptor[] listAccessors, int startIndex);
        private PropertyDescriptorCollection GetListItemPropertiesByType(Type type);
        public PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors);
        private PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors, int startIndex);
        public static Type GetListItemType(object list);
        private static Type GetListItemTypeByEnumerable(IEnumerable iEnumerable);
        protected abstract PropertyDescriptorCollection GetProperties(object component);
        protected abstract PropertyDescriptorCollection GetProperties(Type componentType);
        private static PropertyInfo GetTypedIndexer(Type type);
        protected abstract bool IsCustomType(Type type);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataBrowserHelperBase.<>c <>9;
            public static Predicate<Type> <>9__11_0;

            static <>c();
            internal bool <GetListItemType>b__11_0(Type item);
        }
    }
}

