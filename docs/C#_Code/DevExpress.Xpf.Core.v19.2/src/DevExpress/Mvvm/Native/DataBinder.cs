namespace DevExpress.Mvvm.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Access;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public static class DataBinder
    {
        public static IDataBinder<T> New<T>(T target, object source, bool weakSource = false, bool weakSubSource = false) => 
            new IDataBinderImpl<T>(target, source, weakSource, weakSubSource);

        public static void RegisterProperty<T, TProperty>(string property, Func<T, TProperty> get, Action<T, TProperty> set)
        {
            PropertyCache.RegisterProperty<T, TProperty>(property, get, set);
        }

        public static string[] SplitPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return new string[] { path };
            }
            char[] separator = new char[] { '.' };
            return path.Split(separator).ToArray<string>();
        }

        private class IDataBinderImpl<T> : DataBinderCore<T>, IDataBinder<T>, IDisposable
        {
            public IDataBinderImpl(T target, object source, bool isWeakSource, bool isWeakSubSource) : base(target, source, isWeakSource, isWeakSubSource)
            {
            }

            IDataBinder<T> IDataBinder<T>.BindOneTime(string property, string[] path, object fallbackValue)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, null, fallbackValue, DataBinderCore<T>.BindingMode.OneTime, false), true);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneTime(string property, string[] path, Action<T, object> set, object fallbackValue)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, set, fallbackValue, DataBinderCore<T>.BindingMode.OneTime, false), true);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneTimeToSource(string property, string[] path, object fallbackValue)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, null, fallbackValue, DataBinderCore<T>.BindingMode.OneTimeToSource, false), true);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneTimeToSource(string property, string[] path, System.Func<T, Type, object> get, object fallbackValue)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, get, null, fallbackValue, DataBinderCore<T>.BindingMode.OneTimeToSource, false), true);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneWay(string property, string[] path, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, null, fallbackValue, DataBinderCore<T>.BindingMode.OneWay, false), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneWay(string property, string[] path, Action<T, object> set, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, set, fallbackValue, DataBinderCore<T>.BindingMode.OneWay, false), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneWayExplicit(string property, string[] path, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, null, fallbackValue, DataBinderCore<T>.BindingMode.OneWay, true), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneWayExplicit(string property, string[] path, Action<T, object> set, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, set, fallbackValue, DataBinderCore<T>.BindingMode.OneWay, true), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneWayToSource(string property, string[] path, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, null, fallbackValue, DataBinderCore<T>.BindingMode.OneWayToSource, false), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindOneWayToSource(string property, string[] path, System.Func<T, Type, object> get, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, get, null, fallbackValue, DataBinderCore<T>.BindingMode.OneWayToSource, false), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindTwoWay(string property, string[] path, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, null, fallbackValue, DataBinderCore<T>.BindingMode.TwoWay, false), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindTwoWay(string property, string[] path, System.Func<T, Type, object> get, Action<T, object> set, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, get, set, fallbackValue, DataBinderCore<T>.BindingMode.TwoWay, false), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindTwoWayExplicit(string property, string[] path, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, null, null, fallbackValue, DataBinderCore<T>.BindingMode.TwoWay, true), syncFirstTime);
                return this;
            }

            IDataBinder<T> IDataBinder<T>.BindTwoWayExplicit(string property, string[] path, System.Func<T, Type, object> get, Action<T, object> set, object fallbackValue, bool syncFirstTime)
            {
                base.AddBinding(new DataBinderCore<T>.BindingInfo(property, path, get, set, fallbackValue, DataBinderCore<T>.BindingMode.TwoWay, true), syncFirstTime);
                return this;
            }
        }

        internal static class PropertyCache
        {
            private static Dictionary<PropertyKey, IFastProperty> cache = new Dictionary<PropertyKey, IFastProperty>();

            public static IFastProperty GetProperty(object obj, string property, bool createIfNotExist = true)
            {
                IFastProperty property2;
                Type type = obj.GetType();
                PropertyKey key = new PropertyKey(type, property);
                if (!DataBinder.PropertyCache.cache.TryGetValue(key, out property2))
                {
                    if (!createIfNotExist)
                    {
                        return null;
                    }
                    if (obj is DynamicObject)
                    {
                        return new FastPropertyDynamicObject(property);
                    }
                    if (obj is IDictionary<string, object>)
                    {
                        return new FastPropertyExpando(property);
                    }
                    if (obj is DataRowView)
                    {
                        return new FastPropertyDataRowView(property);
                    }
                    if (obj is DataRow)
                    {
                        return new FastPropertyDataRow(property);
                    }
                    if (obj is ICustomTypeDescriptor)
                    {
                        return new FastPropertyICustomTypeDescriptor(property);
                    }
                    property2 = new FastPropertyDescriptor(type, property);
                    Dictionary<PropertyKey, IFastProperty> cache = DataBinder.PropertyCache.cache;
                    lock (cache)
                    {
                        DataBinder.PropertyCache.cache[key] = property2;
                    }
                }
                return property2;
            }

            public static void RegisterProperty<T, TProperty>(string property, Func<T, TProperty> get, Action<T, TProperty> set)
            {
                PropertyKey key = new PropertyKey(typeof(T), property);
                Dictionary<PropertyKey, IFastProperty> cache = DataBinder.PropertyCache.cache;
                lock (cache)
                {
                    DataBinder.PropertyCache.cache[key] = new FastProperty(typeof(TProperty), x => get((T) x), (x, v) => set((T) x, (TProperty) v));
                }
            }

            public class FastProperty : DataBinder.PropertyCache.IFastProperty
            {
                private readonly Func<object, object> getter;
                private readonly Action<object, object> setter;
                private readonly Type propertyType;

                public FastProperty(Type propertyType, Func<object, object> getter, Action<object, object> setter)
                {
                    this.propertyType = propertyType;
                    this.getter = getter;
                    this.setter = setter;
                }

                public Type GetPropertyType(object obj) => 
                    this.propertyType;

                public object GetValue(object obj) => 
                    this.getter(obj);

                public void SetValue(object obj, object value)
                {
                    this.setter(obj, value);
                }
            }

            private class FastPropertyDataRow : DataBinder.PropertyCache.IFastProperty
            {
                private readonly string property;

                public FastPropertyDataRow(string property)
                {
                    this.property = property;
                }

                public Type GetPropertyType(object obj) => 
                    typeof(object);

                public object GetValue(object obj) => 
                    ((DataRow) obj)[this.property];

                public void SetValue(object obj, object value)
                {
                    ((DataRow) obj)[this.property] = value;
                }
            }

            private class FastPropertyDataRowView : DataBinder.PropertyCache.IFastProperty
            {
                private readonly string property;

                public FastPropertyDataRowView(string property)
                {
                    this.property = property;
                }

                public Type GetPropertyType(object obj) => 
                    typeof(object);

                public object GetValue(object obj) => 
                    ((DataRowView) obj)[this.property];

                public void SetValue(object obj, object value)
                {
                    ((DataRowView) obj)[this.property] = value;
                }
            }

            private class FastPropertyDescriptor : DataBinder.PropertyCache.IFastProperty
            {
                private readonly PropertyDescriptor descriptor;
                private readonly DataColumnInfo column;

                public FastPropertyDescriptor(Type objType, string property)
                {
                    PropertyDescriptor source = TypeDescriptor.GetProperties(objType)[property];
                    if (source == null)
                    {
                        throw new InvalidOperationException($"The '{property}' property is not found on object '{objType.FullName}'.");
                    }
                    this.descriptor = DataListDescriptor.GetFastProperty(source);
                    this.column = new DataColumnInfo(this.descriptor, true);
                }

                public Type GetPropertyType(object obj) => 
                    this.descriptor.PropertyType;

                public object GetValue(object obj) => 
                    this.descriptor.GetValue(obj);

                public void SetValue(object obj, object value)
                {
                    value = this.column.ConvertValue(value, true);
                    this.descriptor.SetValue(obj, value);
                }
            }

            private class FastPropertyDynamicObject : DataBinder.PropertyCache.IFastProperty
            {
                private readonly string property;

                public FastPropertyDynamicObject(string property)
                {
                    this.property = property;
                }

                public Type GetPropertyType(object obj) => 
                    typeof(object);

                public object GetValue(object obj)
                {
                    object obj2;
                    return (!((DynamicObject) obj).TryGetMember(new DynamicObjectGetMemberBinder(this.property), out obj2) ? null : obj2);
                }

                public void SetValue(object obj, object value)
                {
                    ((DynamicObject) obj).TrySetMember(new DynamicObjectSetMemberBinder(this.property), value);
                }

                private class DynamicObjectGetMemberBinder : GetMemberBinder
                {
                    public DynamicObjectGetMemberBinder(string name) : base(name, true)
                    {
                    }

                    public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion) => 
                        null;
                }

                private class DynamicObjectSetMemberBinder : SetMemberBinder
                {
                    public DynamicObjectSetMemberBinder(string name) : base(name, true)
                    {
                    }

                    public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value, DynamicMetaObject errorSuggestion) => 
                        null;
                }
            }

            private class FastPropertyExpando : DataBinder.PropertyCache.IFastProperty
            {
                private readonly string property;

                public FastPropertyExpando(string property)
                {
                    this.property = property;
                }

                public Type GetPropertyType(object obj) => 
                    typeof(object);

                public object GetValue(object obj)
                {
                    object obj2;
                    return (!((IDictionary<string, object>) obj).TryGetValue(this.property, out obj2) ? null : obj2);
                }

                public void SetValue(object obj, object value)
                {
                    ((IDictionary<string, object>) obj)[this.property] = value;
                }
            }

            private class FastPropertyICustomTypeDescriptor : DataBinder.PropertyCache.IFastProperty
            {
                private readonly string property;

                public FastPropertyICustomTypeDescriptor(string property)
                {
                    this.property = property;
                }

                private PropertyDescriptor GetDescriptor(object obj)
                {
                    PropertyDescriptor descriptor = ((ICustomTypeDescriptor) obj).GetProperties()[this.property];
                    if (descriptor == null)
                    {
                        throw new InvalidOperationException($"The '{this.property}' property is not found on object '{obj.GetType().FullName}'.");
                    }
                    return descriptor;
                }

                public Type GetPropertyType(object obj) => 
                    this.GetDescriptor(obj).PropertyType;

                public object GetValue(object obj) => 
                    this.GetDescriptor(obj).GetValue(obj);

                public void SetValue(object obj, object value)
                {
                    this.GetDescriptor(obj).SetValue(obj, value);
                }
            }

            public interface IFastProperty
            {
                Type GetPropertyType(object obj);
                object GetValue(object obj);
                void SetValue(object obj, object value);
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct PropertyKey : IEquatable<DataBinder.PropertyCache.PropertyKey>
            {
                private System.Type type;
                private string property;
                private int hashCode;
                public System.Type Type =>
                    this.type;
                public string Property =>
                    this.property;
                public PropertyKey(System.Type type, string property)
                {
                    this.type = type;
                    this.property = property;
                    this.hashCode = HashCodeHelper.Calculate(type.GetHashCode(), property.GetHashCode());
                }

                public override bool Equals(object obj) => 
                    (obj is DataBinder.PropertyCache.PropertyKey) && this.Equals((DataBinder.PropertyCache.PropertyKey) obj);

                public bool Equals(DataBinder.PropertyCache.PropertyKey obj) => 
                    (this.type == obj.type) && (this.property == obj.property);

                public override int GetHashCode() => 
                    this.hashCode;
            }
        }
    }
}

